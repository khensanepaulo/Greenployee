﻿using Greenployee.API.Controllers;
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
    public class PessoaController : BaseController
    {

        private readonly IPessoaBusiness _business;
        private readonly ICurrentUser _currentUser;

        private List<string> _permissionNeeded = new List<string>() { "Admin" };
        private readonly List<string> _permissionUser;

        public PessoaController(IPessoaBusiness pessoaBusiness, ICurrentUser currentUser)
        {
            _business = pessoaBusiness;
            _currentUser = currentUser;
            _permissionUser = _currentUser?.permissions?.Split(",")?.ToList() ?? new List<string>();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> FindAll()
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                var result = await _business.FindAllWithUsuarios();
                if (result == null)
                    return BadRequest("Não foi possível listar as Pessoas!");

                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> FindById(int id)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                Pessoa result = await _business.FindById(id);
                if (result == null) return NotFound("Não foi possível encontrar a Pessoa!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Pessoa>> Insert(Pessoa pessoa)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                Pessoa result = await _business.Insert(pessoa);
                if (result == null) return BadRequest("Não foi possível inserir pessoa!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpPut]
        public async Task<ActionResult<Pessoa>> Update(Pessoa pessoa)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                Pessoa result = await _business.Update(pessoa);
                if (result == null) return BadRequest("Não foi possível atualizar os dados referentes a pessoa!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pessoa>> Delete(int id)
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

        [HttpGet("/api/[controller]/Usuario/{id}")]
        public async Task<ActionResult<Pessoa>> FindByUserId(int id)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

                Pessoa result = await _business.FindByUserId(id);
                if (result == null) return NotFound("Não foi possível encontrar a Pessoa!");
                return Ok(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [HttpGet]
        [Route("paged")]
        public async Task<ActionResult<PagedBaseRespondeDTO<Pessoa>>> GetPagedAsync([FromQuery] PessoaFilter request)
        {
            try
            {
                _permissionNeeded.Add("Admin");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                {
                    return NotFound();
                }

                var pessoaPaged = await _business.GetPagedAsync(request);
                var result = new PagedBaseRespondeDTO<Pessoa>(pessoaPaged.TotalRegisters, new List<Pessoa>(pessoaPaged.Data));

                if (result == null) return NotFound("Não foi possível encontrar a Pessoa!");
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("usuario/paged")]
        public async Task<ActionResult<PagedBaseRespondeDTO<OrdemServico>>> GetPagedByUserIdAsync([FromQuery] PessoaFilter request)
        {
            try
            {
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                {
                    return NotFound();
                }

                var pessoaPaged = await _business.GetPagedAsync(request);
                var result = new PagedBaseRespondeDTO<Pessoa>(pessoaPaged.TotalRegisters, new List<Pessoa>(pessoaPaged.Data));

                if (result == null) return NotFound("Não foi possível encontrar Pessoa!");
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
