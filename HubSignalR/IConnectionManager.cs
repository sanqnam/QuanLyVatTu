using System.Collections.Concurrent;

namespace QLVT_BE.HubSignalR
{
    public interface IConnectionManager
    {
        ConcurrentDictionary<string, string> ConnectedUsers { get; set; }
    }
    public class ConnectionManager : IConnectionManager
    {
        public ConcurrentDictionary<string, string> ConnectedUsers { get; set; } = new ConcurrentDictionary<string, string>();
    }
}
