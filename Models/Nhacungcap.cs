using System;
using System.Collections.Generic;

namespace DOANCNPM.Models;

public partial class Nhacungcap
{
    public string MaNcc { get; set; } = null!;

    public string? TenNcc { get; set; }

    public string? MoTaNcc { get; set; }

    public virtual ICollection<Sanpham> MaSps { get; set; } = new List<Sanpham>();
}
