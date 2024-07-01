using QLVT_BE.Data;

namespace QLVT_BE.ViewModels
{
    public class NguoiMuaMD
    {
        public int idChiTietPhieu { get; set; }
        public string TenVatTu { get; set; }
        public int IdVatTu { get; set; }
        public string MaVatTu { get; set; }
        public string DonViTinh { get; set; }
        public double SoLuongCanMua { get; set; }
        public string? GhiChuCanMua { get; set; }
        public float? SoLuongTongCanMua { get; set; }
    }
    public class TaoPhieuMuaMD
    {
        public int IdUser { get; set; }
        public int idPhongBan { get; set; }

        public int IdThuTruong { get; set; }

        public int IdLanhDao { get; set; }
        public virtual User? IdUserNavigation { get; set; } = null!;
        public virtual ICollection<PhieuMuaChiTietMD> ChiTietPhieus { get; set; } = new List<PhieuMuaChiTietMD>();
    }
    public class PhieuMuaChiTietMD
    {
        public string TenVatTu { get; set; }
        public int? IdVatTu { get; set; }
        public string? MaVatTu { get; set; }
        public string? DonViTinhDeNghi { get; set; }
        public string? DonViCungCap { get; set; }
        public float? SoLuongMuaThem { get; set; }
        public int? DonGia { get; set; }
        public string? VAT { get; set; }
        public int IdChiTietPhieu { get; set; }
    }
    public class ChiTietPhieuMuaVM : PhieuMuaChiTietMD
    {
        public string? DonViTinh { get; set; }
        public double? Soluong { get; set; }
    }


    public class PhieuTrinhMuaMD
    {
        public int IdPhieuTrinhMua { get; set; }

        public string TenNguoiMua { get; set; }

        public int? SoLanSuaPhieu { get; set; }

        public string? LyDoSuaPhieu { get; set; }

        public double TongTien { get; set; }

        public DateTime TimeTaoPhieu { get; set; }

        public string TinhTrangPhieu { get; set; }

    }
    public class DuyetPhieu
    {
        public int IdPhieu { get; set; }
        public int idUser { get; set; }
        public string? LyDoKhongDuyet { get; set; }
        public string MaBiMat { get; set; }
        public string? role { get; set; }

    }
    public class SuaPhieuMuaMD 
    { 
        public int idPhieu { get; set; }    
        public int idUser { get;set; }
        public string LyDoSuaPhieu { get;set; }
        public virtual ICollection<SuaPhieuMuaChiTietMD> ChiTietPhieus { get; set; } = new List<SuaPhieuMuaChiTietMD>();

    }
    public class SuaPhieuMuaChiTietMD
    {
        public int IdChiTietPhieu { get; set; }
        public int? DonGia { get;set; }
        public string? Vat {  get; set; } 
        public string? DonViCungCap { get; set; }
        public int ? SoLuongMuaThem { get; set; }    
    }
    public class PhieuMuaBiTraMD : PhieuTrinhMuaMD
    {
        public string LyDoTraPhieu { get; set; }
        
    }
}
