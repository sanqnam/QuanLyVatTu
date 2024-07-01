using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLVT_BE.Data;
using QLVT_BE.Repositorys;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using QLVT_BE.ViewModels;

namespace QLVT_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        [HttpGet("/api/User/Search/{pg}/{size}/{search}"), Authorize(Roles = "QTri")] //("{pg}/{size}/{phongBan}")
        //public IActionResult GetAll(int pg, int size, int phongBan, int chucVu)
        public IActionResult Search(int pg, int size, string search)
        {
            return Ok(_userRepo.Search(pg, size, search));
        }


        [HttpGet("/api/User/GetAll/{pg}/{size}/{phongBan}/{chucVu}"), Authorize(Roles = "QTri")] //("{pg}/{size}/{phongBan}")
        //public IActionResult GetAll(int pg, int size, int phongBan, int chucVu)
        public IActionResult Get(int pg, int size, int phongBan, int chucVu)
        {
            return Ok(_userRepo.GetAll(pg, size, phongBan, chucVu));
        }
        
        [HttpGet("/api/User/{id}")]
        public IActionResult GetById(int id) 
        {
            var user = _userRepo.GetById(id);
            if(user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpGet("PhongBan/{id}")]
        public IActionResult GetByPhongBan(int id)
        {
            return Ok(_userRepo.GetByPhongBan(id));
        }
        [HttpGet("GetUserSuaByPhongBan/{id}")]
        public IActionResult GetUserSuaByPhongBan(int id)
        {
            return Ok(_userRepo.GetUserSuaByPhongBan(id));
        }
        [HttpGet("TruongPhong/{id}")]
        public IActionResult GetTruongPhong(int id)
        {
            return Ok(_userRepo.GetTruongPhong(id));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserMD user)
        {
            try
            {
                _userRepo.UpdateUser(id, user);
                return new JsonResult("Đã chỉnh sửa thông tin");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("Active/{id}"), Authorize(Roles = "QTri")]
        public IActionResult ActiveUser(int id)
        {
            try
            {
                int active = _userRepo.ActiveUser(id);
                return Ok(active);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("ResetPass/{id}"), Authorize(Roles = "QTri")]
        public IActionResult ResetPass(int id)
        {
            try
            {
                _userRepo.ResetPass(id);
                return new JsonResult("Đã đặt mật khẩu mặc định cho tài khoản");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("SetConnectionId/{id}/{connectionId}")]
        public IActionResult SetConnectionId(int id, string connectionId)
        {
            try
            {
                _userRepo.SetConnectionId(id, connectionId);
                return new JsonResult("connectedId");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("AddUser"), Authorize(Roles = "QTri")]
        public IActionResult AddUser(UserMD user)
        {
            try
            {
                int Result = _userRepo.AddUser(user);
                if(Result == 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                else
                {
                    return Ok(new JsonResult("Đã tạo tài khoản"));
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("ChangePass/{id}")]
        public IActionResult ChangePass(int id,ChangePassMD changePass)
        {
            try
            {
                _userRepo.ChangePass(id, changePass);
                return new JsonResult("Đã đổi mật khẩu");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("DoiMaBiMat/{id}")]
        public IActionResult DoiMaBiMat(int id, ChangeMaBiMatMD maBiMat)
        {
            try
            {
                return Ok( _userRepo.DoiMaBiMat(id, maBiMat));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
