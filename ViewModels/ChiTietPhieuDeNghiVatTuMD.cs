using QLVT_BE.Data;

namespace QLVT_BE.ViewModels
{
    public class ChiTietPhieuDeNghiVatTuMD : ChiTietPhieuDeNghiVatTuVM
    {
        public int IdPhieuDeNghi { get; set; }
    }
    public class ChiTietPhieuDeNghiVatTuVM 
    {
        public int IdChiTietPhieu { get; set; }
        public string TenVatTu { get; set; }
        public int IdVatTu { get;set; }
        public string? MaVatTu { get; set; }
        public double SoLuongDeNghi { get; set; }
        public string? DonViTinhDeNghi { get; set; }
        public double? SoLuongThayDoi { get; set; }
        public string? GhiChuNguoiDung { get; set; }
        public string? GhiChuDuyetPhieu { get; set; }

        public int IdTinhTrangXuLy { get; set; }

        public string? TenTinhTrangXuLy { get; set; } = null!;
  


    }
}
