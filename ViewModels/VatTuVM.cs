using QLVT_BE.Data;

namespace QLVT_BE.ViewModels
{
    public class VatTuMD : VatTuVM
    {
 
        public int IdVatTu { get; set; }
    }
    public class VatTuVM
    {
        public int? Status { get; set; } 
        public string? TenVatTu { get; set; }

        public string? MaVatTu { get; set; }

        public string DonViTinh { get; set; }

        public double? SoLuongTonKho { get; set; }

        public int? IdKho { get; set; }

        public string? ViTri { get; set; }

        public string? GhiChu { get; set; }

        public string? ThongSo {  get; set; }

        public virtual ICollection<HinhAnhVatTu> HinhAnhVatTus { get; set; } = new List<HinhAnhVatTu>();
    }
    public class VatTuSuDungMD : VatTuSuDungVM
    {
        public int IdChiTietVatTu { get; set; }
       

    }
    public class VatTuSuDungVM
    {
        public string TenVatTu { get; set; }
        public string NguoiDung { get; set; }   

        public string MaVatTu { get; set; }
        public DateTime NgayCap { get; set; }
        public string TinhTrang { get; set; }
        public string? LichSu { get;set; }
        public DateTime? TimeCap { get; set; }  
    }
    public class PhieuCapVatTuMD: PhieuCapVatTuVM
    {
        public int IdPhieuDeNghi { get; set; }
      
    }
    public class PhieuCapVatTuVM
    {
        public string HoVaTen { get; set; }
        public string TenPhongBan { get; set; }
        public int IdUser { get;set; }
        public string TinhTrangPhieu { get; set; }  
    }
    public class ChiTietPhieuCapMD: ChiTietPhieuCapMV
    {
        public int IdChiTietPhieu { get; set; }
    }
    public class ChiTietPhieuCapMV
    {
        public int? Status { get; set; }    
        public string TenVatTu { get; set; }
        public string MaVatTu { get; set; }
        public int IdVatTu { get;set;}
        public int IdUser { get;set; }
        public double? SoLuongYeuCau { get; set; }
        public double? SoLuongTon { get; set; }
        public string? TinhTrangXuLy { get; set; }
    }
    public class AllVatTuYeuCauVM
    {
        public string TenVatTu { get; set; }
        public string MaVatTu { get; set; }
        public int IdVatTu { get; set; }
        public int IdUser { get; set; }
        public double? SoLuongYeuCau { get; set; }
        public string? TinhTrangXuLy { get; set; }
    }
}
