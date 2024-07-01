using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace QLVT_BE.HubSignalR
{
    public class PhieuVT_Hub : Hub
    {
        public async Task ThongBao(string toId, string user, string thongbao)
        {
            await Clients.Client(toId).SendAsync("ThongBaoTaoPhieu", user, thongbao);
        }
        public async Task SendMessage(string fromId, string user, string message, string toId)
        {
            //string idUser = Context.User.Identity.Name;
            //await Clients.All.SendAsync(user, message);
            await Clients.Client(toId).SendAsync("ReceiveMessage", user, message);
            await Clients.Client(fromId).SendAsync("ReceiveMessage", user, message);
            //await Clients.User("1").SendAsync("ReceiveMessage", user, message);
        }
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("a new uss");
        }
        public string GetConnectionId() => Context.ConnectionId;
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Trace.WriteLine(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
