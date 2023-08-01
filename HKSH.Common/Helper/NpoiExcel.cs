using HKSH.Common.Enums;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Data;

namespace HKSH.Common.Helper
{
    /// <summary>
    /// Npoi Excel
    /// </summary>
    public class NpoiExcel
    {
        /// <summary>
        /// The workbook
        /// </summary>
        private IWorkbook workbook = null!;

        /// <summary>
        /// The sheet names
        /// </summary>
        private List<string> sheetNames = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="NpoiExcel" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public NpoiExcel(string filePath)
        {
            FilePath = filePath;
            ExcelVersion = GetExcelVersion(FilePath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpoiExcel" /> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public NpoiExcel(Stream stream)
        {
            FileStream = stream;
            ExcelVersion = ExcelVersion.Above2007;
        }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the file stream.
        /// </summary>
        /// <value>
        /// The file stream.
        /// </value>
        public Stream FileStream { get; set; }

        /// <summary>
        /// Gets the workbook.
        /// </summary>
        /// <value>
        /// The workbook.
        /// </value>
        public IWorkbook Workbook
        {
            get
            {
                return workbook ??= ReadExcelFile(FilePath);
            }
        }

        /// <summary>
        /// Gets the stream workbook.
        /// </summary>
        /// <value>
        /// The stream workbook.
        /// </value>
        public IWorkbook StreamWorkbook
        {
            get
            {
                try
                {
                    return workbook ??= new XSSFWorkbook(FileStream);
                }
                catch (Exception)
                {
                    return new HSSFWorkbook(FileStream);
                }
            }
        }

        /// <summary>
        /// Gets the excel version.
        /// </summary>
        /// <value>
        /// The excel version.
        /// </value>
        public ExcelVersion ExcelVersion { get; }

        /// <summary>
        /// Gets the sheet names.
        /// </summary>
        /// <value>
        /// The sheet names.
        /// </value>
        public List<string> SheetNames
        {
            get
            {
                return sheetNames ??= GetSheetNames(Workbook);
            }
        }

        #region Read Excel

        /// <summary>
        /// Reads the excel file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public static IWorkbook ReadExcelFile(string filePath)
        {
            IWorkbook workbook = null!;
            var file = new FileInfo(filePath);
            using (Stream s = file.OpenRead())
            {
                if (file.Extension.ToLower().Equals(".xlsx"))
                {
                    workbook = new XSSFWorkbook(s);
                }
                else if (file.Extension.ToLower().Equals(".xls"))
                {
                    workbook = new HSSFWorkbook(s);
                }
            }
            return workbook;
        }

        /// <summary>
        /// Excels the sheet to data table.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        public static DataTable ExcelSheetToDataTable(ISheet sheet, int skip = 0)
        {
            var dt = new DataTable
            {
                TableName = sheet.SheetName
            };
            int firstRowIndex = sheet.FirstRowNum + skip;
            //Have sheet data and fill it into dataset
            IRow headRow = sheet.GetRow(firstRowIndex);
            if (headRow != null && headRow.Cells.Count > 0)
            {
                #region Read merged cells

                //Number of cells merged in the current sheet
                int mergedRegionCount = sheet.NumMergedRegions;
                var mergedRegionList = new List<CellRangeAddress>();
                for (int i = 0; i < mergedRegionCount; i++)
                {
                    CellRangeAddress region = sheet.GetMergedRegion(i);
                    if (region != null)
                    {
                        mergedRegionList.Add(region);
                    }
                }

                #endregion Read merged cells

                #region Create columns of datatable

                //Create a DataTable column based on the first row of the excel table
                for (int j = 0; j < headRow.LastCellNum; j++)
                {
                    ICell cell = headRow.GetCell(j);
                    object content = string.Empty;
                    content = GetCellValue(cell);
                    if (cell != null && string.IsNullOrWhiteSpace(content == null ? string.Empty : content.ToString()))
                    {
                        //If the current cell content is empty, judge whether it is a merged cell
                        CellRangeAddress region = mergedRegionList.Find(o => o.FirstRow <= 0 && 0 <= o.LastRow && o.FirstColumn <= j && j <= o.LastColumn);
                        if (region != null)
                        {
                            //Read the contents of merged cells
                            content = GetCellValue(sheet.GetRow(region.FirstRow).GetCell(region.FirstColumn)).ToString();
                        }
                    }
                    string colName = content == null ? string.Empty : content.ToString();
                    if (!dt.Columns.Contains(colName))
                    {
                        dt.Columns.Add(colName.Trim());
                    }
                    else
                    {
                        dt.Columns.Add();
                    }
                }

                #endregion Create columns of datatable

                #region Read all rows and populate the DataTable

                //Read the contents of the excel table and fill it into the DataTable
                for (int i = firstRowIndex + 1; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dr = dt.NewRow();
                    if (row == null)
                    {
                        dt.Rows.Add(dr); continue;
                    }
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        #region Read cell contents

                        ICell cell = row.GetCell(j);
                        object content = string.Empty;
                        content = GetCellValue(cell);
                        if (cell != null && string.IsNullOrWhiteSpace(content == null ? string.Empty : content.ToString()))
                        {
                            //If the current cell content is empty, judge whether it is a merged cell
                            CellRangeAddress region = mergedRegionList.Find(o => o.FirstRow <= i && i <= o.LastRow && o.FirstColumn <= j && j <= o.LastColumn);
                            if (region != null)
                            {
                                //Read the contents of merged cells
                                content = GetCellValue(sheet.GetRow(region.FirstRow).GetCell(region.FirstColumn));
                            }
                        }
                        dr[j] = content?.ToString()?.Trim();

                        #endregion Read cell contents
                    }
                    dt.Rows.Add(dr);
                }

                #endregion Read all rows and populate the DataTable
            }
            return dt;
        }

        /// <summary>
        /// Gets the cell value.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns></returns>
        public static object GetCellValue(ICell cell)
        {
            object content = null;
            if (cell == null)
            {
                return DBNull.Value;
            }

            switch (cell.CellType)
            {
                case CellType.Unknown:
                    content = DBNull.Value;
                    break;

                case CellType.Numeric:
                    try
                    {
                        content = DateUtil.IsCellDateFormatted(cell) ? cell.DateCellValue : (object)cell.NumericCellValue;
                    }
                    catch (Exception)
                    {
                        content = cell.NumericCellValue;
                    }
                    break;

                case CellType.String:
                    content = cell.StringCellValue;
                    break;

                case CellType.Formula:
                    content = GetCellFormulaVal(cell, content);
                    break;

                case CellType.Blank:
                    content = DBNull.Value;
                    break;

                case CellType.Boolean:
                    content = cell.BooleanCellValue;
                    break;

                case CellType.Error:
                    content = cell.ErrorCellValue;
                    break;

                default:
                    break;
            }
            return content ?? DBNull.Value;
        }

        /// <summary>
        /// Gets the cell formula value.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        private static object GetCellFormulaVal(ICell cell, object content)
        {
            try
            {
                switch (cell.CachedFormulaResultType)
                {
                    case CellType.Unknown:
                        content = DBNull.Value;
                        break;

                    case CellType.Numeric:
                        content = DateUtil.IsCellDateFormatted(cell) ? cell.DateCellValue : (object)cell.NumericCellValue;
                        break;

                    case CellType.String:
                        content = cell.RichStringCellValue.String;
                        break;

                    case CellType.Formula:
                        GetCellFormulaVal(cell, content);
                        break;

                    case CellType.Blank:
                        content = DBNull.Value;
                        break;

                    case CellType.Boolean:
                        content = cell.BooleanCellValue;
                        break;

                    case CellType.Error:
                        content = cell.ErrorCellValue;
                        break;

                    default:
                        content = DBNull.Value;
                        break;
                }
            }
            catch (Exception)
            {
                content = DBNull.Value;
            }

            return content;
        }

        /// <summary>
        /// Excels to data table.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        public DataTable ExcelToDataTable(int index = 0, int skip = 0)
        {
            ISheet sheet = Workbook.GetSheetAt(index);
            DataTable table = ExcelSheetToDataTable(sheet, skip);
            return table;
        }

        /// <summary>
        /// Excels the name of to data table by sheet.
        /// </summary>
        /// <param name="sheetName">Name of the sheet.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        public DataTable ExcelToDataTableBySheetName(string sheetName, int skip = 0)
        {
            ISheet sheet = Workbook.GetSheet(sheetName);
            DataTable table = ExcelSheetToDataTable(sheet, skip);
            return table;
        }

        /// <summary>
        /// Gets the name of the sheet by sheet.
        /// </summary>
        /// <param name="sheetName">Name of the sheet.</param>
        /// <returns></returns>
        public ISheet GetSheetBySheetName(string sheetName)
        {
            ISheet sheet = Workbook.GetSheet(sheetName);
            return sheet;
        }

        /// <summary>
        /// Excels to data table.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="index">The index.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string filePath, int index = 0, int skip = 0)
        {
            IWorkbook workbook = ReadExcelFile(filePath);
            ISheet sheet = workbook.GetSheetAt(index);
            DataTable table = ExcelSheetToDataTable(sheet, skip);
            return table;
        }

        /// <summary>
        /// Excels the name of to data table by sheet.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="sheetName">Name of the sheet.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        public DataTable ExcelToDataTableBySheetName(string filePath, string sheetName, int skip = 0)
        {
            IWorkbook workbook = ReadExcelFile(filePath);
            ISheet sheet = workbook.GetSheet(sheetName);
            DataTable table = ExcelSheetToDataTable(sheet, skip);
            return table;
        }

        /// <summary>
        /// Excels to data set.
        /// </summary>
        /// <returns></returns>
        public DataSet ExcelToDataSet()
        {
            var dataSet = new DataSet();

            #region Read sheet data and fill it into dataset

            for (int i = 0; i < Workbook.NumberOfSheets; i++)
            {
                ISheet sheet = Workbook.GetSheetAt(i);
                DataTable dt = ExcelSheetToDataTable(sheet);
                dataSet.Tables.Add(dt);
            }

            #endregion Read sheet data and fill it into dataset

            return dataSet;
        }

        /// <summary>
        /// Excels to data set.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string filePath)
        {
            var dataSet = new DataSet();
            IWorkbook workbook = ReadExcelFile(filePath);

            #region Read sheet data and fill it into dataset

            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                ISheet sheet = workbook.GetSheetAt(i);
                DataTable dt = ExcelSheetToDataTable(sheet);
                dataSet.Tables.Add(dt);
            }

            #endregion Read sheet data and fill it into dataset

            return dataSet;
        }

        #endregion Read Excel

        #region Write Excel

        /// <summary>
        /// Datas the set to excel.
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public static MemoryStream DataSetToExcel(DataSet dataSet, ExcelVersion version)
        {
            var memoryStream = new MemoryStream();
            IWorkbook workbook = version == ExcelVersion.Above2007 ? new XSSFWorkbook() : (IWorkbook)new HSSFWorkbook();
            foreach (DataTable table in dataSet.Tables)
            {
                ISheet sheet = workbook.CreateSheet(table.TableName);
                //Write header
                IRow headRow = sheet.CreateRow(0);
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    headRow.CreateCell(i).SetCellValue(table.Columns[i].ColumnName);
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    int rowIndex = i + 1;
                    IRow row = sheet.CreateRow(rowIndex);
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        cell.SetCellValue(table.Rows[i][j].ToString());
                    }
                }
            }
            workbook.Write(memoryStream);
            return memoryStream;
        }

        /// <summary>
        /// Datas the table to excel.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public static MemoryStream DataTableToExcel(DataTable table, ExcelVersion version)
        {
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(table);
            return DataSetToExcel(dataSet, version);
        }

        #endregion Write Excel

        /// <summary>
        /// Gets the excel version.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public static ExcelVersion GetExcelVersion(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            ExcelVersion version = file.Extension.ToLower().Equals(".xlsx", StringComparison.CurrentCultureIgnoreCase) ? ExcelVersion.Above2007 : ExcelVersion.Below2007;
            return version;
        }

        /// <summary>
        /// Gets the sheet names.
        /// </summary>
        /// <param name="workbook">The workbook.</param>
        /// <returns></returns>
        public static List<string> GetSheetNames(IWorkbook workbook)
        {
            var array = new List<string>();
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                array.Add(workbook.GetSheetName(i));
            }
            return array;
        }
    }

    /// <summary>
    /// npoi excel extension
    /// </summary>
    public static class NpoiExcelExtension
    {
        /// <summary>
        /// Gets the cell value.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns></returns>
        public static object GetCellValue(this ICell cell)
        {
            object content = null;
            if (cell == null)
            {
                return DBNull.Value;
            }

            switch (cell.CellType)
            {
                case CellType.Unknown:
                    content = DBNull.Value;
                    break;

                case CellType.Numeric:
                    content = DateUtil.IsCellDateFormatted(cell) ? cell.DateCellValue : (object)cell.NumericCellValue;

                    break;

                case CellType.String:
                    content = cell.StringCellValue;
                    break;

                case CellType.Formula:
                    content = GetCellFormulaVal(cell, content);
                    break;

                case CellType.Blank:
                    content = DBNull.Value;
                    break;

                case CellType.Boolean:
                    content = cell.BooleanCellValue;
                    break;

                case CellType.Error:
                    content = cell.ErrorCellValue;
                    break;

                default:
                    break;
            }
            return content;
        }

        /// <summary>
        /// Gets the cell formula value.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        private static object GetCellFormulaVal(ICell cell, object content)
        {
            try
            {
                switch (cell.CachedFormulaResultType)
                {
                    case CellType.Unknown:
                        content = DBNull.Value;
                        break;

                    case CellType.Numeric:
                        content = DateUtil.IsCellDateFormatted(cell) ? cell.DateCellValue : (object)cell.NumericCellValue;
                        break;

                    case CellType.String:
                        content = cell.RichStringCellValue.String;
                        break;

                    case CellType.Formula:
                        GetCellFormulaVal(cell, content);
                        break;

                    case CellType.Blank:
                        content = DBNull.Value;
                        break;

                    case CellType.Boolean:
                        content = cell.BooleanCellValue;
                        break;

                    case CellType.Error:
                        content = cell.ErrorCellValue;
                        break;

                    default:
                        content = DBNull.Value;
                        break;
                }
            }
            catch (Exception)
            {
                content = DBNull.Value;
            }

            return content;
        }
    }

    /// <summary>
    /// merge region
    /// </summary>
    internal class MergedRegion
    {
        #region Property

        /// <summary>
        /// Initializes a new instance of the <see cref="MergedRegion"/> class.
        /// </summary>
        public MergedRegion()
        {
            Content = string.Empty;
            StartIndex = -1;
            Length = -1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MergedRegion"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="startIndex">The start index.</param>
        public MergedRegion(string content, int startIndex)
        {
            Content = content;
            StartIndex = startIndex;
            Length = 0;
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the start index.
        /// </summary>
        /// <value>
        /// The start index.
        /// </value>
        public int StartIndex { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is initialize.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is initialize; otherwise, <c>false</c>.
        /// </value>
        public bool IsInit
        {
            get
            {
                return string.IsNullOrEmpty(Content) && StartIndex == -1 && Length == -1;
            }
        }

        #endregion Property
    }
}