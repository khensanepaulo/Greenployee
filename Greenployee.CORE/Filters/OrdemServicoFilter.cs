using Greenployee.CORE.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenployee.CORE.Filters
{
    public class OrdemServicoFilter : PagedBaseRequest
    {
        public int? idUsuario { get; set; }
        public DateTime? dtInicio { get; set; }
        public DateTime? dtFim { get; set; }
        public string? nrOrdem { get; set; }
        public string? nmCliente { get; set; }
        public string? nmFuncionario { get; set; }
    }
}
