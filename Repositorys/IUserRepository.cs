using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QLVT_BE.Data;
using QLVT_BE.Helpers;
using QLVT_BE.Providers;
using QLVT_BE.ViewModels;
using System.ComponentModel;
using System.Security.Claims;

namespace QLVT_BE.Repositorys
{
    public interface IUserRepository
    {
        JsonResult GetAll(int pg, int size, int phongBan, int chucVu);
        JsonResult Search(int pg, int size, string search);
        UserMD GetById(int id);
        List<UserVM> GetByPhongBan (int idPhongBan);
       // string GetRole();
        void UpdateUser(int id, UserMD userMD);
        int AddUser(UserMD userMD);
        int ActiveUser(int id);
        void ResetPass(int id);
        void SetConnectionId(int id, string connectionId);
        void ChangePass(int id, ChangePassMD changePass);
        List<UserVM> GetTruongPhong(int id);
        UserVM GetUserByChucVu(string chucVu);
        UserVM GetUserByPhongBan(string maPB, string chucVu);
        JsonResult DoiMaBiMat(int id, ChangeMaBiMatMD maBiMat);
        List<UserVM> GetUserSuaByPhongBan(int id);
    }
    public class UserRepository : IUserRepository
    {
        private readonly QuanLyVatTuContext _context;
        private readonly IConfiguration _config;
        private readonly IPhieuSuaProviders _phieuPR;
        private readonly IChucVuRepository _chuvuRepo;

        public UserRepository(QuanLyVatTuContext context, IConfiguration config,IPhieuSuaProviders phieuPR,IChucVuRepository chuvuRepo)
        {
            _context = context;
            _config = config;
            _phieuPR = phieuPR;
            _chuvuRepo = chuvuRepo;
        }
        public int ActiveUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.IdUser == id);
            if(user.IsActive == 1)
            {
                user.IsActive = 0;
                _context.SaveChanges();
            }
            else
            {
                user.IsActive = 1;
                _context.SaveChanges();
            }
            return (int)user.IsActive;
        }

       //public List<UserVM> GetAll(int pg, int size, int phongBan)
       public JsonResult GetAll(int pg, int size, int phongBan, int chucVu)
        {
            int count;
            var allUser = _context.Users.AsQueryable();

            if(pg < 1)
            {
                pg = 1;
            }
            if (phongBan!= 0)
            {
                allUser = allUser.Where(u => u.IdPhongBan == phongBan);
                count = allUser.Count();
            }
            if (chucVu != 0)
            {
                allUser = allUser.Where(u => u.IdChucVu == chucVu);
                count = allUser.Count();
            }
            else
            {
                count = allUser.Count();
            }
            allUser = allUser.OrderBy(u => u.IdPhongBan).ThenBy(u =>u.IdChucVu).Skip((pg -1)*size).Take(size);

            var users = allUser.Select(u => new UserVM
            {
                IdUser = u.IdUser,
                Username = u.Username,
                HoTen = u.HoTen,
                TenChucVu = u.IdChucVuNavigation.TenChucVu,
                MaChucVu = u.IdChucVuNavigation.MaChuVu,
                TenPhongBan = u.IdPhongBanNavigation.TenPhongBan,
                MaPhongBan = u.IdPhongBanNavigation.MaPhongBan,
                IdChucVu = u.IdChucVu,
                IdPhongBan = u.IdPhongBan,
                DiaChi = u.DiaChi,
                Email = u.Email,
                DienThoai = u.DienThoai,
                HinhDaiDien = u.HinhDaiDien,
                isActive = u.IsActive,
            }).ToList();
            var result = new JsonResult(users, count);
            return result;
        }

        public JsonResult Search(int pg, int size, string search)
        {
            int count;
            var allUser = _context.Users.AsQueryable();

            if (pg < 1)
            {
                pg = 1;
            }
            if (search!=null)
            {
                allUser = allUser.Where(u => u.Username.Contains(search) || u.HoTen.Contains(search) || u.Email.Contains(search));
                count = allUser.Count();
            }
            else
            {
                count = allUser.Count();
            }
            allUser = allUser.OrderBy(u => u.IdPhongBan).ThenBy(u => u.IdChucVu).Skip((pg - 1) * size).Take(size);

            var users = allUser.Select(u => new UserVM
            {
                IdUser = u.IdUser,
                Username = u.Username,
                HoTen = u.HoTen,
                TenChucVu = u.IdChucVuNavigation.TenChucVu,
                MaChucVu = u.IdChucVuNavigation.MaChuVu,
                TenPhongBan = u.IdPhongBanNavigation.TenPhongBan,
                MaPhongBan = u.IdPhongBanNavigation.MaPhongBan,
                IdChucVu = u.IdChucVu,
                IdPhongBan = u.IdPhongBan,
                DiaChi = u.DiaChi,
                Email = u.Email,
                DienThoai = u.DienThoai,
                HinhDaiDien = u.HinhDaiDien,
                isActive = u.IsActive,
            }).ToList();
            var result = new JsonResult(users, count);
            return result;
        }

        public UserMD GetById(int id)
        {
            var user = _context.Users.Include(u => u.IdPhongBanNavigation).Include(u => u.IdChucVuNavigation).FirstOrDefault(u => u.IdUser == id);
            if (user == null)
            {
                return null;
            }
            else
            {
                return new UserMD
                {                    
                    Username = user.Username,
                    HoTen = user.HoTen,
                    TenChucVu = user.IdChucVuNavigation.TenChucVu,
                    TenPhongBan = user.IdPhongBanNavigation.TenPhongBan,
                    MaPhongBan = user.IdPhongBanNavigation.MaPhongBan,
                    MaChucVu = user.IdChucVuNavigation.MaChuVu,
                    DiaChi = user.DiaChi,
                    Email = user.Email,
                    DienThoai = user.DienThoai,
                    HinhDaiDien = user.HinhDaiDien,
                    IdChucVu = user.IdChucVu,
                    IdPhongBan = user.IdPhongBan,
                    AboutMe = user.AboutMe,
                    isActive=user.IsActive,
                };
            }
        }
        public List<UserVM> GetByPhongBan(int idPhongBan)
        {
            var users = _context.Users.Where(u => u.IdPhongBan == idPhongBan)
                .Select(u => new UserVM
                {
                    IdUser = u.IdUser,
                    Username = u.Username,
                    HoTen = u.HoTen,
                    TenChucVu = u.IdChucVuNavigation.TenChucVu,
                    TenPhongBan = u.IdPhongBanNavigation.TenPhongBan,
                    DiaChi = u.DiaChi,
                    Email = u.Email,
                    DienThoai = u.DienThoai,
                    HinhDaiDien = u.HinhDaiDien,
                    IdChucVu = u.IdChucVu,
                    IdPhongBan = u.IdPhongBan,
                });
            return users.ToList();
        }

        public void ResetPass(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.IdUser == id);
            if (user != null)
            {
                user.MatKhau = "b24331b1a138cde62aa1f679164fc62f";
                _context.SaveChanges();
            }
        }

        public void UpdateUser(int id, UserMD userMD)
        {
            var user = _context.Users.SingleOrDefault(u=> u.IdUser == id);

            if (user != null)
            {
                user.Username = userMD.Username;
                user.HoTen = userMD.HoTen;
                user.AboutMe = userMD.AboutMe;
                user.IdChucVu = userMD.IdChucVu;
                user.IdPhongBan = userMD.IdPhongBan;
                user.DiaChi = userMD.DiaChi;
                user.Email = userMD.Email;
                user.DienThoai = userMD.DienThoai;
                if(userMD.isActive != null)
                {
                    user.IsActive = userMD.isActive;
                }
                user.HinhDaiDien = userMD.HinhDaiDien;
                _context.SaveChanges();
            }
        }
        public void SetConnectionId(int id, string connectionId)
        {
            var user = _context.Users.SingleOrDefault(u => u.IdUser == id);
            user.ConnectionId = connectionId;
            _context.SaveChanges();
        }

        public int AddUser(UserMD userMD)
        {
            var us = _context.Users.Where(u => u.Username == userMD.Username || u.Email == userMD.Email);
            if (us.Count() > 0)
            {
                return 0;
            }
            else
            {
                if (string.IsNullOrEmpty(userMD.HinhDaiDien))
                {
                    userMD.HinhDaiDien = "https://localhost:7006/Upload/Images/user/user-default.png";
                }
                var _user = new User
                {
                    Username = userMD.Username,
                    HoTen = userMD.HoTen,
                    MatKhau = "b24331b1a138cde62aa1f679164fc62f",
                    DiaChi = userMD.DiaChi,
                    Email = userMD.Email,
                    DienThoai = userMD.DienThoai,
                    IdPhongBan = userMD.IdPhongBan,
                    IdChucVu = userMD.IdChucVu,
                    IsActive = userMD.isActive,
                    HinhDaiDien = userMD.HinhDaiDien,
                };
                _context.Add(_user);
                _context.SaveChanges();
                if (_user.IdChucVu >= 1 || _user.IdChucVu <= 7)
                {
                ResetMaBiMat(_user.IdUser);
                }
                var key = _chuvuRepo.GetById(userMD.IdChucVu).MaChucVu;
                _phieuPR.updata(key, 14, "nguoidung");

                return 1;
            }
        }

        public void ChangePass(int id, ChangePassMD changePass)
        {

            var user = _context.Users.SingleOrDefault(u => u.IdUser == id && u.MatKhau == CreateMD5.GetMD5(changePass.OldPass));

            if (user != null)
            {
                user.MatKhau = CreateMD5.GetMD5(changePass.NewPass);
                _context.SaveChanges();
            }
        }

        public List<UserVM> GetTruongPhong(int id)
        {
            var users = _context.Users.Where(u => u.IdPhongBan == id && u.IdChucVu <= 7 && u.IdChucVu >=3)
               .Select(u => new UserVM
               {
                   IdUser = u.IdUser,
                   HoTen = u.HoTen,
                   MaChucVu = u.IdChucVuNavigation.MaChuVu,
                   IdChucVu = u.IdChucVu,
                   IdPhongBan = u.IdPhongBan,
               });
            return users.ToList();
        }
        // lấy iduser dựa theo chức vụ
        public UserVM GetUserByChucVu(string chucVu)
        {
            var user = _context.Users.FirstOrDefault(u=>u.IdChucVuNavigation.MaChuVu == chucVu);
            var result = new UserVM
            {
                IdUser = user.IdUser,
                HoTen= user.HoTen,
                Username= user.Username,
            };
            return result;
        }
        public UserVM GetUserByPhongBan(string maPB, string chucVu)
        {
            var user = _context.Users.FirstOrDefault(u => u.IdPhongBanNavigation.MaPhongBan == maPB && u.IdChucVuNavigation.MaChuVu == chucVu);
            var result = new UserVM
            {
                IdUser = user.IdUser,
                HoTen = user.HoTen,
                Username = user.Username,
            };
            return result;
        }
        public JsonResult DoiMaBiMat(int id, ChangeMaBiMatMD maBiMat)
        {
            
            var user = _context.Users.FirstOrDefault(u => u.IdUser == id && u.MaBiMat ==maBiMat.OldMa);
            if(user != null)
            {
                user.MaBiMat = maBiMat.NewMa;
                _context.SaveChanges();
                return new JsonResult("doi xong ma bi mat") { StatusCode = StatusCodes.Status200OK };
            }
            else
            {
                return new JsonResult("Nhap sai ma bi mat") { StatusCode = StatusCodes.Status400BadRequest};
            }
            
        }
        public JsonResult ResetMaBiMat(int idUser)
        {
            var user = _context.Users.FirstOrDefault(u => u.IdUser == idUser);
            if (user != null)
            {
                user.MaBiMat = "123456";
            }
            _context.SaveChanges();
            return new JsonResult("tao xong ma bi mat") { StatusCode = StatusCodes.Status200OK };
        }
        public List<UserVM> GetUserSuaByPhongBan(int id)
        {
            var users = _context.Users.Where(u => u.IdPhongBan == id && u.IdChucVuNavigation.MaChuVu=="N.Vien")
               .Select(u => new UserVM
               {
                   IdUser = u.IdUser,
                   Username = u.Username,
                   HoTen = u.HoTen,
                   TenChucVu = u.IdChucVuNavigation.TenChucVu,
                   TenPhongBan = u.IdPhongBanNavigation.TenPhongBan,
                   DiaChi = u.DiaChi,
                   Email = u.Email,
                   DienThoai = u.DienThoai,
                   HinhDaiDien = u.HinhDaiDien,
                   IdChucVu = u.IdChucVu,
                   IdPhongBan = u.IdPhongBan,
               });
            return users.ToList();
        }
    }
}
