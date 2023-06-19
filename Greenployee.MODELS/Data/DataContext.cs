using Greenployee.MODELS.Model;
using Microsoft.EntityFrameworkCore;

namespace Greenployee.MODELS.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<Anotacao> Anotacoes { get; set; }
        public virtual DbSet<Meta> Metas { get; set; }
        public virtual DbSet<OrdemServico> OrdensServicos { get; set; }
        public virtual DbSet<OrdemServicoItem> OrdemServicoItens { get; set; }
        public virtual DbSet<Pessoa> Pessoas { get; set; }
        public virtual DbSet<PessoaMeta> PessoaMetas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Permissao> Permissoes { get; set; }
        public virtual DbSet<PermissaoUsuario> PermissaoUsuarios { get; set; }

    }
}
