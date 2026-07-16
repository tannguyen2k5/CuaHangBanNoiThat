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
    public class SanphamsController : ControllerBase
    {
        private readonly ChbntContext _context;

        public SanphamsController(ChbntContext context)
        {
            _context = context;
        }

        // GET: api/Sanphams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sanpham>>> GetSanphams()
        {
            return await _context.Sanphams.ToListAsync();
        }

        // GET: api/Sanphams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sanpham>> GetSanpham(string id)
        {
            var sanpham = await _context.Sanphams.FindAsync(id);

            if (sanpham == null)
            {
                return NotFound();
            }

            return sanpham;
        }

        // PUT: api/Sanphams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSanpham(string id, Sanpham sanpham)
        {
            if (id != sanpham.MaSp)
            {
                return BadRequest();
            }

            _context.Entry(sanpham).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanphamExists(id))
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

        // POST: api/Sanphams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sanpham>> PostSanpham(Sanpham sanpham)
        {
            _context.Sanphams.Add(sanpham);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SanphamExists(sanpham.MaSp))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSanpham", new { id = sanpham.MaSp }, sanpham);
        }

        // DELETE: api/Sanphams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSanpham(string id)
        {
            var sanpham = await _context.Sanphams.FindAsync(id);
            if (sanpham == null)
            {
                return NotFound();
            }

            _context.Sanphams.Remove(sanpham);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SanphamExists(string id)
        {
            return _context.Sanphams.Any(e => e.MaSp == id);
        }
    }
}
