using QLVT_BE.Data;

namespace QLVT_BE.ViewModels
{
    public class TaoPhieuSuaVM 
    {
        public string? LyDo { get; set; }

        public int IdUser { get; set; }

        public int IdPhongBan { get; set; }

        public int IdThuTruongNhan { get; set; }
        public int IdPhongNhan { get; set; }


        public virtual ICollection<ChiTietPhieuSuaVM>? ChiTietPhieuSuas { get; set; } = new List<ChiTietPhieuSuaVM>();

    }
    public class ChiTietPhieuSuaVM
    {

        public int? IdChiTietVatTu { get; set; }

        public string? MoTaTinhTrang { get; set; }  

        public List<string>? Url { get; set; }
    }
    public class PhieuSuaMD : PhieuSuaVM
    {
        public int IdPhieuSuaChua { get; set; }
    }
    public class PhieuSuaVM
    {
        public string? LyDo { get; set; }

        public string HoTen { get;set; }
        public string PhongBanNhan { get; set; }
        public int IdPhongBanNhan { get; set;}
        public string PhongBanYeuCau { get;set; }
        public DateTime? TimeTao { get; set; }
        public string TinhTrangPhieu { get; set; }
       
    }
    public class VatTuSuaMD : VatTuSuaVM
    {
        public int IdChiTietPhieuSua {get; set; }
    }
    public class VatTuSuaVM
    {
        public string TenVatTu { get; set; }
        public string MaVatTu { get; set; }
        public string MoTaTinhTrang { get; set; }
        public string? NguoiThucHien { get; set; }
        public virtual ICollection<HinhAnhSuaChua> HinhAnhSuas { get; set; } = new List<HinhAnhSuaChua>();

    }
    public class HinhAnhSuaVM 
    {
        public int IdHinhAnhSua { get; set; }
        public string Url { get; set; }
    }

    public class ShowPhieuChoNhanVienVM : ShowPhieuChoNhanVienMD
    {
        public int idChiTietPhieuSua { get; set; }
        public int IdPhieuSuaChua { get; set; }

    }
    public class ShowPhieuChoNhanVienMD
    {
        public string TenVatTu { get; set; }
        public string? NguoiYeuCau { get; set; }
        public string PhongBanYeuCau { get; set; }

        public string MoTaTinhTrang { get; set; }

        public string? TrangThaiXuLy { get; set; }
    }


}
