using DOANCNPM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOANCNPM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChitiethoadonsController : ControllerBase
    {
        private readonly ChbntContext _context;

        public ChitiethoadonsController(ChbntContext context)
        {
            _context = context;
        }

        // GET: api/Chitiethoadons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chitiethoadon>>> GetChitiethoadons()
        {
            return await _context.Chitiethoadons.ToListAsync();
        }

        // GET: api/Chitiethoadons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chitiethoadon>> GetChitiethoadon(string id)
        {
            var chitiethoadon = await _context.Chitiethoadons.FindAsync(id);

            if (chitiethoadon == null)
            {
                return NotFound();
            }

            return chitiethoadon;
        }

        // PUT: api/Chitiethoadons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChitiethoadon(string id, Chitiethoadon chitiethoadon)
        {
            if (id != chitiethoadon.MaHd)
            {
                return BadRequest();
            }

            _context.Entry(chitiethoadon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChitiethoadonExists(id))
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

        // POST: api/Chitiethoadons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Chitiethoadon>> PostChitiethoadon(Chitiethoadon chitiethoadon)
        {
            _context.Chitiethoadons.Add(chitiethoadon);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ChitiethoadonExists(chitiethoadon.MaHd))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetChitiethoadon", new { id = chitiethoadon.MaHd }, chitiethoadon);
        }

        // DELETE: api/Chitiethoadons/5
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChitiethoadon(string id)
        {
            var chitiethoadon = await _context.Chitiethoadons.FindAsync(id);
            if (chitiethoadon == null)
            {
                return NotFound();
            }

            _context.Chitiethoadons.Remove(chitiethoadon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChitiethoadonExists(string id)
        {
            return _context.Chitiethoadons.Any(e => e.MaHd == id);
        }
    }
}
