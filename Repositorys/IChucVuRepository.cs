using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using QLVT_BE.Data;
using QLVT_BE.ViewModels;

namespace QLVT_BE.Repositorys
{
    public interface IChucVuRepository
    {
        List<ChucVuVM> GetAll();
        List<ChucVuVM> search(string search);
        ChucVuMD GetById(int id);

        void Update(int id, ChucVuMD chucVuMD);
        int AddChucVu(ChucVuMD chucVuMD);
        //void DeleteChucVu(int id);

    }
    public class ChucVuRepo : IChucVuRepository
    {
        private readonly QuanLyVatTuContext _context;
        private readonly IConfiguration _config;

        public ChucVuRepo(QuanLyVatTuContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public List<ChucVuVM> GetAll()
        {
            var chucVu = _context.ChucVus.Select(c => new ChucVuVM
            {
                IdChucVu = c.IdChucVu,
                MaChucVu = c.MaChuVu,
                TenChucVu = c.TenChucVu,
                //Users = p.Users.ToList(),
                //HoTen = p.User.HoTen
            });
            return chucVu.ToList();
        }

        public List<ChucVuVM> search(string search)
        {
            int count;
            var allChucVu = _context.ChucVus.AsQueryable();
            if (search != null)
            {
                allChucVu = allChucVu.Where(cv => cv.TenChucVu.Contains(search) || cv.MaChuVu.Contains(search));
                count = allChucVu.Count();
            }
            else
            {
                count = allChucVu.Count();
            }
            var chucVu = allChucVu.Select(cv => new ChucVuVM
            {
                TenChucVu = cv.TenChucVu,
                MaChucVu = cv.MaChuVu,
            }
            );
            return chucVu.ToList() ;


        }
        public ChucVuMD GetById(int id)
        {
            var cv = _context.ChucVus.FirstOrDefault(c => c.IdChucVu == id);
            if (cv == null)
            {
                return null;
            }
            else
            {
                return new ChucVuMD
                {
                    TenChucVu = cv.TenChucVu,
                    MaChucVu = cv.MaChuVu,
                };
            }
        }

        public void Update(int id, ChucVuMD chucVuMD)
        {
            var chucVu=_context.ChucVus.SingleOrDefault(cv=> cv.IdChucVu==id);
            if(chucVu != null)
            {
                chucVu.TenChucVu = chucVuMD.TenChucVu;
                chucVu.MaChuVu = chucVuMD.MaChucVu;
                _context.SaveChanges();
            }
            
        }

        public int AddChucVu(ChucVuMD chucVuMD)
        {
            var sl= _context.ChucVus.Where(cv=>cv.TenChucVu==chucVuMD.TenChucVu||cv.MaChuVu == chucVuMD.MaChucVu);
            if (sl.Count()>0)
            {
                return 0;
            }
            else 
            {
                var cv = new ChucVu
                {
                    TenChucVu = chucVuMD.TenChucVu,
                    MaChuVu = chucVuMD.MaChucVu,
                };
                _context.ChucVus.Add(cv);
                _context.SaveChanges();
                return 1;
            }
            
        }

        //public void DeleteChucVu(int id)
        //{
        //    var cv =_context.ChucVus.SingleOrDefault(cv => cv.IdChucVu==id);
        //    if(cv != null){
                
        //    }
        //}
    }
}
