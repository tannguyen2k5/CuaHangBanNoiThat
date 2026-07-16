using System;
using System.Collections.Generic;

namespace DOANCNPM.Models;

public partial class Khachhang
{
    public string MaKhachHang { get; set; } = null!;

    public string? TenDangNhap { get; set; }

    public string? MatKhau { get; set; }

    public string? TenKhachHang { get; set; }

    public string? SdtkhachHang { get; set; }

    public string? DiaChiKhachHang { get; set; }

    public int? TrangThai { get; set; }

    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();
}
