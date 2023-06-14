using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenployee.MODELS.Model
{
    public class PessoaMeta : BaseClass
    {
        [ForeignKey("idPessoa")]
        public virtual Pessoa Pessoa { get; set; }

        [ForeignKey("idMeta")]
        public virtual Meta Meta { get; set; }

    }
}
