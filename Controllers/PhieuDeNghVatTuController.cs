using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLVT_BE.Repositorys;
using QLVT_BE.ViewModels;
using System.Data;

namespace QLVT_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhieuDeNghVatTuController : ControllerBase
    {
        private readonly IPhieuDeNghiVatTuRepository _phieuDN;

        public PhieuDeNghVatTuController(IPhieuDeNghiVatTuRepository phieuDN)
        {
            _phieuDN = phieuDN;
        }

        [HttpPost("TaoPhieu")]
        public async Task<IActionResult> TaoPhieu(TaoPhieuMD phieu )
        {
            var data = _phieuDN.TaoPhieu(phieu);
            return Ok(new
            {
                Success = true,
                Data = data,
            });
        }
        //[HttpGet("DsPhieu/{idUser}/{idPhongBan}")]
        //public IActionResult GetAll(int idUser, int idPhongBan)
        //{
        //    return new JsonResult(_phieuDN.GetAll(idUser, idPhongBan));
        //}
        [HttpGet("DsPhieu/{idPhongBan}")]
        public IActionResult GetAllPhieuDeNghi( int idPhongBan)
        {
            return new JsonResult(_phieuDN.GetAllPhieuDeNghi( idPhongBan));
        }
        [HttpGet("ChiTietPhieu/{idPhieu}")]
        public IActionResult GetById(int idPhieu)
        {
            return new JsonResult(_phieuDN.GetById(idPhieu));
        }
        [HttpGet("ChiTietPhieu")]
        public IActionResult GetAllPhieuChiTiet()
        {
            return new JsonResult(_phieuDN.GetAllPhieuChiTiet());
        }

        [HttpPut("DuyetPhieu/{idPhieu}/{duyet}/{idUser}")]
        public IActionResult DuyetPhieu(PhieuDuyetTruongPhongVM phieuDuyet, int idPhieu, bool duyet, int idUser)
        {

                return Ok(_phieuDN.TruongPhongDuyet(phieuDuyet, idPhieu, duyet, idUser));
           
        }
        [HttpGet("GetAllSapXep/{pg}/{size}/{idPhongBan}/{sortOrder}/{status}/{choose}")]
        public IActionResult GetAllSapXep(int pg, int size, int idPhongBan, int sortOrder, int status, int choose)
        {
            return Ok(_phieuDN.GetAllPhieuDeNghiSapXep(pg,size, idPhongBan, sortOrder, status, choose));
        }
        [HttpGet("GetAllPhieuDuyet/{pg}/{size}/{idPhongBan}/{sortOrder}/{idUser}/{role}"), Authorize(Roles = "PP,TP,GD,PGD")]
        public IActionResult GetAllPhieuDuyet(int pg, int size, int idPhongBan, int sortOrder, int idUser, string role)
        {
            return Ok(_phieuDN.GetAllPhieuDuyet(pg, size, idPhongBan, sortOrder, idUser, role));
        }
        [HttpGet("GetAllSapXepTheoPhongBan/{pg}/{size}/{idPhongBan}/{sortOrder}/{choose}")]
        public IActionResult GetAllSapXepTheoPhongBan(int pg, int size, int idPhongBan, int sortOrder, int choose)
        {
            return Ok(_phieuDN.GetAllPhieuDeNghiTheoPhongBan(pg, size, idPhongBan, sortOrder, choose));
        }
            
    }
}
