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
    public class OrdemServicoController : ControllerBase
    {
        private readonly DataContext _context;

        public OrdemServicoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/OrdemServico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdemServico>>> GetOrdensServicos()
        {
          if (_context.OrdensServicos == null)
          {
              return NotFound();
          }
            return await _context.OrdensServicos.ToListAsync();
        }

        // GET: api/OrdemServico/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdemServico>> GetOrdemServico(int id)
        {
          if (_context.OrdensServicos == null)
          {
              return NotFound();
          }
            var ordemServico = await _context.OrdensServicos.FindAsync(id);

            if (ordemServico == null)
            {
                return NotFound();
            }

            return ordemServico;
        }

        // PUT: api/OrdemServico/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdemServico(int id, OrdemServico ordemServico)
        {
            if (id != ordemServico.Id)
            {
                return BadRequest();
            }

            _context.Entry(ordemServico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdemServicoExists(id))
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

        // POST: api/OrdemServico
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdemServico>> PostOrdemServico(OrdemServico ordemServico)
        {
          if (_context.OrdensServicos == null)
          {
              return Problem("Entity set 'DataContext.OrdensServicos'  is null.");
          }
            _context.OrdensServicos.Add(ordemServico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdemServico", new { id = ordemServico.Id }, ordemServico);
        }

        // DELETE: api/OrdemServico/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdemServico(int id)
        {
            if (_context.OrdensServicos == null)
            {
                return NotFound();
            }
            var ordemServico = await _context.OrdensServicos.FindAsync(id);
            if (ordemServico == null)
            {
                return NotFound();
            }

            _context.OrdensServicos.Remove(ordemServico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdemServicoExists(int id)
        {
            return (_context.OrdensServicos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
