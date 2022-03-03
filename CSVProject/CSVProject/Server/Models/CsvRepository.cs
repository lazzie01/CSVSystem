using CSVProject.Server.Constants;
using CSVProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSVProject.Server.Models
{
    public class CsvRepository : ICsvRepository
    {
        private readonly AppDbContext appDbContext;

        public CsvRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Csv> AddCsv(Csv csv)
        {
            var result = await appDbContext.Csvs.AddAsync(csv);
            await appDbContext.SaveChangesAsync();

            //upload file to the folder
            File.Copy(csv.FileName, $"{CsvConstants.Directory}{Path.GetFileName(csv.FileName)}");

            return result.Entity;
        }

        public async Task DeleteCsv(int id)
        {
            var result = await appDbContext.Csvs.FirstOrDefaultAsync(c => c.Id == id);

            if (result != null)
            {
                //delete file from the folder
                File.Delete($"{CsvConstants.Directory}{result.FileName}");

                appDbContext.Csvs.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Csv>> GetAllCsvs()
        {
            return await appDbContext.Csvs.ToListAsync();
        }

        public async Task<Csv> GetCsv(int id)
        {
            return await appDbContext.Csvs.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Csv> GetCsvByFileName(string fileName)
        {
            return await appDbContext.Csvs.FirstOrDefaultAsync(c => c.FileName == fileName);
        }

        public async Task<Csv> UpdateCsv(Csv csv)
        {
            var result = await appDbContext.Csvs.FirstOrDefaultAsync(c => c.Id == csv.Id);

            if (result != null)
            {
                //Rename the file also
                File.Move($"{CsvConstants.Directory}{result.FileName}", $"{CsvConstants.Directory}{csv.FileName}");

                result.FileName = csv.FileName;
                await appDbContext.SaveChangesAsync();

                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Csv>> Search(string fileName, int? id)
        {
            IQueryable<Csv> query = appDbContext.Csvs;

            if (!string.IsNullOrEmpty(fileName))
            {
                query = query.Where(e => e.FileName.Contains(fileName));
            }

            if (id != null)
            {
                query = query.Where(e => e.Id == id);
            }

            return await query.ToListAsync();
        }
    }
}
