using Greenployee.MODELS.Model;
using Microsoft.EntityFrameworkCore;

namespace Greenployee.MODELS.Data
{
    public class DataContext:DbContext
    {

        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Pessoa>? Pessoas { get; set; }
        public DbSet<OrdemServico> OrdensServicos { get; set; }
        public DbSet<Anotacao>? Anotacoes { get; set; }
        public DbSet<Meta>? Metas { get; set; }

    }
}
