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
    public class ParticipantesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ParticipantesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Participantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetParticipantes()
        {
            if (_context.Participantes == null)
            {
                return NotFound();
            }

            return await _context
                .Participantes.Select(p => new
                {
                    p.Id,
                    p.Nome,
                    p.Email,
                    p.Telefone,
                    Inscricoes = p.Inscricos.Select(i => new
                    {
                        i.Id,
                        i.EventoId,
                        i.ParticipanteId,
                        i.DataInscricao
                    }),
                    TiposUtilizador = p.Tipoutilizadors.Select(t => new
                    {
                        t.TipoId,
                        t.Tipo,
                        t.ParticipanteId
                    })
                }).ToListAsync();
        }

        // GET: api/Participantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participante>> GetParticipante(int id)
        {
            if (_context.Participantes == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes.FindAsync(id);

            if (participante == null)
            {
                return NotFound();
            }

            return participante;
        }

        // PUT: api/Participantes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipante(int id, Participante participante)
        {
            if (id != participante.Id)
            {
                return BadRequest();
            }

            _context.Entry(participante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipanteExists(id))
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

        // POST: api/Participantes
        [HttpPost]
        public async Task<ActionResult<Participante>> PostParticipante(Participante participante)
        {
            if (_context.Participantes == null)
            {
                return Problem("Entity set 'MyDbContext.Participantes' is null.");
            }

            _context.Participantes.Add(participante);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParticipante", new { id = participante.Id }, participante);
        }

        // DELETE: api/Participantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipante(int id)
        {
            if (_context.Participantes == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes.FindAsync(id);
            if (participante == null)
            {
                return NotFound();
            }

            _context.Participantes.Remove(participante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipanteExists(int id)
        {
            return (_context.Participantes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        
        
           
    [HttpPost("login")]
    public async Task<ActionResult<dynamic>> Login(LoginModel loginModel)
    {
        var participante = await _context.Participantes.FirstOrDefaultAsync(p => p.Email == loginModel.Email);

        if (participante == null)
        {
            return NotFound();
        }

        //if (!VerifyPasswordHash(loginModel.Senha, participante.SenhaHash, participante.SenhaSalt))
        //{
           // return Unauthorized();
      //  }

       // var token = GenerateJwtToken(participante);

        return new
        {
            participante.Id,
            participante.Nome,
            participante.Email,
           // Token = token
        };
    }
    
    public class LoginModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    private bool VerifyPasswordHash(string senha, byte[] senhaHash, byte[] senhaSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512(senhaSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != senhaHash[i])
                {
                    return false;
                }
            }
        }

        return true;
    }

   // private string GenerateJwtToken(Participante participante)
   // {
       // var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
       // var key = Encoding.ASCII.GetBytes("SuaChaveSecretaAqui"); // Substitua pela sua chave secreta
       // var tokenDescriptor = new System.IdentityModel.Tokens.SecurityTokenDescriptor
       // {
         //   Subject = new System.Security.Claims.ClaimsIdentity(new[]
          //  {
           //     new System.Security.Claims.Claim("id", participante.Id.ToString()),
          //      new System.Security.Claims.Claim("email", participante.Email)
         //   }),
         //   Expires = DateTime.UtcNow.AddDays(7),
          //  SigningCredentials = new System.IdentityModel.Tokens.SigningCredentials(new System.IdentityModel.Tokens.SymmetricSecurityKey(key),
          //      System.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
      //  };

        //var token = tokenHandler.CreateToken(tokenDescriptor);
        //return tokenHandler.WriteToken(token);
   // }
        
    }
 


}
