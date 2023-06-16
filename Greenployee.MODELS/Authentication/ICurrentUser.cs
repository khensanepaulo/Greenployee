using Greenployee.MODELS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenployee.MODELS.Authentication
{
    public interface ICurrentUser
    {
        public int id { get; set; }
        public string dsLogin { get; set; }
        public string permissions { get; set; }
    }
}
