using Greenployee.CORE.Business;
using Greenployee.MODELS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Greenployee.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MetaController : ControllerBase
    {
        private readonly IMetaBusiness _business;

        public MetaController(IMetaBusiness metaBusiness)
        {
            _business = metaBusiness;
        }

        // GET: api/Meta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meta>>> FindAll()
        {
            try
            {
                var result = await _business.FindAll();
                if (result == null) return BadRequest("Não foi possível listar as metas!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Meta>> FindById(int id)
        {
            try
            {
                Meta result = await _business.FindById(id);
                if (result == null) return NotFound("Não foi possível encontrar a meta!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Meta>> Insert(Meta meta)
        {
            try
            {
                Meta result = await _business.Insert(meta);
                if (result == null) return BadRequest("Não foi possível inserir a meta!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPut]
        public async Task<ActionResult<Meta>> Update(Meta meta)
        {
            try
            {
                Meta result = await _business.Update(meta);
                if (result == null) return BadRequest("Não foi possível atualizar a meta!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Meta>> Delete(int id)
        {
            try
            {
                var status = await _business.Delete(id);
                if (!status) return BadRequest("Não foi possível deletar a meta!");
                return Ok(status);
            }
            catch (Exception exception) 
            { 
                throw exception; 
            }
        }
    }
}
