
using Microsoft.EntityFrameworkCore;

namespace PYBWeb.Infrastructure.Data
{
    public class ColaboradoresDbContext : DbContext
    {
        public ColaboradoresDbContext(DbContextOptions<ColaboradoresDbContext> options)
            : base(options)
        {
        }

        public DbSet<Colaborador> Colaboradores { get; set; }
    }
}
