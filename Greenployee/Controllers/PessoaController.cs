using Greenployee.CORE.Business;
using Greenployee.MODELS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Greenployee.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {

        private readonly IPessoaBusiness _business;


        public PessoaController(IPessoaBusiness pessoaBusiness)
        {
            _business = pessoaBusiness;
        }

        // GET: api/Meta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> FindAll()
        {
            try
            {
                var result = await _business.FindAll();
                if (result == null) return BadRequest("Não foi possível listar as Pessoas!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> FindById(int id)
        {
            try
            {
                Pessoa result = await _business.FindById(id);
                if (result == null) return NotFound("Não foi possível encontrar a Pessoa!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Pessoa>> Insert(Pessoa pessoa)
        {
            try
            {
                Pessoa result = await _business.Insert(pessoa);
                if (result == null) return BadRequest("Não foi possível inserir pessoa!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPut]
        public async Task<ActionResult<Pessoa>> Update(Pessoa pessoa)
        {
            try
            {
                Pessoa result = await _business.Update(pessoa);
                if (result == null) return BadRequest("Não foi possível atualizar os dados referentes a pessoa!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pessoa>> Delete(int id)
        {
            try
            {
                var status = await _business.Delete(id);
                if (!status) return BadRequest("Não foi possível deletar essa pessoa!");
                return Ok(status);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

    }

}
