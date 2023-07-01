using Greenployee.CORE.Page;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenployee.CORE.Filters
{
    public class PessoaFilter : PagedBaseRequest
    {
        public int? idUsuario { get; set; }
        public string nmPessoa { get; set; } 
        public string nrCPF { get; set; } 
        public string nrRG { get; set; } 
        public string dsEmail { get; set; } 
        public string nrTelefone { get; set; } 
        public string flSituacao { get; set; } 
        public string? nrPIS { get; set; } = string.Empty;
        public DateTime? dtInicio { get; set; }
        public DateTime? dtFim { get; set; }

    }
}
