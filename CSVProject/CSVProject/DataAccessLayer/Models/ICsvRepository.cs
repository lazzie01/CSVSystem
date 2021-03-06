using CSVProject.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSVProject.DataAccessLayer.Models
{
    public interface ICsvRepository
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
