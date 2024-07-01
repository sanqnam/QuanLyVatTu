using System;
using System.Collections.Generic;

namespace QLVT_BE.Data;

public partial class HinhAnhVatTu
{
    public int IdHinhAnh { get; set; }

    public string Image { get; set; } = null!;

    public int IdVatTu { get; set; }

    public int? IdUser { get; set; }

    public int? IdPhieuDeNghiNhapKho { get; set; }

    public int? IdChiTietVatTu { get; set; }

    public virtual DeNghiNhapKho? DeNghiNhapKho { get; set; }

    public virtual ChiTietVatTu? IdChiTietVatTuNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual VatTu IdVatTuNavigation { get; set; } = null!;
}
