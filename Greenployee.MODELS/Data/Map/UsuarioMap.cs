using Greenployee.MODELS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Greenployee.MODELS.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.id);

            builder.Property(u => u.id).HasColumnName("id");
            builder.Property(u => u.dsLogin).HasColumnName("dsLogin");
            builder.Property(u => u.dsSenha).HasColumnName("dsSenha");
            builder.HasMany(u => u.PermissoesUsuario).WithOne(p => p.Usuario).HasForeignKey(p => p.idUsuario);
        }
    }
}
