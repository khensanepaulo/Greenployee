using Greenployee.CORE.Page;
using Greenployee.MODELS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenployee.CORE.Filters
{
    public class AnotacaoFilter : PagedBaseRequest
    {
        public DateTime dtInicio { get; set; }
        public DateTime dtFim { get; set; }
        public string dsMensagem { get; set; }
        public string nmPessoa { get; set; }
        public int? idUsuario { get; set; }
    }
}
