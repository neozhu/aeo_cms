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
    public class ExportTemplateParser :TemplateParser
    {
        #region 构造函数

        public ExportTemplateParser(string filePath)
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
            ExportTemplateStructure struc = new ExportTemplateStructure();

            // 解析列节点
            foreach (ExcelCell tec in ExcelCellList)
            {
                ExportTemplateColumnNodeConfigProcessor tproc = new ExportTemplateColumnNodeConfigProcessor();
                ExportTemplateColumnNode tnode = tproc.GetObject(tec.Comment);

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

                    struc.DefaultGroup.ColumnNodeList.Add(tnode);
                }
            }

            // 解析命令节点
            foreach (ExcelCell tec in ExcelCellList)
            {
                ExportTemplateCommandNodeConfigProcessor tproc = new ExportTemplateCommandNodeConfigProcessor();
                ExportTemplateCommandNode tnode = tproc.GetObject(tec.Comment);

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
                ExportTemplatePropertyNodeConfigProcessor tproc = new ExportTemplatePropertyNodeConfigProcessor();
                ExportTemplatePropertyNode tnode = tproc.GetObject(tec.Comment);

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
