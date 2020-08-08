using CsvHelper;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodingChallenge
{
    public class CsvDataProcessor : DataProcessor
    {
        /// <summary>Original input file that is stored in the CodingChallenge\Data folder.</summary>
        protected override string DefaultFileName => "Data.csv";

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
            List<Location> locations = new List<Location>();
            using (var csv = new CsvReader(File.OpenText(inputData.FullName),
                                           System.Globalization.CultureInfo.InvariantCulture))
            {
                await Task.Run(() =>
                {
                    locations = csv.GetRecords<Location>().ToList();
                });
            }

            // strip out the duplicate values
            MakeListUnique(ref locations);

            // strip out invalid locations
            RemoveInvalidLocations(ref locations);

            ImportedLocations = new BindingList<Location>(locations);
        }

    }
}
