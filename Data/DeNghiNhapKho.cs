using System;
using System.Collections.Generic;

namespace QLVT_BE.Data;

public partial class DeNghiNhapKho
{
    public int IdPhieuNhapKho { get; set; }

    public string? MaPhieuDenghi { get; set; }

    public int? IdChiTietPhieu { get; set; }

    public double? SoLuong { get; set; }

    public int? IdPhieuDeNghiMua { get; set; }

    public DateTime? NgayTaoPhieu { get; set; }

    public int? NguoiTaoPhieu { get; set; }

    public int? IdThuKho { get; set; }

    public int? NguoiKiemPhieu { get; set; }

    public string? TinhTrang { get; set; }

    public virtual ICollection<ChiTietVatTu> ChiTietVatTus { get; set; } = new List<ChiTietVatTu>();

    public virtual ChiTietPhieu? IdChiTietPhieuNavigation { get; set; }

    public virtual HinhAnhVatTu IdPhieuNhapKhoNavigation { get; set; } = null!;

    public virtual User? IdThuKhoNavigation { get; set; }

    public virtual User? NguoiKiemPhieuNavigation { get; set; }

    public virtual User? NguoiTaoPhieuNavigation { get; set; }
}
