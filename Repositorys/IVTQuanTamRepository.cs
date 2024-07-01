using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLVT_BE.Data;
using QLVT_BE.ViewModels;

namespace QLVT_BE.Repositorys
{
    public interface IVTQuanTamRepository
    {
        int AddToQuanTam(VatTuQuanTamMD qTamMD);
        JsonResult GetAll(int idUser);
        void Delete(int idVT);
    }
    public class VTQuanTamRepo : IVTQuanTamRepository
    {
        private readonly QuanLyVatTuContext _context;
        private readonly IConfiguration _config;

        public VTQuanTamRepo(QuanLyVatTuContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public int AddToQuanTam(VatTuQuanTamMD qTamMD)
        {
            var count = _context.VatTuQuanTams.FirstOrDefault(q => q.IdUser == qTamMD.IdUser && q.IdVatTu == qTamMD.IdVatTu);
            if (count == null)
            {
                var quanTam = new VatTuQuanTam
                {
                    IdUser = qTamMD.IdUser,
                    IdVatTu = qTamMD.IdVatTu,
                    Status = 1,
                };
                _context.Add(quanTam);
                _context.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void Delete(int idVT)
        {
            var qt = _context.VatTuQuanTams.FirstOrDefault(q =>q.IdQuanTam == idVT);
            if (qt != null)
            {
                _context.Remove(qt);
                _context.SaveChanges();
            }
        }
        // show tất cả vật tư quan để yêu cầu tạo phiếu đề nghị
        public JsonResult GetAll(int idUser)
        {
            var vatTus = _context.VatTuQuanTams.Where(vt => vt.IdUser == idUser && vt.Status ==1).Select(u => new VatTuMD
            {
                Status = u.Status,
                IdVatTu = u.IdVatTuNavigation.IdVatTu,
                TenVatTu = u.IdVatTuNavigation.TenVatTu,
                MaVatTu = u.IdVatTuNavigation.MaVatTu,
                DonViTinh = u.IdVatTuNavigation.DonViTinh,
                SoLuongTonKho = u.IdVatTuNavigation.SoLuongTonKho,              
                IdKho = u.IdVatTuNavigation.IdKho,                                                  
                ViTri = u.IdVatTuNavigation.ViTri,
                GhiChu = u.IdVatTuNavigation.GhiChu,
                ThongSo = u.IdVatTuNavigation.ThongSo,
                HinhAnhVatTus = u.IdVatTuNavigation.HinhAnhVatTus
            }).ToList();
            return new JsonResult(vatTus);
        }
    }
}
