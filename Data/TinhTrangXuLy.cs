using System;
using System.Collections.Generic;

namespace QLVT_BE.Data;

public partial class TinhTrangXuLy
{
    public int IdTinhTrangXuLy { get; set; }

    public string TenTinhTrangXuLy { get; set; } = null!;

    public virtual ICollection<ChiTietPhieu> ChiTietPhieus { get; set; } = new List<ChiTietPhieu>();
}
