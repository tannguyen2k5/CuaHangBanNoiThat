using System;
using System.Collections.Generic;

namespace DOANCNPM.Models;

public partial class Nhanvien
{
    public string MaNv { get; set; } = null!;

    public string? TenDangNhap { get; set; }

    public string? MatKhau { get; set; }

    public string? TenNv { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string? GioiTinh { get; set; }

    public string? SoDt { get; set; }

    public string? DiaChiNv { get; set; }

    public string? VaiTroKhuVucPhuTrach { get; set; }

    public string? TrangThaiLamViec { get; set; }

    public int? TrangThai { get; set; }

    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();
}
