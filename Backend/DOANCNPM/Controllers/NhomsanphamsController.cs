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
    public class NhomsanphamsController : ControllerBase
    {
        private readonly ChbntContext _context;

        public NhomsanphamsController(ChbntContext context)
        {
            _context = context;
        }

        // GET: api/Nhomsanphams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nhomsanpham>>> GetNhomsanphams()
        {
            return await _context.Nhomsanphams.ToListAsync();
        }

        // GET: api/Nhomsanphams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nhomsanpham>> GetNhomsanpham(string id)
        {
            var nhomsanpham = await _context.Nhomsanphams.FindAsync(id);

            if (nhomsanpham == null)
            {
                return NotFound();
            }

            return nhomsanpham;
        }

        // PUT: api/Nhomsanphams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNhomsanpham(string id, Nhomsanpham nhomsanpham)
        {
            if (id != nhomsanpham.MaNhomSp)
            {
                return BadRequest();
            }

            _context.Entry(nhomsanpham).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhomsanphamExists(id))
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

        // POST: api/Nhomsanphams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Nhomsanpham>> PostNhomsanpham(Nhomsanpham nhomsanpham)
        {
            _context.Nhomsanphams.Add(nhomsanpham);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NhomsanphamExists(nhomsanpham.MaNhomSp))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNhomsanpham", new { id = nhomsanpham.MaNhomSp }, nhomsanpham);
        }

        // DELETE: api/Nhomsanphams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNhomsanpham(string id)
        {
            var nhomsanpham = await _context.Nhomsanphams.FindAsync(id);
            if (nhomsanpham == null)
            {
                return NotFound();
            }

            _context.Nhomsanphams.Remove(nhomsanpham);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NhomsanphamExists(string id)
        {
            return _context.Nhomsanphams.Any(e => e.MaNhomSp == id);
        }
    }
}
