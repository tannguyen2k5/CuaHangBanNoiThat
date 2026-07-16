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
    public class MucdichsudungsController : ControllerBase
    {
        private readonly ChbntContext _context;

        public MucdichsudungsController(ChbntContext context)
        {
            _context = context;
        }

        // GET: api/Mucdichsudungs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mucdichsudung>>> GetMucdichsudungs()
        {
            return await _context.Mucdichsudungs.ToListAsync();
        }

        // GET: api/Mucdichsudungs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mucdichsudung>> GetMucdichsudung(string id)
        {
            var mucdichsudung = await _context.Mucdichsudungs.FindAsync(id);

            if (mucdichsudung == null)
            {
                return NotFound();
            }

            return mucdichsudung;
        }

        // PUT: api/Mucdichsudungs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMucdichsudung(string id, Mucdichsudung mucdichsudung)
        {
            if (id != mucdichsudung.MaMd)
            {
                return BadRequest();
            }

            _context.Entry(mucdichsudung).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MucdichsudungExists(id))
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

        // POST: api/Mucdichsudungs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mucdichsudung>> PostMucdichsudung(Mucdichsudung mucdichsudung)
        {
            _context.Mucdichsudungs.Add(mucdichsudung);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MucdichsudungExists(mucdichsudung.MaMd))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMucdichsudung", new { id = mucdichsudung.MaMd }, mucdichsudung);
        }

        // DELETE: api/Mucdichsudungs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMucdichsudung(string id)
        {
            var mucdichsudung = await _context.Mucdichsudungs.FindAsync(id);
            if (mucdichsudung == null)
            {
                return NotFound();
            }

            _context.Mucdichsudungs.Remove(mucdichsudung);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MucdichsudungExists(string id)
        {
            return _context.Mucdichsudungs.Any(e => e.MaMd == id);
        }
    }
}
