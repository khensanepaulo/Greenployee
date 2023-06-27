using System.ComponentModel.DataAnnotations;

namespace Greenployee.MODELS.Model
{
    public class BaseClass
    {

        public int id { get; set; }

        public DateTime dtCadastro { get; set; } = DateTime.Now;

        public DateTime? dtAtualizado { get; set; }
        
        public DateTime? dtExcluido { get; set; }

    }
}
