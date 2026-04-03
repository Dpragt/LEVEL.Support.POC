using LEVEL.Support.POC.Server.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace LEVEL.Support.POC.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Melding> Meldingen { get; set; }
    }
}
