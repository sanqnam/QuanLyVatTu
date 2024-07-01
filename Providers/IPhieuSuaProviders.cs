using MessagePack;
using System.Collections.Concurrent;
using System.Xml.Serialization;

namespace QLVT_BE.Providers
{
    public interface IPhieuSuaProviders
    {
        ConcurrentDictionary<string, List<int>> phieuSuaDictionary { get; set; }
        ConcurrentDictionary<string, int> quanTriNguoiDungDictionary { get; set; }
        ConcurrentDictionary<string, int> quanTriNguoiDungTheoPBDictionary { get; set; }
        ConcurrentDictionary<string, int> quanTriVatTuDictionary { get; set; }
        ConcurrentDictionary<int, double> quanTriTienDictionary { get; set; }
        void updata(string key, int month, string phuongThuc);
        void updatatien(int thang, double tien);


    }
    public class PhieuSuaProviders : IPhieuSuaProviders 
    {
        private readonly IPhieuVTProviders _phieuVT;

        public ConcurrentDictionary<string, List<int>> phieuSuaDictionary { get; set; } = new ConcurrentDictionary<string, List<int>>();
        public ConcurrentDictionary<string, int> quanTriNguoiDungDictionary { get; set; } = new ConcurrentDictionary<string,int>();
        public ConcurrentDictionary<string, int> quanTriNguoiDungTheoPBDictionary { get; set; } = new ConcurrentDictionary<string, int>();
        public ConcurrentDictionary<string, int> quanTriVatTuDictionary { get; set; } = new ConcurrentDictionary<string, int>();
        public ConcurrentDictionary<int, double> quanTriTienDictionary { get; set; } = new ConcurrentDictionary<int, double>();
        public PhieuSuaProviders(IPhieuVTProviders phieuVT) 
        {
            _phieuVT = phieuVT;
        }

        public void updata(string key,  int month, string phuongThuc)
        {
            if(month == 14 || month == null)
            {
                if(phuongThuc == "nguoidung")
                {
                    var old = quanTriNguoiDungDictionary[key];
                    var tonOld= quanTriNguoiDungTheoPBDictionary["TongThanhVien"];
                    quanTriNguoiDungTheoPBDictionary.TryUpdate("TongThanhVien", tonOld+1,tonOld);
                    quanTriNguoiDungDictionary.TryUpdate(key, old + 1, old);

                }
                else if(phuongThuc == "nguoidungbyphongban")
                {
                    var old = quanTriNguoiDungTheoPBDictionary[key];
                    var oldTong = quanTriNguoiDungTheoPBDictionary["TongVatTu"];
                    var newOldTong = oldTong + 1;
                    quanTriNguoiDungTheoPBDictionary.TryUpdate("TongVatTu", newOldTong, oldTong);
                    quanTriNguoiDungTheoPBDictionary.TryUpdate(key, old + 1, old);
                }
                else if(phuongThuc == "vattu")
                {
                    var old = quanTriVatTuDictionary[key];
                    quanTriVatTuDictionary.TryUpdate(key, old + 1, old);
                }
            }
            else
            {
                if (phuongThuc == "denghisua")
                {
                    var old = phieuSuaDictionary.FirstOrDefault(k=>k.Key == key).Value;
                    var oldValue = old[month - 1];
                    var newValue = oldValue + 1;
                    old[month - 1] = newValue;
                    phieuSuaDictionary[key] = old;
                }
                if(phuongThuc == "denghi")
                {
                    var old = _phieuVT.phieuVatTuDictionary.FirstOrDefault(k => k.Key == key).Value;
                    var oldValue = old[month - 1];
                    var newValue = oldValue + 1;
                    old[month - 1] = newValue;
                    _phieuVT.phieuVatTuDictionary[key] = old;
                }
            }
        }
        public void updatatien(int thang, double tien)
        {
            var oldTien = quanTriTienDictionary[thang];
            var newTien = oldTien + tien;
            quanTriTienDictionary.TryUpdate(thang, newTien, oldTien);  
        }
    }
}
