namespace QLVT_BE.ViewModels
{
    public class HoaDonMD
    {
        public string TenHoaDon { get; set; }
        public double TongTien {  get; set; }
        public DateTime TimeTao { get; set; }
        public virtual ICollection<ChiTietHoaDonDM> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDonDM>();
    }
    public class ChiTietHoaDonDM
    {
        public string TenVatTu { get; set;}
        public double? SoLuong { get; set; }
        public int? DonGia {  get; set; } 
        public string Vat {  get; set; }    
        public string DonViTinh { get; set; }
        public string DonViCungCap { get; set; }
    }
}
