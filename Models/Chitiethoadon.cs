using System;
using System.Collections.Generic;

namespace DOANCNPM.Models;

public partial class Chitiethoadon
{
    public string MaHd { get; set; } = null!;

    public string MaChiTietHd { get; set; } = null!;

    public string MaSp { get; set; } = null!;

    public int? SoLuongBan { get; set; }

    public decimal? DonGiaBan { get; set; }

    public decimal? ThanhTien { get; set; }

    public decimal? GiamGia { get; set; }

    public virtual Hoadon MaHdNavigation { get; set; } = null!;

    public virtual Sanpham MaSpNavigation { get; set; } = null!;
}
