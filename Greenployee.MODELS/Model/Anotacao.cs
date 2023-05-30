using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Greenployee.MODELS.Model
{
    public class Anotacao : BaseClass
    {
        [StringLength(255)]
        public string dsMensagem { get; set; }

        public bool flConcluido  { get; set; }

        public int idPessoa { get; set; }

    }
}
