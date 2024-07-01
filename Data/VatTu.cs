using System;
using System.Collections.Generic;

namespace QLVT_BE.Data;

public partial class VatTu
{
    public int IdVatTu { get; set; }

    public string TenVatTu { get; set; } = null!;

    public string MaVatTu { get; set; } = null!;

    public string DonViTinh { get; set; } = null!;

    public double? SoLuongTonKho { get; set; }

    public int? IdKho { get; set; }

    public string? ViTri { get; set; }

    public string? GhiChu { get; set; }

    public string? ThongSo { get; set; }

    public virtual ICollection<ChiTietPhieu> ChiTietPhieus { get; set; } = new List<ChiTietPhieu>();

    public virtual ICollection<ChiTietVatTu> ChiTietVatTus { get; set; } = new List<ChiTietVatTu>();

    public virtual ICollection<HinhAnhVatTu> HinhAnhVatTus { get; set; } = new List<HinhAnhVatTu>();

    public virtual ICollection<VatTuQuanTam> VatTuQuanTams { get; set; } = new List<VatTuQuanTam>();
}
