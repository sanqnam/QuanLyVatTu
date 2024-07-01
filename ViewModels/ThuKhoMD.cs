using System.Runtime.CompilerServices;

namespace QLVT_BE.ViewModels
{
    public class ThuKhoMD
    {
    }
    public class ChiTietMuaMD : ChiTietMuaVM
    {
        public int IdChiTietPhieu { get; set; }
    }
    public class ChiTietMuaVM
    {
        public double? SoLuongChoMua { get; set; }
        public string? TenVatTu { get; set; }
        public string? MaVatTu { get; set; }
        public double SoLuongDeNghi { get; set; }
        public double SoLuongTonKho { get; set; }
        public double SoLuongCanMua { get; set; }
    }


    public class PhieuYeuCauMuaVM
    {
        public int IdChiTietPhieu { get; set; }
        public double SoLuongMuaThem { get; set; }
        public string? GiChuMuaThem { get; set; }
    }
    public class NhapKhoMD
    {
        public int IdPhieu { get; set; }
        public virtual ICollection<NhapKhoChiTietMD> ChiTietNhaps { get; set; } = new List<NhapKhoChiTietMD>();
    }
    public class NhapKhoChiTietMD
    {
        public int IdVatTu { get; set; }
        public double? SoLuong {  get; set; }   

    }
}
