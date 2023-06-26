using Greenployee.API.Controllers;
using Greenployee.CORE.Business;
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
    public class OrdemServicoItemController : BaseController
    {
        private readonly IOrdemServicoItemBusiness _business;
        private readonly ICurrentUser _currentUser;

        private List<string> _permissionNeeded = new List<string>() { "Admin" };
        private readonly List<string> _permissionUser;

        public OrdemServicoItemController(IOrdemServicoItemBusiness ordemServicoItemBusiness, ICurrentUser currentUser)
        {
            _business = ordemServicoItemBusiness;
            _currentUser = currentUser;
            _permissionUser = _currentUser?.permissions?.Split(",")?.ToList() ?? new List<string>();
        }

        // GET: api/Meta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdemServicoItem>>> FindAll()
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
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

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
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

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
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

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
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                var status = await _business.Delete(id);
                if (!status) return BadRequest("Não foi possível deletar a ordem de serviço!");
                return Ok(status);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpGet("/api/[controller]/Usuario/{id}")]
        public async Task<ActionResult<OrdemServicoItem>> FindByUserId(int id)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                {
                    return Forbidden();
                }

                var result = await _business.FindByUserId(id);

                if (result == null) return NotFound("Não foi possível encontrar a Pessoa!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpGet("/api/[controller]/Vendas")]
        public async Task<ActionResult<IEnumerable<OrdemServicoItem>>> GetLastSalesChart()
        {
            try
            {
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                {
                    return Forbidden();
                }

                var result = await _business.GetLastSalesChart();

                if (result == null) return NotFound("Não há vendas registradas na ultima semana");
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}
