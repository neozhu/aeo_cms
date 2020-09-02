using Aim;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Castle.ActiveRecord;
using Com.Feiliks.MDM;
using Com.Feiliks.QDM;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Oncontrol3.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aspose.Cells;

namespace Oncontrol3.Web.Helpers
{
    /// <summary>
    /// 通过表名以及排序字段以及排序方式做简单的查询
    /// </summary>
    public class GetDataHelper
    {
        public static DataTable GetDataOrderBy(string tableName,string wherestr,string oderBy,string descOrAsc)
        {
            string sql = string.Format("SELECT * FROM {0} WHERE {1} ORDER BY {2} {3}",tableName,wherestr,oderBy,descOrAsc);
            return DataHelper.QueryDataTable(sql);
        }
        public static void OutFileToDisk(DataTable dt, string tableName, string path)
        {
            Workbook workbook = new Workbook(); //工作簿
            Worksheet sheet = workbook.Worksheets[0]; //工作表
            Cells cells = sheet.Cells;//单元格

            //为标题设置样式    
            Style styleTitle = workbook.Styles[workbook.Styles.Add()];//新增样式
            styleTitle.HorizontalAlignment = TextAlignmentType.Center;//文字居中
            styleTitle.Font.Name = "宋体";//文字字体
            styleTitle.Font.Size = 16;//文字大小
            styleTitle.Font.IsBold = true;//粗体

            //样式2
            Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式
            style2.HorizontalAlignment = TextAlignmentType.Center;//文字居中
            style2.Font.Name = "宋体";//文字字体
            style2.Font.Size = 14;//文字大小
            style2.Font.IsBold = true;//粗体
            style2.IsTextWrapped = false;//单元格内容自动换行
            style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //样式3
            Style style3 = workbook.Styles[workbook.Styles.Add()];//新增样式
            style3.HorizontalAlignment = TextAlignmentType.Left;//文字居中
            style3.Font.Name = "宋体";//文字字体
            style3.Font.Size = 12;//文字大小
            style3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //样式4
            Style style4 = workbook.Styles[workbook.Styles.Add()];//新增样式
            style4.HorizontalAlignment = TextAlignmentType.Right;//文字居中
            style4.Font.Name = "宋体";//文字字体
            style4.Font.Size = 12;//文字大小
            style4.Number = 49;
            style4.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style4.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style4.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style4.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            int Colnum = dt.Columns.Count;//表格列数
            int Rownum = dt.Rows.Count;//表格行数

            //生成行1 标题行 
            cells.Merge(0, 0, 1, Colnum);//合并单元格
            cells[0, 0].PutValue(tableName);//填写内容
            cells[0, 0].SetStyle(styleTitle);
            cells.SetRowHeight(0, 38);

            //生成行2 列名行
            for (int i = 0; i < Colnum; i++)
            {
                cells[1, i].PutValue(dt.Columns[i].ColumnName);
                cells[1, i].SetStyle(style2);
                cells.SetRowHeight(1, 25);
                cells.SetColumnWidth(i, 20);
            }

            //生成数据行
            for (int i = 0; i < Rownum; i++)
            {
                for (int k = 0; k < Colnum; k++)
                {
                    if (dt.Rows[i][k] + "" != "" && dt.Rows[i][k] + "" == System.Text.RegularExpressions.Regex.Match(dt.Rows[i][k] + "", @"[\-\+]?([0-9]+)([\.]([0-9]+))?").Value)
                    {
                        cells[2 + i, k].PutValue(Convert.ToDouble(dt.Rows[i][k]));
                        cells[2 + i, k].SetStyle(style4);
                    }
                    else
                    {
                        cells[2 + i, k].PutValue(dt.Rows[i][k] + "");
                        cells[2 + i, k].SetStyle(style3);
                    }
                }
                cells.SetRowHeight(2 + i, 24);
            }

            workbook.Save(path);
        }
    }
}