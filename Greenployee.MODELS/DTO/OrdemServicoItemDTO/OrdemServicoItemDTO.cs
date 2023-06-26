using Greenployee.MODELS.Model;
using System.ComponentModel.DataAnnotations;

namespace Greenployee.MODELS.DTO
{
    public class OrdemServicoItemDTO
    {
        [StringLength(100)]
        public string nmProduto { get; set; } = string.Empty;

        public decimal vlUnitario { get; set; }

        public int nrQuantidade { get; set; }

        public OrdemServico OrdemServico { get; set; }
    }
}