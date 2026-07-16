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
    public class NhanviensController : ControllerBase
    {
        private readonly ChbntContext _context;

        public NhanviensController(ChbntContext context)
        {
            _context = context;
        }

        // GET: api/Nhanviens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nhanvien>>> GetNhanviens()
        {
            return await _context.Nhanviens.ToListAsync();
        }

        // GET: api/Nhanviens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nhanvien>> GetNhanvien(string id)
        {
            var nhanvien = await _context.Nhanviens.FindAsync(id);

            if (nhanvien == null)
            {
                return NotFound();
            }

            return nhanvien;
        }

        // PUT: api/Nhanviens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNhanvien(string id, Nhanvien nhanvien)
        {
            if (id != nhanvien.MaNv)
            {
                return BadRequest();
            }

            _context.Entry(nhanvien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhanvienExists(id))
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

        // POST: api/Nhanviens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Nhanvien>> PostNhanvien(Nhanvien nhanvien)
        {
            _context.Nhanviens.Add(nhanvien);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NhanvienExists(nhanvien.MaNv))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNhanvien", new { id = nhanvien.MaNv }, nhanvien);
        }

        // DELETE: api/Nhanviens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNhanvien(string id)
        {
            var nhanvien = await _context.Nhanviens.FindAsync(id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            _context.Nhanviens.Remove(nhanvien);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NhanvienExists(string id)
        {
            return _context.Nhanviens.Any(e => e.MaNv == id);
        }
    }
}
