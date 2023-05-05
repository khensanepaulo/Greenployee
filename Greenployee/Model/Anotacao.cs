using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Greenployee.Model
{
    public class Anotacao:BaseClass
    {
        public DateTime dtAnotacao { get; set; }

        [StringLength(100)]
        public string dsMensagem { get; set; }

        [ForeignKey("Pessoa")]
        public int idPessoa { get; set; }   

    }
}
