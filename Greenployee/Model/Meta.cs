using System.ComponentModel.DataAnnotations;

namespace Greenployee.Model
{
    public class Meta : BaseClass
    { 

        public string dsRecompensa { get; set; } = string.Empty;
        public DateTime dtInicio { get; set; }
        public DateTime dtFim { get; set; }
        public decimal? vlMeta { get; set; }
    }

}