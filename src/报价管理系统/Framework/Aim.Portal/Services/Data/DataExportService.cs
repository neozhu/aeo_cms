using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using Aspose.Cells;
using NHibernate.Id;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.FileSystem;
using Aim.Component.ThirdpartySupport.MsOffice;
using Aim.Common;

namespace Aim.Portal.Data
{
    /// <summary>
    /// 数据导入服务
    /// </summary>
    public class DataExportService
    {
        /// <summary>
        /// 当模版文件变化时操作
        /// </summary>
        /// <param name="ent"></param>
        public static void DoExportTemplateFileChanged(SysDataExportTemplate ent)
        {
            // 当模版文件有变化是，同时更新下载文件与配置模版
            string filePath = FileService.GetFilePathByFullID(ent.TemplateFileID.TrimEnd(','));

            ExportTemplateParser itp = new ExportTemplateParser(filePath);
            ExportTemplateStructure its = itp.GetStructure() as ExportTemplateStructure;

            if (its != null)
            {
                ent.Config = its.GetConfig();
            }
        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="ent"></param>
        public static void DoExport(SysDataExportTemplate ent, IList<EasyDictionary> listValue, EasyDictionary valueDict)
        {
            FileItem fitem = FileService.GetFileItemByFullID(ent.TemplateFileID);
            ExportTemplateStructure struc = JsonHelper.GetObject<ExportTemplateStructure>(ent.Config);

            // 拷贝文件到临时文件夹
            FileInfo fi = new FileInfo(fitem.FilePath);
            FileInfo tfi = fi.CopyTo(Path.Combine(fi.Directory.FullName, "exptmp_" + DateTime.Now.ToFileTime().ToString() + "_" + fi.Name));

            try
            {
                using (ExcelProcessor processor = ExcelService.GetProcessor(tfi.FullName))
                {
                    Worksheet ws = processor.GetFirstSheet();

                    ClearTmplComments(struc.DefaultGroup, ws);

                    IList<ExportTemplateColumnNode> singlecols = struc.DefaultGroup.ColumnNodeList.Where(v => v.IsSingle).ToList();
                    IList<ExportTemplateColumnNode> cols = struc.DefaultGroup.ColumnNodeList.Where(v => !v.IsSingle).ToList();

                    if (singlecols.Count > 0 && valueDict != null)
                    {
                        object tval = null;

                        foreach (ExportTemplateColumnNode tmplnode in singlecols)
                        {
                            tval = valueDict.Get(tmplnode.Name);

                            ws.Cells[tmplnode.ValueColumnIndex.Value, tmplnode.ValueRowIndex.Value].PutValue(tval ?? tmplnode.DefaultValue);
                        }
                    }

                    // 导出列表数据(listValue的数据)
                    if (cols.Count > 0)
                    {
                        ExportTemplateCommandNode itcnBegin = struc.DefaultGroup.CommandNodeList.First(tent => tent.CommandCode == ExportTemplateCommandCode.Begin);
                        ExportTemplateCommandNode itcnEnd = struc.DefaultGroup.CommandNodeList.First(tent => tent.CommandCode == ExportTemplateCommandCode.End);

                        int startRowIndex = itcnBegin.RowIndex;
                        int endRowIndex = itcnEnd.RowIndex;

                        int startColumnIndex = itcnBegin.ColumnIndex;
                        int endColumnIndex = itcnEnd.ColumnIndex;

                        int exportcount = 0;

                        if (itcnBegin.RowIndex == itcnEnd.RowIndex)
                        {
                            // 导出所有listValue行
                            exportcount = listValue.Count;
                        }
                        else
                        {
                            // 导出部分listValue行
                            exportcount = endRowIndex - startRowIndex;
                            exportcount = exportcount < listValue.Count ? exportcount : listValue.Count;
                        }
                        if (exportcount != 0)
                            ws.Cells.InsertRows(startRowIndex + 1, exportcount);

                        for (int i = 1; i <= exportcount; i++)
                        {
                            EasyDictionary dict = listValue[i - 1];

                            int rowIndex = startRowIndex + i;

                            ws.Cells.CopyRow(ws.Cells, startRowIndex, rowIndex);

                            if (dict != null)
                            {
                                object tval = null;

                                foreach (ExportTemplateColumnNode tmplnode in cols)
                                {
                                    tval = dict.Get(tmplnode.Name);

                                    ws.Cells[rowIndex, tmplnode.ValueColumnIndex.Value].PutValue(tval ?? tmplnode.DefaultValue);
                                }
                            }
                        }

                        ws.Cells.DeleteRow(startRowIndex);
                    }

                    // 循环赋值
                    processor.Workbook.Save(tfi.FullName);
                }

                WebHelper.ResponseFile(tfi.FullName, fi.Name);
            }
            finally
            {
                // 导出结束后删除文件
                tfi.Delete();
            }

            //FileItem tfitem = FileService.CreateFileItem(tfi, fitem.Folder);

            //return tfitem.Id;
        }

        /// <summary>
        /// 清除模板批注
        /// </summary>
        public static void ClearTmplComments(ExportTemplateGroup tmplgrp, Worksheet ws)
        {
            /*
            struc.GroupList.All((ExportTemplateGroup tgrp) =>
            {
                return true;
            });
             * */

            tmplgrp.ColumnNodeList.All((ExportTemplateColumnNode node) =>
            {
                ws.Comments.RemoveAt(node.ValueRowIndex.Value, node.ValueColumnIndex.Value);
                return true;
            });

            tmplgrp.CommandNodeList.All((ExportTemplateCommandNode node) =>
            {
                ws.Comments.RemoveAt(node.RowIndex, node.ColumnIndex);
                return true;
            });
        }
    }

    /// <summary>
    /// 数据导出处理
    /// </summary>
    public class DataExportProcessor : IDisposable
    {
        #region 属性成员

        private ExcelProcessor _Processor;

        private ExportTemplateStructure _TemplateStructure;

        /// <summary>
        /// Excel 处理
        /// </summary>
        public ExcelProcessor Processor
        {
            get { return _Processor; }
        }

        public ExportTemplateStructure TemplateStructure
        {
            get { return _TemplateStructure; }
        }

        #endregion

        #region 构造函数

        public DataExportProcessor(ExportTemplateStructure struc, string filePath)
        {
            _TemplateStructure = struc;
            _Processor = ExcelService.GetProcessor(filePath);
        }

        public DataExportProcessor(ExportTemplateStructure struc, ExcelProcessor processor)
        {
            _TemplateStructure = struc;
            _Processor = processor;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetValue(string key, object value)
        {
        }

        public void InsertData()
        {
            ExportTemplateCommandNode itcnBegin = _TemplateStructure.DefaultGroup.CommandNodeList.First(tent => tent.CommandCode == ExportTemplateCommandCode.Begin);
            ExportTemplateCommandNode itcnEnd = _TemplateStructure.DefaultGroup.CommandNodeList.First(tent => tent.CommandCode == ExportTemplateCommandCode.End);

        }

        #endregion

        #region IDisposable 成员

        /// <summary>
        /// 释放信息
        /// </summary>
        public void Dispose()
        {
            if (_Processor != null)
            {
                _Processor.Dispose();
            }
        }

        #endregion
    }
}
