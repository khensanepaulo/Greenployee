using Greenployee.MODELS.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenployee.MODELS.Model
{
    public class PermissaoUsuario : BaseClass
    {
        public PermissaoUsuario(int idUsuario, int idPermissao)
        {
            Validation(idUsuario, idPermissao);
        }

        public int idUsuario { get; set; }

        public int idPermissao { get; set; }

        [ForeignKey("idUsuario")]
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("idPermissao")]
        public virtual Permissao Permissao { get; set; }

        private void Validation(int idUsuario, int idPermissao)
        {
            ModelValidationException.When(idUsuario == 0, "O id do usuário deve ser informado");
            ModelValidationException.When(idPermissao == 0, "O id da permissão deve ser informado");

            this.idUsuario = idUsuario;
            this.idPermissao = idPermissao;
        }
}
}
