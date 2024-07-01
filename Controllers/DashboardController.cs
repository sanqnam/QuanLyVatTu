using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLVT_BE.Repositorys;

namespace QLVT_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepo _idaRepo;

        public DashboardController(IDashboardRepo idaRepo)
        {
            _idaRepo = idaRepo;
        }
        [HttpGet("GetDashboard/{phongBan}")]
        public IActionResult TotolPhieuVT(string phongBan)
        {
            return Ok(_idaRepo.TotolPhieuVT(phongBan));
        }
        [HttpGet("PhieuVTByMonth/{phongBan}/{month}")]
        public IActionResult PhieuVTByMonth(string phongBan, int month)
        {
            return Ok(_idaRepo.PhieuVTByMonth(phongBan,month));
        }
        [HttpGet("GetPhieuVTByPB/{phongBan}")]
        public IActionResult GetPhieuVTByPB(string phongBan )
        {
            return Ok(_idaRepo.GetPhieuVTByPB(phongBan));
        }

        [HttpGet("CountPhieuSuaByPB/{phongBan}")]
        public IActionResult CountPhieuSuaByPB(string phongBan)
        {
            return Ok(_idaRepo.CountPhieuSuaByPB(phongBan));
        }
        [HttpGet("CountPhieuSuaByMonth/{phongBan}/{month}")]
        public IActionResult CountPhieuSuaByMonth(string phongBan, int month)
        {
            return Ok(_idaRepo.CountPhieuSuaByMonth(phongBan, month));
        }
        // admin
        [HttpGet("CountNhanVienTheoChucVu/{chucVu}")]
        public IActionResult CountNhanVienTheoChucVu(string chucVu )
        {
            return Ok(_idaRepo.CountNhanVienTheoChucVu(chucVu));
        }
        [HttpGet("CountAllNhanVienTheoChucVu")]
        public IActionResult CountAllNhanVienTheoChucVu()
        {
            return Ok(_idaRepo.CountAllNhanVienTheoChucVu());
        }
        [HttpGet("CountTatCaNhanVien")]
        public IActionResult CountTatCaNhanVien()
        {
            return Ok(_idaRepo.CountTatCaNhanVien());
        }
        [HttpGet("CountNhanVienTheoPB/{phongBan}")]
        public IActionResult CountNhanVienTheoPB(string phongBan)
        {
            return Ok(_idaRepo.CountNhanVienTheoPB(phongBan));
        }
        [HttpGet("CountAllChucVu")]
        public IActionResult CountAllChucVu()
        {
            return Ok(_idaRepo.CountAllChucVu());
        }
        // phần vật tư
        [HttpGet("CountVatTuTrongPhong/{pb}")]
        public IActionResult CountVatTuTrongPhong(string pb)
        {
            return Ok(_idaRepo.CountVatTuTrongPhong(pb));
        }
        [HttpGet("CountAllVatTuSD")]
        public IActionResult CountAllVatTuSD()
        {
            return Ok(_idaRepo.CountAllVatTuSD());
        }
        // phần quản trị của thu rkho
        [HttpGet("CountAllVatTuSuDungTheoPhong")]
        public IActionResult CountAllVatTuSuDungTheoPhong()
        {
            return Ok(_idaRepo.CountAllVatTuSuDungTheoPhong());
        }
        [HttpGet("CountTongVatTuSuDung")]
        public IActionResult CountTongVatTuSuDung()
        {
            return Ok(_idaRepo.CountTongVatTuSuDung());
        }
        [HttpGet("CoutTongVatTu")]
        public IActionResult CoutTongVatTu()
        {
            return Ok(_idaRepo.CoutTongVatTu());
        }
        // quản trị tiền
        [HttpGet("TongTienTrongNam")]
        public IActionResult TongTienTrongNam()
        {
            return Ok(_idaRepo.TongTienTrongNam());
        }
        [HttpGet("TienTheoThang")]
        public IActionResult TienTheoThang()
        {
            return Ok(_idaRepo.TienTheoThang());
        }

    }
}
