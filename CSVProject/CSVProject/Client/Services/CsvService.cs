using CSVProject.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CSVProject.Client.Services
{
    public class CsvService : ICsvService
    {
        private readonly HttpClient httpClient;

        public CsvService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<Csv> AddCsv(Csv csv)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCsv(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Csv>> GetAllCsvs()
        {
            return await httpClient
              .GetFromJsonAsync<IEnumerable<Csv>>($"api/csvs/all");
        }

        public Task<Csv> GetCsv(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Csv> GetCsvByFileName(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Csv>> Search(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<Csv> UpdateCsv(Csv csv)
        {
            throw new NotImplementedException();
        }
    }
}
