using QLVT_BE.Data;

namespace QLVT_BE.ViewModels
{
    public class CTPhieuDNghiVM
    {
        public int IdPhieuDeNghi { get; set; }

        public int IdTinhTrangXuLy { get; set; }

        public int IdVatTu { get; set; }

        public double SoLuongDeNghi { get; set; }

        public string DonViTinhDeNghi { get; set; }

        public string GhiChuNguoiDung { get; set; }

        public double? SoLuongThayDoi { get; set; }

        public string? DonViTinhThayDoi { get; set; }

        public double? SoLuongMuaThem { get; set; }

        public string? GiChuThuKho { get; set; }

        public string? GiChuMuaThem { get; set; }

        public string? GiChuXuatVatTu { get; set; }

        public int? DonGia { get; set; }

        public string? Vat { get; set; }

        public int? ThanhTien { get; set; }

        public int? IdPhieuDeNghiMua { get; set; }

        public string? DonViCungCap { get; set; }

        public int? IdHoaDon { get; set; }

        public string? GhiChuSuaPhieuMua { get; set; }

        public string? GhiChuSuaTheoHoaDon { get; set; }

        public int? SoLanSuaPhieuMua { get; set; }

        public virtual ICollection<DeNghiNhapKho> DeNghiNhapKhos { get; set; } = new List<DeNghiNhapKho>();

        public virtual HoaDon? IdHoaDonNavigation { get; set; }

        public virtual PhieuTrinhMua? IdPhieuDeNghiMuaNavigation { get; set; }

        public virtual PhieuDeNghiVatTu? IdPhieuDeNghiNavigation { get; set; }

        public virtual TinhTrangXuLy? IdTinhTrangXuLyNavigation { get; set; }

        public virtual VatTu? IdVatTuNavigation { get; set; }
    }
    public class CTPhieuDNghiMD: CTPhieuDNghiVM
    {
        public int IdChiTietPhieu { get; set; }
    }

    public class ChiTietPhieuTaoVM  {
        public int IdVatTu { get; set; }
        public int IdPhieuTam { get; set; }
        public int IdPhieuDeNghi { set; get; }
        public string TenVatTu { get; set; }
        public string? MaVatTu { get; set; }
        public double SoLuongDeNghi { get; set; }
        public string? DonViTinhDeNghi { get; set; }
        public string? GhiChuNguoiDung { get; set; }
        public int IdTinhTrangXuLy { get; set; }
        public string TenTinhTrangXuLy { get; set; } = null!;
        public virtual ICollection<ChiTietPhieu>? ChiTietPhieus { get; set; } = new List<ChiTietPhieu>();
    }
    public class ChiTietPhieuDuyetVM
    {
        public int IdChiTietPhieu { get; set; }
        public double? SoLuongThayDoi { get; set; }
    }
} 
