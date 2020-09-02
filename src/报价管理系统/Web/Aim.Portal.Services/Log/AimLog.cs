using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using Aim.Portal.Model;
using NHibernate.Criterion;
using Newtonsoft.Json.Linq;
namespace Aim.Portal.Services.Log
{
    public class AimLog : IDisposable
    {
        private static AimLog _instance = null;
        private static SysUser _cacheUser = null;
        private static readonly object _synObject = new object();

        /// <summary>
        ///单例
        /// </summary>
        public static AimLog Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (_synObject)
                    {
                        if (null == _instance)
                        {
                            _instance = new AimLog();
                        }
                    }
                }
                return _instance;
            }
        }
        /// <summary>
        /// 日志对象的缓存队列
        /// </summary>
        private static Queue<SYSLOG> _msgs;
        /// <summary>
        /// 日志写入线程的控制标记  ture写中|false没有写
        /// </summary>
        private bool _state;

        /// <summary>
        /// 创建日志对象的新实例,根据指定的日志文件路径和指定的日志文件创建类型
        /// </summary>
        private AimLog()
        {
            if (_msgs == null)
            {
                _state = true;
                _msgs = new Queue<SYSLOG>();
                Thread thread = new Thread(work);
                thread.Start();
            }
        }
        //日志文件写入线程执行的方法
        private void work()
        {
            while (true)
            {
                //判断队列中是否存在待写入的日志
                if (_msgs.Count > 0)
                {
                    SYSLOG msg = null;
                    lock (_msgs)
                    {
                        msg = _msgs.Dequeue();
                        if (msg != null)
                        {
                            //加入适配字段代码
                            //try
                            //{
                            //    if (WriteLogCheck(msg))
                            //    {
                            //        msg.CreateAndFlush();
                            //    }
                            //}
                            //catch (Exception ex)
                            //{
                            //    msg.REMARK = "monitor adapter exception: " + Environment.NewLine + ex.Message;
                            //    msg.CreateAndFlush();
                            //}
                        }
                    }
                }
                else
                {
                    //判断是否已经发出终止日志并关闭的消息
                    if (_state)
                    {
                        Thread.Sleep(1);
                    }
                    else
                    {
                    }
                }
            }
        }
        public void LogWrite(SYSLOG msg)
        {
            if (_msgs != null)
            {
                lock (_msgs)
                {
                    _msgs.Enqueue(msg);
                }
            }
        }
        /// <summary>
        /// 销毁日志对象
        /// </summary>
        public void Dispose()
        {
            _state = false;
        }

        /// <summary>
        /// 拦截日志判断
        /// </summary>
        public bool WriteLogCheck(SYSLOG msg)
        {
            string tblName = string.Empty; //表名
            tblName = msg.TABLEEN.Contains(".") ? msg.TABLEEN.ToUpper().Split('.')[msg.TABLEEN.Split('.').Length - 1] : msg.TABLEEN;

            SYSMONTORSET Ent = null;
            if (!CacheHelpTooler.MonitorCacheChange && CacheHelpTooler.HasMonitorVal())
            {
                Ent = CacheHelpTooler.MonitorCacheSet.Where(ten => ten.TBLENCODE == tblName).FirstOrDefault();
            }
            else
            {
                Ent = SYSMONTORSET.FindFirstByProperties(SYSMONTORSET.Prop_TBLENCODE, tblName);
            }

            if (Ent == null) return false;
            if (!Ent.ISMONTOR.ToUpper().Contains("Y")) return false;//是否监视

            //获取人员,公司,部门
            var Users = SysUser.Find(msg.CREATEID);
            SysGroup Corp = null;
            if (!CacheHelpTooler.GroupCacheChange && CacheHelpTooler.HasGrourpVal())
            {
                Corp = CacheHelpTooler.GrourpCacheSet.Where(ten => ten.GroupID == Users.Pk_corp).FirstOrDefault();
            }
            else
            {
                Corp = SysGroup.TryFind(Users.Pk_corp);
            }

            SysGroup Dept = null;
            if (!CacheHelpTooler.GroupCacheChange && CacheHelpTooler.HasGrourpVal())
            {
                Dept = CacheHelpTooler.GrourpCacheSet.Where(ten => ten.GroupID == Users.Pk_deptdoc).FirstOrDefault();
            }
            else
            {
                Dept = SysGroup.TryFind(Users.Pk_corp);
            }

            #region 判断拦截代码
            //是否包含该人员 Leavel:1
            if (!string.IsNullOrEmpty(Ent.PERSONIDS) && !Ent.PERSONIDS.Contains(msg.CREATEID)) return false;

            //组织结构
            if (!string.IsNullOrEmpty(Ent.ORGANIZATIONIDS))
            {
                bool bol = false;
                //有可能一人在多个公司
                foreach (string item in Ent.ORGANIZATIONIDS.Split(','))
                {
                    SysGroup[] Groups = null;
                    if (!CacheHelpTooler.GroupCacheChange && CacheHelpTooler.HasGrourpVal())
                    {
                        Groups = CacheHelpTooler.GrourpCacheSet;
                    }
                    else
                    {
                        Groups = SysGroup.FindAll();
                    }

                    var groups = Groups.Where(g => (g.Path + "").Contains(item) && ((g.Path + "").Contains(Users.Pk_deptdoc) || (g.GroupID + "") == Users.Pk_deptdoc)).Count();
                    if (groups > 0)
                    {
                        bol = true;
                        break;
                    }
                }
                if (!bol) return false;
            }

            if (!string.IsNullOrEmpty(Ent.TIMEPOINT))
            //时间点
            {
                if (DateTime.Parse(Ent.TIMEPOINT).ToLocalTime() != msg.CREATETIME.GetValueOrDefault().ToLocalTime()) return false;
            }

            if (Ent.STARTTIME.HasValue || Ent.ENDTIME.HasValue)
            //时间段
            {
                if (Ent.STARTTIME.HasValue && Ent.ENDTIME.HasValue)
                {
                    if (!(msg.CREATETIME.GetValueOrDefault().CompareTo(Ent.STARTTIME) >= 0 && msg.CREATETIME.GetValueOrDefault().CompareTo(Ent.ENDTIME) <= 0))
                    {
                        return false;
                    }
                }
                if (Ent.STARTTIME.HasValue)
                {
                    if (!(msg.CREATETIME.GetValueOrDefault().CompareTo(Ent.STARTTIME) >= 0)) return false;
                }
                if (Ent.ENDTIME.HasValue)
                {
                    if (!(msg.CREATETIME.GetValueOrDefault().CompareTo(Ent.ENDTIME) <= 0)) return false;
                }
            }

            //日期
            if (Ent.DATATIMEPOINT.HasValue)
            {
                if (msg.CREATETIME.GetValueOrDefault().ToString("yyyy-MM-dd") != Ent.DATATIMEPOINT.GetValueOrDefault().ToString("yyyy-MM-dd"))
                {
                    return false;
                }
            }

            //星期
            if (!string.IsNullOrEmpty(Ent.WEEK) && !Ent.WEEK.Trim().Contains(getDayOfWeek(msg.CREATETIME))) return false;
            #endregion

            return SysLogEntProcess(msg, Ent, Corp, Dept, true);
        }

        private static bool SysLogEntProcess(SYSLOG msg, SYSMONTORSET Ent, SysGroup Corp, SysGroup Dept, bool checkState)
        {
            if (Dept != null)
            {
                msg.DEPTID = Dept.GroupID;
                msg.DEPTNAME = Dept.Name;
            }
            if (Corp != null)
            {
                msg.COMPANYID = Corp.GroupID;
                msg.COMPANYNAME = Corp.Name;
            }
            msg.TABLECN = Ent.TBLNAME + "";

            //监视表
            if (!string.IsNullOrEmpty(msg.ACTION) && "DELETE|UPDATEANDFLUSH|CREATEANDFLUSH".Contains((msg.ACTION).ToUpper()))
            {
                return true;
            }

            //监视列
            List<SYSMONTBLCLNSSET> fieldSet = JArray.Parse(Ent.TBLCLNS).Select(ten => JsonHelper.GetObject<SYSMONTBLCLNSSET>(JsonHelper.GetJsonString(ten)) as SYSMONTBLCLNSSET).ToList();
            foreach (var item in fieldSet.Where(ten => ten.ISCHECKED == "Y" && ten.CLNCODE.ToUpper() == msg.COLUMNEN.ToUpper()).ToList())
            {
                msg.COLUMNCN = item.CLNNAME;
                return true;
            }
            return false;

        }

        private static string getDayOfWeek(DateTime? dt)
        {
            DateTime now = dt.GetValueOrDefault();
            int n = (int)now.DayOfWeek;
            string[] weekDays = { "7", "1", "2", "3", "4", "5", "6" };
            return weekDays[n];
        }
    }

}
