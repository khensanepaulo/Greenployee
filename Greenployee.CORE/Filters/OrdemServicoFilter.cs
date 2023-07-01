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
        public string? dtInicio { get; set; }
        public string? dtFim { get; set; }
        public string? nrOrdem { get; set; }
        public string? nmCliente { get; set; }
        public string? nmFuncionario { get; set; }
    }
}
