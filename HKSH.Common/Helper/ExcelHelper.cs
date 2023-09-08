using NPOI.SS.UserModel;
using System.Data;

namespace HKSH.Common.Helper
{
    /// <summary>
    /// Summary description for OfficeHelper
    /// </summary>
    public static class ExcelHelper
    {
        /// <summary>
        /// Reads the excel to data table.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="sheetName">Name of the sheet.</param>
        /// <param name="isFirstRowColumn">if set to <c>true</c> [is first row column].</param>
        /// <param name="columnNameStartRow">The column name start row.</param>
        /// <param name="startRowNumber">The start row number.</param>
        /// <returns></returns>
        public static DataTable ReadExcelToDataTable(string fileName, string sheetName = "", bool isFirstRowColumn = true, int? columnNameStartRow = null, int? startRowNumber = null)
        {
            DataTable data = new();
            if (!System.IO.File.Exists(fileName))
            {
                return null!;
            }
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            IWorkbook workbook = WorkbookFactory.Create(fs);
            ISheet sheet;
            if (!string.IsNullOrEmpty(sheetName))
            {
                sheet = workbook.GetSheet(sheetName);
                sheet ??= workbook.GetSheetAt(0);
            }
            else
            {
                sheet = workbook.GetSheetAt(0);
            }
            if (sheet != null)
            {
                IRow firstRow = columnNameStartRow == null ? sheet.GetRow(0) : sheet.GetRow(columnNameStartRow.Value);
                int cellCount = firstRow.LastCellNum;
                int startRow;
                if (isFirstRowColumn || columnNameStartRow != null)
                {
                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        ICell cell = firstRow.GetCell(i);
                        if (cell != null)
                        {
                            string cellValue = cell.StringCellValue;
                            if (cellValue != null)
                            {
                                DataColumn column = new DataColumn(cellValue);
                                data.Columns.Add(column);
                            }
                        }
                    }
                    if (startRowNumber != null)
                    {
                        startRow = startRowNumber.Value;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum + 1;
                    }
                }
                else
                {
                    if (startRowNumber != null)
                    {
                        startRow = startRowNumber.Value;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }
                }
                int rowCount = sheet.LastRowNum;
                for (int i = startRow; i <= rowCount; ++i)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null)
                    {
                        continue;
                    }

                    DataRow dataRow = data.NewRow();
                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                    {
                        if (row.GetCell(j) != null)
                        {
                            dataRow[j] = row.GetCell(j).ToString();
                        }
                    }
                    data.Rows.Add(dataRow);
                }
            }
            return data;
        }

        /// <summary>
        /// Reads the stream to data table.
        /// </summary>
        /// <param name="fileStream">The file stream.</param>
        /// <param name="sheetName">Name of the sheet.</param>
        /// <param name="isFirstRowColumn">if set to <c>true</c> [is first row column].</param>
        /// <param name="columnNameStartRow">The column name start row.</param>
        /// <param name="startRowNumber">The start row number.</param>
        /// <param name="startCellNum">The start row number.</param>
        /// <returns></returns>
        public static DataTable ReadStreamToDataTable(Stream fileStream, string sheetName = "", bool isFirstRowColumn = true, int? columnNameStartRow = null, int? startRowNumber = null, int startCellNum = 0)
        {
            DataTable data = new();
            IWorkbook workbook = WorkbookFactory.Create(fileStream);
            ISheet sheet;
            if (!string.IsNullOrEmpty(sheetName))
            {
                sheet = workbook.GetSheet(sheetName);
                if (sheet == null)
                {
                    sheet = workbook.GetSheetAt(0);
                }
            }
            else
            {
                sheet = workbook.GetSheetAt(0);
            }
            if (sheet != null)
            {
                IRow firstRow = columnNameStartRow == null ? sheet.GetRow(0) : sheet.GetRow(columnNameStartRow.Value);
                int cellCount = firstRow.LastCellNum;
                int startRow;
                if (isFirstRowColumn || columnNameStartRow != null)
                {
                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        ICell cell = firstRow.GetCell(i);
                        if (cell != null)
                        {
                            cell.SetCellType(CellType.String);
                            string cellValue = cell.StringCellValue;
                            if (cellValue != null)
                            {
                                DataColumn column = new DataColumn(cellValue);
                                data.Columns.Add(column);
                            }
                        }
                    }

                    if (startRowNumber != null)
                    {
                        startRow = startRowNumber.Value;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum + 1;
                    }
                }
                else
                {
                    if (startRowNumber != null)
                    {
                        startRow = startRowNumber.Value;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }
                }
                int rowCount = sheet.LastRowNum;
                for (int i = startRow; i <= rowCount; ++i)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null || row.FirstCellNum < 0)
                    {
                        continue;
                    }
                    DataRow dataRow = data.NewRow();
                    for (int j = row.FirstCellNum; j < cellCount - startCellNum; ++j)
                    {
                        ICell cell = row.GetCell(j);
                        if (cell != null)
                        {
                            if (cell.CellType == CellType.Numeric)
                            {
                                if (DateUtil.IsCellDateFormatted(cell))
                                {
                                    dataRow[j] = row.GetCell(j + startCellNum).DateCellValue;
                                }
                                else
                                {
                                    dataRow[j] = row.GetCell(j + startCellNum).ToString()?.Trim();
                                }
                            }
                            else
                            {
                                dataRow[j] = row.GetCell(j + startCellNum).ToString()?.Trim();
                            }
                        }
                    }
                    data.Rows.Add(dataRow);
                }
            }
            return data;
        }
    }
}