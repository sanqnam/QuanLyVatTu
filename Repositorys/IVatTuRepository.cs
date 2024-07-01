using Microsoft.AspNetCore.Mvc;
using QLVT_BE.Data;
using QLVT_BE.ViewModels;
using System.Drawing;

namespace QLVT_BE.Repositorys
{
    public interface IVatTuRepository
    {
        JsonResult Search(int pg, int size, string search);
        JsonResult GetAllVatTuSuDung(int idUser);
        JsonResult GetVatTuDangYeuCau(int idPhongBan, int pg);
        JsonResult SearchVatTuDangYeuCau(string search, int pg, int idPhongBan);
        JsonResult GetAllBySearch(int idPhongBan, string searchTen, string searchVatTu, int pg, int size);
        JsonResult GetAllByIdPhongBan(int pg, int size, int idPhongBan);


    }

    public class VatTuRepo : IVatTuRepository
    {
        private readonly QuanLyVatTuContext _context;
        private readonly IConfiguration _config;

        public VatTuRepo(QuanLyVatTuContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public JsonResult Search(int pg, int size, string search)
        {
            int count;
            var allVatTu = _context.VatTus.AsQueryable();

            if (pg < 1)
            {
                pg = 1;
            }
            if (search != null)
            {
                allVatTu = allVatTu.Where(u => u.TenVatTu.Contains(search) || u.MaVatTu.Contains(search));
                count = allVatTu.Count();
            }
            else
            {
                count = 0;
            }
            allVatTu = allVatTu.Skip((pg - 1) * size).Take(size);

            var vatTus = allVatTu.Select(u => new VatTuMD
            {
                IdVatTu = u.IdVatTu,
                TenVatTu = u.TenVatTu,
                MaVatTu = u.MaVatTu,
                DonViTinh = u.DonViTinh,
                SoLuongTonKho = u.SoLuongTonKho,
                //IdKho = u.IdKho,
                ViTri = u.ViTri,
                //GhiChu = u.GhiChu,
                ThongSo = u.ThongSo,
                HinhAnhVatTus = u.HinhAnhVatTus
            }).ToList();
            var result = new JsonResult(vatTus, count);
            return result;
        }
        // showw tất cả các vật tư sử dụng theo user
        public JsonResult GetAllVatTuSuDung(int idUser)
        {
            var vatTu = _context.ChiTietVatTus.Where(vt => vt.IdUser == idUser).Select(v => new VatTuSuDungMD
            {
                IdChiTietVatTu= v.IdChiTietVatTu,
                TenVatTu = v.IdVatTuNavigation.TenVatTu,
                MaVatTu = v.MaVatTu,
                TinhTrang = v.IdTinhTrangNavigation.TenTinhTrang,
                LichSu =v.LichSu,
            }).ToList();
            return new JsonResult(vatTu);
        }
        // show vật tư đang sử dụng search theo tên người dùng và tên vật tư
        public JsonResult GetAllBySearch(int idPhongBan, string searchTen, string searchVatTu, int pg, int size)
        {
            if(searchTen == "undefined")
            {
                searchTen = null;
            }
            if(searchVatTu == "undefined")
            {
                searchVatTu = null;
            }    
            var vtAll = _context.ChiTietVatTus.AsQueryable();
            if (pg <= 1)
            {
                pg = 1;
            }
            if (searchTen == null && searchVatTu != null)
            {
                vtAll = vtAll.Where(vt => vt.IdUserNavigation.IdPhongBan == idPhongBan && vt.IdVatTuNavigation.TenVatTu.Contains(searchVatTu));
                int count = vtAll.Count();
                vtAll = vtAll.Skip((pg - 1) * size).Take(size);

                var phieu = vtAll.Select(vt => new VatTuSuDungMD
                {
                    TenVatTu = vt.IdVatTuNavigation.TenVatTu,
                    NguoiDung = vt.IdUserNavigation.HoTen,
                    LichSu = vt.LichSu,
                    MaVatTu = vt.MaVatTu,
                    IdChiTietVatTu = vt.IdChiTietVatTu,
                    TinhTrang = vt.IdTinhTrangNavigation.TenTinhTrang
                }).ToList();
                return new JsonResult(phieu, count);
            }
            else if (searchTen != null && searchVatTu == null)
            {
                vtAll = vtAll.Where(vt => vt.IdUserNavigation.IdPhongBan == idPhongBan && vt.IdUserNavigation.HoTen.Contains(searchTen));
                int count = vtAll.Count();
                vtAll = vtAll.Skip((pg - 1) * size).Take(size);

                var phieu = vtAll.Select(vt => new VatTuSuDungMD
                {
                    TenVatTu = vt.IdVatTuNavigation.TenVatTu,
                    NguoiDung = vt.IdUserNavigation.HoTen,
                    LichSu = vt.LichSu,
                    MaVatTu = vt.MaVatTu,
                    IdChiTietVatTu = vt.IdChiTietVatTu,
                    TinhTrang = vt.IdTinhTrangNavigation.TenTinhTrang
                }).ToList();
                return new JsonResult(phieu, count);

            }
            else if (searchTen != null && searchVatTu != null)
            {
                vtAll = vtAll.Where(vt => vt.IdUserNavigation.IdPhongBan == idPhongBan &&vt.IdVatTuNavigation.TenVatTu.Contains(searchVatTu) && vt.IdUserNavigation.HoTen.Contains(searchTen));
                int count = vtAll.Count();
                vtAll = vtAll.Skip((pg - 1) * size).Take(size);

                var phieu = vtAll.Select(vt => new VatTuSuDungMD
                {
                    TenVatTu = vt.IdVatTuNavigation.TenVatTu,
                    NguoiDung = vt.IdUserNavigation.HoTen,
                    LichSu = vt.LichSu,
                    MaVatTu = vt.MaVatTu,
                    IdChiTietVatTu = vt.IdChiTietVatTu,
                    TinhTrang = vt.IdTinhTrangNavigation.TenTinhTrang
                }).ToList();
                return new JsonResult(phieu, count);
            }
            else
            {
                vtAll = vtAll.Where(vt => vt.IdUserNavigation.IdPhongBan == idPhongBan);
                int count = vtAll.Count();
                vtAll = vtAll.Skip((pg - 1) * size).Take(size);
                var phieu = vtAll.Select(vt => new VatTuSuDungMD
                {
                    TenVatTu = vt.IdVatTuNavigation.TenVatTu,
                    NguoiDung = vt.IdUserNavigation.HoTen,
                    LichSu = vt.LichSu,
                    MaVatTu = vt.MaVatTu,
                    IdChiTietVatTu = vt.IdChiTietVatTu,
                    TinhTrang = vt.IdTinhTrangNavigation.TenTinhTrang
                }).ToList();
                return new JsonResult(phieu, count);
            }
        }

        // show tất cả các vật tư đang sử dụng theo phòng ban pg và size
        public JsonResult GetAllByIdPhongBan( int idPhongBan, int pg, int size)
        {
           
            var vtAll = _context.ChiTietVatTus.AsQueryable();
            vtAll = vtAll.Where(vt => vt.IdUserNavigation.IdPhongBan == idPhongBan);
            int count = vtAll.Count();
            vtAll = vtAll.Skip((pg - 1) * size).Take(size);
            var phieu = vtAll.Select(vt => new VatTuSuDungMD
            {
                TenVatTu = vt.IdVatTuNavigation.TenVatTu,
                NguoiDung = vt.IdUserNavigation.HoTen,
                LichSu = vt.LichSu,
                MaVatTu = vt.MaVatTu,
                IdChiTietVatTu = vt.IdChiTietVatTu,
                TinhTrang = vt.IdTinhTrangNavigation.TenTinhTrang
            }).ToList();
            return new JsonResult(phieu, count);
        }

        public JsonResult GetVatTuDangYeuCau(int idPhongBan, int pg)
        {
            if(pg <= 0)
            {
                pg = 1;
            }
            var phieus = _context.PhieuDeNghiVatTus.Where(p => p.IdPhongBan == idPhongBan && p.IdTinhTrangPhieu == 1 || p.IdTinhTrangPhieu == 2 ||p.IdTinhTrangPhieu==6).ToList();
            List<AllVatTuYeuCauVM> allVatTuList = new List<AllVatTuYeuCauVM>();
            foreach (var ps in phieus)
            {
                var allVatTu = _context.ChiTietPhieus.Where(p => p.IdPhieuDeNghi == ps.IdPhieuDeNghi).Select(p => new AllVatTuYeuCauVM
                {
                    MaVatTu = p.MaVatTu,
                    IdVatTu = (int)p.IdVatTu,
                    TenVatTu = p.TenVatTu,
                    SoLuongYeuCau = p.SoLuongThayDoi != null ? p.SoLuongThayDoi : p.SoLuongDeNghi,
                    TinhTrangXuLy = p.IdTinhTrangXuLyNavigation.TenTinhTrangXuLy,
                }).ToList();
                allVatTuList.AddRange(allVatTu);
            }
            var pagedVatTuList = allVatTuList.Skip((pg - 1) * 10).Take(10).ToList();
            int count = allVatTuList.Count();
           
            return new JsonResult(pagedVatTuList, count);
        }
        public JsonResult SearchVatTuDangYeuCau(string search, int pg, int idPhongBan)
        {
            if (pg <= 1)
            {
                pg = 1;
            }
            var phieus = _context.PhieuDeNghiVatTus.Where(p => p.IdPhongBan == idPhongBan && p.IdTinhTrangPhieu == 1 || p.IdTinhTrangPhieu == 2 || p.IdTinhTrangPhieu == 6).ToList();
            List<AllVatTuYeuCauVM> allVatTuList = new List<AllVatTuYeuCauVM>();
            foreach (var ps in phieus)
            {
                var allVatTu = _context.ChiTietPhieus.Where(p => p.IdPhieuDeNghi == ps.IdPhieuDeNghi && (p.TenVatTu.Contains(search) || p.MaVatTu.Contains(search)))
                    .Select(p => new AllVatTuYeuCauVM
                {
                    MaVatTu = p.MaVatTu,
                    IdVatTu = (int)p.IdVatTu,
                    TenVatTu = p.TenVatTu,
                    SoLuongYeuCau = p.SoLuongThayDoi != null ? p.SoLuongThayDoi : p.SoLuongDeNghi,
                    TinhTrangXuLy = p.IdTinhTrangXuLyNavigation.TenTinhTrangXuLy,
                }).ToList();
                allVatTuList.AddRange(allVatTu);
            }
            var pagedVatTuList = allVatTuList.Skip((pg - 1) * 10).Take(10).ToList();
            int count = allVatTuList.Count();

            return new JsonResult(pagedVatTuList, count);

        }
        // public JsonResult GetAllChoThuKho
    }
}
