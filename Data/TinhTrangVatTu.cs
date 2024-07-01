using System;
using System.Collections.Generic;

namespace QLVT_BE.Data;

public partial class TinhTrangVatTu
{
    public int IdTinhTrang { get; set; }

    public string? TenTinhTrang { get; set; }

    public virtual ICollection<ChiTietVatTu> ChiTietVatTus { get; set; } = new List<ChiTietVatTu>();
}
