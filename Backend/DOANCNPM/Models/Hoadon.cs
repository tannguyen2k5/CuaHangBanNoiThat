using System;
using System.Collections.Generic;

namespace DOANCNPM.Models;

public partial class Hoadon
{
    public string MaHd { get; set; } = null!;

    public string MaNv { get; set; } = null!;

    public string MaKhachHang { get; set; } = null!;

    public DateTime? NgayLapHd { get; set; }

    public DateTime? NgayGiaoHang { get; set; }

    public string? TrangThaiGiaoHang { get; set; }

    public virtual ICollection<Chitiethoadon> Chitiethoadons { get; set; } = new List<Chitiethoadon>();

    public virtual Khachhang MaKhachHangNavigation { get; set; } = null!;

    public virtual Nhanvien MaNvNavigation { get; set; } = null!;
}
