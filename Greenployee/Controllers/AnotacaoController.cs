using Greenployee.API.Controllers;
using Greenployee.CORE.Business;
using Greenployee.MODELS.Authentication;
using Greenployee.MODELS.DTO.Anotacao;
using Greenployee.MODELS.DTO.Anotacao;
using Greenployee.MODELS.Model;
using Greenployee.MODELS.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Greenployee.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnotacaoController : BaseController
    {
        private readonly IAnotacaoBusiness _business;
        private readonly ICurrentUser _currentUser;

        private List<string> _permissionNeeded = new List<string>() { "Admin" };
        private readonly List<string> _permissionUser;

        public AnotacaoController(IAnotacaoBusiness anotacaoBusiness, ICurrentUser currentUser)
        {
            _business = anotacaoBusiness;
            _currentUser = currentUser;
            _permissionUser = _currentUser?.permissions?.Split(",")?.ToList() ?? new List<string>();
        }

        // GET: api/Meta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Anotacao>>> FindAll()
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

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
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

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
        public async Task<ActionResult<Anotacao>> Insert(AnotacaoDTO anotacao)
        {
            try
            {
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                Anotacao result = await _business.Insert(anotacao);
                if (result == null) return BadRequest("Não foi possível inserir a anotacao!");
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public async Task<ActionResult<Anotacao>> Update(Anotacao anotacao)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

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
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                var status = await _business.Delete(id);
                if (!status) return BadRequest("Não foi possível deletar a anotação!");
                return Ok(status);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpGet("/api/[controller]/Usuario/{id}")]
        public async Task<ActionResult<Anotacao>> FindByUserId(int id)
        {
            try
            {
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                var result = await _business.FindByUserId(id);
                if (result == null) return NotFound("Não foi possível encontrar a Pessoa!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
