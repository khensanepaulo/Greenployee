using Greenployee.CORE.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenployee.CORE.Filters
{
    public class MetaFilter : PagedBaseRequest
    {
        public int? idUsuario { get; set; }
        public string dsRecompensa { get; set; }
        public DateTime dtInicio { get; set; }
        public DateTime dtFim { get; set; }
        public string flConcluida { get; set; }
    }
}
