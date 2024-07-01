using System;
using System.Collections.Generic;

namespace QLVT_BE.Data;

public partial class PhieuDeNghiSuaChua
{
    public int IdPhieuSuaChua { get; set; }

    public string? MaPhieuDeNghi { get; set; }

    public string? LyDo { get; set; }

    public int IdUser { get; set; }

    public int? IdPhongBan { get; set; }

    public int IdThuTruong { get; set; }

    public DateTime? NgayTaoPhieu { get; set; }

    public int IdPhongNhan { get; set; }

    public int? IdTinhTrangPhieu { get; set; }

    public string? LyDoTraPhieu { get; set; }

    public DateTime? NgayDuyetPhieu { get; set; }

    public virtual ICollection<ChiTietPhieuSuaChua> ChiTietPhieuSuaChuas { get; set; } = new List<ChiTietPhieuSuaChua>();

    public virtual PhongBan IdPhongNhanNavigation { get; set; } = null!;

    public virtual TinhTrangPhieu? IdTinhTrangPhieuNavigation { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
