using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using xz_ypcl_unit.Attribute;

namespace xz_ypcl_unit.Excel
{
    public class NPOIExcel
    {
        private string _title;
        private string _sheetName;

        public NPOIExcel(string title, string sheetName)
        {
            this._title = title;
            this._sheetName = sheetName;
        }

        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public MemoryStream ToExcel<T>(List<T> table) where T : new()
        {
            int excelColumnNum = 0;
            MemoryStream memoryStream = new MemoryStream();
            IWorkbook workBook = new HSSFWorkbook();
            this._sheetName = string.IsNullOrEmpty(this._sheetName) ? "sheet1" : this._sheetName;
            ISheet sheet = workBook.CreateSheet(this._sheetName);

            //处理表格标题
            IRow row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue(this._title);
            Type type = typeof(T);
            PropertyInfo[] propers = type.GetProperties();
            row.Height = 500;

            ICellStyle cellStyle = workBook.CreateCellStyle();
            IFont font = workBook.CreateFont();
            font.FontName = "微软雅黑";
            font.FontHeightInPoints = 17;
            cellStyle.SetFont(font);
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.Alignment = HorizontalAlignment.Center;
            row.Cells[0].CellStyle = cellStyle;
            object[] excelTitletributes = null;
            //处理表格列头
            row = sheet.CreateRow(1);

            for (int i = 0; i < propers.Length; i++)
            {
                excelTitletributes = propers[i].GetCustomAttributes(typeof(ExcelAttribute), false);
                if (excelTitletributes != null && excelTitletributes.Count() > 0)
                {
                    row.CreateCell(excelColumnNum).SetCellValue(((ExcelAttribute)excelTitletributes[0]).Title);
                    row.Height = 350;
                    sheet.AutoSizeColumn(excelColumnNum);
                    excelColumnNum++;
                }
            }
            int cellFlag = 0;
            //处理数据内容
            for (int i = 0; i < table.Count; i++)
            {
                cellFlag = 0;
                row = sheet.CreateRow(2 + i);
                row.Height = 250;
                for (int j = 0; j < propers.Length; j++)
                {
                    excelTitletributes = propers[j].GetCustomAttributes(typeof(ExcelAttribute), false);
                    if (excelTitletributes != null && excelTitletributes.Count() > 0)
                    {

                        row.CreateCell(cellFlag).SetCellValue(propers[j].GetValue(table[i], null) == null ? "" : propers[j].GetValue(table[i], null).ToString());
                        sheet.SetColumnWidth(cellFlag, 256 * 15);
                        cellFlag++;
                    }

                }
            }
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, excelColumnNum - 1));
            //写入数据流
            workBook.Write(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }

        ///// <summary>
        ///// 导出到Excel
        ///// </summary>
        ///// <param name="table"></param>
        ///// <param name="title"></param>
        ///// <param name="sheetName"></param>
        ///// <returns></returns>
        //public bool ToExcel<T>(List<T> table, string title, string sheetName, string filePath) where T : new()
        //{
        //    this._title = title;
        //    this._sheetName = sheetName;
        //    this._filePath = filePath;
        //    return ToExcel(table);
        //}
    }
}
