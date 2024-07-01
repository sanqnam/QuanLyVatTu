using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using QLVT_BE.Data;
using QLVT_BE.HubSignalR;
using QLVT_BE.Providers;
using QLVT_BE.Singleton;
using QLVT_BE.ViewModels;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;

namespace QLVT_BE.Repositorys
{
    public interface IPhieuDeNghiVatTuRepository
    {
        Task<JsonResult> TaoPhieu(TaoPhieuMD phieu);
        List<PhieuDeNghiVatTuMD> GetAllPhieuDeNghi(int idPhongBan);
        //List<PhieuDeNghiVatTuMD> GetAllChiTiet(int idUsern, int idPhongBan);
        List<ChiTietPhieuDeNghiVatTuMD> GetById(int idPhieu);
        List<ChiTietPhieuDeNghiVatTuMD> GetAllPhieuChiTiet();
        //List<PhieuDeNghiVatTuMD> PhieuHoanThanh(int idUser);
        JsonResult GetAllPhieuDuyet(int pg, int size, int idPhongBan, int sortOrder, int idUser, string role);
        Task<JsonResult> TruongPhongDuyet(PhieuDuyetTruongPhongVM phieuDuyet, int idPhieu, bool duyet, int User);

        JsonResult GetAllPhieuDeNghiSapXep(int pg, int size, int idUser, int sortOrder, int status, int choose);
        JsonResult GetAllPhieuDeNghiTheoPhongBan(int pg, int size, int idPhongBan, int sortOrder , int choose);
    }
    public class PhieuDeNghiVatTuRepository : IPhieuDeNghiVatTuRepository
    {
        private readonly QuanLyVatTuContext _context;
        private readonly IConfiguration _config;
        private readonly HubFunction _hubFunction;
        private readonly IUserRepository _userRepo;
        private readonly INotificaRepo _notiRepo;
        private readonly INotificationsManager _singlNoti;
        private readonly IPhieuSuaProviders _phieuPRo;

        public PhieuDeNghiVatTuRepository(QuanLyVatTuContext context, IConfiguration config, HubFunction hubFunction, IUserRepository userRepo, INotificaRepo notiRepo,
         INotificationsManager singlNoti, IPhieuSuaProviders phieuPRo)
        {
            _context = context;
            _config = config;
            _hubFunction = hubFunction;
            _userRepo = userRepo;
            _notiRepo = notiRepo;
            _singlNoti = singlNoti;
            _phieuPRo = phieuPRo;
        }


        public List<PhieuDeNghiVatTuMD> GetAllPhieuDeNghi(int idPhongBan)
        {
            var _phieu = _context.PhieuDeNghiVatTus.Where(p => p.IdPhongBan == idPhongBan).Select(phieu => new PhieuDeNghiVatTuMD
            {
                IdPhieuDeNghi = phieu.IdPhieuDeNghi,
                IdUser = phieu.IdUser,
                HoVaTen = phieu.IdUserNavigation.HoTen,
                IdPhieuTam = phieu.IdPhieuTam,
                LyDoLapPhieu = phieu.LyDoLapPhieu,
                IdPhongBan = phieu.IdPhongBan,
                TenPhongBan = phieu.IdPhongBanNavigation.TenPhongBan,
                IdThuTruong = phieu.IdThuTruong,
                TenThuTruong = phieu.IdThuTruongNavigation.HoTen,
                IdLanhDao = phieu.IdLanhDao,
                TenLanhDao = phieu.IdLanhDaoNavigation.HoTen,
                TimeTaoPhieu = phieu.TimeTaoPhieu,
                TinhTrangPhieu = phieu.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
            }).ToList();

            return _phieu.ToList();
        }

        // showw tất cả các phiếu đề nghị vật tư kể cả phiểu đã hoàn thành
        public JsonResult GetAllPhieuDeNghiSapXep(int pg, int size, int idUser, int sortOrder, int status, int choose)
        {
            var allPhieuDeNghi = _context.PhieuDeNghiVatTus.AsQueryable();
            if (pg < 1)
            {
                pg = 1;
            }
            // status == 1 thì là show tất cả phiếu mới được tạo và đang xử lý
            if (status == 1)
            {
                if (choose == 1)
                {
                    // chosse  bằng 1 thì xếp theo phiếu yêu cầu đang chờ duyệt
                    if (idUser != 0)
                    {
                        allPhieuDeNghi = allPhieuDeNghi.Where(p => p.IdUser == idUser && (p.IdTinhTrangPhieu == 1 || p.IdTinhTrangPhieu == 2));
                    }

                    switch (sortOrder)
                    {
                        case 1:
                            allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                            break;
                        case 2:
                            allPhieuDeNghi = allPhieuDeNghi.OrderBy(p => p.TimeTaoPhieu);
                            break;
                        default:
                            allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                            break;
                    }

                    int count = allPhieuDeNghi.Count();
                    allPhieuDeNghi = allPhieuDeNghi.Skip((pg - 1) * size).Take(size);

                    var phieuDeNghi = allPhieuDeNghi.Select(phieu => new PhieuDeNghiVatTuMD
                    {
                        IdPhieuDeNghi = phieu.IdPhieuDeNghi,
                        IdUser = phieu.IdUser,
                        HoVaTen = phieu.IdUserNavigation.HoTen,
                        IdPhieuTam = phieu.IdPhieuTam,
                        LyDoLapPhieu = phieu.LyDoLapPhieu,
                        IdPhongBan = phieu.IdPhongBan,
                        TenPhongBan = phieu.IdPhongBanNavigation.TenPhongBan,
                        IdThuTruong = phieu.IdThuTruong,
                        TenThuTruong = phieu.IdThuTruongNavigation.HoTen,
                        IdLanhDao = phieu.IdLanhDao,
                        TenLanhDao = phieu.IdLanhDaoNavigation.HoTen,
                        TimeTaoPhieu = phieu.TimeTaoPhieu,

                        TinhTrangPhieu = phieu.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                    }).ToList();

                    var result = new JsonResult(phieuDeNghi, count);
                    return result;
                }
                else
                {
                    // choose bằng 2 thì show thong tin theo đang xử lý (chuyển về cho thủ kho)
                    if (idUser != 0)
                    {
                        allPhieuDeNghi = allPhieuDeNghi.Where(p => p.IdUser == idUser && p.IdTinhTrangPhieu == 6);
                    }

                    switch (sortOrder)
                    {
                        case 1:
                            allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                            break;
                        case 2:
                            allPhieuDeNghi = allPhieuDeNghi.OrderBy(p => p.TimeTaoPhieu);
                            break;
                        default:
                            allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                            break;
                    }

                    int count = allPhieuDeNghi.Count();
                    allPhieuDeNghi = allPhieuDeNghi.Skip((pg - 1) * size).Take(size);

                    var phieuDeNghi = allPhieuDeNghi.Select(phieu => new PhieuDeNghiVatTuMD
                    {
                        IdPhieuDeNghi = phieu.IdPhieuDeNghi,
                        IdUser = phieu.IdUser,
                        HoVaTen = phieu.IdUserNavigation.HoTen,
                        IdPhieuTam = phieu.IdPhieuTam,
                        LyDoLapPhieu = phieu.LyDoLapPhieu,
                        IdPhongBan = phieu.IdPhongBan,
                        TenPhongBan = phieu.IdPhongBanNavigation.TenPhongBan,
                        IdThuTruong = phieu.IdThuTruong,
                        TenThuTruong = phieu.IdThuTruongNavigation.HoTen,
                        IdLanhDao = phieu.IdLanhDao,
                        TenLanhDao = phieu.IdLanhDaoNavigation.HoTen,
                        TimeTaoPhieu = phieu.TimeTaoPhieu,

                        TinhTrangPhieu = phieu.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                    }).ToList();

                    var result = new JsonResult(phieuDeNghi, count);
                    return result;
                }
            }
            // status == 2 show tất cả phiếu đã hoàn thành
            else
            {
                if (choose == 1)
                {

                    if (idUser != 0)
                    {
                        allPhieuDeNghi = allPhieuDeNghi.Where(p => p.IdUser == idUser && p.IdTinhTrangPhieu == 7);
                    }

                    switch (sortOrder)
                    {
                        case 1:
                            allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                            break;
                        case 2:
                            allPhieuDeNghi = allPhieuDeNghi.OrderBy(p => p.TimeTaoPhieu);
                            break;
                        default:
                            allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                            break;
                    }
                    int count = allPhieuDeNghi.Count();
                    allPhieuDeNghi = allPhieuDeNghi.Skip((pg - 1) * size).Take(size);
                    var phieuDeNghi = allPhieuDeNghi.Select(phieu => new PhieuDeNghiVatTuMD
                    {
                        IdPhieuDeNghi = phieu.IdPhieuDeNghi,
                        IdUser = phieu.IdUser,
                        HoVaTen = phieu.IdUserNavigation.HoTen,
                        IdPhieuTam = phieu.IdPhieuTam,
                        LyDoLapPhieu = phieu.LyDoLapPhieu,
                        IdPhongBan = phieu.IdPhongBan,
                        TenPhongBan = phieu.IdPhongBanNavigation.TenPhongBan,
                        IdThuTruong = phieu.IdThuTruong,
                        TenThuTruong = phieu.IdThuTruongNavigation.HoTen,
                        IdLanhDao = phieu.IdLanhDao,
                        TenLanhDao = phieu.IdLanhDaoNavigation.HoTen,
                        TimeTaoPhieu = phieu.TimeTaoPhieu,
                        TimeDuyetPhieu = phieu.TimeDuyetPhieu,
                        TinhTrangPhieu = phieu.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                        SoPhieu = phieu.SoPhieu,
                    }).ToList();

                    var result = new JsonResult(phieuDeNghi, count);
                    return result;
                }
                // show tất cả các phiếu bị trả
                else
                {
                    if (idUser != 0)
                    {
                        allPhieuDeNghi = allPhieuDeNghi.Where(p => p.IdUser == idUser && p.IdTinhTrangPhieu == 4 || p.IdTinhTrangPhieu == 5);
                    }

                    switch (sortOrder)
                    {
                        case 1:
                            allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                            break;
                        case 2:
                            allPhieuDeNghi = allPhieuDeNghi.OrderBy(p => p.TimeTaoPhieu);
                            break;
                        default:
                            allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                            break;
                    }

                    int count = allPhieuDeNghi.Count();
                    allPhieuDeNghi = allPhieuDeNghi.Skip((pg - 1) * size).Take(size);

                    var phieuDeNghi = allPhieuDeNghi.Select(phieu => new PhieuDeNghiVatTuMD
                    {
                        IdPhieuDeNghi = phieu.IdPhieuDeNghi,
                        IdUser = phieu.IdUser,
                        HoVaTen = phieu.IdUserNavigation.HoTen,
                        IdPhieuTam = phieu.IdPhieuTam,
                        LyDoLapPhieu = phieu.LyDoLapPhieu,
                        IdPhongBan = phieu.IdPhongBan,
                        TenPhongBan = phieu.IdPhongBanNavigation.TenPhongBan,
                        IdThuTruong = phieu.IdThuTruong,
                        TenThuTruong = phieu.IdThuTruongNavigation.HoTen,
                        IdLanhDao = phieu.IdLanhDao,
                        TenLanhDao = phieu.IdLanhDaoNavigation.HoTen,
                        TimeDuyetPhieu = phieu.TimeDuyetPhieu,
                        TimeTaoPhieu = phieu.TimeTaoPhieu,
                        LyDoTraPhieu = phieu.LyDoTraPhieu,
                        NguoiTraPhieu = phieu.NguoiTraPhieu,
                        TinhTrangPhieu = phieu.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                    }).ToList();
                    var result = new JsonResult(phieuDeNghi, count);
                    return result;
                }

            }


        }
        public JsonResult GetAllPhieuDuyet(int pg, int size, int idPhongBan, int sortOrder, int idUser, string role)
        {

            if (role == "TP" || role == "PP")
            {
                var allPhieuDeNghi = _context.PhieuDeNghiVatTus.AsQueryable();
                //var check = _context.PhieuDeNghiVatTus

                if (pg < 1)
                {
                    pg = 1;
                }
                if (idPhongBan != 0)
                {
                    allPhieuDeNghi = allPhieuDeNghi.Where(p => p.IdThuTruong == idUser && p.IdTinhTrangPhieu == 1);

                }

                switch (sortOrder)
                {
                    case 1:
                        allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                        break;
                    case 2:
                        allPhieuDeNghi = allPhieuDeNghi.OrderBy(p => p.TimeTaoPhieu);
                        break;
                    default:
                        allPhieuDeNghi = allPhieuDeNghi.OrderBy(p => p.IdPhieuDeNghi);
                        break;
                }

                int count = allPhieuDeNghi.Count();
                allPhieuDeNghi = allPhieuDeNghi.Skip((pg - 1) * size).Take(size);

                var phieuDeNghi = allPhieuDeNghi.Select(phieu => new PhieuDeNghiVatTuMD
                {
                    IdPhieuDeNghi = phieu.IdPhieuDeNghi,
                    IdUser = phieu.IdUser,
                    HoVaTen = phieu.IdUserNavigation.HoTen,
                    IdPhieuTam = phieu.IdPhieuTam,
                    LyDoLapPhieu = phieu.LyDoLapPhieu,
                    IdPhongBan = phieu.IdPhongBan,
                    TenPhongBan = phieu.IdPhongBanNavigation.TenPhongBan,
                    IdThuTruong = phieu.IdThuTruong,
                    TenThuTruong = phieu.IdThuTruongNavigation.HoTen,
                    IdLanhDao = phieu.IdLanhDao,
                    TenLanhDao = phieu.IdLanhDaoNavigation.HoTen,
                    TimeTaoPhieu = phieu.TimeTaoPhieu,
                    TinhTrangPhieu = phieu.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                }).ToList();

                var result = new JsonResult(phieuDeNghi, count);
                return result;

            }
            else
            {
                var allPhieuDeNghi = _context.PhieuDeNghiVatTus.AsQueryable();
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
                    case 1:
                        allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                        break;
                    case 2:
                        allPhieuDeNghi = allPhieuDeNghi.OrderBy(p => p.TimeTaoPhieu);
                        break;
                    default:
                        allPhieuDeNghi = allPhieuDeNghi.OrderBy(p => p.IdPhieuDeNghi);
                        break;
                }

                int count = allPhieuDeNghi.Count();
                allPhieuDeNghi = allPhieuDeNghi.Skip((pg - 1) * size).Take(size);

                var phieuDeNghi = allPhieuDeNghi.Select(phieu => new PhieuDeNghiVatTuMD
                {
                    IdPhieuDeNghi = phieu.IdPhieuDeNghi,
                    IdUser = phieu.IdUser,
                    HoVaTen = phieu.IdUserNavigation.HoTen,
                    IdPhieuTam = phieu.IdPhieuTam,
                    LyDoLapPhieu = phieu.LyDoLapPhieu,
                    IdPhongBan = phieu.IdPhongBan,
                    TenPhongBan = phieu.IdPhongBanNavigation.TenPhongBan,
                    IdThuTruong = phieu.IdThuTruong,
                    TenThuTruong = phieu.IdThuTruongNavigation.HoTen,
                    IdLanhDao = phieu.IdLanhDao,
                    TenLanhDao = phieu.IdLanhDaoNavigation.HoTen,
                    TimeTaoPhieu = phieu.TimeTaoPhieu,
                    TinhTrangPhieu = phieu.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                }).ToList();

                var result = new JsonResult(phieuDeNghi, count);
                return result;


            }


        }
        // show tất cả phiếu đề nghị vật tư theo phòng ban
        public JsonResult GetAllPhieuDeNghiTheoPhongBan(int pg, int size, int idPhongBan, int sortOrder, int choose)
        {
            var allPhieuDeNghi = _context.PhieuDeNghiVatTus.AsQueryable();
            if (pg < 1)
            {
                pg = 1;
            }
            //chọn theo tình trạng đang chờ duyệt  /// 1 là đang chờ duyệt
            if(choose == 1)
            {
                allPhieuDeNghi = allPhieuDeNghi.Where(p => p.IdPhongBan == idPhongBan && (p.IdTinhTrangPhieu == 1 || p.IdTinhTrangPhieu == 2));
                switch (sortOrder)
                {
                    case 1:
                        allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                        break;
                    case 2:
                        allPhieuDeNghi = allPhieuDeNghi.OrderBy(p => p.TimeTaoPhieu);
                        break;
                    default:
                        allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                        break;
                }
                int count = allPhieuDeNghi.Count();
                allPhieuDeNghi = allPhieuDeNghi.Skip((pg - 1) * size).Take(size);

                var phieuDeNghi = allPhieuDeNghi.Select(phieu => new PhieuDeNghiVatTuMD
                {
                    IdPhieuDeNghi = phieu.IdPhieuDeNghi,
                    IdUser = phieu.IdUser,
                    HoVaTen = phieu.IdUserNavigation.HoTen,
                    IdPhieuTam = phieu.IdPhieuTam,
                    LyDoLapPhieu = phieu.LyDoLapPhieu,
                    IdPhongBan = phieu.IdPhongBan,
                    TenPhongBan = phieu.IdPhongBanNavigation.TenPhongBan,
                    IdThuTruong = phieu.IdThuTruong,
                    TenThuTruong = phieu.IdThuTruongNavigation.HoTen,
                    IdLanhDao = phieu.IdLanhDao,
                    TenLanhDao = phieu.IdLanhDaoNavigation.HoTen,
                    TimeTaoPhieu = phieu.TimeTaoPhieu,

                    TinhTrangPhieu = phieu.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                }).ToList();
                var result = new JsonResult(phieuDeNghi, count);
                return result;
            }
            //chọn theo tình trạng đã duyệt xong và đang xử lý
            else if (choose == 2) // 2 là đang xử lý
            {
                allPhieuDeNghi = allPhieuDeNghi.Where(p => p.IdPhongBan == idPhongBan && (p.IdTinhTrangPhieu == 6 ));
                switch (sortOrder)
                {
                    case 1:
                        allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                        break;
                    case 2:
                        allPhieuDeNghi = allPhieuDeNghi.OrderBy(p => p.TimeTaoPhieu);
                        break;
                    default:
                        allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                        break;
                }
                int count = allPhieuDeNghi.Count();
                allPhieuDeNghi = allPhieuDeNghi.Skip((pg - 1) * size).Take(size);

                var phieuDeNghi = allPhieuDeNghi.Select(phieu => new PhieuDeNghiVatTuMD
                {
                    IdPhieuDeNghi = phieu.IdPhieuDeNghi,
                    IdUser = phieu.IdUser,
                    HoVaTen = phieu.IdUserNavigation.HoTen,
                    IdPhieuTam = phieu.IdPhieuTam,
                    LyDoLapPhieu = phieu.LyDoLapPhieu,
                    IdPhongBan = phieu.IdPhongBan,
                    TenPhongBan = phieu.IdPhongBanNavigation.TenPhongBan,
                    IdThuTruong = phieu.IdThuTruong,
                    TenThuTruong = phieu.IdThuTruongNavigation.HoTen,
                    IdLanhDao = phieu.IdLanhDao,
                    TenLanhDao = phieu.IdLanhDaoNavigation.HoTen,
                    TimeTaoPhieu = phieu.TimeTaoPhieu,

                    TinhTrangPhieu = phieu.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                }).ToList();
                var result = new JsonResult(phieuDeNghi, count);
                return result;
            }
            // chon thoe tinh trạng phiếu bị trả
            else if(choose ==3) // 3 laf phiếu bị trả
            {
                allPhieuDeNghi = allPhieuDeNghi.Where(p => p.IdPhongBan == idPhongBan && (p.IdTinhTrangPhieu == 5 || p.IdTinhTrangPhieu ==4 ));
                switch (sortOrder)
                {
                    case 1:
                        allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                        break;
                    case 2:
                        allPhieuDeNghi = allPhieuDeNghi.OrderBy(p => p.TimeTaoPhieu);
                        break;
                    default:
                        allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                        break;
                }
                int count = allPhieuDeNghi.Count();
                allPhieuDeNghi = allPhieuDeNghi.Skip((pg - 1) * size).Take(size);

                var phieuDeNghi = allPhieuDeNghi.Select(phieu => new PhieuDeNghiVatTuMD
                {
                    IdPhieuDeNghi = phieu.IdPhieuDeNghi,
                    IdUser = phieu.IdUser,
                    HoVaTen = phieu.IdUserNavigation.HoTen,
                    IdPhieuTam = phieu.IdPhieuTam,
                    LyDoLapPhieu = phieu.LyDoLapPhieu,
                    IdPhongBan = phieu.IdPhongBan,
                    TenPhongBan = phieu.IdPhongBanNavigation.TenPhongBan,
                    IdThuTruong = phieu.IdThuTruong,
                    TenThuTruong = phieu.IdThuTruongNavigation.HoTen,
                    IdLanhDao = phieu.IdLanhDao,
                    TenLanhDao = phieu.IdLanhDaoNavigation.HoTen,
                    TimeTaoPhieu = phieu.TimeTaoPhieu,

                    TinhTrangPhieu = phieu.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                }).ToList();
                var result = new JsonResult(phieuDeNghi, count);
                return result;
            }
            // chọn theo phiếu đã duyệt theo và hoàn thành hết
            else // 4 là phiếu đã hoàn thành
            {
                allPhieuDeNghi = allPhieuDeNghi.Where(p => p.IdPhongBan == idPhongBan && (p.IdTinhTrangPhieu == 7));
                switch (sortOrder)
                {
                    case 1:
                        allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                        break;
                    case 2:
                        allPhieuDeNghi = allPhieuDeNghi.OrderBy(p => p.TimeTaoPhieu);
                        break;
                    default:
                        allPhieuDeNghi = allPhieuDeNghi.OrderByDescending(p => p.TimeTaoPhieu);
                        break;
                }
                int count = allPhieuDeNghi.Count();
                allPhieuDeNghi = allPhieuDeNghi.Skip((pg - 1) * size).Take(size);

                var phieuDeNghi = allPhieuDeNghi.Select(phieu => new PhieuDeNghiVatTuMD
                {
                    IdPhieuDeNghi = phieu.IdPhieuDeNghi,
                    IdUser = phieu.IdUser,
                    HoVaTen = phieu.IdUserNavigation.HoTen,
                    IdPhieuTam = phieu.IdPhieuTam,
                    LyDoLapPhieu = phieu.LyDoLapPhieu,
                    IdPhongBan = phieu.IdPhongBan,
                    TenPhongBan = phieu.IdPhongBanNavigation.TenPhongBan,
                    IdThuTruong = phieu.IdThuTruong,
                    TenThuTruong = phieu.IdThuTruongNavigation.HoTen,
                    IdLanhDao = phieu.IdLanhDao,
                    TenLanhDao = phieu.IdLanhDaoNavigation.HoTen,
                    TimeTaoPhieu = phieu.TimeTaoPhieu,

                    TinhTrangPhieu = phieu.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
                }).ToList();
                var result = new JsonResult(phieuDeNghi, count);
                return result;
            }


            
        }

        public async Task<JsonResult> TruongPhongDuyet(PhieuDuyetTruongPhongVM phieuDuyet, int idPhieu, bool duyet, int idUser)
        {                  
            var maBiMat = _context.Users.FirstOrDefault(U =>U.IdUser== idUser && U.MaBiMat == phieuDuyet.MaBiMat );
           
            // khiểm tra thử có nhập đúng mã bí mật hay khong
            if(maBiMat != null)
            {
                var idPhieuChinhThuc = _context.PhieuDeNghiVatTus.Where(p => p.IdPhongBan == phieuDuyet.IdPhongBan).Max(t => t.IdPhieuChinhThuc);
                var user = _userRepo.GetById(idUser);
                var phieu = _context.PhieuDeNghiVatTus.FirstOrDefault(p => p.IdPhieuDeNghi == idPhieu);
                var dsChiTietCu = _context.ChiTietPhieus.Where(ct => ct.IdPhieuDeNghi == phieu.IdPhieuDeNghi).ToList();
                foreach (var chiTietMoi in phieuDuyet.ChiTietPhieus)
                {
                    var ctCu = dsChiTietCu.FirstOrDefault(ct => ct.IdChiTietPhieu == chiTietMoi.IdChiTietPhieu);
                    if (chiTietMoi.SoLuongThayDoi != null)
                    {
                        ctCu.SoLuongThayDoi = chiTietMoi.SoLuongThayDoi;
                        ctCu.GhiChuDuyetPhieu = "--" + user.HoTen + " đã thay đổi số lượng" + ctCu.TenVatTu + ": từ " +
                        ctCu.SoLuongDeNghi + "->" + chiTietMoi.SoLuongThayDoi;
                    }
                    if (user.IdChucVu == 1 || user.IdChucVu == 2 && duyet == true)
                    {
                        ctCu.IdTinhTrangXuLy = 2;
                    }

                    if (duyet == false)
                    {
                        ctCu.IdTinhTrangXuLy = 5;

                    }
                    _context.SaveChanges();
                }
                if (duyet == true)
                {
                    if (user.IdChucVu >= 3 && user.IdChucVu <= 7)
                    {
                        phieu.IdTinhTrangPhieu = 2;
                        phieu.TimeDuyetPhieu = DateTime.Now;
                        _notiRepo.DeleteNoti(phieuDuyet.IdPhieuDeNghi);
                        _notiRepo.CreateNotiVatTu(phieu.IdLanhDao, phieu.IdUser, phieu.IdPhieuDeNghi, "có phiếu đề nghị vật tư mới", "phieuduyet");
                        var userName = _userRepo.GetById(phieu.IdLanhDao).Username;
                        var oldCount = _notiRepo.GetCurrentUserName(userName);
                        var newCount = oldCount + 1;
                        _singlNoti.CountNotifis.TryUpdate(userName, newCount, oldCount);
                        _hubFunction.SendNotifica(newCount, userName);

                    }
                    else
                    {
                        var maPB = _context.PhongBans.FirstOrDefault(pb => pb.IdPhongBan == phieu.IdPhongBan).MaPhongBan;
                        phieu.IdTinhTrangPhieu = 6;
                        phieu.IdPhieuChinhThuc = idPhieuChinhThuc + 1;
                        phieu.TimeDuyetPhieu = DateTime.Now;
                        phieu.SoPhieu = (idPhieuChinhThuc + 1) + "/" + maPB + "/" + phieu.TimeDuyetPhieu.Value.ToString("dd-MM-yyyy");
                        _notiRepo.DeleteNoti(phieuDuyet.IdPhieuDeNghi);
                        int idThuKho = _userRepo.GetUserByChucVu("T.Kho").IdUser;
                        var nameThuKho = _userRepo.GetUserByChucVu("T.Kho").Username;
                        int oldCountThuKho = _notiRepo.GetCurrentUserName(nameThuKho);
                        int newCountTKho = oldCountThuKho + 1;
                        var nameUser = _userRepo.GetById(phieu.IdUser).Username;
                        int oldCountUser = _notiRepo.GetCurrentUserName(nameUser);
                        int newCountUser = oldCountUser + 1;
                        _notiRepo.CreateNotiVatTu(phieu.IdUser, phieu.IdLanhDao, idPhieu, "Phiếu đề nghị vật tư đã duyệt", "choduyet");
                        await _hubFunction.SendNotifica(newCountUser, nameUser);
                        _notiRepo.CreateNotiVatTu(idThuKho, phieu.IdUser, idPhieu, "Có phiếu cấp vật tư mơi", "phieucapvattu");
                        await _hubFunction.SendNotifica(newCountTKho, nameThuKho);
                        var key = _userRepo.GetById(idUser).MaPhongBan;
                        var mount = DateTime.Now.Month;
                        if(key=="GD"|| key == "P.GD")
                        {
                            key = "LanhDao";
                        }    
                        _phieuPRo.updata(key,mount, "denghi");

                    }
                    _context.SaveChanges();
                    return new JsonResult("da duyet thanh cong")
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                else if (duyet == false)
                {
                    if (user.IdChucVu >= 3 && user.IdChucVu <= 7)
                    {
                        phieu.IdTinhTrangPhieu = 4;
                        phieu.NguoiTraPhieu = phieu.IdThuTruongNavigation.HoTen;
                        phieu.LyDoTraPhieu = phieuDuyet.LyDoTraPhieu;
                        phieu.TimeDuyetPhieu = DateTime.Now;
                        _context.SaveChanges();

                    }
                    else
                    {
                        phieu.IdTinhTrangPhieu = 5;
                        phieu.NguoiTraPhieu = phieu.IdLanhDaoNavigation.HoTen;
                        phieu.LyDoTraPhieu = phieuDuyet.LyDoTraPhieu;
                        phieu.TimeDuyetPhieu = DateTime.Now;
                        _context.SaveChanges();

                    }

                    var nameGui = _userRepo.GetById(phieu.IdUser).Username;
                    var chucVu = _userRepo.GetById(idUser).TenChucVu;
                    var oldCount = _notiRepo.GetCurrentUserName(nameGui);
                    var newCount = oldCount + 1;
                    _notiRepo.CreateNotiVatTu(phieu.IdUser, idUser, idPhieu, chucVu + " không duyệt phiếu", "phieuhoanthanh");
                    await _hubFunction.SendNotifica(newCount, nameGui);
                    _context.SaveChanges();
                    return new JsonResult(chucVu + " khong duyet")
                    {
                        StatusCode = StatusCodes.Status200OK
                    };

                }
            }
            return new JsonResult("sai ma bi mat")
            {
                StatusCode = StatusCodes.Status400BadRequest
            };


        }
        public List<ChiTietPhieuDeNghiVatTuMD> GetById(int idPhieu)
        {

            var chiTietPhieuts = _context.ChiTietPhieus.Where(ct => ct.IdPhieuDeNghi == idPhieu).Include(p => p.IdTinhTrangXuLyNavigation).
            Select(p => new ChiTietPhieuDeNghiVatTuMD
            {
                IdPhieuDeNghi = (int)p.IdPhieuDeNghi,
                IdChiTietPhieu = p.IdChiTietPhieu,
                TenVatTu = p.TenVatTu,
                MaVatTu = p.MaVatTu,
                DonViTinhDeNghi = p.DonViTinhDeNghi,
                GhiChuDuyetPhieu = p.GhiChuDuyetPhieu,
                SoLuongDeNghi = (double)(p.SoLuongThayDoi != null ? p.SoLuongThayDoi : p.SoLuongDeNghi),
                GhiChuNguoiDung = p.GhiChuNguoiDung,
                TenTinhTrangXuLy = p.IdTinhTrangXuLyNavigation.TenTinhTrangXuLy,
            }).ToList();
            return chiTietPhieuts;
        }

        public List<ChiTietPhieuDeNghiVatTuMD> GetAllPhieuChiTiet()
        {

            var chiTietPhieuts = _context.ChiTietPhieus.
            Select(p => new ChiTietPhieuDeNghiVatTuMD
            {
                IdPhieuDeNghi = (int)p.IdPhieuDeNghi,
                IdChiTietPhieu = p.IdChiTietPhieu,
                TenVatTu = p.TenVatTu,
                MaVatTu = p.MaVatTu,
                DonViTinhDeNghi = p.DonViTinhDeNghi,
                GhiChuNguoiDung = p.GhiChuNguoiDung,
                SoLuongDeNghi = p.SoLuongDeNghi,
                TenTinhTrangXuLy = p.IdTinhTrangXuLyNavigation.TenTinhTrangXuLy,
            }).ToList();
            return chiTietPhieuts;

        }




        public async Task<JsonResult> TaoPhieu(TaoPhieuMD phieu)
        {
            int phieuTam = _context.PhieuDeNghiVatTus.Count() + 1;
            var _phieu = new PhieuDeNghiVatTu
            {
                IdPhieuDeNghi = phieuTam,
                IdPhieuTam = phieuTam,
                LyDoLapPhieu = phieu.LyDoLapPhieu,
                IdUser = phieu.IdUser,
                IdPhongBan = phieu.IdPhongBan,
                IdThuTruong = phieu.IdThuTruong,
                IdLanhDao = phieu.IdLanhDao,
                TimeTaoPhieu = DateTime.Now,
                IdTinhTrangPhieu = 1
            };
            _context.Add(_phieu);
            _context.SaveChanges();

            foreach (var chiTiet in phieu.ChiTietPhieus)
            {
                var chiTietPhieu = new ChiTietPhieu()
                {
                    IdPhieuDeNghi = phieuTam,
                    IdPhieuTam = phieuTam,
                    IdVatTu = chiTiet.IdVatTu,
                    MaVatTu = chiTiet.MaVatTu,
                    TenVatTu = chiTiet.TenVatTu,
                    DonViTinhDeNghi = chiTiet.DonViTinhDeNghi,
                    SoLuongDeNghi = chiTiet.SoLuongDeNghi,
                    GhiChuNguoiDung = chiTiet.GhiChuNguoiDung,
                    IdTinhTrangXuLy = 1,
                };
                _context.ChiTietPhieus.Add(chiTietPhieu);
                _context.SaveChanges();
            }


            var thongBao = new Notification
            {
                NguoiGui = phieu.IdUser,
                NguoiNhan = phieu.IdThuTruong,
                Mess = "Có phiếu đề nghị vật tư mới ",
                Url = "phieuduyet",
                TimeTao = DateTime.Now,
                DaDoc = false,
                IdPhieu = _phieu.IdPhieuDeNghi,

            };
            _context.Notifications.Add(thongBao);
            await _context.SaveChangesAsync();
            var idTo = _userRepo.GetById(phieu.IdThuTruong).Username;
            var userName = _userRepo.GetById(phieu.IdThuTruong).Username;
            var oldCount = _notiRepo.GetCurrentUserName(userName);
            var newCount = oldCount + 1;
            _singlNoti.CountNotifis.TryUpdate(userName, newCount, oldCount);
            _hubFunction.SendNotifica(newCount, idTo);
            return new JsonResult("Đã Tạo Phiếu Đề Nghị Vật Tư");
        }
    }
}
