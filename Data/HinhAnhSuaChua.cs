using System;
using System.Collections.Generic;

namespace QLVT_BE.Data;

public partial class HinhAnhSuaChua
{
    public int IdHinhSuaChua { get; set; }

    public int? IdChiTietSuaChua { get; set; }

    public string? Url { get; set; }

    public virtual ChiTietPhieuSuaChua? IdChiTietSuaChuaNavigation { get; set; }
}
