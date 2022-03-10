using CSVProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVProject.Client.Services
{
    public interface ICsvService
    {
        Task<IEnumerable<Csv>> Search(string fileName);
        Task<IEnumerable<Csv>> GetAllCsvs();
        Task<Csv> GetCsv(int id);
        Task<Csv> GetCsvByFileName(string fileName);
        Task<Csv> AddCsv(Csv csv);
        Task<Csv> UpdateCsv(Csv csv);
        Task DeleteCsv(int id);
    }
}
