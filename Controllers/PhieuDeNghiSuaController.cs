using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLVT_BE.Repositorys;
using QLVT_BE.Service;
using QLVT_BE.ViewModels;
using System.ComponentModel;

namespace QLVT_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhieuDeNghiSuaController : ControllerBase
    {
        private readonly IPhieuDeNghiSuaRepo _suaRepo;

        public PhieuDeNghiSuaController(IPhieuDeNghiSuaRepo suaRepo) {
            _suaRepo = suaRepo;
        }   
        [HttpPost("AddPhieuSua")]
        public IActionResult AddPhieuSua(TaoPhieuSuaVM phieu)
        {
            return Ok(_suaRepo.TaoPhieuSua(phieu));
        }
        [HttpGet("GetAllPhieu/{idPhongBan}/{idNguoiDuyet}/{pg}/{size}")]
        public IActionResult GetAllPhieu(int idPhongBan, int idNguoiDuyet, int pg, int size)
        {
            return Ok(_suaRepo.GetAllPhieu(idPhongBan, idNguoiDuyet, pg,size));
        }
        [HttpGet("GetImgByPhieu/{idChiTietPhieu}")]
        public IActionResult GetImgByPhieu(int idChiTietPhieu)
        {
            return Ok(_suaRepo.GetImgByPhieu(idChiTietPhieu));
        }
        [HttpGet("GetVatTuByPhieu/{idphieu}")]
        public IActionResult GetVatTuByPhieu(int idphieu)
        {
            return Ok(_suaRepo.GetVatTuByPhieu(idphieu));
        }

        [HttpGet("DuyetPhieuSua/{duyet}/{idPhieu}/{lyDo}/{maBiMat}")]
        public IActionResult DuyetPhieuSua(Boolean duyet, int idPhieu, string lyDo, string maBiMat)
        {
            return Ok(_suaRepo.DuyetPhieuSua(duyet,idPhieu,lyDo,maBiMat));
        }
        [HttpGet("SetDeNghiThanhLy/{idPhieu}")]
        public IActionResult SetDeNghiThanhLy( int idPhieu)
        {
            try
            {
                _suaRepo.SetDeNghiThanhLy(idPhieu);
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(500);
            }          
        }
        [HttpGet("SetSuaXong/{idPhieu}")]
        public IActionResult SetSuaXong(int idPhieu)
        {
            try
            {
                _suaRepo.SetSuaXong(idPhieu);
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(500);
            }

        }

        [HttpGet("SetNhanVienSua/{idNhanVien}/{idPhieu}/{idUserGui}")]
        public IActionResult SetNhanVienSua(int idNhanVien,int idPhieu, int idUserGui)
        {
            try
            {
                _suaRepo.SetNhanVienSua(idNhanVien,idPhieu, idUserGui);

                return new JsonResult("xong");
            }
            catch
            {
                return StatusCode(500);
            }

        }
        [HttpGet("ShowVTCanSuaByNVSua/{idUser}/{pg}/{size}")]
        public IActionResult ShowVTCanSuaByNVSua(int idUser, int pg, int size)
        {
            return Ok(_suaRepo.ShowVTCanSuaByNVSua(idUser, pg, size));
        }

        [HttpGet("AllPhieuSuaByPhongBanPhuTrach/{idPhongBan}/{role}/{pg}/{size}/{orderShort}")]
        public IActionResult AllPhieuSuaByPhongBanPhuTrach(int idPhongBan, string role, int pg, int size, int orderShort)
        {
            return Ok(_suaRepo.AllPhieuSuaByPhongBanPhuTrach(idPhongBan, role, pg,size, orderShort));
        }
        [HttpGet("GetAllStatusPhieuByPB/{idPhongBan}/{size}/{pg}/{status}")]
        public IActionResult GetAllStatusPhieuByPB(int idPhongBan, int size, int pg, int status)
        {
            return Ok(_suaRepo.GetAllStatusPhieuByPB(idPhongBan, size,pg, status));
        }
        [HttpGet("GetAllPhieuDangCho/{idUser}/{pg}/{size}/{oderSort}/{chosse}")]
        public IActionResult GetAllPhieuDangCho(int idUser, int pg, int size, int oderSort, int chosse)
        {
            return Ok(_suaRepo.GetAllPhieuDangCho(idUser, pg, size, oderSort, chosse));
        }
        [HttpGet("GetAllPhieuSussces/{idUser}/{pg}/{size}/{oderSort}/{chosse}")]
        public IActionResult GetAllPhieuSussces(int idUser, int pg, int size, int oderSort, int chosse)
        {
            return Ok(_suaRepo.GetAllPhieuSussces(idUser, pg, size, oderSort, chosse));
        }
    }
}
    