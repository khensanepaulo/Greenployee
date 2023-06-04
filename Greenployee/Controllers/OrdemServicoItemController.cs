using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Greenployee.MODELS.Data;
using Greenployee.MODELS.Model;
using Greenployee.CORE.Business;

namespace Greenployee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdemServicoItemController : ControllerBase
    {
        private readonly IOrdemServicoItemBusiness _business;

        public OrdemServicoItemController(IOrdemServicoItemBusiness ordemServicoItemBusiness)
        {
            _business = ordemServicoItemBusiness;
        }

        // GET: api/Meta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdemServicoItem>>> FindAll()
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
        public async Task<ActionResult<OrdemServicoItem>> FindById(int id)
        {
            try
            {
                OrdemServicoItem result = await _business.FindById(id);
                if (result == null) return NotFound("Não foi possível encontrar a ordem de serviço!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrdemServicoItem>> Insert(OrdemServicoItem ordemServicoItem)
        {
            try
            {
                OrdemServicoItem result = await _business.Insert(ordemServicoItem);
                if (result == null) return BadRequest("Não foi possível inserir a ordem de serviço!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPut]
        public async Task<ActionResult<OrdemServicoItem >> Update(OrdemServicoItem ordemServicoItem)
        {
            try
            {
                OrdemServicoItem result = await _business.Update(ordemServicoItem);
                if (result == null) return BadRequest("Não foi possível atualizar a ordem de serviço!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrdemServicoItem>> Delete(int id)
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
