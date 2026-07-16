using System;
using System.Collections.Generic;

namespace DOANCNPM.Models;

public partial class Sanpham
{
    public string MaSp { get; set; } = null!;

    public string MaMd { get; set; } = null!;

    public string MaNhomSp { get; set; } = null!;

    public string? TenSp { get; set; }

    public string? DonViTinh { get; set; }

    public int? SoLuongTon { get; set; }

    public decimal? GiaBan { get; set; }

    public string? MoTa { get; set; }

    public string? HinhAnh { get; set; }

    public int? TrangThai { get; set; }

    public virtual ICollection<Chitiethoadon> Chitiethoadons { get; set; } = new List<Chitiethoadon>();

    public virtual Mucdichsudung MaMdNavigation { get; set; } = null!;

    public virtual Nhomsanpham MaNhomSpNavigation { get; set; } = null!;

    public virtual ICollection<Nhacungcap> MaNccs { get; set; } = new List<Nhacungcap>();

    public virtual ICollection<Vatlieu> MaVls { get; set; } = new List<Vatlieu>();
}
