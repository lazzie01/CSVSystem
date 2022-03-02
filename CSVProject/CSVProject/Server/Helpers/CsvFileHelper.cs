using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

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
            csvConfiguration = csvConfiguration ?? defaultCsvConfiguration;
            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, csvConfiguration);
            return csv.GetRecords<T>();
        }

        public static void UpdateFile<T>(string csvFilePath, IEnumerable<T> dataToWrite, bool truncateFileBeforeWriting = false, CsvConfiguration csvConfiguration = null)
        {
            csvConfiguration = csvConfiguration ?? defaultCsvConfiguration;

            if (truncateFileBeforeWriting)
            {
                File.WriteAllText(csvFilePath, string.Empty);
            }

            using (var writer = new StreamWriter(csvFilePath))
            using (var csvWriter = new CsvWriter(writer, csvConfiguration))
            {
                csvWriter.WriteHeader<T>();
                csvWriter.WriteRecords(dataToWrite);
                writer.Flush();
            }
        }

    }
}
