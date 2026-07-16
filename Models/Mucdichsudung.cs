using System;
using System.Collections.Generic;

namespace DOANCNPM.Models;

public partial class Mucdichsudung
{
    public string MaMd { get; set; } = null!;

    public string? TenMd { get; set; }

    public string? MoTaMd { get; set; }

    public virtual ICollection<Sanpham> Sanphams { get; set; } = new List<Sanpham>();
}
