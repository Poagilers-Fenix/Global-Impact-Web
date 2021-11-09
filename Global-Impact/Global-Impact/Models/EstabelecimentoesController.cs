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
    public class EstabelecimentoesController : ControllerBase
    {
        private readonly WefeedContext _context;

        public EstabelecimentoesController(WefeedContext context)
        {
            _context = context;
        }

        // GET: api/Estabelecimentoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estabelecimento>>> GetEstabelecimentos()
        {
            return await _context.Estabelecimentos.ToListAsync();
        }

        // GET: api/Estabelecimentoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estabelecimento>> GetEstabelecimento(int id)
        {
            var estabelecimento = await _context.Estabelecimentos.FindAsync(id);

            if (estabelecimento == null)
            {
                return NotFound();
            }

            return estabelecimento;
        }

        // PUT: api/Estabelecimentoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstabelecimento(int id, Estabelecimento estabelecimento)
        {
            if (id != estabelecimento.EstabelecimentoId)
            {
                return BadRequest();
            }

            _context.Entry(estabelecimento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstabelecimentoExists(id))
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

        // POST: api/Estabelecimentoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Estabelecimento>> PostEstabelecimento(Estabelecimento estabelecimento)
        {
            _context.Estabelecimentos.Add(estabelecimento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstabelecimento", new { id = estabelecimento.EstabelecimentoId }, estabelecimento);
        }

        // DELETE: api/Estabelecimentoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Estabelecimento>> DeleteEstabelecimento(int id)
        {
            var estabelecimento = await _context.Estabelecimentos.FindAsync(id);
            if (estabelecimento == null)
            {
                return NotFound();
            }

            _context.Estabelecimentos.Remove(estabelecimento);
            await _context.SaveChangesAsync();

            return estabelecimento;
        }

        private bool EstabelecimentoExists(int id)
        {
            return _context.Estabelecimentos.Any(e => e.EstabelecimentoId == id);
        }
    }
}
