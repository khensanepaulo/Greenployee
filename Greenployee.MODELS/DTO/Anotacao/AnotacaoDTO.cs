
using Greenployee.MODELS.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Greenployee.MODELS.DTO.Anotacao
{
    public class AnotacaoDTO
    {
        public string dsMensagem { get; set; }

        public int idPessoa { get; set; }
    }
}
