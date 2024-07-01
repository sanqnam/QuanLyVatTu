using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QLVT_BE.HubSignalR;

namespace QLVT_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatBoxController : ControllerBase
    {
        private IHubContext<ChatHub> _hubContext;

        public ChatBoxController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }
        [HttpPost]
        public async Task SendMessage(ChatMessage message)
        {
  
            await _hubContext.Clients.All.SendAsync("messageReceivedFromApi", message);
        }
    }
}
