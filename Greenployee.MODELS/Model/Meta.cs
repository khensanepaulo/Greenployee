using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Greenployee.MODELS.Model
{
    public class Meta : BaseClass
    {
        [StringLength(255)]
        public string dsRecompensa { get; set; } = string.Empty;

        public DateTime dtInicio { get; set; }

        public DateTime dtFim { get; set; }

        public decimal? vlMeta { get; set; }

        [InverseProperty("Meta")]
        public virtual ICollection<PessoaMeta> PessoasMeta { get; set; }
    }

}