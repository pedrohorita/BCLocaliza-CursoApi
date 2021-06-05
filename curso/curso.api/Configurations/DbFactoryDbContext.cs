using curso.api.Infraestruture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace curso.api.Configurations
{
    public class DbFactoryDbContext : IDesignTimeDbContextFactory<CursoDBContext>
    {
        public CursoDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CursoDBContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CURSO;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            CursoDBContext context = new CursoDBContext(optionsBuilder.Options);

            return context;
        }
    }
}
