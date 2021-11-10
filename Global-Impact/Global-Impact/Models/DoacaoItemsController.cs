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
    public class DoacaoItemsController : ControllerBase
    {
        private readonly WefeedContext _context;

        public DoacaoItemsController(WefeedContext context)
        {
            _context = context;
        }

        // GET: api/DoacaoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoacaoItem>>> GetDoacoesAlimentos()
        {
            return await _context.DoacoesItens.ToListAsync();
        }

        // GET: api/DoacaoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoacaoItem>> GetDoacaoItem(int id)
        {
            var doacaoItem = await _context.DoacoesItens.FindAsync(id);

            if (doacaoItem == null)
            {
                return NotFound();
            }

            return doacaoItem;
        }

        // PUT: api/DoacaoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoacaoItem(int id, DoacaoItem doacaoItem)
        {
            if (id != doacaoItem.DoacaoId)
            {
                return BadRequest();
            }

            _context.Entry(doacaoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoacaoItemExists(id))
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

        // POST: api/DoacaoItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DoacaoItem>> PostDoacaoItem(DoacaoItem doacaoItem)
        {
            _context.DoacoesItens.Add(doacaoItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DoacaoItemExists(doacaoItem.DoacaoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDoacaoItem", new { id = doacaoItem.DoacaoId }, doacaoItem);
        }

        // DELETE: api/DoacaoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DoacaoItem>> DeleteDoacaoItem(int id)
        {
            var doacaoItem = await _context.DoacoesItens.FindAsync(id);
            if (doacaoItem == null)
            {
                return NotFound();
            }

            _context.DoacoesItens.Remove(doacaoItem);
            await _context.SaveChangesAsync();

            return doacaoItem;
        }

        private bool DoacaoItemExists(int id)
        {
            return _context.DoacoesItens.Any(e => e.DoacaoId == id);
        }
    }
}
