using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLVT_BE.Repositorys;
using QLVT_BE.ViewModels;

namespace QLVT_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhongBanController : ControllerBase
    {
        private readonly IPhongBanRepository _phongBan;
        public PhongBanController(IPhongBanRepository phongBanRepo)
        {
            _phongBan = phongBanRepo;
        }
        [HttpGet, Authorize(Roles = "QTri")]
        public IActionResult GetAll()
        {
            return Ok(_phongBan.GetAll());
        }
        [HttpGet("GetAllPBSua")]
        public IActionResult GetAllPBSua()
        {
            return Ok(_phongBan.GetAllPBSua());
        }
        [HttpGet("IdPhongBan/{idPhongBan}"), Authorize(Roles = "QTri")]
        public IActionResult GetById(int id)
        {
            var pb = _phongBan.GetById(id);
            if (pb == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pb);
            }
        }
        [HttpGet("Search/{search}"),Authorize(Roles ="QTri")]
        public IActionResult Search(string search)
        {
            return Ok(_phongBan.Search(search));
        }
        [HttpPost]
        public IActionResult Add (PhongBanMD phongBanMD)
        {
            var pb = _phongBan.Add(phongBanMD);
            if (pb == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else
            {
                return new JsonResult("Tạo thành công");
            }
        }
        [HttpPut("Update/{id}"),Authorize(Roles ="QTri")]
        public IActionResult Update (int id, PhongBanMD pbMD)
        {
            try{
                _phongBan.Update(id, pbMD);
                return new JsonResult("sữa thành công");
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
