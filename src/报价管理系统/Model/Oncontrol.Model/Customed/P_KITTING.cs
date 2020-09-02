using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using Aim.Data;
using System.Data;

namespace OnControl.Model
{
    /// <summary>
    /// 自定义实体类
    /// </summary>
    [Serializable]
    public partial class P_KITTING
    {
        #region 成员变量

        #endregion

        #region 成员属性

        #endregion

        #region 公共方法

        /// <summary>
        /// 验证操作
        /// </summary>
        public void DoValidate()
        {
            // 检查是否存在重复键
            /*if (!this.IsPropertyUnique("UniqueKey"))
            {
                throw new RepeatedKeyException("存在重复的 UniqueKey “" + this.UniqueKey + "”");
            }*/
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void DoSave()
        {
            if (String.IsNullOrEmpty(ID))
            {
                this.DoCreate();
            }
            else
            {
                this.DoUpdate();
            }
        }

        /// <summary>
        /// 创建操作
        /// </summary>
        public void DoCreate()
        {
            this.DoValidate();


            // 事务开始
            this.CreateAndFlush();
        }

        /// <summary>
        /// 修改操作
        /// </summary>
        /// <returns></returns>
        public void DoUpdate()
        {
            this.DoValidate();


            this.UpdateAndFlush();
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        public void DoDelete()
        {
            this.Delete();
        }

        #endregion

        #region 静态成员

        /// <summary>
        /// 批量删除操作
        /// </summary>
        public static void DoBatchDelete(params object[] args)
        {
            P_KITTING[] tents = P_KITTING.FindAll(Expression.In("ID", args));

            foreach (P_KITTING tent in tents)
            {
                tent.DoDelete();
            }
        }
        /// <summary>
        /// 检查Kitting导入行的数据是否合格
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="row"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool CheckDataRules(Aim.Portal.Model.SysUser user, DataTable dataRows, ref string message)
        {
            message = "";
            int index = 1;
            //e)	对工单明细行的重复行判断：对同一Call Off中，依据【工单号】、【料号】、【from库别】及【to库别】做工单明细行的重复判断，且【项次】栏位不能重复，否则不予导入。
            HashSet<string> typeItem = new HashSet<string>();
            HashSet<string> hasItem = new HashSet<string>();
            HashSet<string> hasseq = new HashSet<string>();
            bool flag = true;
            switch (user.Ext1.ToUpper())
            {
                case "纬创":
                    foreach (DataRow row in dataRows.Rows)
                    {
                        index++;
                        CheckWC(ref flag, index, row, ref message);
                        //厂别和当前导入人必须一致
                        if (row[1].ToString() == "" || user.Ext2.IndexOf(row[1].ToString()) < 0)
                        {
                            message += "第" + index.ToString() + "行[" + row[7] + "]:厂别必须和导入人所在的厂别一致!";
                            flag = false;
                        }
                        if (hasseq.Contains(row[0].ToString().Trim()))
                        {
                            message += "第" + index.ToString() + "行[" + row[7] + "]:【项次】栏位不能重复!";
                            flag = false;
                        }
                        else
                        {
                            hasseq.Add(row[0].ToString().Trim());
                        }
                        if (!typeItem.Contains(row[3].ToString().Trim()) && typeItem.Count > 1)
                        {
                            message += "一笔Call Off只允许一个移转类型!";
                            flag = false;
                        }
                        else
                        {
                            typeItem.Add(row[3].ToString().Trim());
                        }
                        //0項次1厂别2PD Line3移转类型4工单号5站别6Item Line7料號8品名9Keeper Code10From 庫別
                        //11To 庫別12需求數量13交货Building14送貨碼頭15请求发货时间16備註
                        string checkunique = row[4].ToString().Trim() + row[2].ToString().Trim() + row[7].ToString().Trim() + row[10].ToString().Trim() + row[11].ToString().Trim();
                        if (row[3].ToString().Trim() == "261")
                            checkunique = row[4].ToString().Trim() + row[7].ToString().Trim() + row[10].ToString().Trim() + row[11].ToString().Trim();
                        if (hasItem.Contains(checkunique))
                        {
                            if (row[3].ToString().Trim() == "261")
                                message += "第" + index.ToString() + "行[" + row[7] + "]:【工单号】、【料号】、【from库别】及【to库别】不能重复!";
                            else
                                message += "第" + index.ToString() + "行[" + row[7] + "]:【工单号】、【PD LINE】、【from库别】及【to库别】、【料号】不能重复!";
                            flag = false;
                        }
                        else
                        {
                            hasItem.Add(checkunique);
                        }
                    }
                    break;
                case "世硕":
                    foreach (DataRow row in dataRows.Rows)
                    {
                        index++;
                        CheckSS(ref flag, index, row, ref message);
                        //厂别和当前导入人必须一致
                        if (row[1].ToString() == "" || user.Ext2.IndexOf(row[1].ToString()) < 0)
                        {
                            message += "第" + index.ToString() + "行[" + row[7] + "]:厂别必须和导入人的厂别一致!";
                            flag = false;
                        }
                        if (hasseq.Contains(row[0].ToString().Trim()))
                        {
                            message += "第" + index.ToString() + "行[" + row[7] + "]:【项次】栏位不能重复!";
                            flag = false;
                        }
                        else
                        {
                            hasseq.Add(row[0].ToString().Trim());
                        }
                        if (hasItem.Contains(row[4].ToString().Trim() + row[7].ToString().Trim() + row[10].ToString().Trim() + row[11].ToString().Trim()))
                        {
                            message += "第" + index.ToString() + "行[" + row[7] + "]:【工单号】、【料号】、【from库别】及【to库别】不能重复!";
                            flag = false;
                        }
                        else
                        {
                            hasItem.Add(row[4].ToString().Trim() + row[7].ToString().Trim() + row[10].ToString().Trim() + row[11].ToString().Trim());
                        }
                        if (!typeItem.Contains(row[3].ToString().Trim()) && typeItem.Count > 1)
                        {
                            message += "一笔Call Off只允许一个移转类型!";
                            flag = false;
                        }
                        else
                        {
                            typeItem.Add(row[3].ToString().Trim());
                        }
                    }
                    break;
                case "WOK":
                    foreach (DataRow row in dataRows.Rows)
                    {
                        index++;
                        CheckWOK(ref flag, index, row, ref message);
                        //厂别和当前导入人必须一致
                        if (row[1].ToString() == "" || user.Ext2.IndexOf(row[1].ToString()) < 0)
                        {
                            message += "第" + index.ToString() + "行[" + row[7] + "]:厂别必须和导入人的厂别一致!";
                            flag = false;
                        }
                        if (hasseq.Contains(row[0].ToString().Trim()))
                        {
                            message += "第" + index.ToString() + "行[" + row[7] + "]:【项次】栏位不能重复!";
                            flag = false;
                        }
                        else
                        {
                            hasseq.Add(row[0].ToString().Trim());
                        }
                        if (!typeItem.Contains(row[3].ToString().Trim()) && typeItem.Count > 1)
                        {
                            message += "一笔Call Off只允许一个移转类型!";
                            flag = false;
                        }
                        else
                        {
                            typeItem.Add(row[3].ToString().Trim());
                        }
                        string checkunique = row[4].ToString().Trim() + row[2].ToString().Trim() + row[7].ToString().Trim() + row[10].ToString().Trim() + row[11].ToString().Trim();
                        if (row[3].ToString().Trim() == "261")
                            checkunique = row[4].ToString().Trim() + row[7].ToString().Trim() + row[10].ToString().Trim() + row[11].ToString().Trim();
                        if (hasItem.Contains(checkunique))
                        {
                            if (row[3].ToString().Trim() == "261")
                                message += "第" + index.ToString() + "行[" + row[7] + "]:【工单号】、【料号】、【from库别】及【to库别】、【料号】不能重复!";
                            else
                                message += "第" + index.ToString() + "行[" + row[7] + "]:【工单号】、【PD LINE】、【from库别】及【to库别】、【料号】不能重复!";
                            flag = false;
                        }
                        else
                        {
                            hasItem.Add(checkunique);
                        }
                    }
                    break;
            }
            return flag;
        }


        public static void CheckWOK(ref bool flag, int index, DataRow row, ref string message)
        {
            //a)	限定移转类型数据为‘311’和‘261’
            //b)	若【移转类型】为311，则【工单号】为空，否则不允许导入。
            //c)	若【移转类型】为261，则【工单号】必须有值，且【工单号】必须为8位字符，否则不允许导入。
            //d)	Call Off模板数据维护中，【PD Line】为必输项，长度为4码；261模式中，同一批次号（即同一笔Call Off上传）中【PD Line】必须一致，否则导入报错。
            //e)	对工单明细行的重复行判断：对同一Call Off中，依据【工单号】、【料号】、【from库别】及【to库别】做工单明细行的重复判断，且【项次】栏位不能重复，否则不予导入。
            //f)	Call Off模板中【站别】及【Item line】为空，则不允许导入；同一个Call Off上传只允许一个移转类型，如存在2个或者2个以上的移转形态，否则不允许导入。
            //g)	Call Off模板中from库别】及【to库别】的信息，不予许两者一致，否则不允许导入。
            //h)	【需求数量】必须大于0，否则不允许导入。
            //i)	【请求发货时间】不能小于当前系统时间，否则不允许导入。
            //j)	验证信息全部做alert弹窗，弹出对话框告知使用人员错误信息。

            if (!string.IsNullOrEmpty(row[3].ToString()) && (row[3].ToString() == "311" || row[3].ToString() == "261"))//a
            {
                if (row[3].ToString() == "311")//b
                {
                    if (row[4].ToString().Trim() != "")
                    {
                        message += "第" + index.ToString() + "行[" + row[7] + "]:【工单号】必须为空!\\n";
                        flag = false;
                    }
                }
                else if (row[3].ToString() == "261")//c
                {
                    if (row[4].ToString().Trim() == "" || row[6].ToString().Trim() == "" || row[4].ToString().Trim().Length != 8 || row[10].ToString().Trim() == "" || row[11].ToString().Trim() != "")
                    {
                        message += "第" + index.ToString() + "行[" + row[7] + "]:【Item line】不能为空,【工单号】必须为8位.【from库别】必填且【to库别】为空!\\n";
                        flag = false;
                    }
                }
            }
            else
            {
                message += "第" + index.ToString() + "行[" + row[7] + "]:【移转类型】必须为‘311’和‘261’\\n";
                flag = false;
            }
            if (row[2].ToString().Trim() == "" || row[2].ToString().Trim().Length != 4)//d
            {
                message += "第" + index.ToString() + "行[" + row[7] + "]:【PD Line】不能为空，且长度为4码\\n";
                flag = false;
            }
            /*if (row[5].ToString().Trim() == "" || row[6].ToString().Trim() == "")//f
            {
                message += "第" + index.ToString() + "行[" + row[7] + "]:【站别】及【Item line】不能为空\\n";
                flag = false;
            }*/
            decimal qty = 0;
            if (row[12].ToString().Trim() == "" || !decimal.TryParse(row[12].ToString().Trim(), out qty) || decimal.Parse(row[12].ToString().Trim()) <= 0)
            {
                message += "第" + index.ToString() + "行[" + row[7] + "]:【需求数量】必须大于0\\n";
                flag = false;
            }
            DateTime dt = new DateTime();
            if (row[15].ToString().Trim() == "" || !DateTime.TryParse(row[15].ToString().Trim(), out dt) || DateTime.Parse(row[15].ToString().Trim()) < DateTime.Now)
            {
                message += "第" + index.ToString() + "行[" + row[7] + "]:【请求发货时间】不能小于当前系统时间,请使用格式:2016/12/09 11:03:00\\n";
                flag = false;
            }
        }

        public static void CheckSS(ref bool flag, int index, DataRow row, ref string message)
        {
            //a)	限定【移转类型】数据为‘311’、‘261’、‘SO’和‘Z41’(261、SO及Z41订单类型，以下暂用261指代)
            // b)	若【移转类型】为311，则【工单号】为空，否则不允许导入；同时，【from库别】及【to库别】必填且不允许一致，否则不予导入；
            // c)	若移转类型为261，则【工单号】及【Item line】必须有值，且【工单号】必须为8位字符，否则不允许导入；同时，【from库别】必填且to库别为空，否则不予导入。
            // d)	Call Off模板数据维护中，【PD Line】为必输项，长度为4码；一笔Call Off只允许一个移转类型，如存在2个或2个以上的移转形态，否则不允许导入。
            // e)	对工单明细行的重复行判断：对同一Call Off中，依据【工单号】、【料号】、【from库别】及【to库别】做工单明细行的重复判断，且【项次】栏位不能重复，否则不予导入。
            // f)	【需求数量】必须大于0，则不允许导入。
            // g)	【请求发货时间】不能小于当前系统时间，则不允许导入。
            // h)	验证信息全部做alert弹窗，弹出对话框告知使用人员错误信息。
            if (!string.IsNullOrEmpty(row[3].ToString()) && (row[3].ToString() == "311" || row[3].ToString() == "261" || row[3].ToString().ToUpper() == "SO" || row[3].ToString().ToUpper() == "Z41"))//a
            {
                if (row[3].ToString() == "311")//b
                {
                    if (row[4].ToString().Trim() != "" || row[10].ToString().Trim() == "" || row[11].ToString().Trim() == "" || row[10].ToString().Trim() == row[11].ToString().Trim())
                    {
                        message += "第" + index.ToString() + "行[" + row[7] + "]:【工单号】必须为空 ,【from库别】及【to库别】必填且不允许一致!\\n";
                        flag = false;
                    }
                }
                else if (row[3].ToString() == "261" || row[3].ToString().ToUpper() == "SO" || row[3].ToString().ToUpper() == "Z41")//c
                {
                    if (row[6].ToString().Trim() == "" || row[4].ToString().Trim().Length != 8 || row[10].ToString().Trim() == "" || row[11].ToString().Trim() != "")
                    {
                        message += "第" + index.ToString() + "行[" + row[7] + "]:【Item line】不能为空,【工单号】必须为8位.【from库别】必填且【to库别】为空!\\n";
                        flag = false;
                    }
                }
            }
            else
            {
                message += "第" + index.ToString() + "行[" + row[7] + "]:【移转类型】必须为‘311’、‘261’、‘SO’和‘Z41’\\n";
                flag = false;
            }
            if (row[2].ToString().Trim() == "" || row[2].ToString().Trim().Length != 4)
            {
                message += "第" + index.ToString() + "行[" + row[7] + "]:【PD Line】不能为空，且长度为码\\n";
                flag = false;
            }
            decimal qty = 0;
            if (row[12].ToString().Trim() == "" || !decimal.TryParse(row[12].ToString().Trim(), out qty) || decimal.Parse(row[12].ToString().Trim()) <= 0)
            {
                message += "第" + index.ToString() + "行[" + row[7] + "]:【需求数量】必须大于0\\n";
                flag = false;
            }
            DateTime dt = new DateTime();
            if (row[15].ToString().Trim() == "" || !DateTime.TryParse(row[15].ToString().Trim(), out dt) || DateTime.Parse(row[15].ToString().Trim()) < DateTime.Now)
            {
                message += "第" + index.ToString() + "行[" + row[7] + "]:【请求发货时间】不能小于当前系统时间,请使用格式:2016/12/09 11:03:00\\n";
                flag = false;
            }
        }


        public static void CheckWC(ref bool flag, int index, DataRow row, ref string message)
        {

            //0項次1厂别2PD Line3移转类型4工单号5站别6Item Line7料號8品名9Keeper Code10From 庫別
            //11To 庫別12需求數量13交货Building14送貨碼頭15请求发货时间16備註

            //           a)	限定【移转类型】数据为‘库房移转’和‘工单发料’。
            //b)	若【移转类型】为库房移转，则【工单号】为空，否则不允许导入；同时，【from库别】及【to库别】必填且不允许一致，否则不予导入。
            //c)	若【移转类型】为工单发料，则【工单号】及【Item line】必须有值，且【工单号】必须为8位字符，否则不允许导入；同时，【from库别】必填且【to库别】为空，否则不予导入。
            //d)	Call Off模板数据维护中，【PD Line】为必输项，且长度为3或4码，不允许为空；一笔Call Off只允许一个移转类型，如存在2个或2个以上的移转形态，否则不允许导入。
            //e)	对工单明细行的重复行判断：对同一Call Off中，依据【工单号】、【料号】、【from库别】及【to库别】做工单明细行的重复判断，且【项次】栏位不能重复，否则不予导入。
            //修改 :e)f)	在同一个From库别、To库别、WORKNO和PDLINE信息下，明细行中的料号信息不允许重复
            //f)	【需求数量】必须大于0，否则不允许导入。
            //g)	【请求发货时间】不能小于当前系统时间，否则不允许导入。
            if (!string.IsNullOrEmpty(row[3].ToString()) && (row[3].ToString().Trim() == "311" || row[3].ToString().Trim() == "261"))//a
            {
                if (row[3].ToString().Trim() == "311")//b
                {
                    if (row[4].ToString().Trim() != "" || row[10].ToString().Trim() == "" || row[11].ToString().Trim() == "" || row[10].ToString().Trim() == row[11].ToString().Trim())
                    {
                        message += "第" + index.ToString() + "行[" + row[7] + "]:【工单号】必须为空,【from库别】及【to库别】不允许一致!\\n";
                        flag = false;
                    }
                }
                else if (row[3].ToString().Trim() == "261")//c
                {
                    if (row[6].ToString().Trim() == "" || row[4].ToString().Trim().Length != 8 || row[10].ToString().Trim() == "" || row[11].ToString().Trim() != "")
                    {
                        message += "第" + index.ToString() + "行[" + row[7] + "]:【Item line】不能为空,【工单号】必须为8位.【from库别】必填且【to库别】为空!\\n";
                        flag = false;
                    }
                }
            }
            else
            {
                message += "第" + index.ToString() + "行[" + row[7] + "]:【移转类型】必须为‘311’和‘261’\\n";
                flag = false;
            }
            if (row[2].ToString().Trim() == "" || row[2].ToString().Trim().Length != 3 && row[2].ToString().Trim().Length != 4)
            {
                message += "第" + index.ToString() + "行[" + row[7] + "]:【PD Line】不能为空，且长度为3或4码\\n";
                flag = false;
            }
            decimal qty = 0;
            if (row[12].ToString().Trim() == "" || !decimal.TryParse(row[12].ToString().Trim(), out qty) || decimal.Parse(row[12].ToString().Trim()) <= 0)
            {
                message += "第" + index.ToString() + "行[" + row[7] + "]:【需求数量】必须大于0\\n";
                flag = false;
            }
            DateTime dt = new DateTime();
            if (row[15].ToString().Trim() == "" || !DateTime.TryParse(row[15].ToString().Trim(), out dt) || DateTime.Parse(row[15].ToString().Trim()) < DateTime.Now)
            {
                message += "第" + index.ToString() + "行[" + row[7] + "]:【请求发货时间】不能小于当前系统时间,请使用格式:2016/12/09 11:03:00\\n";
                flag = false;
            }
        }

        #endregion

    } // P_KITTING
}


