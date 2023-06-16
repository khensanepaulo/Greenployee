using Greenployee.MODELS.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenployee.MODELS.Model
{
    public sealed class Permissao : BaseClass
    {
        public Permissao(string nmVisual, string nmPermissao)
        {
            Validation(nmVisual, nmPermissao);
            PermissoesUsuario = new List<PermissaoUsuario>();
        }

        public string nmVisual { get; set; } = string.Empty;

        public string nmPermissao { get; set; } = string.Empty;

        public ICollection<PermissaoUsuario> PermissoesUsuario { get; set; }

        private void Validation(string nmVisual, string nmPermissao)
        {
            ModelValidationException.When(string.IsNullOrEmpty(nmVisual), "O nome visual da permissão deve ser informado");
            ModelValidationException.When(string.IsNullOrEmpty(nmPermissao), "O nome da permissão deve ser informado");

            this.nmVisual = nmVisual;
            this.nmPermissao = nmPermissao;
        }
    }
}
