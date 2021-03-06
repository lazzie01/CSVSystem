using CSVProject.Shared;
using Microsoft.EntityFrameworkCore;

namespace CSVProject.DataAccessLayer.Models
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
                Id = 1,
                FileName = "StudentData.csv", 
            });
      
        }
    }
}
