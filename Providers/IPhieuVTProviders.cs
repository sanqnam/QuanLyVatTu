using System.Collections.Concurrent;

namespace QLVT_BE.Providers
{
    public interface IPhieuVTProviders
    {
        ConcurrentDictionary<string, List<int>> phieuVatTuDictionary { get; set; }

        void UpdateData(string maPhongBan, int newCount);
        int GetDataPhongBan(string maPhongBan);
    }
    public class PhieuVTProviders : IPhieuVTProviders
    {
        public ConcurrentDictionary<string, List<int>> phieuVatTuDictionary { get; set; } = new ConcurrentDictionary<string, List<int>>();

        public int GetDataPhongBan(string maPhongBan)
        {
            var data = phieuVatTuDictionary[maPhongBan];
            return data.Sum();
        }
        public void UpdateData(string maPhongBan, int newCount)
        {
            int month = DateTime.UtcNow.Month;

            var dataPhongBan = phieuVatTuDictionary[maPhongBan];
            dataPhongBan[month - 1] = dataPhongBan[month - 1] + 1;

            var dataLanhDao = phieuVatTuDictionary["LanhDao"];
            dataLanhDao[month - 1] = dataLanhDao[month - 1] + 1;
        }
    }
}
