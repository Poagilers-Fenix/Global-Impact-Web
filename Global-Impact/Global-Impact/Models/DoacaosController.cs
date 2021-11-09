using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Global_Impact.Persistence;

namespace Global_Impact.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoacaosController : ControllerBase
    {
        private readonly WefeedContext _context;

        public DoacaosController(WefeedContext context)
        {
            _context = context;
        }

        // GET: api/Doacaos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doacao>>> GetDoacoes()
        {
            return await _context.Doacoes.ToListAsync();
        }

        // GET: api/Doacaos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doacao>> GetDoacao(int id)
        {
            var doacao = await _context.Doacoes.FindAsync(id);

            if (doacao == null)
            {
                return NotFound();
            }

            return doacao;
        }

        // PUT: api/Doacaos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoacao(int id, Doacao doacao)
        {
            if (id != doacao.DoacaoId)
            {
                return BadRequest();
            }

            _context.Entry(doacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoacaoExists(id))
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

        // POST: api/Doacaos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Doacao>> PostDoacao(Doacao doacao)
        {
            _context.Doacoes.Add(doacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoacao", new { id = doacao.DoacaoId }, doacao);
        }

        // DELETE: api/Doacaos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Doacao>> DeleteDoacao(int id)
        {
            var doacao = await _context.Doacoes.FindAsync(id);
            if (doacao == null)
            {
                return NotFound();
            }

            _context.Doacoes.Remove(doacao);
            await _context.SaveChangesAsync();

            return doacao;
        }

        private bool DoacaoExists(int id)
        {
            return _context.Doacoes.Any(e => e.DoacaoId == id);
        }
    }
}
