using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Greenployee.Data;
using Greenployee.Model;

namespace Greenployee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnotacaoController : ControllerBase
    {
        private readonly DataContext _context;

        public AnotacaoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Anotacao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Anotacao>>> GetAnotacoes()
        {
          if (_context.Anotacao == null)
          {
              return NotFound();
          }
            return await _context.Anotacao.ToListAsync();
        }

        // GET: api/Anotacao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Anotacao>> GetAnotacao(int id)
        {
          if (_context.Anotacao == null)
          {
              return NotFound();
          }
            var anotacao = await _context.Anotacao.FindAsync(id);

            if (anotacao == null)
            {
                return NotFound();
            }

            return anotacao;
        }

        // PUT: api/Anotacao/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnotacao(int id, Anotacao anotacao)
        {
            if (id != anotacao.Id)
            {
                return BadRequest();
            }

            _context.Entry(anotacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnotacaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Anotacao
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Anotacao>> PostAnotacao(Anotacao anotacao)
        {
          if (_context.Anotacao == null)
          {
              return Problem("Entity set 'DataContext.Anotacao'  is null.");
          }
            _context.Anotacao.Add(anotacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnotacao", new { id = anotacao.Id }, anotacao);
        }

        // DELETE: api/Anotacao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnotacao(int id)
        {
            if (_context.Anotacao == null)
            {
                return NotFound();
            }
            var anotacao = await _context.Anotacao.FindAsync(id);
            if (anotacao == null)
            {
                return NotFound();
            }

            _context.Anotacao.Remove(anotacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnotacaoExists(int id)
        {
            return (_context.Anotacao?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
