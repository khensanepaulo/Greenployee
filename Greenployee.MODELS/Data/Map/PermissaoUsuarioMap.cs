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
    public class PermissaoUsuarioMap : IEntityTypeConfiguration<PermissaoUsuario>
    {
        public void Configure(EntityTypeBuilder<PermissaoUsuario> builder)
        {
            builder.ToTable("PermissaoUsuarios");

            builder.HasKey(u => u.id);

            builder.Property(u => u.id).HasColumnName("id");
            builder.Property(u => u.idPermissao).HasColumnName("idPermissao");
            builder.Property(u => u.idUsuario).HasColumnName("idUsuario");
            //builder.HasOne(u => u.Permissao).WithMany(p => p.PermissaoUsuarios);    
            //builder.HasOne(u => u.Usuario).WithMany(p => p.PermissaoUsuarios);
        }
    }
}
