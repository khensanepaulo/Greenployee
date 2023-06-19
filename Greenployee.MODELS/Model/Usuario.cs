using Greenployee.MODELS.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenployee.MODELS.Model
{
    public class Usuario : BaseClass
    {
        public Usuario(string dsLogin, string dsSenha)
        {
            Validation(dsLogin, dsSenha);
            PermissaoUsuarios = new List<PermissaoUsuario>();
        }

        [StringLength(60)]
        public string dsLogin { get; set; } = string.Empty;

        [StringLength(60)]
        public string dsSenha { get; set; } = string.Empty;

        public ICollection<PermissaoUsuario> PermissaoUsuarios { get; set; }

        private void Validation(string dsLogin, string dsSenha)
        {
            ModelValidationException.When(string.IsNullOrEmpty(dsLogin), "O nome de usuário deve ser informado");
            ModelValidationException.When(string.IsNullOrEmpty(dsSenha), "A senha deve ser informado");

            this.dsLogin = dsLogin;
            this.dsSenha = dsSenha;
        }
    }
}
