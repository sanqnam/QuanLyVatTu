using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLVT_BE.Repositorys;
using QLVT_BE.ViewModels;
using System.Diagnostics.Metrics;

namespace QLVT_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatTuController : ControllerBase
    {
        private readonly IVatTuRepository _vatTuRepo;

        public VatTuController(IVatTuRepository vatTuRepo)
        {
            _vatTuRepo = vatTuRepo;
        }
        [HttpGet("/api/VatTu/Search/{pg}/{size}/{search}"), Authorize] //("{pg}/{size}/{phongBan}")
        public IActionResult Search(int pg, int size, string search)
        {
            return Ok(_vatTuRepo.Search(pg, size, search));
        }
        [HttpGet("VatTuSuDung/{idUser}")]
        public IActionResult GetVatTuSuDung(int idUser)
        {
            return Ok(_vatTuRepo.GetAllVatTuSuDung(idUser));
        }
        [HttpGet("GetVatTuDangYeuCau/{idPhongBan}/{pg}")]
        public IActionResult GetVatTuDangYeuCau(int idPhongBan, int pg)
        {
            return Ok(_vatTuRepo.GetVatTuDangYeuCau(idPhongBan, pg));
        }
        [HttpGet("SearchVatTuDangYeuCau/{search}/{pg}/{idPhongBan}")]
        public IActionResult SearchVatTuDangYeuCau (string search, int pg, int idPhongBan)
        {
            return Ok(_vatTuRepo.SearchVatTuDangYeuCau(search,pg, idPhongBan));
        }

        // gọi all vật tư đang sử dụng theo phòng ban
        [HttpGet("GetAllBySearch/{idPhongBan}/{searchTen}/{searchVatTu}/{pg}/{size}")]
        public IActionResult GetAllBySearch(int idPhongBan, string searchTen, string searchVatTu, int pg, int size)
        {
            return Ok(_vatTuRepo.GetAllBySearch(idPhongBan, searchTen, searchVatTu, pg, size));
        }
        [HttpGet("GetAllByIdPhongBan/{idPhongBan}/{pg}/{size}")]
        public IActionResult GetAllByIdPhongBan(int idPhongBan, int pg, int size)
        {
            return Ok(_vatTuRepo.GetAllByIdPhongBan(idPhongBan, pg, size));
        }


    }
}
