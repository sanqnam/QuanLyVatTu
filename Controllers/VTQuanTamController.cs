using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLVT_BE.Repositorys;
using QLVT_BE.ViewModels;

namespace QLVT_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VTQuanTamController : ControllerBase
    {
        private readonly IVTQuanTamRepository _QTvatTuRepo;

        public VTQuanTamController(IVTQuanTamRepository QTvatTuRepo)
        {
            _QTvatTuRepo = QTvatTuRepo;
        }


        [HttpPost("AddQuanTam"), Authorize]
        public IActionResult QuanTam(VatTuQuanTamMD qTamMD)
        {
            return Ok(_QTvatTuRepo.AddToQuanTam(qTamMD));
        }
        [HttpGet("GetAll/{id}"), Authorize] //
        public IActionResult GetAll(int id)
        {
            return Ok(_QTvatTuRepo.GetAll(id));
        }
        [HttpDelete("{id}"),Authorize]
        public IActionResult Delete(int id)
        {
            _QTvatTuRepo.Delete(id);
            return new JsonResult("đã xóa thành công");
        }
    }
}
