using CSVProject.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSVProject.Server.Models
{
    public interface ICsvRepository
    {
        Task<IEnumerable<Csv>> GetAllCsvs();
        Task<Csv> GetCsv(int id);
        Task<Csv> GetCsvByFileName(string fileName);
        Task<Csv> AddCsv(Csv csv);
        Task<Csv> UpdateCsv(Csv csv);
        Task DeleteCsv(int id);
    }
}
