using Microsoft.AspNetCore.SignalR;
using QLVT_BE.Providers;
using System.Numerics;

namespace QLVT_BE.HubSignalR
{
    public class Dashboard
    {
        private readonly IHubContext<ChatHub> _hub;
        private readonly IPhieuVTProviders _IPhieuPr;
        private readonly IConnectionManager _IConnext;
        private readonly HubFunction _hubFun;

        public Dashboard(IHubContext<ChatHub> hubContext, IPhieuVTProviders IPhieuPr,IConnectionManager IConnext, HubFunction hubFun) 
        {
            _hub = hubContext;
            _IPhieuPr = IPhieuPr;
            _IConnext = IConnext;
            _hubFun = hubFun;
        }

        public async Task SendPhieuVatTu(string userName, string PhongBan)
        {
            var name = _hubFun.GetClientsWithUserID(userName);
            int count = GetPhieuByPhongBan(PhongBan);
            await _hub.Clients.Clients(name).SendAsync("ReceiveTongPhieuVT", count);
        }
        public int GetPhieuByPhongBan(string PhongBan)
        {
            var count = _IPhieuPr.phieuVatTuDictionary.Where(k => k.Key == PhongBan).SelectMany(v => v.Value);
            return count.Sum();
        }
    }
}
