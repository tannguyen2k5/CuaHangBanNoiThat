using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DOANCNPM.Models;

namespace DOANCNPM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatlieuxController : ControllerBase
    {
        private readonly ChbntContext _context;

        public VatlieuxController(ChbntContext context)
        {
            _context = context;
        }

        // GET: api/Vatlieux
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vatlieu>>> GetVatlieus()
        {
            return await _context.Vatlieus.ToListAsync();
        }

        // GET: api/Vatlieux/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vatlieu>> GetVatlieu(string id)
        {
            var vatlieu = await _context.Vatlieus.FindAsync(id);

            if (vatlieu == null)
            {
                return NotFound();
            }

            return vatlieu;
        }

        // PUT: api/Vatlieux/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVatlieu(string id, Vatlieu vatlieu)
        {
            if (id != vatlieu.MaVl)
            {
                return BadRequest();
            }

            _context.Entry(vatlieu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VatlieuExists(id))
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

        // POST: api/Vatlieux
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vatlieu>> PostVatlieu(Vatlieu vatlieu)
        {
            _context.Vatlieus.Add(vatlieu);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VatlieuExists(vatlieu.MaVl))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVatlieu", new { id = vatlieu.MaVl }, vatlieu);
        }

        // DELETE: api/Vatlieux/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVatlieu(string id)
        {
            var vatlieu = await _context.Vatlieus.FindAsync(id);
            if (vatlieu == null)
            {
                return NotFound();
            }

            _context.Vatlieus.Remove(vatlieu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VatlieuExists(string id)
        {
            return _context.Vatlieus.Any(e => e.MaVl == id);
        }
    }
}
