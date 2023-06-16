using Greenployee.API.Authentication;
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
    public class PessoaMetaController : BaseController
    {

        private readonly IPessoaMetaBusiness _business;
        private readonly ICurrentUser _currentUser;

        private List<string> _permissionNeeded = new List<string>() { "Admin" };
        private readonly List<string> _permissionUser;

        public PessoaMetaController(IPessoaMetaBusiness pessoaMetaBusiness, ICurrentUser currentUser)
        {
            _business = pessoaMetaBusiness;
            _currentUser = currentUser;
            _permissionUser = _currentUser?.permissions?.Split(",")?.ToList() ?? new List<string>();
        }

        // GET: api/Meta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaMeta>>> FindAll()
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                var result = await _business.FindAll();
                if (result == null) return BadRequest("Não foi possível listar as metas referente a esse funcionario!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaMeta>> FindById(int id)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                PessoaMeta result = await _business.FindById(id);
                if (result == null) return NotFound("Não foi possível as metas referente a esse funcionario");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPost]
        public async Task<ActionResult<PessoaMeta>> Insert(PessoaMeta pessoaMeta)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                PessoaMeta result = await _business.Insert(pessoaMeta);
                if (result == null) return BadRequest("Não foi possível inserir as metas referente a esse funcionario!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPut]
        public async Task<ActionResult<PessoaMeta>> Update(PessoaMeta pessoaMeta)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                PessoaMeta result = await _business.Update(pessoaMeta);
                if (result == null) return BadRequest("Não foi possível atualizar os dados referentes a Pessoa!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PessoaMeta>> Delete(int id)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

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
