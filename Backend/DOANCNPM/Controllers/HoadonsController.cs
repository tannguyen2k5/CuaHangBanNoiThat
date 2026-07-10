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
    public class HoadonsController : ControllerBase
    {
        private readonly ChbntContext _context;

        public HoadonsController(ChbntContext context)
        {
            _context = context;
        }

        // GET: api/Hoadons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hoadon>>> GetHoadons()
        {
            return await _context.Hoadons.ToListAsync();
        }

        // GET: api/Hoadons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hoadon>> GetHoadon(string id)
        {
            var hoadon = await _context.Hoadons.FindAsync(id);

            if (hoadon == null)
            {
                return NotFound();
            }

            return hoadon;
        }

        // PUT: api/Hoadons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHoadon(string id, Hoadon hoadon)
        {
            if (id != hoadon.MaHd)
            {
                return BadRequest();
            }

            _context.Entry(hoadon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoadonExists(id))
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

        // POST: api/Hoadons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hoadon>> PostHoadon(Hoadon hoadon)
        {
            _context.Hoadons.Add(hoadon);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HoadonExists(hoadon.MaHd))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHoadon", new { id = hoadon.MaHd }, hoadon);
        }

        // DELETE: api/Hoadons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoadon(string id)
        {
            var hoadon = await _context.Hoadons.FindAsync(id);
            if (hoadon == null)
            {
                return NotFound();
            }

            _context.Hoadons.Remove(hoadon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HoadonExists(string id)
        {
            return _context.Hoadons.Any(e => e.MaHd == id);
        }
    }
}
