using CSVProject.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CSVProject.Server.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Csv> Csvs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Csv>().HasData(new Csv
            { 
                FileName = "Student Data", 
                FilePath = "StudentData.csv" 
            });
      
        }
    }
}
