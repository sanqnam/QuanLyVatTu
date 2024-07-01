using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using QLVT_BE.Data;
using QLVT_BE.ViewModels;

namespace QLVT_BE.Repositorys
{
    public interface IPhongBanRepository
    {
        List<PhongBanVM> GetAll();

        PhongBanMD GetById(int id);

        List<PhongBanVM> Search(string search);

        void Update(int id, PhongBanMD pbMD);
        int Add(PhongBanMD pbMD);
        JsonResult GetAllPBSua();
    }
    public class PhongBanRepo : IPhongBanRepository
    {
        private readonly QuanLyVatTuContext _context;
        private readonly IConfiguration _config;

        public PhongBanRepo(QuanLyVatTuContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public List<PhongBanVM> GetAll()
        {
            var phongBan = _context.PhongBans.Select(p => new PhongBanVM
            {
                IdPhongBan = p.IdPhongBan,
                MaPhongBan = p.MaPhongBan,
                TenPhongBan = p.TenPhongBan,
            });
            return phongBan.ToList();
        }
        public void Update(int id, PhongBanMD pbMD)
        {
            var pb = _context.PhongBans.SingleOrDefault(pb => pb.IdPhongBan == id);
            if (pb != null)
            {
                pb.TenPhongBan = pbMD.TenPhongBan;
                pb.MaPhongBan = pbMD.MaPhongBan;
                _context.SaveChanges();
            }

        }

        public PhongBanMD GetById(int id)
        {
            var pb = _context.PhongBans.FirstOrDefault(p => p.IdPhongBan == id);
            if (pb == null)
            {
                return null;
            }
            else
            {
                return new PhongBanMD
                {
                    TenPhongBan = pb.TenPhongBan,
                    MaPhongBan = pb.MaPhongBan,
                };
            }
        }

        public List<PhongBanVM> Search(string search)
        {
            var allPhongBan = _context.PhongBans.AsQueryable();
            if(search != null)
            {
                allPhongBan = allPhongBan.Where(pb => pb.TenPhongBan.Contains(search) || pb.MaPhongBan.Contains(search));
            }
            var _pb = allPhongBan.Select(pb => new PhongBanVM { 
            TenPhongBan = pb.TenPhongBan,
            MaPhongBan = pb.MaPhongBan,
            });
            return _pb.ToList();

        }

        public int Add(PhongBanMD pbMD)
        {
            var sl = _context.PhongBans.Where(pb=> pb.TenPhongBan == pbMD.TenPhongBan || pb.MaPhongBan ==pbMD.MaPhongBan);
            if(sl.Count()>0)
            {
                return 0;

            }
            else
            {
                var pb = new PhongBan {
                    TenPhongBan = pbMD.MaPhongBan,
                    MaPhongBan = pbMD.MaPhongBan, };
                _context.PhongBans.Add(pb);
                _context.SaveChanges();
                return 1;

            }
        }
        public JsonResult GetAllPBSua()
        {
            var pb = _context.PhongBans.Where(p => new[] { "KT&AT" }.Contains(p.MaPhongBan)).Select(p => new PhongBanVM
            {
                IdPhongBan = p.IdPhongBan,
                MaPhongBan = p.MaPhongBan,
                TenPhongBan = p.TenPhongBan
            }).ToList();
            return new JsonResult(pb);
        }

    }
}
