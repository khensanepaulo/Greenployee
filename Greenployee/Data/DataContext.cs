using Greenployee.Model;
using Microsoft.EntityFrameworkCore;

namespace Greenployee.Data
{
    public class DataContext:DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<OrdemServico> OrdensServicos { get; set; }

        public DbSet<Greenployee.Model.Pessoa>? Pessoa { get; set; }

        public DbSet<Greenployee.Model.Anotacao>? Anotacao { get; set; }

        public DbSet<Greenployee.Model.Meta>? Meta { get; set; }

    }
}
