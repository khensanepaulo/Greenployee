﻿using Greenployee.API.Controllers;
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
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

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
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

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
                _permissionNeeded.Add("Admin");
                _permissionNeeded.Add("User");
                if (!ValidatePermission(_permissionNeeded, _permissionUser))
                    return Forbidden();

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
                _permissionNeeded.Add("Admin");
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
    }
}
