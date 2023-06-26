using Greenployee.MODELS.DTO;
using Greenployee.MODELS.Validation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Greenployee.MODELS.Model
{
    public class OrdemServico : BaseClass
    {

        
        [StringLength(15)]
        public string nrOrdem { get; set; } = string.Empty;

        [StringLength(60)]
        public string nmCliente { get; set; } = string.Empty;

        [StringLength(20)]
        public string nrTelefone { get; set; } = string.Empty;

        [StringLength(15)]
        public string flSituacao { get; set; } = string.Empty;

        [StringLength(15)]
        public string dsFormaPagamento { get; set; } = string.Empty;

        [StringLength(100)]
        public string dsEndereco { get; set; } = string.Empty;

        public bool flEntrega { get; set; } = false;

        public decimal? vlTotal { get; set; }

        public int idFuncionario { get; set; }  

        [ForeignKey("idFuncionario")]
        public virtual Pessoa Funcionario { get; set; }
        
        [InverseProperty("OrdemServico")]
        public virtual ICollection<OrdemServicoItem> OrdemServicoItem { get; set; }      
    }
}
