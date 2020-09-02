using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Aim.Component;
using Aim.Component.ThirdpartySupport.MsOffice;
using Aim.Portal.Model;

namespace Aim.Portal.Data
{
    public class ImportTemplateParser :TemplateParser
    {
        #region 构造函数

        public ImportTemplateParser(string filePath)
            : base(filePath)
        {
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 获取模版结构
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public override TemplateStructure GetStructure()
        {
            ImportTemplateStructure struc = new ImportTemplateStructure();

            // 解析列节点
            foreach (ExcelCell tec in ExcelCellList)
            {
                ImportTemplateColumnNodeConfigProcessor tproc = new ImportTemplateColumnNodeConfigProcessor();
                ImportTemplateColumnNode tnode = tproc.GetObject(tec.Comment);

                if (tnode != null)
                {
                    if (!tnode.ValueColumnIndex.HasValue)
                    {
                        tnode.ValueColumnIndex = tec.ColumnIndex;
                    }

                    if (!tnode.ValueRowIndex.HasValue)
                    {
                        tnode.ValueRowIndex = tec.RowIndex;
                    }

                    if (struc.DefaultGroup.ColumnNodeList.Count(tent => tent.ColumnName == tnode.ColumnName) <= 0)
                    {
                        struc.DefaultGroup.ColumnNodeList.Add(tnode);
                    }
                    else
                    {
                        throw new Exception("不合法模版：模版中存在同名数据列“" + tnode.ColumnName + "”。");
                    }
                }
            }

            // 解析命令节点
            foreach (ExcelCell tec in ExcelCellList)
            {
                ImportTemplateCommandNodeConfigProcessor tproc = new ImportTemplateCommandNodeConfigProcessor();
                ImportTemplateCommandNode tnode = tproc.GetObject(tec.Comment);

                if (tnode != null)
                {
                    tnode.ColumnIndex = tec.ColumnIndex;
                    tnode.RowIndex = tec.RowIndex;

                    struc.DefaultGroup.CommandNodeList.Add(tnode);
                }
            }

            // 解析属性节点
            foreach (ExcelCell tec in ExcelCellList)
            {
                ImportTemplatePropertyNodeConfigProcessor tproc = new ImportTemplatePropertyNodeConfigProcessor();
                ImportTemplatePropertyNode tnode = tproc.GetObject(tec.Comment);

                if (tnode != null)
                {
                    struc.DefaultGroup.PropertyNodeList.Add(tnode);
                }
            }

            return struc;
        }

        #endregion
    }
}
