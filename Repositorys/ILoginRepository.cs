using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QLVT_BE.Data;
using QLVT_BE.Helpers;
using QLVT_BE.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QLVT_BE.Repositorys
{
    public interface ILoginRepository
    {
        string Login(LoginVM login);
        UserVM GetName(LoginVM login);
    }

    public class LoginRepository : ILoginRepository
    {
        private readonly QuanLyVatTuContext _context;
        private readonly IConfiguration _config;

        public LoginRepository(QuanLyVatTuContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public UserVM GetName(LoginVM login)
        {
            string MatKhau = CreateMD5.GetMD5(login.MatKhau);
            var user = _context.Users.Include(l => l.IdChucVuNavigation).Include(l => l.IdPhongBanNavigation)
                .SingleOrDefault(l => l.Username == login.Username && l.MatKhau == MatKhau);
            return new UserVM
            {
                IdUser = user.IdUser,
                Username = user.Username,
                HoTen = user.HoTen,
                MaChucVu = user.IdChucVuNavigation.MaChuVu,
                MaPhongBan = user.IdPhongBanNavigation.MaPhongBan,
                DiaChi = user.DiaChi,
                Email = user.Email,
                DienThoai = user.DienThoai,
                HinhDaiDien = user.HinhDaiDien,
                IdChucVu = user.IdChucVu,
                IdPhongBan = user.IdPhongBan,
            };
        }
        public string Login(LoginVM login)
        {
            string MatKhau = CreateMD5.GetMD5(login.MatKhau);
            var user = _context.Users.Include(l => l.IdChucVuNavigation).Include(l => l.IdPhongBanNavigation)
                .SingleOrDefault(l => l.Username == login.Username && l.MatKhau == MatKhau);
            //var us = _context.Entry(user);
            if (user != null && user.IsActive == 1)
            {                
                string roles = user.IdChucVuNavigation.MaChuVu;
                string phongBan = user.IdPhongBanNavigation.MaPhongBan;

                // kiểm tra user có tồn tại thì tạo token (Jwt) để đăng nhập
                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(_config["Jwt:SecretKey"]); // chuyển sang dạng byte để phục vụ mã hóa

                var tokenDesciptor = new SecurityTokenDescriptor // truyền vào các giá trị để mã hóa
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, roles),
                        new Claim("PhongBan", phongBan),
                    }),
                    Expires = DateTime.Now.AddMinutes(45), // khai báo thời gian sống của token
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                    // tạo chữ ký trên token
                };
                var token = tokenHandler.CreateToken(tokenDesciptor); // tạo token
                var jwtToken = tokenHandler.WriteToken(token); // đọc token
                return jwtToken;
            }
            else if(user != null && user.IsActive != 1)
            {
                return "2";
            }
            else 
            {
                return "1";
            }
        }
    }
}
