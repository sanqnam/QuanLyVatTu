using System;
using System.Collections.Generic;

namespace QLVT_BE.Data;

public partial class ChiTietVatTu
{
    public int IdChiTietVatTu { get; set; }

    public string? MaVatTu { get; set; }

    public int? IdVatTu { get; set; }

    public int? IdDeNghiNhapKho { get; set; }

    public int? IdTinhTrang { get; set; }

    public string? LichSu { get; set; }

    public int? IdUser { get; set; }

    public virtual ICollection<ChiTietPhieuSuaChua> ChiTietPhieuSuaChuas { get; set; } = new List<ChiTietPhieuSuaChua>();

    public virtual ICollection<HinhAnhVatTu> HinhAnhVatTus { get; set; } = new List<HinhAnhVatTu>();

    public virtual DeNghiNhapKho? IdDeNghiNhapKhoNavigation { get; set; }

    public virtual TinhTrangVatTu? IdTinhTrangNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual VatTu? IdVatTuNavigation { get; set; }
}
