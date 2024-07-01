using QLVT_BE.Data;

namespace QLVT_BE.ViewModels
{
    public class PhieuDeNghiVatTuVM
    {
        public int IdPhieuTam { get; set; }

        public int IdPhieuChinhThuc { get; set; }

        public string LyDoLapPhieu { get; set; } = null!;

        public int IdUser { get; set; }
        public string? HoVaTen { get; set; }
        public string? SoPhieu { get;set; }


        public int? IdPhongBan { get; set; }
        public string? TenPhongBan { get; set; }

        public int IdThuTruong { get; set; }
        public string? TenThuTruong { get; set; }


        public int IdLanhDao { get; set; }
        public string? TenLanhDao { get; set; }  

        public DateTime? TimeTaoPhieu { get; set; }

        public DateTime? TimeDuyetPhieu { get; set; }

        public string? LyDoTraPhieu { get; set; }

        public string? NguoiTraPhieu { get; set; }

        public int IdTinhTrangPhieu { get; set; }
        public string? TinhTrangPhieu { get; set; }

        public virtual ICollection<ChiTietPhieuDeNghiVatTuVM> ChiTietPhieus { get; set; } = new List<ChiTietPhieuDeNghiVatTuVM>();


        public virtual User? IdThuTruongNavigation { get; set; } 

        public virtual TinhTrangPhieu? IdTinhTrangPhieuNavigation { get; set; }

        public virtual User? IdUserNavigation { get; set; }

    }
    public class PhieuDeNghiVatTuMD : PhieuDeNghiVatTuVM
    {
       
        public int IdPhieuDeNghi { get; set; }

    }
    public class TaoPhieuVM : PhieuDeNghiVatTuMD {
        public int? Status { get; set; }
        public int IdPhieuDeNghi { get; set; }

        public int IdPhieuTam { get; set; }

        public int IdPhieuChinhThuc { get; set; }

        public string LyDoLapPhieu { get; set; } = null!;

        public int IdUser { get; set; }

        public int? IdPhongBan { get; set; }

        public int IdThuTruong { get; set; }

        public int IdLanhDao { get; set; }

        public DateTime TimeTaoPhieu { get; set; }
        public virtual User? IdUserNavigation { get; set; }

    }

    public class PhieuDuyetTruongPhongVM
    {
        public int IdPhieuDeNghi { get; set; }
        public string? LyDoTraPhieu { get; set; }
        public string? MaBiMat { get; set; }
        public int IdPhongBan { get; set; }
        public virtual ICollection<ChiTietPhieuDuyetVM> ChiTietPhieus { get; set; } = new List<ChiTietPhieuDuyetVM>();
    }
    public class TaoPhieuMD
    {
        public int IdUser { get; set; }
        public int IdPhongBan { get;set; }
        public int IdThuTruong { get; set; }
        public int IdLanhDao { get;set; }   
        public string LyDoLapPhieu { get; set; }
        public virtual ICollection<PhieuTaoChiTietMD> ChiTietPhieus { get; set; } = new List<PhieuTaoChiTietMD>();
    }
    public class PhieuTaoChiTietMD
    {
        public int IdVatTu { get; set; }
        public string TenVatTu { get; set; }
        public string MaVatTu { get; set;}
        public double SoLuongDeNghi { get; set;}
        public string GhiChuNguoiDung { get; set; }
        
        public string DonViTinhDeNghi { get; set; }
    }
}
