using System;
using System.Collections.Generic;

namespace DOANCNPM.Models;

public partial class Nhomsanpham
{
    public string MaNhomSp { get; set; } = null!;

    public string? TenNhomSp { get; set; }

    public int? TrangThai { get; set; }

    public virtual ICollection<Sanpham> Sanphams { get; set; } = new List<Sanpham>();
}
