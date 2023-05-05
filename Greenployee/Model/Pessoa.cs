using System.ComponentModel.DataAnnotations;

namespace Greenployee.Model
{
    public class Pessoa: BaseClass
    {
        [StringLength(60)]
        public string nmPessoa { get; set; } = string.Empty;

        [StringLength(11)]
        public string nrCPF { get; set; } = string.Empty;

        [StringLength(15)]
        public string nrRG { get; set;} = string.Empty;

        [StringLength(50)]
        public string dsEmail { get; set; } = string.Empty;

        [StringLength(20)]
        public string nrTelefone { get; set; } = string.Empty;

        [StringLength(15)]
        public string flSituacao { get; set; } = string.Empty;

        [StringLength(15)]
        public string nrPIS { get; set; } = string.Empty;

        public decimal flEntrega { get; set; }
        public DateTime dtAdmissao { get; set; }
        
    }
}
