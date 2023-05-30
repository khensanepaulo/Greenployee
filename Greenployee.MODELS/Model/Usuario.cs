using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenployee.MODELS.Model
{
    public class Usuario : BaseClass
    {
        [StringLength(60)]
        public string dsLogin { get; set; } = string.Empty;

        [StringLength(60)]
        public string dsSenha { get; set; } = string.Empty;

        [StringLength(15)]
        public string tpAcesso { get; set; } = string.Empty;
    }
}
