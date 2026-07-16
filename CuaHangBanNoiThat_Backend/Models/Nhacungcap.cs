using System;
using System.Collections.Generic;

namespace DOANCNPM.Models;

public partial class Nhacungcap
{
    // TODO (SCRUM 9): TÂN - Kiểm tra và bổ sung các Data Annotations (như [Required], [MaxLength]) cho các cột dữ liệu nhà cung cấp nếu cần.
    public string MaNcc { get; set; } = null!;

    public string? TenNcc { get; set; }

    public string? MoTaNcc { get; set; }

    public virtual ICollection<Sanpham> MaSps { get; set; } = new List<Sanpham>();
}
