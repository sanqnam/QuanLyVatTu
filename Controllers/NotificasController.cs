using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLVT_BE.Repositorys;

namespace QLVT_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificasController : ControllerBase
    {
        private readonly INotificaRepo _notiRepo;

        public NotificasController(INotificaRepo notiRepo) 
        {
            _notiRepo = notiRepo;
        }
        [HttpGet("GetCountNoti/{idNhan}")]
        public int GetCountNoti(int idNhan) 
        {
            int count = _notiRepo.CountNoti(idNhan);
            return count;  
        }
        [HttpGet("GetNotiByNguoiNhan/{idNhan}")]
        public IActionResult GetNotiByNguoiNhan (int idNhan)
        {
            return Ok(_notiRepo.GetNotiById(idNhan));
        }
        [HttpGet("SetStatus/{idNoti}")]
        public IActionResult SetStatus(int idNoti)
        {
           
            _notiRepo.SetStatus(idNoti);
            return new JsonResult("set status success");
        }

    }
}
