using Greenployee.API.Controllers;
using Greenployee.CORE.Business;
using Greenployee.MODELS.Authentication;
using Greenployee.MODELS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Greenployee.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissaoUsuarioController : BaseController
    {

        private readonly IPermissaoUsuarioBusiness _business;
        private readonly ICurrentUser _currentUser;

        private List<string> _permissionNeeded = new List<string>() { "Admin" };
        private readonly List<string> _permissionUser;

        public PermissaoUsuarioController(IPermissaoUsuarioBusiness permissaoUsuarioBusiness, ICurrentUser currentUser)
        {
            _business = permissaoUsuarioBusiness;
            _currentUser = currentUser;
            _permissionUser = _currentUser?.permissions?.Split(",")?.ToList() ?? new List<string>();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissaoUsuario>>> FindAll()
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                var result = await _business.FindAllWithUsuarios();
                if (result == null)
                    return BadRequest("Não foi possível listar as PermissaoUsuarios!");

                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PermissaoUsuario>> FindById(int id)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                PermissaoUsuario result = await _business.FindById(id);
                if (result == null) return NotFound("Não foi possível encontrar a PermissaoUsuario!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPost]
        public async Task<ActionResult<PermissaoUsuario>> Insert(PermissaoUsuario permissaoUsuario)
        {
            try
            {
                
                PermissaoUsuario result = await _business.Insert(permissaoUsuario);
                if (result == null) return BadRequest("Não foi possível inserir permissaoUsuario!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPut]
        public async Task<ActionResult<PermissaoUsuario>> Update(PermissaoUsuario permissaoUsuario)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                PermissaoUsuario result = await _business.Update(permissaoUsuario);
                if (result == null) return BadRequest("Não foi possível atualizar os dados referentes a permissaoUsuario!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PermissaoUsuario>> Delete(int id)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                var status = await _business.Delete(id);
                if (!status) return BadRequest("Não foi possível deletar essa permissaoUsuario!");
                return Ok(status);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpGet("/api/[controller]/Usuario/{id}")]
        public async Task<ActionResult<PermissaoUsuario>> FindByUserId(int id)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                PermissaoUsuario result = await _business.FindByUserId(id);
                if (result == null) return NotFound("Não foi possível encontrar a PermissaoUsuario!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

    }

}
