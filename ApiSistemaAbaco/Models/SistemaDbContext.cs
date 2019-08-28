using Microsoft.EntityFrameworkCore;

namespace ApiSistemaAbaco.Models
{
    public class SistemaDbContext : DbContext
    {
        public SistemaDbContext(DbContextOptions<SistemaDbContext> options)
            : base(options)
        { }

        public DbSet<Projeto> Projeto { get; set; }
    }
}