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
    public class NguoiMuaController : ControllerBase
    {
        private readonly INguoiMuaRepo _muaRepo;

        public NguoiMuaController(INguoiMuaRepo muaRepo)
        {
            _muaRepo = muaRepo;
        }
        [HttpGet("GetVatTuChoMua/{pg}"), Authorize(Roles ="M.Hang")]
        public IActionResult GetVatTuChoMua(int pg)
        {
            return Ok(_muaRepo.GetVatTuChoMua(pg)); 
        }
        [HttpGet("SearchVatTuChoMua/{pg}/{search}"), Authorize(Roles = "M.Hang")]
        public IActionResult SearchVatTuChoMua(int pg, string search)
        {
            return Ok(_muaRepo.SearchVatTuChoMua(pg, search));
        }
        [HttpPost("TaoPhieuMua"), Authorize(Roles = "M.Hang")]
        public IActionResult TaoPhieuMua(TaoPhieuMuaMD phieu) 
        {
            var data = _muaRepo.TaoPhieuMua(phieu);
            return Ok(new
            {
                Success = true,
                Data = data,
            });
        }
        [HttpGet("GetAllPhieuTrinhMua/{pg}/{size}/{idPhongBan}/{sortOrder}/{idUser}/{role}"), Authorize(Roles = "TP,PP,GD,PGD")]
        public IActionResult GetAllPhieuTrinhMua(int pg, int size, int idPhongBan, int sortOrder, int idUser, string role) {
            return Ok(_muaRepo.GetAllPhieuTrinhMua(pg, size, idPhongBan, sortOrder, idUser, role));
        }
        [HttpGet("GetChiTietPhieuMua/{idPhieu}"), Authorize(Roles = "TP,PP,GD,PGD,M.Hang,T.Kho")]
        public IActionResult GetChiTietPhieuMua(int  idPhieu)
        {
            var data = _muaRepo.GetChiTietPhieuMua(idPhieu);
            return Ok(data);
        }
        [HttpGet("GetPhieuById/{idPhieu}"), Authorize(Roles = "TP,PP,GD,PGD")]
        public IActionResult GetPhieuById(int idPhieu)
        {
            var data = _muaRepo.GetPhieuById(idPhieu);
            return Ok(data);
        }
        [HttpPut("DuyetPhieuMua/{isDuyet}"), Authorize(Roles = "TP,PP,GD,PGD")]
        public IActionResult DuyetPhieuMua(DuyetPhieu phieu,Boolean isDuyet)
        {
            return Ok(_muaRepo.DuyetPhieu(phieu,isDuyet));

        }
        [HttpGet("ShowPhieuKhongDuyet/{pg}/{size}/{idUser}"), Authorize(Roles = "M.Hang")]
        public IActionResult ShowPhieuKhongDuyet(int pg, int size, int idUser)
        {
            return Ok(_muaRepo.ShowPhieuKhongDuyet( pg,  size,  idUser));
        }
        [HttpPut("PhieuCanSua"), Authorize(Roles = "M.Hang")]
        public IActionResult PhieuCanSua(SuaPhieuMuaMD phieu)
        {
            return Ok(_muaRepo.PhieuCanSua(phieu));
        }
        [HttpGet("GetPhieuSuaTra/{idPhieu}/{idUser}"), Authorize(Roles = "M.Hang,T.Kho")]
        public IActionResult GetPhieuSuaTra( int idPhieu, int idUser)
        {
            return Ok(_muaRepo.GetPhieuSuaTra( idPhieu, idUser));
        }
        [HttpGet("GetPhieuHoanThanh/{pg}/{idUser}"), Authorize(Roles = "M.Hang")]
        public IActionResult GetPhieuHoanThanh(int pg, int idUser)
        {
            return Ok(_muaRepo.GetPhieuHoanThanh(pg, idUser));
        }
        // của thủ kho
        [HttpGet("GetPhieuChoThuKho/{pg}"), Authorize(Roles = "T.Kho")]
        public IActionResult GetPhieuChoThuKho(int pg)
        {
            return Ok(_muaRepo.GetPhieuChoThuKho(pg));
        }
        [HttpPut("XacNhanThuKho/{isXacNhan}"), Authorize(Roles = "T.Kho")]
        public IActionResult XacNhanThuKho(NhapKhoMD nhapKho, Boolean isXacNhan)
        {
            return Ok(_muaRepo.XacNhanThuKho(nhapKho, isXacNhan));
        }
        [HttpGet("YeuCauNhapKho/{idPhieu}"), Authorize(Roles = "M.Hang")]
        public IActionResult YeuCauNhapKho(int idPhieu)
        {
            return Ok(_muaRepo.YeuCauNhapKho(idPhieu));
        }
        [HttpGet("GetPhieuNhapKho/{pg}/{idUser}"), Authorize(Roles = "M.Hang")]
        public IActionResult GetPhieuNhapKho(int pg,int idUser)
        {
            return Ok(_muaRepo.GetPhieuNhapKho(pg,idUser));
        }
        [HttpGet("XuatHoaDonTheoThang/{month}/{idUser}"), Authorize(Roles = "M.Hang")]
        public IActionResult XuatHoaDonTheoThang(int month, int idUser)
        {
            return Ok(_muaRepo.XuatHoaDonTheoThang(month, idUser));
        }
    }
}
