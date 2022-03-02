﻿using CSVProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CSVProject.Server.Models
{
    public class CsvRepository : ICsvRepository
    {
        private readonly AppDbContext appDbContext;

        private const string CsvDirectory = "Data\\";

        public CsvRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Csv> AddCsv(Csv csv)
        {
            var result = await appDbContext.Csvs.AddAsync(csv);
            await appDbContext.SaveChangesAsync();

            //upload file to the folder
            File.Copy(csv.FilePath, $"{CsvDirectory}{csv.FileName}");

            return result.Entity;
        }

        public async Task DeleteCsv(int id)
        {
            var result = await appDbContext.Csvs.FirstOrDefaultAsync(c => c.Id == id);

            if (result != null)
            {
                //delete file from the folder
                File.Delete(result.FilePath);

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
                File.Move($"{CsvDirectory}{result.FileName}", $"{CsvDirectory}{csv.FileName}");

                result.FileName = csv.FileName;
                await appDbContext.SaveChangesAsync();

                return result;
            }
            return null;
        }
    }
}