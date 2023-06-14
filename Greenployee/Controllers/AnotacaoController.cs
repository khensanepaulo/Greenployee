using Greenployee.CORE.Business;
using Greenployee.MODELS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Greenployee.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnotacaoController : ControllerBase
    {
        private readonly IAnotacaoBusiness _business;

        public AnotacaoController(IAnotacaoBusiness anotacaoBusiness)
        {
            _business = anotacaoBusiness;
        }

        // GET: api/Meta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Anotacao>>> FindAll()
        {
            try
            {
                var result = await _business.FindAll();
                if (result == null) return BadRequest("Não foi possível listar as anotações!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Anotacao>> FindById(int id)
        {
            try
            {
                Anotacao result = await _business.FindById(id);
                if (result == null) return NotFound("Não foi possível encontrar a anotação!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Anotacao>> Insert(Anotacao anotacao)
        {
            try
            {
                Anotacao result = await _business.Insert(anotacao);
                if (result == null) return BadRequest("Não foi possível inserir a anotação!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPut]
        public async Task<ActionResult<Anotacao>> Update(Anotacao anotacao)
        {
            try
            {
                Anotacao result = await _business.Update(anotacao);
                if (result == null) return BadRequest("Não foi possível atualizar a anotação!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Anotacao>> Delete(int id)
        {
            try
            {
                var status = await _business.Delete(id);
                if (!status) return BadRequest("Não foi possível deletar a anotação!");
                return Ok(status);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
