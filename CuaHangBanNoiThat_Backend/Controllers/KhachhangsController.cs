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
    public class KhachhangsController : ControllerBase
    {
        // TODO (SCRUM 11): TÂN - Cập nhật logic API Quản lý thông tin "Khách hàng".
        // Yêu cầu bắt buộc: Đảm bảo lưu đúng Tên, SĐT, Địa chỉ của khách hàng.
        // Phân quyền (SCRUM 15): Cho phép quyền Staff (Nhân viên) được thực hiện các thao tác trên file này.
        private readonly ChbntContext _context;

        public KhachhangsController(ChbntContext context)
        {
            _context = context;
        }

        // GET: api/Khachhangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Khachhang>>> GetKhachhangs()
        {
            return await _context.Khachhangs.ToListAsync();
        }

        // GET: api/Khachhangs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Khachhang>> GetKhachhang(string id)
        {
            var khachhang = await _context.Khachhangs.FindAsync(id);

            if (khachhang == null)
            {
                return NotFound();
            }

            return khachhang;
        }

        // PUT: api/Khachhangs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhachhang(string id, Khachhang khachhang)
        {
            if (id != khachhang.MaKhachHang)
            {
                return BadRequest();
            }

            _context.Entry(khachhang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachhangExists(id))
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

        // POST: api/Khachhangs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Khachhang>> PostKhachhang(Khachhang khachhang)
        {
            _context.Khachhangs.Add(khachhang);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KhachhangExists(khachhang.MaKhachHang))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKhachhang", new { id = khachhang.MaKhachHang }, khachhang);
        }

        // DELETE: api/Khachhangs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachhang(string id)
        {
            var khachhang = await _context.Khachhangs.FindAsync(id);
            if (khachhang == null)
            {
                return NotFound();
            }

            _context.Khachhangs.Remove(khachhang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhachhangExists(string id)
        {
            return _context.Khachhangs.Any(e => e.MaKhachHang == id);
        }
    }
}
