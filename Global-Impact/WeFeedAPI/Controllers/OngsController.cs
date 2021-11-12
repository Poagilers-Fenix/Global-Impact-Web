using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeFeedAPI.Models;

namespace WeFeedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OngsController : ControllerBase
    {
        private readonly WefeedContext _context;

        public OngsController(WefeedContext context)
        {
            _context = context;
        }

        // GET: api/Ongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ong>>> GetONGs()
        {
            return await _context.ONGs.Include(a => a.Endereco).ToListAsync();
        }

        // GET: api/Ongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ong>> GetOng(int id)
        {
            var ong = await _context.ONGs.FindAsync(id);

            if (ong == null)
            {
                return NotFound();
            }

            return ong;
        }

        // PUT: api/Ongs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOng(int id, Ong ong)
        {
            if (id != ong.OngId)
            {
                return BadRequest();
            }

            _context.Entry(ong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OngExists(id))
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

        // POST: api/Ongs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Ong>> PostOng(Ong ong)
        {
            _context.ONGs.Add(ong);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOng", new { id = ong.OngId }, ong);
        }

        // DELETE: api/Ongs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ong>> DeleteOng(int id)
        {
            var ong = await _context.ONGs.FindAsync(id);
            if (ong == null)
            {
                return NotFound();
            }

            _context.ONGs.Remove(ong);
            await _context.SaveChangesAsync();

            return ong;
        }

        private bool OngExists(int id)
        {
            return _context.ONGs.Any(e => e.OngId == id);
        }
    }
}
