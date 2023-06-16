using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Context;
using BusinessLogic.Entities;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUtilizadorsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public TipoUtilizadorsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoUtilizadors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoUtilizador>>> GetTipoUtilizadors()
        {
            if (_context.Tipoutilizadors == null)
            {
                return NotFound();
            }

            return await _context.Tipoutilizadors.ToListAsync();
        }

        // GET: api/TipoUtilizadors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUtilizador>> GetTipoUtilizador(int id)
        {
            if (_context.Tipoutilizadors == null)
            {
                return NotFound();
            }

            var tipoUtilizador = await _context.Tipoutilizadors.FindAsync(id);

            if (tipoUtilizador == null)
            {
                return NotFound();
            }

            return tipoUtilizador;
        }

        // PUT: api/TipoUtilizadors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoUtilizador(int id, TipoUtilizador tipoUtilizador)
        {
            if (id != tipoUtilizador.TipoId)
            {
                return BadRequest();
            }

            _context.Entry(tipoUtilizador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoUtilizadorExists(id))
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

        // POST: api/TipoUtilizadors
        [HttpPost]
        public async Task<ActionResult<TipoUtilizador>> PostTipoUtilizador(TipoUtilizador tipoUtilizador)
        {
            if (_context.Tipoutilizadors == null)
            {
                return Problem("Entity set 'MyDbContext.TipoUtilizadors' is null.");
            }

            _context.Tipoutilizadors.Add(tipoUtilizador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoUtilizador", new { id = tipoUtilizador.TipoId }, tipoUtilizador);
        }

        // DELETE: api/TipoUtilizadors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoUtilizador(int id)
        {
            if (_context.Tipoutilizadors == null)
            {
                return NotFound();
            }

            var tipoUtilizador = await _context.Tipoutilizadors.FindAsync(id);
            if (tipoUtilizador == null)
            {
                return NotFound();
            }

            _context.Tipoutilizadors.Remove(tipoUtilizador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoUtilizadorExists(int id)
        {
            return (_context.Tipoutilizadors?.Any(e => e.TipoId == id)).GetValueOrDefault();
        }
    }
}
