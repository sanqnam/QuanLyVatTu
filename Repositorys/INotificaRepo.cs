using Azure.Core.Pipeline;
using QLVT_BE.Data;
using QLVT_BE.HubSignalR;
using QLVT_BE.Singleton;
using QLVT_BE.ViewModels;
using System.Xml.Serialization;

namespace QLVT_BE.Repositorys
{
    public interface INotificaRepo
    {
        List<NotificationMD> GetNotiById(int IdNhan);
        int CountNoti(int idNhan);
        int GetCurrentUserName(string userName);
        void SetStatus(int idNoti);
        void DeleteNoti(int idPhieu);
        void CreateNotiVatTu(int idNhan, int idGui, int idPhieu, string contentTB, string url);
        int CountNotiByFalse(int idNhan);
    }
    public class NotificaRepo : INotificaRepo
    {
        private readonly QuanLyVatTuContext _context;
        private readonly INotificationsManager _notiCounts;
        private readonly IUserRepository _uerRepo;
        private readonly HubFunction _hubFunc;
        private readonly INotificationsManager _notiMana;

        public NotificaRepo(QuanLyVatTuContext context, INotificationsManager notiCounts, IUserRepository uerRepo, HubFunction hubFunc, INotificationsManager notiMana)
        {
            _context = context;
            _notiCounts = notiCounts;
            _uerRepo = uerRepo;
            _hubFunc = hubFunc;
            _notiMana = notiMana;
        }
        public int GetCurrentUserName(string userName)
        {
            var clients = _notiCounts.CountNotifis.Where(pair => pair.Key == userName).Select(pair => pair.Value);
            int counts = clients.FirstOrDefault();
            return counts;
        }
        public int CountNoti(int idNhan)
        {
            var userName = _uerRepo.GetById(idNhan).Username;
            int oldCount = GetCurrentUserName(userName);
            if (oldCount != 0)
            {
                return oldCount;
            }
            else
            {
                int counts = CountNotiByFalse(idNhan);
                _notiCounts.CountNotifis.TryAdd(userName, counts);
                return counts;
            }
        }
        // xóa thông báo theo idUser vvà idPhieu
        public void DeleteNotiByIdNhanAndIdPhieu(int idNhan, int idPhieu)
        {
            var noti = _context.Notifications.FirstOrDefault(n => n.IdPhieu == idPhieu && n.NguoiNhan == idNhan);
            if (noti != null)
            {
                _context.Remove(noti);
            }

        }

        public List<NotificationMD> GetNotiById(int IdNhan)
        {
            var Notifis = _context.Notifications.Where(n => n.NguoiNhan == IdNhan).OrderByDescending(n => n.TimeTao).Select(n => new NotificationMD
            {
                IdNoti = n.IdNoti,
                DaDoc = n.DaDoc,
                Mess = n.Mess,
                NameGui = n.NguoiGuiNavigation.HoTen,
                NameNhan = n.NguoiNhanNavigation.HoTen,
                TimeTao = n.TimeTao,
                Url = n.Url,
            }).ToList();
            return Notifis;
        }
        //  đếm tất cả thông báo với tình trạng chưa đọc
        public int CountNotiByFalse(int idNhan)
        {
            var noti = _context.Notifications.Where(n => n.NguoiNhan == idNhan && n.DaDoc == false).Count();
            if (noti != null)
            {
                return noti;
            }
            else
            {
                return 0;
            }
        }
        public async void SetStatus(int idNoti)
        {
            var noti = _context.Notifications.FirstOrDefault(n => n.IdNoti == idNoti);
            noti.DaDoc = true;
            _context.SaveChanges();
            var idTo = _uerRepo.GetById(noti.NguoiNhan).Username;
            var count = CountNotiByFalse(noti.NguoiNhan);
            var oldCount = count + 1;
            _notiMana.CountNotifis.TryUpdate(idTo, count, oldCount);
            await _hubFunc.SendNotifica(count, idTo);
        }
        public void DeleteNoti(int idPhieu)
        {
            var noti = _context.Notifications.FirstOrDefault(n => n.IdPhieu == idPhieu);
            _context.Remove(noti);
            _context.SaveChanges();
        }
        public void CreateNotiVatTu(int idNhan, int idGui, int idPhieu, string contentTB, string url)
        {
            var noti = new Notification
            {
                DaDoc = false,
                Mess = contentTB,
                IdPhieu = idPhieu,
                NguoiGui = idGui,
                NguoiNhan = idNhan,
                TimeTao = DateTime.Now,
                Url = url,
            };
            _context.Add(noti);
            _context.SaveChanges();
        }

    }
}
