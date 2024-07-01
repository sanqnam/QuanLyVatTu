using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLVT_BE.Data;
using QLVT_BE.Providers;
using QLVT_BE.ViewModels;

namespace QLVT_BE.Repositorys
{
    public interface IThuKhoRepo
    {
        JsonResult GetAllVatTu(int pg, int size);
        JsonResult GetAllDetailPhieuDaDuyet(int idPhieu);
        JsonResult GetAllPhieuDaDuyet(int pg, int size);
        void CapVatTu(int idVatTu, int idUser, int idPhieu);
        int AddVatTu(VatTuVM vatTuVM);
        JsonResult TaoPhieuMua(TaoPhieuVM phieu);
        void XoaVatTuCanMua(int idQuanTam);
        void YeuCauMua(PhieuYeuCauMuaVM phieu);
        ChiTietMuaMD GetPhieuMua(int idPhieu);
        JsonResult AddMaVatTu(int idVatTu, int idUser, int idPhieu);
    }
    public class ThuKhoRepo : IThuKhoRepo
    {
        private readonly QuanLyVatTuContext _context;
        private readonly IPhieuSuaProviders _phieuPRO;
        private readonly IUserRepository _userR;
        private readonly INotificaRepo _notiRe;

        public ThuKhoRepo(QuanLyVatTuContext context , IPhieuSuaProviders phieuPRO, IUserRepository userR,INotificaRepo notiRe)
        {
            _context = context;
            _phieuPRO = phieuPRO;
            _userR = userR;
            _notiRe = notiRe;
        }
        // showw tất cả các phiếu đề nghị cấp vật tư
        public JsonResult GetAllPhieuDaDuyet(int pg, int size)
        {
            if (pg < 1)
            {
                pg = 1;
            }
            var phieu = _context.PhieuDeNghiVatTus.Where(p => p.IdTinhTrangPhieu == 6).Select(p => new PhieuCapVatTuMD
            {
                IdPhieuDeNghi = p.IdPhieuDeNghi,
                HoVaTen = p.IdUserNavigation.HoTen,
                IdUser = p.IdUser,
                TenPhongBan = p.IdPhongBanNavigation.TenPhongBan,
                TinhTrangPhieu = p.IdTinhTrangPhieuNavigation.TenTinhTrangDuyet,
            });
            int count = phieu.Count();
            phieu = phieu.OrderByDescending(p => p.IdPhieuDeNghi).Skip((pg - 1) * size).Take(size);

            var result = phieu.ToList();
            return new JsonResult(result, count);
        }
        // show tất cả các chi tiết vật tư trong 1 phiếu đề nghị cấp vật tư
        public JsonResult GetAllDetailPhieuDaDuyet(int idPhieu)
        {
            var phieu = _context.ChiTietPhieus.Where(p => p.IdPhieuDeNghi == idPhieu && p.IdTinhTrangXuLy != 5).Select(p => new ChiTietPhieuCapMD
            {
                IdUser = p.IdPhieuDeNghiNavigation.IdUser,
                IdChiTietPhieu = p.IdChiTietPhieu,
                MaVatTu = p.MaVatTu,
                IdVatTu = (int)p.IdVatTu,
                SoLuongTon = p.IdVatTuNavigation.SoLuongTonKho,
                SoLuongYeuCau = p.SoLuongThayDoi != null ? (double)p.SoLuongThayDoi : (double)p.SoLuongDeNghi,
                TenVatTu = p.TenVatTu,
                TinhTrangXuLy = p.IdTinhTrangXuLyNavigation.TenTinhTrangXuLy,
            }).ToList();
        var result = phieu;
            return new JsonResult(result);
    }
    // tạo yêu cầu mua vật tư
    public void YeuCauMua(PhieuYeuCauMuaVM phieu)
    {
        var _phieu = _context.ChiTietPhieus.FirstOrDefault(p => p.IdChiTietPhieu == phieu.IdChiTietPhieu);
        _phieu.SoLuongMuaThem = phieu.SoLuongMuaThem;
        if (phieu.GiChuMuaThem != null)
        {
            _phieu.GiChuMuaThem = phieu.GiChuMuaThem;
        }
        _context.SaveChanges();
    }
    // show chi tiết phiếu vật tư cần phải mua
    public ChiTietMuaMD GetPhieuMua(int idPhieu)
    {
        var phieu = _context.ChiTietPhieus.Include(p => p.IdVatTuNavigation).FirstOrDefault(p => p.IdChiTietPhieu == idPhieu);
        var totalCounts = _context.ChiTietPhieus.Where(p => p.IdVatTu == phieu.IdVatTu && p.SoLuongMuaThem != null && p.DonGia == null).Sum(p => p.SoLuongMuaThem);
        var soLuong = phieu.SoLuongThayDoi != null ? (double)phieu.SoLuongThayDoi : (double)phieu.SoLuongDeNghi;

        var showPhieu = new ChiTietMuaMD
        {
            SoLuongChoMua = totalCounts,
            IdChiTietPhieu = phieu.IdChiTietPhieu,
            TenVatTu = phieu.TenVatTu,
            MaVatTu = phieu.MaVatTu,
            SoLuongCanMua = (double)( soLuong- phieu.IdVatTuNavigation.SoLuongTonKho ),
            SoLuongDeNghi = phieu.SoLuongDeNghi,
            SoLuongTonKho = (double)phieu.IdVatTuNavigation.SoLuongTonKho,
        };
        return showPhieu;
    }
    // cấp vật tư yêu cầu cho user

    public void CapVatTu(int idVatTu, int idUser, int idPhieu)
    {
        var vaTu = _context.VatTus.SingleOrDefault(vt => vt.IdVatTu == idVatTu);
        if (vaTu != null)
        {
            var phieu = _context.ChiTietPhieus.SingleOrDefault(p => p.IdChiTietPhieu == idPhieu);
            phieu.IdTinhTrangXuLy = 5;
            if (phieu.SoLuongThayDoi != null)
            {
                vaTu.SoLuongTonKho = vaTu.SoLuongTonKho - phieu.SoLuongThayDoi;
            }
            else
            {
                vaTu.SoLuongTonKho = vaTu.SoLuongTonKho - phieu.SoLuongDeNghi;
            }
            _context.SaveChanges();
            var setupPhieu = _context.ChiTietPhieus.Where(p => p.IdTinhTrangXuLy != 5 && p.IdPhieuDeNghi == phieu.IdPhieuDeNghi);
            if (setupPhieu.Count() == 0)
            {
                var phieuDeNghi = _context.PhieuDeNghiVatTus.SingleOrDefault(p => p.IdPhieuDeNghi == phieu.IdPhieuDeNghi);
                phieuDeNghi.IdTinhTrangPhieu = 7;
                  
                _context.SaveChanges();
            }
  
                var key = _userR.GetById(idUser).MaPhongBan;
                _phieuPRO.updata(key, 14, "vattu");
        }
    }
    //add mã vật tư cần quản lý khi cấp
    public JsonResult AddMaVatTu(int idVatTu, int idUser, int idPhieu)
    {
        var vaTu = _context.VatTus.SingleOrDefault(vt => vt.IdVatTu == idVatTu);
        var maVt = vaTu.MaVatTu.Substring(0, 4);
        var phieu = _context.ChiTietPhieus.SingleOrDefault(p => p.IdChiTietPhieu == idPhieu);
        var soPhieu = _context.PhieuDeNghiVatTus.FirstOrDefault(o => o.IdPhieuDeNghi == phieu.IdPhieuDeNghi).SoPhieu;

        int v = (int)(phieu.SoLuongThayDoi != null ? phieu.SoLuongThayDoi : phieu.SoLuongDeNghi);
        for (int i = 0; i < v; i++)
        {
            var detailVT = new ChiTietVatTu
            {
                IdVatTu = vaTu.IdVatTu,
                MaVatTu = maVt + "/" + soPhieu,
                IdTinhTrang = 1,
                LichSu = "",
                IdUser = idUser,
            };
            _context.Add(detailVT);
        }
        phieu.IdTinhTrangXuLy = 5;
        if (phieu.SoLuongThayDoi != null)
        {
            vaTu.SoLuongTonKho = vaTu.SoLuongTonKho - phieu.SoLuongThayDoi;
        }
        else
        {
            vaTu.SoLuongTonKho = vaTu.SoLuongTonKho - phieu.SoLuongDeNghi;
        }
        _context.SaveChanges();
        var setupPhieu = _context.ChiTietPhieus.Where(p => p.IdTinhTrangXuLy != 5 && p.IdPhieuDeNghi == phieu.IdPhieuDeNghi);
        if (setupPhieu.Count() == 0)
        {
            var phieuDeNghi = _context.PhieuDeNghiVatTus.SingleOrDefault(p => p.IdPhieuDeNghi == phieu.IdPhieuDeNghi);
            phieuDeNghi.IdTinhTrangPhieu = 7;
            _context.SaveChanges();
        }
            var key = _userR.GetById(idUser).MaPhongBan;
            _phieuPRO.updata(key, 14, "vattu");
            return new JsonResult("Cap ma thanh cong");
    }
    // xóa vật tư mua
    public void XoaVatTuCanMua(int idQuanTam)
    {
        var find = _context.VatTuQuanTams.FirstOrDefault(vt => vt.IdQuanTam == idQuanTam);
        _context.Remove(find);
    }
    // tạo phiếu mua vật tư
    public JsonResult TaoPhieuMua(TaoPhieuVM phieu)
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
        return new JsonResult("đã tạo phiếu mua");
    }
    // thêm vật tư mới vào kho
    public int AddVatTu(VatTuVM vatTuVM)
    {
        var check = _context.VatTus.SingleOrDefault(vt => vt.TenVatTu == vatTuVM.TenVatTu || vt.MaVatTu == vatTuVM.MaVatTu);
        if (check == null)
        {
            var vatTu = new VatTu
            {
                TenVatTu = vatTuVM.TenVatTu,
                MaVatTu = vatTuVM.MaVatTu,
                DonViTinh = vatTuVM.DonViTinh,
                GhiChu = vatTuVM.GhiChu,
                ViTri = vatTuVM.ViTri,
                SoLuongTonKho = vatTuVM.SoLuongTonKho,
                ThongSo = vatTuVM.ThongSo,
            };
            _context.Add(vatTu);
            _context.SaveChanges();
            return 1;
        }
        else
        {
            return 0;
        }
    }

    // show tất cả các vật tư trong kho
    public JsonResult GetAllVatTu(int pg, int size)
    {
        if (pg < 1)
        {
            pg = 1;
        }

        var vatTu = _context.VatTus.Select(vt => new VatTuMD
        {
            IdVatTu = vt.IdVatTu,
            DonViTinh = vt.DonViTinh,
            GhiChu = vt.GhiChu,
            IdKho = vt.IdKho,
            MaVatTu = vt.MaVatTu,
            SoLuongTonKho = vt.SoLuongTonKho,
            TenVatTu = vt.TenVatTu,
            ThongSo = vt.ThongSo,
            ViTri = vt.ViTri
        });

        int count = vatTu.Count();

        vatTu = vatTu.OrderBy(vt => vt.IdVatTu).Skip((pg - 1) * size).Take(size);

        var result = vatTu.ToList();
        return new JsonResult(result, count);
    }

}

    
}
