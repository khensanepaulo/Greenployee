﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Greenployee.CORE.Business;
using Greenployee.MODELS.Model;

namespace Greenployee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaMetaController : ControllerBase
    {

        private readonly IOPessoaMetaBusiness _business;


        public PessoaMetaController(IOPessoaMetaBusiness pessoaMetaBusiness)
        {
            _business = pessoaMetaBusiness;
        }

        // GET: api/Meta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaMeta>>> FindAll()
        {
            try
            {
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
