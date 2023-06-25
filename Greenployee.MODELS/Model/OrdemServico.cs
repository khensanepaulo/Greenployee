using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Greenployee.MODELS.Model
{
    public class OrdemServico : BaseClass
    {
        [StringLength(15)]
        public string nrOrdem { get; set; } = string.Empty;

        [StringLength(60)]
        public string? nmCliente { get; set; } = string.Empty;

        [StringLength(20)]
        public string? nrTelefone { get; set; } = string.Empty;

        [StringLength(15)]
        public string? flSituacao { get; set; } = string.Empty;

        [StringLength(15)]
        public string? dsFormaPagamento { get; set; } = string.Empty;

        [StringLength(100)]
        public string? dsEndereco { get; set; } = string.Empty;

        public bool flEntrega { get; set; } = false;

        public decimal? vlTotal { get; set; }

        [ForeignKey("idFuncionario")]
        public virtual Pessoa? Funcionario { get; set; }

        public ICollection<OrdemServicoItem> OrdemServicoItem { get; set; }

        //public ICollection<OrdemServicoItem>? OrdemServicoItens { get; set; } = new List<OrdemServicoItem>();

    }
}
