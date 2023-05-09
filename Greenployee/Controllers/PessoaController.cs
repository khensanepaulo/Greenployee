using System;
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
    public class PessoaController : ControllerBase
    {
        PessoaBusiness _psb = new PessoaBusiness();

        // GET: api/Pessoa
        [HttpGet]
        public List<Pessoa> GetListPessoas()
        {
            try
            {
                return _psb.GetListPessoas();
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível listar as pessoas!");
            }
        }

        // GET: api/Pessoa/5
        [HttpGet("{id}")]
        public Pessoa GetPessoa(int id)
        {
            try
            {
                return _psb.GetPessoa(id);
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível encontrar a pessoa procurada!");
            }
        }

        // POST: api/Pessoa
        [HttpPost]
        public Pessoa PostPessoa(Pessoa pessoa)
        {
            try
            {
                return _psb.SavePessoa(pessoa);
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível salvar a pessoa!");
            }
        }

        // PUT: api/Pessoa/5
        [HttpPut("{id}")]
        public Pessoa PutPessoa(Pessoa pessoa)
        {
            try
            {
                return _psb.SavePessoa(pessoa);
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível editar a pessoa!");
            }
        }

        // DELETE: api/Pessoa/5
        [HttpDelete("{id}")]
        public void DeletePessoa(int id)
        {
            try
            {
                _psb.DeletePessoa(id);
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível remover a pessoa!");
            }
        }

    }
}
