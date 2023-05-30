using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenployee.MODELS.Model
{
    public class OrdemServicoItem : BaseClass
    {
        [StringLength(100)]
        public string nmProduto { get; set; } = string.Empty;

        public decimal? vlUnitario { get; set; }

        public int nrQuantidade { get; set; }
    }
}
