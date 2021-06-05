using curso.api.Business.Entities;
using curso.api.Infraestruture.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace curso.api.Infraestruture.Data
{
    public class CursoDBContext : DbContext
    {
        public CursoDBContext(DbContextOptions<CursoDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CursoMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Usuario { get; set; }
        public DbSet<Curso> Curso { get; set; }
    }
}
