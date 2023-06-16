using Greenployee.CORE.Business;
using Greenployee.MODELS.Authentication;
using Greenployee.MODELS.DTO;
using Greenployee.MODELS.Model;
using Greenployee.MODELS.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Greenployee.API.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioBusiness _business;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ICurrentUser _currentUser;

        private List<string> _permissionNeeded = new List<string>() { "Admin" };
        private readonly List<string> _permissionUser;

        public UsuarioController(IUsuarioBusiness usuarioBusiness, ITokenGenerator tokenGenerator, ICurrentUser currentUser)
        {
            _business = usuarioBusiness;
            _tokenGenerator = tokenGenerator;
            _currentUser = currentUser;
            _permissionUser = _currentUser?.permissions?.Split(",")?.ToList() ?? new List<string>();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> FindAll()
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                var result = await _business.FindAll();
                if (result == null) return BadRequest("Não foi possível listar os usuários!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> FindById(int id)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                Usuario result = await _business.FindById(id);
                if (result == null) return NotFound("Não foi possível localizar o usuário");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Insert(Usuario usuario)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                Usuario result = await _business.Insert(usuario);
                if (result == null) return BadRequest("Não foi possível inserir o usuário!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPut]
        public async Task<ActionResult<Usuario>> Update(Usuario usuario)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                Usuario result = await _business.Update(usuario);
                if (result == null) return BadRequest("Não foi possível atualizar o usuário!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                var status = await _business.Delete(id);
                if (!status) return BadRequest("Não foi possível deletar o usuário!");
                return Ok(status);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPost]
        [Route("/token")]
        public async Task<ActionResult<dynamic>> GenerateTokenAsync(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null) return BadRequest("Objeto deve ser informado!");

            var validator = new UsuarioValidator().Validate(usuarioDTO);
            if (!validator.IsValid) return ("Problemas de validação");

            var user = await _business.GetUserByLoginAndSenhaAsync(usuarioDTO.dsLogin, usuarioDTO.dsSenha);
            if (user == null) return BadRequest("Usuario ou Senha não encontrado!");

            return Ok(_tokenGenerator.Generator(user));

        }
    }
}
