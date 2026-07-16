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

        // --- API TẠO HÓA ĐƠN TỰ ĐỘNG (Đã tích hợp thuật toán tính tiền và trừ kho) ---
        [HttpPost("TaoHoaDonTuDong")]
        [Authorize] // Đã đăng nhập là dùng được (Manager hay Staff đều ok)
        public async Task<IActionResult> TaoHoaDonTuDong([FromBody] TaoHoaDonRequest request)
        {
            // 1. Tự động sinh Mã Hóa Đơn ngắn gọn (Ví dụ: HD4512) để không bị quá tải Database
            string maHoaDonMoi = "HD" + new Random().Next(1000, 9999).ToString();

            var hoadon = new Hoadon
            {
                MaHd = maHoaDonMoi,
                MaNv = request.MaNv,
                MaKhachHang = request.MaKhachHang,
                NgayLapHd = DateTime.Now,
                TrangThaiGiaoHang = "Chờ xử lý"
            };
            _context.Hoadons.Add(hoadon);

            // 2. Xử lý giỏ hàng: Tự động tính tiền và trừ Tồn kho
            decimal tongTienToanBoHoaDon = 0;

            foreach (var item in request.DanhSachSanPham)
            {
                // Vào DB tìm đúng sản phẩm để lấy Giá bán chính xác nhất
                var sanpham = await _context.Sanphams.FindAsync(item.MaSp);
                if (sanpham == null)
                {
                    return BadRequest($"Không tìm thấy sản phẩm có mã: {item.MaSp}");
                }
                if (sanpham.SoLuongTon < item.SoLuongMua)
                {
                    return BadRequest($"Sản phẩm {sanpham.TenSp} chỉ còn {sanpham.SoLuongTon} cái trong kho, không đủ bán!");
                }

                // Tự động tính tiền
                decimal donGia = sanpham.GiaBan ?? 0;
                decimal thanhTien = (donGia * item.SoLuongMua) - item.GiamGia;
                tongTienToanBoHoaDon += thanhTien;

                // Lưu Chi tiết hóa đơn với mã ngắn gọn (Ví dụ: CT89123)
                var chiTiet = new Chitiethoadon
                {
                    MaHd = maHoaDonMoi,
                    MaChiTietHd = "CT" + new Random().Next(10000, 99999).ToString(),
                    MaSp = item.MaSp,
                    SoLuongBan = item.SoLuongMua,
                    DonGiaBan = donGia,
                    ThanhTien = thanhTien,
                    GiamGia = item.GiamGia
                };
                _context.Chitiethoadons.Add(chiTiet);

                // Trừ kho
                sanpham.SoLuongTon -= item.SoLuongMua;
                _context.Sanphams.Update(sanpham);
            }

            // 3. Đẩy toàn bộ dữ liệu xuống SQL Server trong 1 nhịp
            await _context.SaveChangesAsync();

            // 4. Trả về hóa đơn cho Frontend hiển thị
            return Ok(new
            {
                Message = "Tạo hóa đơn thành công rực rỡ!",
                MaHoaDon = maHoaDonMoi,
                TongTienPhaiThu = tongTienToanBoHoaDon
            });
        }

        // --- API POST MẶC ĐỊNH CỦA VISUAL STUDIO (Đã thêm lại nhãn [HttpPost] để không bị lỗi Swagger) ---
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

        // --- CLASS HỖ TRỢ ĐỂ HỨNG DỮ LIỆU TỪ SWAGGER / FRONTEND ---
        public class TaoHoaDonRequest
        {
            public string MaNv { get; set; } = string.Empty;
            public string MaKhachHang { get; set; } = string.Empty;
            public List<ChiTietDatHang> DanhSachSanPham { get; set; } = new List<ChiTietDatHang>();
        }

        public class ChiTietDatHang
        {
            public string MaSp { get; set; } = string.Empty;
            public int SoLuongMua { get; set; }
            public decimal GiamGia { get; set; }
        }
    }
}