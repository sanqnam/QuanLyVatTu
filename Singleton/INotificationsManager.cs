using System.Collections.Concurrent;

namespace QLVT_BE.Singleton
{
    public interface INotificationsManager
    {
        ConcurrentDictionary<string, int> CountNotifis { get; set; }
    }
    public class NotificationsManager : INotificationsManager
    {
        public ConcurrentDictionary<string, int> CountNotifis { get; set; } = new ConcurrentDictionary<string, int>();
    }
}