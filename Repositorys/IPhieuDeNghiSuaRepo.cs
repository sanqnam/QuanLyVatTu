using Microsoft.AspNetCore.Mvc;
using QLVT_BE.Data;
using QLVT_BE.HubSignalR;
using QLVT_BE.Providers;
using QLVT_BE.Singleton;
using QLVT_BE.ViewModels;
using System.Runtime.CompilerServices;

namespace QLVT_BE.Repositorys
{
    public interface IPhieuDeNghiSuaRepo
    {

        JsonResult TaoPhieuSua(TaoPhieuSuaVM phieuSuaVM);
        JsonResult GetAllPhieu(int idPhongBan, int idNguoiDuyet, int pg, int size);
        JsonResult GetImgByPhieu(int idChiTietPhieu);
        JsonResult GetVatTuByPhieu(int idPhieu);
        Task<JsonResult> DuyetPhieuSua(Boolean duyet, int idPhieu, string lyDoTra, string maBiMat);
        void SetDeNghiThanhLy(int idChiTietPhieu);
        void SetSuaXong(int idChiTietPhieu);
        void SetNhanVienSua(int idNhanVien, int idChiTietPhieuSua, int idUserGui);
        JsonResult ShowVTCanSuaByNVSua(int idUser, int pg, int size);
        JsonResult AllPhieuSuaByPhongBanPhuTrach(int idPhongBan, string role, int pg, int size, int orderShort);
        JsonResult GetAllStatusPhieuByPB(int idPhongBan, int size, int pg, int status);
        JsonResult GetAllPhieuDangCho(int idUser, int pg, int size, int oderSort, int chosse);
        JsonResult GetAllPhieuSussces(int idUser, int pg, int size, int oderSort, int chosse);
    }
    public class PhieuDeNghiSuaRepo : IPhieuDeNghiSuaRepo
    {
        private readonly IUserRepository _userRepo;
        private readonly QuanLyVatTuContext _context;
        private readonly INotificaRepo _notiRepo;
        private readonly INotificationsManager _singlNoti;
        private readonly HubFunction _hubFunction;
        private readonly IPhongBanRepository _pbRepo;
        private readonly IPhieuSuaProviders _phieuPRO;

        public PhieuDeNghiSuaRepo(QuanLyVatTuContext context, IUserRepository userRepo, INotificaRepo notiRepo,
            INotificationsManager singlNoti, HubFunction hubFunction,
            IPhongBanRepository pbRepo, IPhieuSuaProviders phieuPRO)
        {
            _userRepo = userRepo;
            _context = context;
            _notiRepo = notiRepo;
            _singlNoti = singlNoti;
            _hubFunction = hubFunction;
            _pbRepo = pbRepo;
            _phieuPRO = phieuPRO;
        }
        //tạo phiếu yêu cầu sửa
        public JsonResult TaoPhieuSua(TaoPhieuSuaVM phieuSuaVM)
        {
            string forder = "VatTuSuaChua";
            var newPhieu = new PhieuDeNghiSuaChua
            {
                IdPhongBan = phieuSuaVM.IdPhongBan,
                IdThuTruong = phieuSuaVM.IdThuTruongNhan,
                IdPhongNhan = phieuSuaVM.IdPhongNhan,
                IdUser = phieuSuaVM.IdUser,
                LyDo = phieuSuaVM.LyDo,
                NgayTaoPhieu = DateTime.Now,
                IdTinhTrangPhieu = 1,
            };
            _context.Add(newPhieu);
            _context.SaveChanges();
            foreach (var phieu in phieuSuaVM.ChiTietPhieuSuas)
            {
                var newChiTiet = new ChiTietPhieuSuaChua
                {
                    MoTaTinhTrang = phieu.MoTaTinhTrang,
                    IdPhieuSuaChua = newPhieu.IdPhieuSuaChua,
                    IdChiTietVatTu = phieu.IdChiTietVatTu,

                };
                _context.Add(newChiTiet);
                _context.SaveChanges();
                if (phieu.Url != null)
                {
                    for (int i = 0; phieu.Url.Count > i; i++)
                    {
                        var newHinh = new HinhAnhSuaChua
                        {
                            IdChiTietSuaChua = newChiTiet.IdChiTietSuaChua,
                            Url = "https://localhost:7006/" + phieu.Url[i],
                        };
                        _context.Add(newHinh);
                    }
                }
            }
            var thongBao = new Notification
            {
                NguoiGui = newPhieu.IdUser,
                NguoiNhan = newPhieu.IdThuTruong,
                Mess = "Có phiếu đề nghị sửa mới ",
                Url = "phieuduyetsua",
                TimeTao = DateTime.Now,
                DaDoc = false,
                IdPhieu = newPhieu.IdPhieuSuaChua,

            };
            _context.Notifications.Add(thongBao);
            var idTo = _userRepo.GetById(phieuSuaVM.IdThuTruongNhan).Username;
            var userName = _userRepo.GetById(phieuSuaVM.IdThuTruongNhan).Username;
            var oldCount = _notiRepo.GetCurrentUserName(userName);
            var newCount = oldCount + 1;
            _singlNoti.CountNotifis.TryUpdate(userName, newCount, oldCount);
            _hubFunction.SendNotifica(newCount, idTo);
            _context.SaveChanges();
            return new JsonResult("tao xong");
        }

        // show phiếu đề nghị sữa đang chờ duyệt
        public JsonResult GetAllPhieu(int idPhongBan, int idNguoiDuyet, int pg, int size)   
        {
            if (pg <= 1)
            {
                pg = 1;
            }
            var phieus = _context.PhieuDeNghiSuaChuas.AsQueryable();
            phieus = phieus.Where(p => p.IdPhongBan == idPhongBan && p.IdThuTruong == idNguoiDuyet && p.IdTinhTrangPhieu == 1)
                .OrderByDescending(p => p.IdPhieuSuaChua);
            var counts = phieus.Count();
            phieus = phieus.Skip((pg - 1) * size).Take(size);
            var phieu = phieus.Select(p => new PhieuSuaMD
            {
                HoTen = p.IdUserNavigation.HoTen,
                IdPhieuSuaChua = p.IdPhieuSuaChua,
                LyDo = p.LyDo,
                PhongBanNhan = p.IdPhongNhanNavigation.TenPhongBan,
                IdPhongBanNhan = p.IdPhongNhan,
                TimeTao = p.NgayTaoPhieu,
                TinhTrangPhieu = p.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
            }).ToList();
            return new JsonResult(phieu, counts);
        }
        // showw chi tiết vật tư  của từng phiếu đề nghị sữa
        public JsonResult GetVatTuByPhieu(int idPhieu)
        {
            var phieus = _context.ChiTietPhieuSuaChuas.Where(p => p.IdPhieuSuaChua == idPhieu).Select(p => new VatTuSuaMD
            {
                IdChiTietPhieuSua = p.IdChiTietSuaChua,
                MaVatTu = p.IdChiTietVatTuNavigation.MaVatTu,
                MoTaTinhTrang = p.MoTaTinhTrang,
                NguoiThucHien = p.NguoiThucHienNavigation.HoTen,
                TenVatTu = p.IdChiTietVatTuNavigation.IdVatTuNavigation.TenVatTu,
                HinhAnhSuas = _context.HinhAnhSuaChuas.Where(h => h.IdChiTietSuaChua == p.IdChiTietSuaChua).ToList(),
            }).ToList();
            return new JsonResult(phieus);
        }
        //show tất cả các hình đề nghị sửa
        public JsonResult GetImgByPhieu(int idChiTietPhieu)
        {
            var imgs = _context.HinhAnhSuaChuas.Where(h => h.IdChiTietSuaChua == idChiTietPhieu).Select(h => new HinhAnhSuaVM
            {
                IdHinhAnhSua = h.IdHinhSuaChua,
                Url = h.Url,
            }).ToList();
            return new JsonResult(imgs);
        }
        // trưởng phòng duyệt phiếu đề nghị sửa
        public async Task<JsonResult> DuyetPhieuSua(Boolean duyet, int idPhieu, string lyDoTra, string maBiMat)
        {
            var phieu = _context.PhieuDeNghiSuaChuas.FirstOrDefault(p => p.IdPhieuSuaChua == idPhieu);
            var idThuTruong = _context.PhieuDeNghiSuaChuas.FirstOrDefault(p => p.IdThuTruong == phieu.IdThuTruong).IdThuTruong;
            var ma = _context.Users.FirstOrDefault(u => u.IdUser == idThuTruong && u.MaBiMat == maBiMat);
            if (ma != null)
            {
                if (duyet == true)
                {
                    if (phieu != null)
                    {

                        phieu.IdTinhTrangPhieu = 2;
                        phieu.NgayDuyetPhieu= DateTime.Now;
                    }
                    var noti = _context.Notifications.FirstOrDefault(n => n.IdPhieu == idPhieu);
                    if (noti != null)
                    {
                        _context.Remove(noti);

                    }
                    
                    var maPB = _pbRepo.GetById(phieu.IdPhongNhan).MaPhongBan;
                    var key = _pbRepo.GetById((int)phieu.IdPhongBan).MaPhongBan;
                    var month = DateTime.Now.Month;
                    _phieuPRO.updata(key, month, "denghisua");
                    var nameNhan1 = _userRepo.GetUserByPhongBan(maPB, "TP");
                    var nameNhan2 = _userRepo.GetUserByPhongBan(maPB, "PP");
                    var nameGui = _userRepo.GetById(phieu.IdUser).Username;
                    int oldCountNhan1 = _notiRepo.GetCurrentUserName(nameNhan1.Username);
                    int newCountNhan1 = oldCountNhan1 + 1;
                    _notiRepo.CreateNotiVatTu(nameNhan1.IdUser, phieu.IdUser, idPhieu, "Có phiếu Đề nghị sữa mới", "phieusuatiepnhan");
                    await _hubFunction.SendNotifica(newCountNhan1, nameNhan1.Username);
                    int oldCountNhan2 = _notiRepo.GetCurrentUserName(nameNhan2.Username);
                    int newCountNhan2 = oldCountNhan2 + 1;
                    _notiRepo.CreateNotiVatTu(nameNhan2.IdUser, phieu.IdUser, idPhieu, "Có phiếu Đề nghị sữa mới", "phieusuatiepnhan");
                    await _hubFunction.SendNotifica(newCountNhan2, nameNhan2.Username);
                    _context.SaveChanges();
                }
                else
                {
                    if (phieu != null)
                    {
                        phieu.LyDoTraPhieu = lyDoTra;
                        phieu.IdTinhTrangPhieu = 4;
                    }

                    var noti = _context.Notifications.FirstOrDefault(n => n.IdPhieu == idPhieu);
                    if (noti != null)
                    {
                        _context.Remove(noti);
                    }
                    var nameNhan = _userRepo.GetById(phieu.IdUser).Username;
                    int oldCount = _notiRepo.GetCurrentUserName(nameNhan);
                    int newCount = oldCount + 1;
                    _notiRepo.CreateNotiVatTu(phieu.IdUser, phieu.IdThuTruong, idPhieu, "Thủ trưởng Không duyệt phiếu sửa", "khongduyet");
                    await _hubFunction.SendNotifica(newCount, nameNhan);
                    _context.SaveChanges();
                }
                return new JsonResult("duyet xong")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            else
            {
                return new JsonResult("sai mã")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            
        }
        //  show tất cả các phiếu đề nghị sữa đã duyệt cho phòng ban phụ trách sửa vật tư
        public JsonResult AllPhieuSuaByPhongBanPhuTrach(int idPhongBan, string role, int pg, int size, int orderShort)
        {
            if (role == "TP" || role == "PP")
            {
                var tenpb = _pbRepo.GetById(idPhongBan).TenPhongBan;
                var allPhieu = _context.PhieuDeNghiSuaChuas.AsQueryable();
                if (pg <= 1)
                {
                    pg = 1;
                }
                allPhieu = allPhieu.Where(p => p.IdPhongNhan == idPhongBan && p.IdTinhTrangPhieu == 2);
                switch (orderShort)
                {
                    case 1:
                        allPhieu = allPhieu.OrderByDescending(p => p.NgayTaoPhieu);
                        break;
                    case 2:
                        allPhieu = allPhieu.OrderBy(p => p.NgayTaoPhieu);
                        break;
                    default:
                        allPhieu = allPhieu.OrderByDescending(p => p.NgayTaoPhieu);
                        break;
                }
                int count = allPhieu.Count();
                allPhieu = allPhieu.Skip((pg - 1) * size).Take(size);
                var phieus = allPhieu.Select(p => new PhieuSuaMD
                {
                     IdPhieuSuaChua = p.IdPhieuSuaChua,
                     HoTen = p.IdUserNavigation.HoTen,
                     PhongBanYeuCau = tenpb,
                     LyDo = p.LyDo,
                     TimeTao = p.NgayTaoPhieu,
                     TinhTrangPhieu = p.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                });
                return new JsonResult(phieus, count);
            }
            else
            {
                return new JsonResult("err");
            }
        }

        // set vật tư đưa cho nhân viên của mình đi sửa
        public async void SetNhanVienSua(int idNhanVien, int idChiTietPhieuSua, int idUserGui)
        {
            var phieu = _context.ChiTietPhieuSuaChuas.FirstOrDefault(p => p.IdChiTietSuaChua == idChiTietPhieuSua);
            phieu.NguoiThucHien = idNhanVien;
            phieu.TrangThaiXuLy = "Tiếp Nhận, Đang Xử Lý";
            var nameNhan = _userRepo.GetById(idNhanVien).Username;
            int oldCount = _notiRepo.GetCurrentUserName(nameNhan);
            int newCount = oldCount + 1;
            _notiRepo.CreateNotiVatTu(idNhanVien, idUserGui, idChiTietPhieuSua, "Có vật tư cần sửa mới", "vattucansua");
            await _hubFunction.SendNotifica(newCount, nameNhan);
            _context.SaveChanges();
        }
        // show các vật tư cần sữa cho nhân viên phụ trách
        public JsonResult ShowVTCanSuaByNVSua(int idUser, int pg, int size)
        {
            if(pg <= 1)
            {
                pg =1;
            }
            var phieus = _context.ChiTietPhieuSuaChuas.Where(p => p.NguoiThucHien == idUser);
            var counts = phieus.Count();
            phieus = phieus.Skip((pg-1)*size).Take(size);
            var phieu = phieus.OrderByDescending(p => p.IdChiTietSuaChua).Where(p => p.NguoiThucHien == idUser).Select(p => new ShowPhieuChoNhanVienVM
            {
                idChiTietPhieuSua = p.IdChiTietSuaChua,
                IdPhieuSuaChua = p.IdPhieuSuaChua,
                TrangThaiXuLy = p.TrangThaiXuLy,
                MoTaTinhTrang = p.MoTaTinhTrang,
                NguoiYeuCau = _context.PhieuDeNghiSuaChuas.FirstOrDefault(ps => ps.IdPhieuSuaChua == p.IdPhieuSuaChua).IdUserNavigation.HoTen,
                PhongBanYeuCau = _context.PhieuDeNghiSuaChuas.FirstOrDefault(ps => ps.IdPhieuSuaChua == p.IdPhieuSuaChua).IdPhongNhanNavigation.TenPhongBan,
                TenVatTu =p.IdChiTietVatTuNavigation.IdVatTuNavigation.TenVatTu,

            }).ToList();
            return new JsonResult(phieu,counts);
        }
        // set phiếu sữa xong
        public void SetSuaXong(int idChiTietPhieu)
        {
            var phieu = _context.ChiTietPhieuSuaChuas.FirstOrDefault(p=>p.IdChiTietSuaChua==idChiTietPhieu);
            phieu.TrangThaiXuLy = "Đã Xong";
            _context.SaveChanges();
        }
        public void SetDuaRaNgoai(int idChiTietPhieu) 
        {
            var phieu = _context.ChiTietPhieuSuaChuas.FirstOrDefault(p => p.IdChiTietSuaChua == idChiTietPhieu);
        }
        // set vật từ cần thanh lý
        public void SetDeNghiThanhLy(int idChiTietPhieu)
        {
            var phieu = _context.ChiTietPhieuSuaChuas.FirstOrDefault(p => p.IdChiTietSuaChua == idChiTietPhieu);
            phieu.TrangThaiXuLy = "Đề Nghị Thanh Ly, Đã Xong";
            _context.SaveChanges();
        }
        // show tất cả các phiếu của từng phòng ban cho trưởng phòng quản lý(xong, đã tạo, đang làm)
        public JsonResult GetAllStatusPhieuByPB(int idPhongBan, int size, int pg, int status)
        {
            if (pg <= 1)
            {
                pg = 1;
            }
            var phieus = _context.PhieuDeNghiSuaChuas.AsQueryable();
            // status  = 1 là đang chờ duyệt
            if(status == 1)
            {
                phieus = phieus.Where(p => p.IdPhongBan == idPhongBan && p.IdTinhTrangPhieu == 1);
                var counts = phieus.Count();
                phieus = phieus.Skip((pg - 1) * size).Take(size);
                var phieu = phieus.OrderByDescending(p => p.NgayTaoPhieu).Select(p => new PhieuSuaMD
                {
                    HoTen = p.IdUserNavigation.HoTen,
                    IdPhieuSuaChua = p.IdPhieuSuaChua,
                    LyDo = p.LyDo,
                    PhongBanNhan = p.IdPhongNhanNavigation.TenPhongBan,
                    IdPhongBanNhan = p.IdPhongNhan,
                    TimeTao = p.NgayTaoPhieu,
                    TinhTrangPhieu = p.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                }).ToList();
                return new JsonResult(phieu, counts);
            } 
            // status= 2 là đã duyệt xong đang chờ người phân công đến sửa
            else if( status == 2)
            {
                phieus = phieus.Where(p => p.IdPhongBan == idPhongBan && p.IdTinhTrangPhieu == 8);
                var counts = phieus.Count();
                phieus = phieus.Skip((pg - 1) * size).Take(size);
                var phieu = phieus.OrderByDescending(p => p.NgayTaoPhieu).Select(p => new PhieuSuaMD
                {
                    HoTen = p.IdUserNavigation.HoTen,
                    IdPhieuSuaChua = p.IdPhieuSuaChua,
                    LyDo = p.LyDo,
                    PhongBanNhan = p.IdPhongNhanNavigation.TenPhongBan,
                    IdPhongBanNhan = p.IdPhongNhan,
                    TimeTao = p.NgayTaoPhieu,
                    TinhTrangPhieu = p.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                }).ToList();
                return new JsonResult(phieu, counts);
            }
            // đã hoàn thành  sửa xong
            else if (status == 3)
            {
                phieus = phieus.Where(p => p.IdPhongBan == idPhongBan && p.IdTinhTrangPhieu == 7);
                var counts = phieus.Count();
                phieus = phieus.Skip((pg - 1) * size).Take(size);
                var phieu = phieus.OrderByDescending(p => p.NgayTaoPhieu).Select(p => new PhieuSuaMD
                {
                    HoTen = p.IdUserNavigation.HoTen,
                    IdPhieuSuaChua = p.IdPhieuSuaChua,
                    LyDo = p.LyDo,
                    PhongBanNhan = p.IdPhongNhanNavigation.TenPhongBan,
                    IdPhongBanNhan = p.IdPhongNhan,
                    TimeTao = p.NgayTaoPhieu,
                    TinhTrangPhieu = p.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                }).ToList();
                return new JsonResult(phieu, counts);
            }
            return new JsonResult("xong");
        }
        // show tất cả các phiếu sữa đã hoàn thành(bao gồm phiếu trả và phiếu đã hoàn thành)
        public JsonResult GetAllPhieuSussces(int idUser, int pg, int size, int oderSort, int chosse)
        {
            if(pg <= 1)
            {
                pg = 1;
            }
            var phieus = _context.PhieuDeNghiSuaChuas.AsQueryable();
            // chosse == 1 thì phiếu đã sữa xử lý xong
            if(chosse == 1)
            {
                phieus = phieus.Where(p=>p.IdUser == idUser && p.IdTinhTrangPhieu == 7);
                switch (oderSort) 
                { 
                    case 1:
                        phieus = phieus.OrderByDescending(p => p.IdPhieuSuaChua);
                        break;
                    case 2:
                        phieus = phieus.OrderBy(p => p.IdPhieuSuaChua);
                        break;
                    default:
                         phieus = phieus.OrderByDescending(p => p.IdPhieuSuaChua);
                        break;
                }
                int count = phieus.Count();
                phieus = phieus.Skip((pg - 1) * size).Take(size);
                var phieu = phieus.Select(p => new PhieuSuaMD
                {
                    IdPhieuSuaChua = p.IdPhieuSuaChua,
                    HoTen = p.IdUserNavigation.HoTen,
                    LyDo = p.LyDo,
                    TimeTao = p.NgayTaoPhieu,
                    TinhTrangPhieu = p.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                    PhongBanNhan = p.IdPhongNhanNavigation.TenPhongBan
                });
                return new JsonResult(phieu, count);
            }
            // phiếu bị trả không hoàn thành
            else
            {
                phieus = phieus.Where(p => p.IdUser == idUser && p.IdTinhTrangPhieu == 4);
                switch (oderSort)
                {
                    case 1:
                        phieus = phieus.OrderByDescending(p => p.IdPhieuSuaChua);
                        break;
                    case 2:
                        phieus = phieus.OrderBy(p => p.IdPhieuSuaChua);
                        break;
                    default:
                        phieus = phieus.OrderByDescending(p => p.IdPhieuSuaChua);
                        break;
                }
                int count = phieus.Count();
                phieus = phieus.Skip((pg - 1) * size).Take(size);
                var phieu = phieus.Select(p => new PhieuSuaMD
                {
                    IdPhieuSuaChua = p.IdPhieuSuaChua,
                    HoTen = p.IdUserNavigation.HoTen,
                    LyDo = p.LyDo,
                    TimeTao = p.NgayTaoPhieu,
                    TinhTrangPhieu = p.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                    PhongBanNhan = p.IdPhongNhanNavigation.TenPhongBan
                });
                return new JsonResult( phieus, count);
            }

        }
        // show tất cả các phiếu đang thực hiện( vừa tạo xong đang chờ duyệt và đang chờ sửa)
        public JsonResult GetAllPhieuDangCho(int idUser, int pg, int size, int oderSort, int chosse)
        {
            if (pg <= 1)
            {
                pg = 1;
            }
            var phieus = _context.PhieuDeNghiSuaChuas.AsQueryable();
            // chosse == 1 thì phiếu vừa mới tạo đang chờ duyệt
            if (chosse == 1)
            {
                phieus = phieus.Where(p => p.IdUser == idUser && (p.IdTinhTrangPhieu == 1|| p.IdTinhTrangPhieu == 2));
                switch (oderSort)
                {
                    case 1:
                        phieus = phieus.OrderByDescending(p => p.IdPhieuSuaChua);
                        break;
                    case 2:
                        phieus = phieus.OrderBy(p => p.IdPhieuSuaChua);
                        break;
                    default:
                        phieus = phieus.OrderByDescending(p => p.IdPhieuSuaChua);
                        break;
                }
                int count = phieus.Count();
                phieus = phieus.Skip((pg - 1) * size).Take(size);
                var phieu = phieus.Select(p => new PhieuSuaMD
                {
                    IdPhieuSuaChua = p.IdPhieuSuaChua,
                    HoTen = p.IdUserNavigation.HoTen,
                    LyDo = p.LyDo,
                    TimeTao = p.NgayTaoPhieu,
                    TinhTrangPhieu = p.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                    PhongBanNhan = p.IdPhongNhanNavigation.TenPhongBan
                });
                return new JsonResult(phieu, count);
            }
            // phiếu đang chờ sửa
            else
            {
                phieus = phieus.Where(p => p.IdUser == idUser && p.IdTinhTrangPhieu == 8);
                switch (oderSort)
                {
                    case 1:
                        phieus = phieus.OrderByDescending(p => p.IdPhieuSuaChua);
                        break;
                    case 2:
                        phieus = phieus.OrderBy(p => p.IdPhieuSuaChua);
                        break;
                    default:
                        phieus = phieus.OrderByDescending(p => p.IdPhieuSuaChua);
                        break;
                }
                int count = phieus.Count();
                phieus = phieus.Skip((pg - 1) * size).Take(size);
                var phieu = phieus.Select(p => new PhieuSuaMD
                {
                    IdPhieuSuaChua = p.IdPhieuSuaChua,
                    HoTen = p.IdUserNavigation.HoTen,
                    LyDo = p.LyDo,
                    TimeTao = p.NgayTaoPhieu,
                    TinhTrangPhieu = p.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                    PhongBanNhan = p.IdPhongNhanNavigation.TenPhongBan
                });
                return new JsonResult(phieu, count);
            }
        }
    }
}

