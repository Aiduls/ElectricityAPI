using ElectricityAPI1.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectricityAPI1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Electricity>? Electricities { get; set; }

    }
}
