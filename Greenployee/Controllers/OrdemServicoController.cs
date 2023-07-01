using Greenployee.API.Controllers;
using Greenployee.CORE.Business;
using Greenployee.CORE.Filters;
using Greenployee.MODELS.Authentication;
using Greenployee.MODELS.DTO;
using Greenployee.MODELS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Greenployee.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdemServicoController : BaseController
    {
        private readonly IOrdemServicoBusiness _business;
        private readonly ICurrentUser _currentUser;

        private List<string> _permissionNeeded = new List<string>() { "Admin" };
        private readonly List<string> _permissionUser;

        public OrdemServicoController(IOrdemServicoBusiness ordemServicoBusiness, ICurrentUser currentUser)
        {
            _business = ordemServicoBusiness;
            _currentUser = currentUser;
            _permissionUser = _currentUser?.permissions?.Split(",")?.ToList() ?? new List<string>();
        }

        // GET: api/Meta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdemServico>>> FindAll()
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                var result = await _business.FindAll();
                if (result == null) return BadRequest("Não foi possível listar as ordens de serviço!");
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdemServico>> FindById(int id)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                OrdemServico result = await _business.FindById(id);
                if (result == null) return NotFound("Não foi possível encontrar a ordem de serviço!");
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrdemServico>> Insert(OrdemServico ordemServico)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                OrdemServico result = await _business.Insert(ordemServico);
                if (result == null) return BadRequest("Não foi possível inserir a ordem de serviço!");
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public async Task<ActionResult<OrdemServico>> Update(OrdemServico ordemServico)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                OrdemServico result = await _business.Update(ordemServico);
                if (result == null) return BadRequest("Não foi possível atualizar a ordem de serviço!");
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrdemServico>> Delete(int id)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                var status = await _business.Delete(id);
                if (!status) return BadRequest("Não foi possível deletar a ordem de serviço!");
                return Ok(status);
            }
            catch (Exception) 
            { 
                throw; 
            }
        }

        [HttpGet("/api/[controller]/Usuario/{id}")]
        public async Task<ActionResult<dynamic>> FindByUserId(int id)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                var result = await _business.FindByUserId(id);
                if (result == null) return NotFound("Não foi possível encontrar a Pessoa!");
                return Ok(result);
            }
            catch (Exception)
            { 
                throw; 
            }
        }

        [HttpGet("/api/[controller]/OrdemPorMes/{id}")]
        public async Task<ActionResult<dynamic>> FindBycommissionsByMonthById(int id)
        {
            try
            {
               
                _permissionNeeded.Add("User");

                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                {
                    return NotFound();
                }

                var result = await _business.FindBycommissionsByMonthById(id);

                if (result == null) return NotFound("Não foi possível encontrar comissões!");
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("/api/[controller]/OrdemPorMes")]
        public async Task<ActionResult<dynamic>> FindByCommissionsByMonthAll()
        {
            try
            {
                _permissionNeeded.Add("Admin");

                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                {
                    return NotFound();
                }

                var result = await _business.FindByCommissionsByMonthAll();

                if (result == null) return NotFound("Não foi possível encontrar as comissões!");
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("paged")]
        public async Task<ActionResult<PagedBaseRespondeDTO<OrdemServico>>> GetPagedAsync([FromQuery] OrdemServicoFilter ordemServicoFilter)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                {
                    return NotFound();
                }

                var ordensPaged = await _business.GetPagedAsync(ordemServicoFilter);
                var result = new PagedBaseRespondeDTO<OrdemServico>(ordensPaged.TotalRegisters, new List<OrdemServico>(ordensPaged.Data));

                if (result == null) return NotFound("Não foi possível encontrar as ordens de serviço!");
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("usuario/paged")]
        public async Task<ActionResult<PagedBaseRespondeDTO<OrdemServico>>> GetPagedByUserIdAsync([FromQuery] OrdemServicoFilter ordemServicoFilter)
        {
            try
            {
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                {
                    return NotFound();
                }

                var ordensPaged = await _business.GetPagedAsync(ordemServicoFilter);
                var result = new PagedBaseRespondeDTO<OrdemServico>(ordensPaged.TotalRegisters, new List<OrdemServico>(ordensPaged.Data));

                if (result == null) return NotFound("Não foi possível encontrar as ordens de serviço!");
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
