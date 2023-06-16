using Greenployee.MODELS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenployee.MODELS.Data.Map
{
    public class PermissaoMap : IEntityTypeConfiguration<Permissao>
    {
        public void Configure(EntityTypeBuilder<Permissao> builder)
        {
            builder.ToTable("Permissoes");

            builder.HasKey(u => u.id);

            builder.Property(u => u.id).HasColumnName("id");
            builder.Property(u => u.nmVisual).HasColumnName("nmVisual");
            builder.Property(u => u.nmPermissao).HasColumnName("nmPermissao");
            builder.HasMany(u => u.PermissoesUsuario).WithOne(p => p.Permissao).HasForeignKey(p => p.idPermissao);
        }
    }
}
