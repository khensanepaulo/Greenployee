using Greenployee.MODELS.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Greenployee.MODELS.Model
{
    public class Anotacao : BaseClass
    {
        public Anotacao(int idPessoa, string dsMensagem)
        {
            Validation(idPessoa, dsMensagem);
        }

        [StringLength(255)]
        public string dsMensagem { get; set; }

        public int idPessoa { get; set; }

        [ForeignKey("idPessoa")]
        public virtual Pessoa Pessoa { get; set; }

        private void Validation(int idPessoa, string dsMensagem)
        {
            ModelValidationException.When(idPessoa == 0, "O id da pessoa deve ser informado");
            //ModelValidationException.When(string.IsNullOrEmpty(dsMensagem), "A mensagem da Anotação deve ser informada");

            this.idPessoa = idPessoa;
            this.dsMensagem = dsMensagem;
        }

    }
}
