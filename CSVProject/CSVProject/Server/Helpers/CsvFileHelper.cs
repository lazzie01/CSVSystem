using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CSVProject.Server.Helpers
{
    public static class CsvFileHelper
    {
        public static readonly CsvConfiguration defaultCsvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = true
        };

        public static IEnumerable<T> ReadFile<T>(string csvFilePath, CsvConfiguration csvConfiguration = null)
        {
            csvConfiguration ??= defaultCsvConfiguration;
            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, csvConfiguration);
            return csv.GetRecords<T>().ToList();
        }

        public static void UpdateFile<T>(string csvFilePath, IEnumerable<T> dataToWrite, bool truncateFileBeforeWriting = false, CsvConfiguration csvConfiguration = null)
        {
            if (truncateFileBeforeWriting)
            {
              File.WriteAllText(csvFilePath, string.Empty);

              csvConfiguration ??= defaultCsvConfiguration;
              using var writer = new StreamWriter(csvFilePath);
              using var csv = new CsvWriter(writer, csvConfiguration);
              csv.WriteHeader<T>();
              csv.NextRecord();
              csv.WriteRecords<T>(dataToWrite);
              csv.Flush();
            }
            else
            {
                var lines = dataToWrite.Select(d => d.ToString());
                File.AppendAllLines(csvFilePath, lines);
            }

        }

    }
}
