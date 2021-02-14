using System.IO;
using Microsoft.EntityFrameworkCore;
using MunicipalTax.Data.Entities;

namespace MunicipalTax.Data
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Tax> Taxes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path2 = Directory.GetCurrentDirectory();
            var dbPath = path2.Substring(0, path2.Length - 12) + "DataBase\\TaxDataBase.sqlite";
            string connectionString = string.Format("Data Source={0};", dbPath);

            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Municipality>().ToTable("Municipalities");
            modelBuilder.Entity<Tax>().ToTable("Taxes");
        }
    }
}
