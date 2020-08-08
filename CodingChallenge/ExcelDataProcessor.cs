using OfficeOpenXml;
using CodingChallenge.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace CodingChallenge
{
    public class ExcelDataProcessor : DataProcessor
    {
        /// <summary>License for the <see cref="ExcelPackage"/> functionality.</summary>
        public LicenseContext License { get; private set; } = LicenseContext.NonCommercial;

        /// <summary>Original input file that is stored in the "CodingChallenge\Data" folder.</summary>
        protected override string DefaultFileName => "Coding_Challenge_Data.xlsx";

        /// <summary>Loads the data into the <see cref="DataProcessor.ImportedLocations"/> object.</summary>
        /// <param name="fileName">Input filename to be loaded.</param>
        /// <returns><see cref="Task"/>.</returns>
        public async override Task LoadDataAsync(string fileName)
        {
            FileInfo inputData;
            if (string.IsNullOrEmpty(fileName))
            {
                inputData = FileInputUtil.GetFileInfo("Data", DefaultFileName);
            }
            else
            {
                inputData = new FileInfo(fileName);
            }

            using (ExcelPackage package = new ExcelPackage())
            {
                await package.LoadAsync(inputData).ConfigureAwait(false);

                DataTable dataTable = ExcelPackageToDataTable(package);
                ImportedLocations = DataTableToLocationCollection(dataTable);

                List<Location> locations = ImportedLocations.ToList();

                // strip out the duplicate values
                MakeListUnique(ref locations);

                // strip out invalid locations
                RemoveInvalidLocations(ref locations);

                ImportedLocations = new BindingList<Location>(locations);
                OnDataUpdated(EventArgs.Empty);
            }

        }

        /// <summary>Method takes in the input <see cref="DataTable"/> and converts it to 
        /// a <see cref="LocationCollection"/>.</summary>
        /// <param name="inputTable"><see cref="DataTable"/> object to be converted.</param>
        /// <returns><see cref="LocationCollection"/> object based off of the <param name="inputTable"/>.</returns>
        private BindingList<Location> DataTableToLocationCollection(DataTable inputTable)
        {
            BindingList<Location> resultList = new BindingList<Location>();

            foreach (DataRow row in inputTable.Rows)
            {
                Location loc = new Location
                {
                    Address = row[Resources.Address].ToString(),
                    City = row[Resources.City].ToString(),
                    State = row[Resources.State].ToString(),
                    Zip = row[Resources.Zip].ToString(),
                    Latitude = double.Parse(row[Resources.Latitude].ToString()),
                    Longitude = double.Parse(row[Resources.Longitude].ToString())
                };
                resultList.Add(loc);
            }

            return resultList;
        }

        /// <summary>Takes an input <see cref="ExcelPackage"/> and converts it into a 
        /// <see cref="DataTable"/> object.</summary>
        /// <param name="excelPackage"><see cref="ExcelPackage"/> object to be converted.</param>
        /// <returns><see cref="DataTable"/> object based off of the <param name="excelPackage"/>.</returns>
        private DataTable ExcelPackageToDataTable(ExcelPackage excelPackage)
        {
            DataTable dt = new DataTable();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

            //check if the worksheet is completely empty
            if (worksheet.Dimension == null)
            {
                return dt;
            }

            //create a list to hold the column names
            List<string> columnNames = new List<string>();

            //needed to keep track of empty column headers
            int currentColumn = 1;

            //loop all columns in the sheet and add them to the datatable
            foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                string columnName = cell.Text.Trim();

                //check if the previous header was empty and add it if it was
                if (cell.Start.Column != currentColumn)
                {
                    columnNames.Add("Header_" + currentColumn);
                    _ = dt.Columns.Add("Header_" + currentColumn);
                    currentColumn++;
                }

                //add the column name to the list to count the duplicates
                columnNames.Add(columnName);

                //count the duplicate column names and make them unique to avoid the exception
                //A column named 'Name' already belongs to this DataTable
                int occurrences = columnNames.Count(x => x.Equals(columnName));
                if (occurrences > 1)
                {
                    columnName = columnName + "_" + occurrences;
                }

                //add the column to the datatable
                _ = dt.Columns.Add(columnName);

                currentColumn++;
            }

            DataRowCount = worksheet.Dimension.End.Row - 1;
            //start adding the contents of the excel file to the datatable
            for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
            {
                ExcelRange row = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                DataRow newRow = dt.NewRow();

                //loop all cells in the row
                foreach (ExcelRangeBase cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }

                dt.Rows.Add(newRow);
            }

            return dt;
        }

    }
}
