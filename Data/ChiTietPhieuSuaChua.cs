using System;
using System.Collections.Generic;

namespace QLVT_BE.Data;

public partial class ChiTietPhieuSuaChua
{
    public int IdPhieuSuaChua { get; set; }

    public int IdChiTietSuaChua { get; set; }

    public int? IdChiTietVatTu { get; set; }

    public string? MoTaTinhTrang { get; set; }

    public int? NguoiThucHien { get; set; }

    public int? PhongBanThucHien { get; set; }

    public string? GhiChuThucHien { get; set; }

    public int? SuaChuaNgooai { get; set; }

    public DateTime? NgayDuaRaNgoai { get; set; }

    public int? DeNghiThanhLy { get; set; }

    public DateTime? NgayNhanLai { get; set; }

    public string? TrangThaiXuLy { get; set; }

    public virtual ICollection<HinhAnhSuaChua> HinhAnhSuaChuas { get; set; } = new List<HinhAnhSuaChua>();

    public virtual ChiTietVatTu? IdChiTietVatTuNavigation { get; set; }

    public virtual PhieuDeNghiSuaChua IdPhieuSuaChuaNavigation { get; set; } = null!;

    public virtual User? NguoiThucHienNavigation { get; set; }

    public virtual PhongBan? PhongBanThucHienNavigation { get; set; }
}
