using QLVT_BE.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLVT_BE.ViewModels
{
    public class UserVM:UserMD
    {
        public int IdUser { get; set; }
        public PhongBan PhongBan { get; set; }
        public ChucVu ChucVu { get; set; }

    }
    public class UserMD
    {       
        public string Username { get; set; }

        public string HoTen { get; set; }

        public string Email { get; set; }

        public string? DiaChi { get; set; }

        public string? DienThoai { get; set; }

        public string? HinhDaiDien { get; set; }
        public int IdChucVu { get; set; }
        public int IdPhongBan { get; set; }
        public string? MaPhongBan { get; set; }
        public string? MaChucVu { get; set; }
        public string? TenPhongBan { get; set; }
        public string? TenChucVu { get; set; }
        public string? AboutMe { get; set; }
        public int? isActive {  get; set; }
        public string? ConnectionId { get; set; }
    }
}
