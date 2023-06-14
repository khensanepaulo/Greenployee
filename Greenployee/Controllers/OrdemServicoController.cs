using Greenployee.CORE.Business;
using Greenployee.MODELS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Greenployee.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdemServicoController : ControllerBase
    {
        private readonly IOrdemServicoBusiness _business;

        public OrdemServicoController(IOrdemServicoBusiness ordemServicoBusiness)
        {
            _business = ordemServicoBusiness;
        }

        // GET: api/Meta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdemServico>>> FindAll()
        {
            try
            {
                var result = await _business.FindAll();
                if (result == null) return BadRequest("Não foi possível listar as ordens de serviço!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdemServico>> FindById(int id)
        {
            try
            {
                OrdemServico result = await _business.FindById(id);
                if (result == null) return NotFound("Não foi possível encontrar a ordem de serviço!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrdemServico>> Insert(OrdemServico ordemServico)
        {
            try
            {
                OrdemServico result = await _business.Insert(ordemServico);
                if (result == null) return BadRequest("Não foi possível inserir a ordem de serviço!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPut]
        public async Task<ActionResult<OrdemServico>> Update(OrdemServico ordemServico)
        {
            try
            {
                OrdemServico result = await _business.Update(ordemServico);
                if (result == null) return BadRequest("Não foi possível atualizar a ordem de serviço!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrdemServico>> Delete(int id)
        {
            try
            {
                var status = await _business.Delete(id);
                if (!status) return BadRequest("Não foi possível deletar a ordem de serviço!");
                return Ok(status);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
