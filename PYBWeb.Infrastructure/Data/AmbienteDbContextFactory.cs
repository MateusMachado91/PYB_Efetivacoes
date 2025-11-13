using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PYBWeb.Infrastructure.Data
{
    public class AmbienteDbContextFactory : IDesignTimeDbContextFactory<AmbienteDbContext>
    {
        public AmbienteDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AmbienteDbContext>();
            var connectionString = "Data Source=X:\\DATA_PYB\\dados2025.db";
            optionsBuilder.UseSqlite(connectionString);

            return new AmbienteDbContext(connectionString);
        }
    }
}