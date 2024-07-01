using Microsoft.AspNetCore.SignalR;
using QLVT_BE.Data;
using QLVT_BE.Singleton;
using System.Diagnostics;

namespace QLVT_BE.HubSignalR
{
    public class ChatHub : Hub
    {
        public IConnectionManager _connectionManager;
        private readonly INotificationsManager _singleNoti;

        public ChatHub(IConnectionManager connectionManager, INotificationsManager singleNoti)
        {
            _connectionManager = connectionManager;
            _singleNoti = singleNoti;
        }

        public async Task SendMessage(string fromId, string user, string message, string toId)
        {
            var toUser = GetClientsWithUserID(toId);
            var formUser = GetClientsWithUserID(fromId);
            await Clients.Clients(toUser).SendAsync("ReceiveMessage", user, message);
            await Clients.Clients(formUser).SendAsync("ReceiveMessage", user, message);
        }
        public override Task OnConnectedAsync()
        {
            string username = Context.User.Identity.Name;
            _connectionManager.ConnectedUsers.TryAdd(Context.ConnectionId, username);
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Trace.WriteLine(Context.ConnectionId);
            _connectionManager.ConnectedUsers.TryRemove(Context.ConnectionId, out _);
            if (GetClientsWithUserID(Context.User.Identity.Name).Count() ==0)
            {
                _singleNoti.CountNotifis.TryRemove(Context.User.Identity.Name, out _);
            }
            await base.OnDisconnectedAsync(exception);
        }
        public IEnumerable<string> GetClientsWithUserID(string AccountID)   
        {
            var clients = _connectionManager.ConnectedUsers.Where(pair => pair.Value == AccountID).Select(pair => pair.Key);
            return clients;
        }
    }

}
