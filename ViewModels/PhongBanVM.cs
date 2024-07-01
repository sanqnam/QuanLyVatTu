using QLVT_BE.Data;

namespace QLVT_BE.ViewModels
{
    public class PhongBanVM : PhongBanMD
    {
        public int IdPhongBan { get; set; }

    }
    public class PhongBanMD
    {
        public string TenPhongBan { get; set; }
        public string MaPhongBan { get; set; }
    }
}
