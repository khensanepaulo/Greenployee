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
    public class MetaController : BaseController
    {
        private readonly IMetaBusiness _business;
        private readonly ICurrentUser _currentUser;

        private List<string> _permissionNeeded = new List<string>() { "Admin" };
        private readonly List<string> _permissionUser;

        public MetaController(IMetaBusiness metaBusiness, ICurrentUser currentUser)
        {
            _business = metaBusiness;
            _currentUser = currentUser;
            _permissionUser = _currentUser?.permissions?.Split(",")?.ToList() ?? new List<string>();
        }

        // GET: api/Meta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meta>>> FindAll()
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

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
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

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
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

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
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

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
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                var status = await _business.Delete(id);
                if (!status) return BadRequest("Não foi possível deletar a meta!");
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
