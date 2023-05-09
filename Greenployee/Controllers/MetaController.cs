using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greenployee.MODELS.Data;
using Greenployee.MODELS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Greenployee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetasController : ControllerBase
    {
        private readonly DataContext _context;

        public MetasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Metas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meta>>> GetMetas()
        {
          if (_context.Metas == null)
          {
              return NotFound();
          }
            return await _context.Metas.ToListAsync();
        }

        // GET: api/Metas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Meta>> GetByIdMeta(int id)
        {
          if (_context.Metas == null)
          {
              return NotFound();
          }
            var meta = await _context.Metas.FindAsync(id);

            if (meta == null)
            {
                return NotFound();
            }

            return meta;
        }

        // PUT: api/Metas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeta(int id, Meta meta)
        {
            if (id != meta.Id)
            {
                return BadRequest();
            }

            _context.Entry(meta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaExists(id))
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

        // POST: api/Metas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Meta>> PostMeta(Meta meta)
        {
          if (_context.Metas == null)
          {
              return Problem("Entity set 'DataContext.Meta'  is null.");
          }
            _context.Metas.Add(meta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeta", new { id = meta.Id }, meta);
        }

        // DELETE: api/Metas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeta(int id)
        {
            if (_context.Metas == null)
            {
                return NotFound();
            }
            var meta = await _context.Metas.FindAsync(id);
            if (meta == null)
            {
                return NotFound();
            }

            _context.Metas.Remove(meta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MetaExists(int id)
        {
            return (_context.Metas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
