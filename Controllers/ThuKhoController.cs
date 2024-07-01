using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLVT_BE.Data;
using QLVT_BE.Repositorys;
using QLVT_BE.ViewModels;
using System.Drawing;

namespace QLVT_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuKhoController : ControllerBase
    {
        private readonly QuanLyVatTuContext _context;
        private readonly IThuKhoRepo _thuKhoRepo;

        public ThuKhoController(QuanLyVatTuContext context, IThuKhoRepo thuKho) 
        {
        _context = context;
        _thuKhoRepo = thuKho;
        }
        [HttpGet("GetAllVatTuTrongKho/{pg}/{size}"), Authorize(Roles = "T.Kho")]
        public IActionResult GetAllPhieuCap(int pg, int size)
        {
            return Ok(_thuKhoRepo.GetAllVatTu(pg, size));
        }
        [HttpGet("GetAllPhieuCapVatTu/{pg}/{size}"),Authorize(Roles = "T.Kho")]
        public IActionResult GetAllPhieuCapVatTu(int pg, int size)
        {
            return Ok(_thuKhoRepo.GetAllPhieuDaDuyet( pg,  size));
        }
        [HttpGet("GetAllDetailPhieuCapVatTu/{idPhieu}"),Authorize(Roles ="T.Kho")]
        public IActionResult GetAllDetailPhieuCapVatTu(int idPhieu)
        {
            return Ok(_thuKhoRepo.GetAllDetailPhieuDaDuyet(idPhieu));
        }
        [HttpGet("CapVatTu/{idVatTu}/{idUser}/{idPhieu}"), Authorize(Roles = "T.Kho")]
        public IActionResult CapVatTu(int idVatTu, int idUser, int idPhieu)
        {
            try
            {
                _thuKhoRepo.CapVatTu(idVatTu, idUser,idPhieu);
                return new JsonResult("CapThanhCong");
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet("AddMaVatTu/{idVatTu}/{idUser}/{idPhieu}"), Authorize(Roles = "T.Kho")]
        public IActionResult AddMaVatTu(int idVatTu, int idUser, int idPhieu)
        {
            try
            {
                _thuKhoRepo.AddMaVatTu(idVatTu, idUser, idPhieu);
                return new JsonResult("CapThanhCong");
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPost("AddVatTu"), Authorize(Roles = "T.Kho")]
        public IActionResult AddVatTu(VatTuVM vatTuVM)
        {
          var result = _thuKhoRepo.AddVatTu(vatTuVM);
            if(result == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else
            {
                return Ok(new JsonResult("Đã tạo tài khoản"));
            }
        
        }
        [HttpGet("GetYeuCauMuaVT/{idPhieu}"), Authorize(Roles ="T.Kho")]
        public IActionResult GetYeuCauMuaVT(int idPhieu)
        {
            return Ok(_thuKhoRepo.GetPhieuMua(idPhieu));
        }
        [HttpPost("TaoYeuCauMua"),Authorize(Roles = "T.Kho")]
        public IActionResult TaoYeuCauMua(PhieuYeuCauMuaVM phieu)
        {
            try
            {
                _thuKhoRepo.YeuCauMua(phieu);
                return new JsonResult("Tao thanh cong");
            }
            catch
            {
                return StatusCode(statusCode: 500);
            }
        }
        //[HttpPost("TaoPhieuMua"), Authorize(Roles = "T.Kho")]
        //public IActionResult TaoPhieuMua(TaoPhieuVM phieu)
        //{
        //    return Ok(_thuKhoRepo.TaoPhieuMua(phieu));
        //}

        [HttpDelete("XoaVatTuCanMua/{idQT}"), Authorize(Roles ="T.Kho")]
        public IActionResult XoaVatTuCanMua(int idQT)
        {
            _thuKhoRepo.XoaVatTuCanMua(idQT);
            return new JsonResult("xóa thành công");
        }
    }
}
