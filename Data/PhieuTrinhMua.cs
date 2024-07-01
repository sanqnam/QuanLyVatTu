using System;
using System.Collections.Generic;

namespace QLVT_BE.Data;

public partial class PhieuTrinhMua
{
    public int IdPhieuDeNghiMua { get; set; }

    public int IdUser { get; set; }

    public int? SoLanSuaPhieu { get; set; }

    public string? LyDoSuaPhieu { get; set; }

    public double TongTien { get; set; }

    public DateTime TimeTaoPhieu { get; set; }

    public int IdThuTruong { get; set; }

    public int IdLanhDao { get; set; }

    public int IdTinhTrangPhieu { get; set; }

    public int? IdPhongBan { get; set; }

    public string? LyDo { get; set; }

    public bool? IsNhap { get; set; }

    public virtual ICollection<ChiTietPhieu> ChiTietPhieus { get; set; } = new List<ChiTietPhieu>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual User IdLanhDaoNavigation { get; set; } = null!;

    public virtual PhongBan? IdPhongBanNavigation { get; set; }

    public virtual User IdThuTruongNavigation { get; set; } = null!;

    public virtual TinhTrangPhieu IdTinhTrangPhieuNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
