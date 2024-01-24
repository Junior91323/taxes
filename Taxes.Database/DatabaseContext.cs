using Microsoft.EntityFrameworkCore;
using Taxes.Database.Models;

namespace Taxes.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<City> Cities { get; set; }

        public DbSet<TaxSchedule> TaxSchedules { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
