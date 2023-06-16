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
    public class InscricoesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public InscricoesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Inscricoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetInscricoes()
        {
            if (_context.Inscricoes == null)
            {
                return NotFound();
            }

            return await _context
                .Inscricoes.Select(i => new
                {
                    i.Id,
                    i.EventoId,
                    i.ParticipanteId,
                    i.DataInscricao,
                    Evento = new
                    {
                        i.Evento.Id,
                        i.Evento.Nome,
                        i.Evento.DataInicio,
                        i.Evento.DataFim,
                        i.Evento.Local
                    },
                    Participante = new
                    {
                        i.Participante.Id,
                        i.Participante.Nome,
                        i.Participante.Email,
                        i.Participante.Telefone
                    }
                }).ToListAsync();
        }

        // GET: api/Inscricoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inscricao>> GetInscricao(int id)
        {
            if (_context.Inscricoes == null)
            {
                return NotFound();
            }

            var inscricao = await _context.Inscricoes.FindAsync(id);

            if (inscricao == null)
            {
                return NotFound();
            }

            return inscricao;
        }

        // PUT: api/Inscricoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInscricao(int id, Inscricao inscricao)
        {
            if (id != inscricao.Id)
            {
                return BadRequest();
            }

            _context.Entry(inscricao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscricaoExists(id))
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

        // POST: api/Inscricoes
        [HttpPost]
        public async Task<ActionResult<Inscricao>> PostInscricao(Inscricao inscricao)
        {
            if (_context.Inscricoes == null)
            {
                return Problem("Entity set 'MyDbContext.Inscricoes' is null.");
            }

            _context.Inscricoes.Add(inscricao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInscricao", new { id = inscricao.Id }, inscricao);
        }

        // DELETE: api/Inscricoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInscricao(int id)
        {
            if (_context.Inscricoes == null)
            {
                return NotFound();
            }

            var inscricao = await _context.Inscricoes.FindAsync(id);
            if (inscricao == null)
            {
                return NotFound();
            }

            _context.Inscricoes.Remove(inscricao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InscricaoExists(int id)
        {
            return (_context.Inscricoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
