using Greenployee.MODELS.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Greenployee.MODELS.DTO
{
    public class OrdemServicoDTO
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

        public ICollection<OrdemServicoItem> OrdemServicoItem { get; set; }
    }
}