using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLVT_BE.Repositorys;
using QLVT_BE.ViewModels;

namespace QLVT_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class loginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepo;

        public loginController(ILoginRepository loginRepo)
        {
            _loginRepo = loginRepo;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginVM login)
        {
            return Ok(_loginRepo.Login(login));
        }

        [HttpPost("getName")]
        public IActionResult GetName(LoginVM getName)
        {
            return Ok(_loginRepo.GetName(getName));
        }
    }
}
