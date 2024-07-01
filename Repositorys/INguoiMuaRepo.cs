using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using QLVT_BE.Data;
using QLVT_BE.HubSignalR;
using QLVT_BE.Providers;
using QLVT_BE.Singleton;
using QLVT_BE.ViewModels;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime;

namespace QLVT_BE.Repositorys
{
    public interface INguoiMuaRepo
    {
        JsonResult GetVatTuChoMua(int pg);
        JsonResult SearchVatTuChoMua(int pg, string search);
        JsonResult TaoPhieuMua(TaoPhieuMuaMD phieu);
        JsonResult GetAllPhieuTrinhMua(int pg, int size, int idPhongBan, int sortOrder, int idUser, string role);
        JsonResult GetChiTietPhieuMua(int idPhieu);
        PhieuTrinhMuaMD GetPhieuById(int phieuId);
        JsonResult DuyetPhieu(DuyetPhieu phieuDuyet, Boolean isDuyet);
        JsonResult ShowPhieuKhongDuyet(int pg, int size, int idUser);
        JsonResult PhieuCanSua(SuaPhieuMuaMD phieuMD);
        JsonResult GetPhieuSuaTra(int idPhieu, int idUser);
        JsonResult GetPhieuChoThuKho(int pg);
        JsonResult XacNhanThuKho(NhapKhoMD nhapKho, Boolean isXacNhan);
        JsonResult GetPhieuHoanThanh(int pg, int idUser);
        JsonResult YeuCauNhapKho(int idPhieu);
        JsonResult GetPhieuNhapKho(int pg,  int idUser);
        JsonResult XuatHoaDonTheoThang(int month, int idUser);
    }
    public class NguoiMuaRepo : INguoiMuaRepo
    {
        private readonly QuanLyVatTuContext _context;
        private readonly IUserRepository _userRepo;
        private readonly HubFunction _hubFunction;
        private readonly INotificaRepo _notiRepo;
        private readonly INotificationsManager _singlNoti;
        private readonly IPhieuSuaProviders _phieuPRO;

        public NguoiMuaRepo(QuanLyVatTuContext context, HubFunction hubFunction, 
            IUserRepository userRepo, INotificaRepo notiRepo, INotificationsManager singlNoti, IPhieuSuaProviders phieuPRO)
        {
            _context = context;
            _userRepo = userRepo;
            _hubFunction = hubFunction;
            _notiRepo = notiRepo;
            _singlNoti = singlNoti;
            _phieuPRO = phieuPRO;
        }

        public JsonResult GetVatTuChoMua(int pg)
        {
            if (pg <= 1)
            {
                pg = 1;
            }
            var vt = _context.ChiTietPhieus.Where(v => v.SoLuongMuaThem != null && v.DonGia == null).Select(v => new NguoiMuaMD
            {
                DonViTinh = v.DonViTinhDeNghi,
                GhiChuCanMua = v.GiChuMuaThem,
                IdVatTu = (int)v.IdVatTu,
                MaVatTu = v.MaVatTu,
                idChiTietPhieu = v.IdChiTietPhieu,
                SoLuongCanMua = (double)v.SoLuongMuaThem,
                SoLuongTongCanMua = (float?)(_context.ChiTietPhieus.Where(vt => vt.IdVatTu == v.IdVatTu && vt.SoLuongMuaThem != null && vt.DonGia == null).Sum(vt => vt.SoLuongMuaThem) ?? 0),
                TenVatTu = v.TenVatTu,
            });
            int counts = vt.Count();
            vt = vt.OrderBy(v => v.idChiTietPhieu).Skip((pg - 1) * 10).Take(10);
            var result = vt.ToList();
            return new JsonResult(result, counts);

        }
        public JsonResult SearchVatTuChoMua(int pg, string search)
        {
            if (pg <= 1)
            {
                pg = 1;
            }
            var vt = _context.ChiTietPhieus.Where(v => v.SoLuongMuaThem != null && v.DonGia == null && v.TenVatTu.Contains(search)).Select(v => new NguoiMuaMD
            {
                DonViTinh = v.DonViTinhDeNghi,
                GhiChuCanMua = v.GiChuMuaThem,
                idChiTietPhieu = v.IdChiTietPhieu,
                IdVatTu = (int)v.IdVatTu,
                MaVatTu = v.MaVatTu,
                SoLuongCanMua = (double)v.SoLuongMuaThem,
                SoLuongTongCanMua = (float?)(_context.ChiTietPhieus.Where(vt => vt.IdVatTu == v.IdVatTu && vt.SoLuongMuaThem != null && vt.DonGia == null).Sum(vt => vt.SoLuongMuaThem) ?? 0),
                TenVatTu = v.TenVatTu,

            });
            int counts = vt.Count();
            vt = vt.OrderBy(v => v.idChiTietPhieu).Skip((pg - 1) * 10).Take(10);
            var result = vt.ToList();
            return new JsonResult(result, counts);

        }

        // tạo phiếu yêu cầu mua vật tư
        public JsonResult TaoPhieuMua(TaoPhieuMuaMD phieu)
        {
            var _phieu = new PhieuTrinhMua
            {
                IdUser = phieu.IdUser,
                IdThuTruong = phieu.IdThuTruong,
                IdLanhDao = phieu.IdLanhDao,
                TimeTaoPhieu = DateTime.Now,
                IdPhongBan = phieu.idPhongBan,
                IdTinhTrangPhieu = 1,
                IsNhap = false
            };
            _context.Add(_phieu);
            _context.SaveChanges();
            foreach (var chiTiet in phieu.ChiTietPhieus)
            {
                var check = _context.ChiTietPhieus.FirstOrDefault(p => p.IdChiTietPhieu == chiTiet.IdChiTietPhieu);
                check.DonGia = chiTiet.DonGia;
                check.DonViCungCap = chiTiet.DonViCungCap;
                check.Vat = chiTiet.VAT;
                check.IdTinhTrangXuLy = 1;
                check.IdPhieuDeNghiMua = _phieu.IdPhieuDeNghiMua;
                var addTien = _context.PhieuTrinhMuas.SingleOrDefault(p => p.IdPhieuDeNghiMua == _phieu.IdPhieuDeNghiMua);
                double sum = addTien.TongTien;
                double tinhTien = 0;
                double.TryParse(chiTiet.VAT, out double vatValue);
                if (chiTiet.VAT == null || chiTiet.VAT == "")
                {

                    tinhTien = (double)(chiTiet.DonGia * chiTiet.SoLuongMuaThem);
                }
                else
                {
                    tinhTien = (double)((chiTiet.DonGia * chiTiet.SoLuongMuaThem) + ((double)(chiTiet.DonGia * chiTiet.SoLuongMuaThem) * (vatValue / 100)));
                }
                addTien.TongTien = (double)(sum + tinhTien);
                var idTo = _userRepo.GetById(phieu.IdThuTruong).Username;
                var notifi = new Notification
                {
                    NguoiGui = phieu.IdUser,
                    NguoiNhan = phieu.IdThuTruong,
                    Url = "phieuduyetmuavattu",
                    TimeTao = DateTime.Now,
                    DaDoc = false,
                    Mess = "có phiếu yêu cầu mua mới",
                    IdPhieu = _phieu.IdPhieuDeNghiMua,

                };
                _context.Add(notifi);
                _context.SaveChanges();
                var userName = _userRepo.GetById(phieu.IdThuTruong).Username;
                var oldCount = _notiRepo.GetCurrentUserName(userName);
                var newCount = oldCount + 1;
                _singlNoti.CountNotifis.TryUpdate(userName, newCount, oldCount);

                _hubFunction.SendNotifica(newCount, idTo);
            }
            return new JsonResult("đã tạo phiếu");
        }
        // show tất cả phiếu đề nghị mua để duyệt
        public JsonResult GetAllPhieuTrinhMua(int pg, int size, int idPhongBan, int sortOrder, int idUser, string role)
        {

            if (role == "TP" || role == "PP")
            {
                var allPhieuTrinhMua = _context.PhieuTrinhMuas.AsQueryable();
                //var check = _context.PhieuDeNghiVatTus

                if (pg < 1)
                {
                    pg = 1;
                }
                allPhieuTrinhMua = allPhieuTrinhMua.Where(p => p.IdThuTruong == idUser && p.IdTinhTrangPhieu == 1);

                switch (sortOrder)
                {
                    case 1:
                        allPhieuTrinhMua = allPhieuTrinhMua.OrderByDescending(p => p.TimeTaoPhieu);
                        break;
                    case 2:
                        allPhieuTrinhMua = allPhieuTrinhMua.OrderBy(p => p.TimeTaoPhieu);
                        break;
                    default:
                        allPhieuTrinhMua = allPhieuTrinhMua.OrderByDescending(p => p.TimeTaoPhieu);
                        break;
                }
                int count = allPhieuTrinhMua.Count();
                allPhieuTrinhMua = allPhieuTrinhMua.Skip((pg - 1) * size).Take(size);

                var phieuDeNghi = allPhieuTrinhMua.Select(phieu => new PhieuTrinhMuaMD
                {
                    IdPhieuTrinhMua = phieu.IdPhieuDeNghiMua,
                    TenNguoiMua = phieu.IdUserNavigation.HoTen,
                    SoLanSuaPhieu = phieu.SoLanSuaPhieu,
                    LyDoSuaPhieu = phieu.LyDo,
                    TinhTrangPhieu = phieu.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                    TimeTaoPhieu = phieu.TimeTaoPhieu,
                    TongTien = phieu.TongTien,

                }).ToList();
                var result = new JsonResult(phieuDeNghi, count);
                return result;
            }
            else
            {
                var allPhieuDeNghi = _context.PhieuTrinhMuas.AsQueryable();
                //var check = _context.PhieuDeNghiVatTus

                if (pg < 1)
                {
                    pg = 1;
                }
                if (idPhongBan != 0)
                {
                    allPhieuDeNghi = allPhieuDeNghi.Where(p => p.IdLanhDao == idUser && p.IdTinhTrangPhieu == 2);

                }

                switch (sortOrder)
                {
                    case 2:
                        allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                        break;
                    case 1:
                        allPhieuDeNghi = allPhieuDeNghi.OrderBy(p => p.TimeTaoPhieu);
                        break;
                    default:
                        allPhieuDeNghi = allPhieuDeNghi.OrderBy(p => p.TimeTaoPhieu);
                        break;
                }

                int count = allPhieuDeNghi.Count();
                allPhieuDeNghi = allPhieuDeNghi.Skip((pg - 1) * size).Take(size);

                var phieuDeNghi = allPhieuDeNghi.Select(phieu => new PhieuTrinhMuaMD
                {
                    IdPhieuTrinhMua = phieu.IdPhieuDeNghiMua,
                    TenNguoiMua = phieu.IdUserNavigation.HoTen,
                    SoLanSuaPhieu = phieu.SoLanSuaPhieu,
                    LyDoSuaPhieu = phieu.LyDo,
                    TinhTrangPhieu = phieu.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                    TimeTaoPhieu = phieu.TimeTaoPhieu,
                    TongTien = phieu.TongTien
                }).ToList();
                var result = new JsonResult(phieuDeNghi, count);
                return result;
            }
        }
        public JsonResult GetChiTietPhieuMua(int idPhieu)
        {
            var phieus = _context.ChiTietPhieus.Where(p => p.IdPhieuDeNghiMua == idPhieu).Select(p => new ChiTietPhieuMuaVM
            {
                IdVatTu = p.IdVatTu,
                Soluong = p.SoLuongMuaThem,
                DonGia = p.DonGia,
                DonViTinh = p.DonViTinhDeNghi ?? p.DonViTinhThayDoi,
                DonViCungCap = p.DonViCungCap,
                TenVatTu = p.TenVatTu,
                VAT = p.Vat,
                IdChiTietPhieu =p.IdChiTietPhieu,
            }).ToList();
            return new JsonResult(phieus);
        }
        public PhieuTrinhMuaMD GetPhieuById(int phieuId)
        {
            var phieu = _context.PhieuTrinhMuas.Include(p => p.IdUserNavigation).Include(p => p.IdTinhTrangPhieuNavigation).FirstOrDefault(p => p.IdPhieuDeNghiMua == phieuId);

            var phieus = new PhieuTrinhMuaMD
            {
                IdPhieuTrinhMua = phieu.IdPhieuDeNghiMua,
                TenNguoiMua = phieu.IdUserNavigation.HoTen,
                LyDoSuaPhieu = phieu.LyDo,
                SoLanSuaPhieu = phieu.SoLanSuaPhieu,
                TinhTrangPhieu = phieu.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                TimeTaoPhieu = phieu.TimeTaoPhieu,
                TongTien = phieu.TongTien,
            };
            return phieus;

        }
        public JsonResult DuyetPhieu(DuyetPhieu phieuDuyet, Boolean isDuyet)
        {
            var check = _context.Users.FirstOrDefault(p => p.IdUser == phieuDuyet.idUser && p.MaBiMat == phieuDuyet.MaBiMat);
            if (check != null)
            {
                if (phieuDuyet.role == "TP" || phieuDuyet.role == "PP")
                {
                    if (isDuyet)
                    {
                        var phieu = _context.PhieuTrinhMuas.FirstOrDefault(p => p.IdPhieuDeNghiMua == phieuDuyet.IdPhieu);
                        phieu.IdTinhTrangPhieu = 2;
                        _context.SaveChanges();
                        return new JsonResult("truong phong duyet")
                        {
                            StatusCode = StatusCodes.Status200OK
                        };
                    }
                    else
                    {
                        var phieu = _context.PhieuTrinhMuas.FirstOrDefault(p => p.IdPhieuDeNghiMua == phieuDuyet.IdPhieu);
                        phieu.LyDo = phieuDuyet.LyDoKhongDuyet;
                        phieu.IdTinhTrangPhieu = 4;
                        _context.SaveChanges();
                        return new JsonResult("truong phong tra")
                        {
                            StatusCode = StatusCodes.Status200OK
                        };
                    }
                }
                else
                {
                    if (isDuyet)
                    {
                        var phieu = _context.PhieuTrinhMuas.FirstOrDefault(p => p.IdPhieuDeNghiMua == phieuDuyet.IdPhieu);
                        phieu.IdTinhTrangPhieu = 3;
                        var chiTiet = _context.ChiTietPhieus.Where(p => p.IdPhieuDeNghiMua == phieu.IdPhieuDeNghiMua);               
                        _context.SaveChanges();
                        return new JsonResult("lanh dao duyet")
                        {
                            StatusCode = StatusCodes.Status200OK
                        };

                    }
                    else
                    {
                        var phieu = _context.PhieuTrinhMuas.FirstOrDefault(p => p.IdPhieuDeNghiMua == phieuDuyet.IdPhieu);
                        phieu.LyDo = phieuDuyet.LyDoKhongDuyet;
                        phieu.IdTinhTrangPhieu = 5;
                        _context.SaveChanges();
                        return new JsonResult("lanh dao tra")
                        {
                            StatusCode = StatusCodes.Status200OK
                        };
                    }
                }
            }
            else
            {
                return new JsonResult("sai ma")
                {
                    StatusCode = StatusCodes.Status400BadRequest

                };

            }
        }
        // sửa phiếu mua
        public JsonResult PhieuCanSua(SuaPhieuMuaMD phieuMD)
        {
            var phieu = _context.PhieuTrinhMuas.FirstOrDefault(p => p.IdPhieuDeNghiMua == phieuMD.idPhieu && p.IdUser == phieuMD.idUser);
            if(phieu.SoLanSuaPhieu == null)
            {
                phieu.SoLanSuaPhieu =  1;
            }
            else
            {
                phieu.SoLanSuaPhieu = phieu.SoLanSuaPhieu + 1;
            }
            phieu.TimeTaoPhieu = DateTime.Now;

            phieu.LyDoSuaPhieu = phieuMD.LyDoSuaPhieu;
            phieu.TongTien = 0;
            foreach (var ct in phieuMD.ChiTietPhieus)
            {
                var phieuCT = _context.ChiTietPhieus.FirstOrDefault(p => p.IdChiTietPhieu == ct.IdChiTietPhieu);
                phieuCT.DonGia = ct.DonGia;
                phieuCT.Vat = ct.Vat;
                phieuCT.DonViCungCap = ct.DonViCungCap;

                double sum = phieu.TongTien;
                double tinhTien = 0;
                double.TryParse(ct.Vat, out double vatValue);
                if (ct.Vat == null || ct.Vat == "")
                {

                    tinhTien = (double)(ct.DonGia * ct.SoLuongMuaThem);
                }
                else
                {
                    tinhTien = (double)((ct.DonGia * ct.SoLuongMuaThem) + ((double)(ct.DonGia * ct.SoLuongMuaThem) * (vatValue / 100)));
                }
                phieu.TongTien = (double)(sum + tinhTien);
                phieu.IdTinhTrangPhieu = 1;
            }
            _context.SaveChanges();
            return new JsonResult("sua xong");
        }
        // show phiếu khong duyệt và cần sửa
        public JsonResult ShowPhieuKhongDuyet(int pg, int size, int idUser)
        {
            if (pg < 1)
            {
                pg = 1;
            }
            var allPhieuTrinhMua = _context.PhieuTrinhMuas.AsQueryable();
            allPhieuTrinhMua = allPhieuTrinhMua.Where(p => (p.IdTinhTrangPhieu == 4 || p.IdTinhTrangPhieu == 5) && p.IdUser == idUser)
                .OrderByDescending(p => p.IdPhieuDeNghiMua);
            int count = allPhieuTrinhMua.Count();
            allPhieuTrinhMua = allPhieuTrinhMua.Skip((pg - 1) * size).Take(size);
            var phieu = allPhieuTrinhMua.Select(p => new PhieuMuaBiTraMD
            {
                IdPhieuTrinhMua = p.IdPhieuDeNghiMua,
                LyDoTraPhieu = p.LyDo,
                SoLanSuaPhieu = p.SoLanSuaPhieu,
                TongTien = p.TongTien,
                TinhTrangPhieu = p.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                TimeTaoPhieu =p.TimeTaoPhieu,
            }).ToList();
            return new JsonResult(phieu, count);
        }
        public JsonResult GetPhieuSuaTra(int idPhieu , int idUser)
        {
            var phieu = _context.PhieuTrinhMuas.Include(p=>p.IdTinhTrangPhieuNavigation).FirstOrDefault(p => p.IdPhieuDeNghiMua == idPhieu && p.IdUser ==idUser);
            var result = new PhieuMuaBiTraMD
            {  
             IdPhieuTrinhMua = phieu.IdPhieuDeNghiMua,
             LyDoSuaPhieu = phieu.LyDoSuaPhieu,
             SoLanSuaPhieu= phieu.SoLanSuaPhieu,
             TimeTaoPhieu = phieu.TimeTaoPhieu,
             TongTien = phieu.TongTien,
             LyDoTraPhieu= phieu.LyDo,
             TinhTrangPhieu = phieu.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet
            };

            return new JsonResult ( result);  


        }
        // thủ kho
        // show phieu cho thủ kho nhập kho
        public JsonResult GetPhieuChoThuKho(int pg)
        {
            if (pg < 1)
            {
                pg = 1;
            }
            var allPhieuTrinhMua = _context.PhieuTrinhMuas.AsQueryable();
            allPhieuTrinhMua = allPhieuTrinhMua.Where(p => p.IdTinhTrangPhieu == 3 && p.IsNhap == true)
                .OrderByDescending(p => p.IdPhieuDeNghiMua);
            int count = allPhieuTrinhMua.Count();
            allPhieuTrinhMua = allPhieuTrinhMua.Skip((pg - 1) * 10).Take(10);
            var phieu = allPhieuTrinhMua.Select(p => new PhieuTrinhMuaMD
            {
                IdPhieuTrinhMua = p.IdPhieuDeNghiMua,          
                TinhTrangPhieu = p.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                TimeTaoPhieu = p.TimeTaoPhieu,
                TenNguoiMua= p.IdUserNavigation.HoTen,
            }).ToList();
            return new JsonResult(phieu, count);
        }
        public JsonResult XacNhanThuKho(NhapKhoMD nhapKho, Boolean isXacNhan)
        {
            if (isXacNhan)
            {

                var phieu = _context.PhieuTrinhMuas.FirstOrDefault(p => p.IdPhieuDeNghiMua == nhapKho.IdPhieu);
                phieu.IdTinhTrangPhieu = 7;
                foreach( var ct in nhapKho.ChiTietNhaps)
                {
                    var vatTu = _context.VatTus.FirstOrDefault(p=>p.IdVatTu == ct.IdVatTu);
                    vatTu.SoLuongTonKho = vatTu.SoLuongTonKho + ct.SoLuong;
                }
                _context.SaveChanges();
                var thang = phieu.TimeTaoPhieu.Month;
                _phieuPRO.updatatien(thang, phieu.TongTien);
                return new JsonResult( "Thành công");
            }
            else
            {
                var phieu = _context.PhieuTrinhMuas.FirstOrDefault(p => p.IdPhieuDeNghiMua == nhapKho.IdPhieu);
                phieu.IdTinhTrangPhieu = 7;
                _context.SaveChanges();
                return new JsonResult("trả lại");
            }
         
           
        }
        // show phiếu đã hoàn thành xong của người mua
        public JsonResult GetPhieuHoanThanh(int pg, int idUser)
        {
            var allPhieu = _context.PhieuTrinhMuas.AsQueryable();
            allPhieu = allPhieu.Where(p => p.IdTinhTrangPhieu == 7 && p.IdUser ==idUser);
            allPhieu = allPhieu.OrderByDescending(p => p.IdPhieuDeNghiMua);
            int count = allPhieu.Count();
            allPhieu = allPhieu.Skip((pg - 1) * 10).Take(10);
            var phieus = allPhieu.Select(p => new PhieuTrinhMuaMD
            {
                IdPhieuTrinhMua = p.IdPhieuDeNghiMua,
                SoLanSuaPhieu = p.SoLanSuaPhieu ?? 0,
                TongTien =p.TongTien,
                TimeTaoPhieu =p.TimeTaoPhieu,
                
            } ).ToList();
            return new JsonResult( phieus,count);

        }
        public JsonResult GetPhieuNhapKho(int pg, int idUser)
        {
            if (pg < 1)
            {
                pg = 1;
            }
            var allPhieuTrinhMua = _context.PhieuTrinhMuas.AsQueryable();
            allPhieuTrinhMua = allPhieuTrinhMua.Where(p => p.IdTinhTrangPhieu == 3 && p.IsNhap == false && p.IdUser == idUser)
                .OrderByDescending(p => p.IdPhieuDeNghiMua);
            int count = allPhieuTrinhMua.Count();
            allPhieuTrinhMua = allPhieuTrinhMua.Skip((pg - 1) * 10).Take(10);
            var phieu = allPhieuTrinhMua.Select(p => new PhieuTrinhMuaMD
            {
                IdPhieuTrinhMua = p.IdPhieuDeNghiMua,
                SoLanSuaPhieu = p.SoLanSuaPhieu ?? 0,
                TongTien = p.TongTien,
                TimeTaoPhieu = p.TimeTaoPhieu,
            }).ToList();
            return new JsonResult(phieu, count);

        }

        public JsonResult YeuCauNhapKho(int idPhieu)
        {
            var phieu = _context.PhieuTrinhMuas.FirstOrDefault(p => p.IdPhieuDeNghiMua == idPhieu);
            phieu.IsNhap = true;
            _context.SaveChanges();
            return new JsonResult("da yeu cau")
            {
                StatusCode = 200,
            };
        }
        public JsonResult XuatHoaDonTheoThang(int month, int idUser)
        {
            var phieus = _context.PhieuTrinhMuas.Where(phieu=>phieu.IdUser == idUser && phieu.TimeTaoPhieu.Month == month && phieu.IdTinhTrangPhieu==7).ToList();
            double tongTien = 0;
            var hoaDon = new HoaDonMD
            {
                TenHoaDon = "Hóa Đơn Tháng " + month.ToString(),
                TimeTao = DateTime.Now,
                TongTien = 0,
                ChiTietHoaDons = new List<ChiTietHoaDonDM>()
            };

            foreach (var phieu in phieus)
            {                
                var chiTiet = _context.ChiTietPhieus.Where(p => p.IdPhieuDeNghiMua == phieu.IdPhieuDeNghiMua).ToList();
                foreach(var ct in chiTiet)
                {
                    var chiTietHoaDon = new ChiTietHoaDonDM
                    {
                        DonGia = ct.DonGia,
                        DonViCungCap = ct.DonViCungCap,
                        DonViTinh = ct.DonViTinhDeNghi,
                        SoLuong = ct.SoLuongMuaThem,
                        TenVatTu = ct.TenVatTu,
                        Vat = ct.Vat
                    };
                    hoaDon.ChiTietHoaDons.Add(chiTietHoaDon);
                }
                tongTien = tongTien + phieu.TongTien;
            }
            hoaDon.TongTien = tongTien;
            return new JsonResult(hoaDon);
            
        }
  
    }
}
