using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aim;
using Aim.Data;
using Aim.Portal;
using Aim.Portal.Model;
using Aim.Portal.ServicesProvider.QuartzService;
using Aim.Portal.Web;
using Aim.Portal.Web.UI;
using CRM.Model;
using Quartz;

namespace CRM.Web.CommonPages
{
    public partial class SysSubscribeEdit : BasePage
    {
        #region 变量

        string op = String.Empty; // 用户编辑操作
        string id = String.Empty;   // 对象id
        string type = String.Empty; // 对象类型

        private string qurUrl = String.Empty;

        #endregion

        #region ASP.NET 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            op = RequestData.Get<string>("op");
            id = RequestData.Get<string>("id");
            type = RequestData.Get<string>("type");

            qurUrl = RequestData.Get<string>("qururl");
            IList<string> entStrList = RequestData.GetList<string>("ddt");


            SYSSUBSCRIBE ent = null;

            switch (this.RequestAction)
            {
                case RequestActionEnum.Update:
                    ent = this.GetMergedData<SYSSUBSCRIBE>();
                    ent.CONDITION = GetConditonStr(entStrList);
                    ent.TASKRULE = GetTaskRule(ent);
                    ent.DoUpdate();
                    this.SetMessage("修改成功！");
                    break;
                case RequestActionEnum.Insert:
                case RequestActionEnum.Create:
                    ent = this.GetPostedData<SYSSUBSCRIBE>();
                    ent.USERID = UserInfo.UserID;
                    ent.USERNAME = UserInfo.Name;
                    ent.USERLOGINNAME = UserInfo.LoginName;
                    ent.CONDITION = GetConditonStr(entStrList);
                    ent.TASKRULE = GetTaskRule(ent);
                    ent.DoSave();
                    this.SetMessage("新建成功！");
                    break;
                case RequestActionEnum.Delete:
                    ent = this.GetTargetData<SYSSUBSCRIBE>();
                    ent.DoDelete();
                    this.SetMessage("删除成功！");
                    break;
                default:
                    Quartz.CronExpression ccc = new CronExpression("0 0 8 * * ?");
                    ccc.GetFinalFireTime();
                    //ccc.
                    if (RequestActionString == "undo")
                    {
                        string url = RequestData.Get<string>("id");
                        SYSSUBSCRIBE sub = SYSSUBSCRIBE.Find(id);
                        if (sub != null)
                            sub.DoDelete();
                    }
                    else
                    {
                        SYSSUBSCRIBE[] subs = SYSSUBSCRIBE.FindAllByProperties(SYSSUBSCRIBE.Prop_LISTURL, qurUrl, SYSSUBSCRIBE.Prop_USERID, UserInfo.UserID);
                        if (subs.Any())
                        {
                            this.SetFormData(subs.FirstOrDefault());
                        }
                        else
                        {
                            SysUser user = SysUser.Find(UserInfo.UserID);
                            var obj = new
                            {
                                EMAIL = user.Email
                            };
                            this.SetFormData(obj);
                        }
                    }

                    break;
            }


        }

        private string GetTaskRule(SYSSUBSCRIBE ent)
        {
            string cronExpress = "";

            if (ent.TASKMODE.ToLower() == "simple")
            {
                /*
                 * 简单任务
                 */

            }
            else if (ent.TASKMODE.ToLower() == "cron")
            {
                /*
                 *复杂任务 
                 */
                switch (ent.CRONFSPL)
                {
                    case "月":
                        if (ent.CRONMTTYPE + "" == "一次")
                        {
                            DateTime dttemp = Convert.ToDateTime("2013/10/10 " + ent.MYMTZXYC);
                            cronExpress = dttemp.Second + " " + dttemp.Minute + " " + dttemp.Hour + " " + ent.MYZXTS + " * ?";
                        }
                        else if (ent.CRONMTTYPE + "" == "周期")
                        {
                            int ms = Convert.ToInt32(ent.MYMTZXDCM);
                            if (ms < 60)
                            {
                                cronExpress = "*/" + ms + " *";
                            }
                            else if (ms < 3600)
                            {
                                cronExpress = "0 */" + Convert.ToInt32(ms / 60);
                            }

                            if (ent.XZMTZXSJ + "" == "on")
                            {
                                cronExpress += " " + ent.XZMTKSSJ + "-" + ent.XZMTJSSJ + " " + ent.MYZXTS + " * ?";
                            }
                            else
                            {
                                cronExpress += " * * * ?";
                            }
                        }
                        break;
                    case "周":
                        //判断是一天一次还是多次
                        string weekday = "";
                        if (ent.CRONWEEK1 + "" == "on")
                        {
                            weekday += "1,";
                        }
                        if (ent.CRONWEEK2 + "" == "on")
                        {
                            weekday += "2,";
                        }
                        if (ent.CRONWEEK3 + "" == "on")
                        {
                            weekday += "3,";
                        }
                        if (ent.CRONWEEK4 + "" == "on")
                        {
                            weekday += "4,";
                        }
                        if (ent.CRONWEEK5 + "" == "on")
                        {
                            weekday += "5,";
                        }
                        if (ent.CRONWEEK6 + "" == "on")
                        {
                            weekday += "6,";
                        }
                        if (ent.CRONWEEK7 + "" == "on")
                        {
                            weekday += "7,";
                        }

                        if (ent.CRONMTTYPE + "" == "一次")
                        {
                            DateTime dttemp = Convert.ToDateTime("2013/10/10 " + ent.MYMTZXYC);
                            cronExpress = dttemp.Second + " " + dttemp.Minute + " " + dttemp.Hour + " ? * " + weekday;
                        }
                        else if (ent.CRONMTTYPE + "" == "周期")
                        {
                            int ms = Convert.ToInt32(ent.MYMTZXDCM);
                            if (ms < 60)
                            {
                                cronExpress = "*/" + ms + " *";
                            }
                            else if (ms < 3600)
                            {
                                cronExpress = "0 */" + Convert.ToInt32(ms / 60);
                            }

                            if (ent.XZMTZXSJ + "" == "on")
                            {
                                cronExpress += " " + ent.XZMTKSSJ + "-" + ent.XZMTJSSJ + " ? * " + weekday;
                            }
                            else
                            {
                                cronExpress += " * ? * " + weekday;
                            }
                        }
                        break;
                    case "天":
                        if (ent.CRONMTTYPE == "一次")
                        {
                            DateTime dttemp = Convert.ToDateTime("2013/10/10 " + ent.MYMTZXYC);
                            cronExpress = dttemp.Second + " " + dttemp.Minute + " " + dttemp.Hour + " * * ?";
                        }
                        else if (ent.CRONMTTYPE == "周期")
                        {
                            int ms = Convert.ToInt32(ent.MYMTZXDCM);
                            if (ms < 60)
                            {
                                cronExpress = "*/" + ms + " *";
                            }
                            else if (ms < 3600)
                            {
                                cronExpress = "0 */" + Convert.ToInt32(ms / 60);
                            }

                            if (ent.XZMTZXSJ + "" == "on")
                            {
                                cronExpress += " " + ent.XZMTKSSJ + "-" + ent.XZMTJSSJ + " * * ?";
                            }
                            else
                            {
                                cronExpress += " * * * ?";
                            }
                        }
                        break;
                }

            }
            return cronExpress;
        }

        /// <summary>
        /// 获取查询条件的json字符串
        /// 这里加工一层为了简化查询条件,去除没有用的字段
        /// </summary>
        /// <param name="entStrList"></param>
        /// <returns></returns>
        private string GetConditonStr(IList<string> entStrList)
        {
            //HttpUtility.UrlEncode(JsonHelper.GetJsonString(RequestData.GetList<string>("ddt")));
            List<SubscribeQuery> ents = entStrList.Select(tent => JsonHelper.GetObject<SubscribeQuery>(tent) as SubscribeQuery).ToList();
            return HttpUtility.UrlEncode(JsonHelper.GetJsonString(ents));
        }

        #endregion
    }
}

