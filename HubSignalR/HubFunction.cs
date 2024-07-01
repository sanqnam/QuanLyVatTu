using Microsoft.AspNetCore.SignalR;
using QLVT_BE.Data;

namespace QLVT_BE.HubSignalR
{
    public class HubFunction
    {
        private readonly IConnectionManager _connectionManager;
        private readonly IHubContext<ChatHub> _hubContext;
        public HubFunction(IHubContext<ChatHub> hubContext, IConnectionManager connectionManager) 
        {
            _hubContext = hubContext;
            _connectionManager = connectionManager;
        }
        public async Task SendNotifica(int count, string toId)
        {
            var toUser = GetClientsWithUserID(toId);
            await _hubContext.Clients.Clients(toUser).SendAsync("ReceiveNotifica", count);
        }
        public IEnumerable<string> GetClientsWithUserID(string AccountID)
        {
            var clients = _connectionManager.ConnectedUsers.Where(pair => pair.Value == AccountID).Select(pair => pair.Key);
            return clients;
        }
    }
}
