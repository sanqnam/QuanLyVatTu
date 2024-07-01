using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLVT_BE.Repositorys;
using QLVT_BE.ViewModels;

namespace QLVT_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChucVuController : ControllerBase
    {
        private readonly IChucVuRepository _chucVu;
        public ChucVuController(IChucVuRepository chucVuRepo)
        {
            _chucVu = chucVuRepo;
        }
        [HttpGet("search/{search}"), Authorize(Roles = "QTri")]
        public IActionResult search(string search)
        {
            return Ok(_chucVu.search(search));
        }
        [HttpGet,Authorize(Roles = "QTri")]
        public IActionResult GetAll()
        {
            return Ok(_chucVu.GetAll());
        }

        [HttpPut("Update/{idChucVu}"), Authorize(Roles = "QTri")]
        public IActionResult Update(int idChucVu, ChucVuMD chucVuMD)
        {
            try
            {
                _chucVu.Update(idChucVu ,chucVuMD);
                return new JsonResult("da sua thanh cong");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("AddChucVu"), Authorize(Roles ="QTri")]
        public IActionResult AddchucVu(ChucVuMD chucVuMD)
        {
            try
            {
                var Result = _chucVu.AddChucVu(chucVuMD);
                if(Result == 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                else
                {
                    return new JsonResult("da them thanh cong");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("IdPhongBan"), Authorize(Roles = "QTri")]
        public IActionResult GetById(int id)
        {
            var cv = _chucVu.GetById(id);
            if (cv == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(cv);
            }
        }
    }
}
