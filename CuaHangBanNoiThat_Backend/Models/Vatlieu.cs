using System;
using System.Collections.Generic;

namespace DOANCNPM.Models;

public partial class Vatlieu
{
    public string MaVl { get; set; } = null!;

    public string? TenVl { get; set; }

    public virtual ICollection<Sanpham> MaSps { get; set; } = new List<Sanpham>();
}
