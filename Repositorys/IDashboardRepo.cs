using QLVT_BE.Data;
using QLVT_BE.HubSignalR;
using QLVT_BE.Providers;
using System;
using System.Collections.Specialized;

namespace QLVT_BE.Repositorys
{
    public interface IDashboardRepo
    {
        int TotolPhieuVT(string phongBan);
        int PhieuVTByMonth(string PhongBan, int month);
        IEnumerable<int> GetPhieuVTByPB(string pB);
        int CountAllPhieuVT();
        int CountPhieuSuaByPB(string pB);
        int CountPhieuSuaByMonth(string pB, int month);
        //admin
        int CountNhanVienTheoChucVu(string chucVu);
        List<KeyValuePair<string, int>> CountAllNhanVienTheoChucVu();
        int CountTatCaNhanVien();
        int CountNhanVienTheoPB(string phongBan);
        int CountAllChucVu();
        // phần vật tư
        int CountAllVatTuSD();
        int CountVatTuTrongPhong(string pb);
        // phàn quả trị của thủ kho
        List<KeyValuePair<string, int>> CountAllVatTuSuDungTheoPhong();
        int CountTongVatTuSuDung();
        int CoutTongVatTu();
        // quản trị tiền
        double TongTienTrongNam();
        List<KeyValuePair<int, double>> TienTheoThang();
    }
    public class DashboardRepo : IDashboardRepo
    {
        private readonly Dashboard _dashBo;
        private readonly IUserRepository _userRe;
        private readonly IPhieuVTProviders _IProvi;
        private readonly IPhieuSuaProviders _iPhieuSuaProvi;

        public DashboardRepo(Dashboard dashBo, IUserRepository userRe, IPhieuVTProviders IProvi, IPhieuSuaProviders iPhieuSuaProvi)
        {
            _dashBo = dashBo;
            _userRe = userRe;
            _IProvi = IProvi;
            _iPhieuSuaProvi = iPhieuSuaProvi;
        }
        // lấy tất cả các phiếu vật tư theo phòng ban
        public int TotolPhieuVT(string phongBan)
        {
            var count = _IProvi.phieuVatTuDictionary.Where(k => k.Key == phongBan).SelectMany(v => v.Value);
            return count.Sum();
        }
        // đếm phiếu vật tư theo tháng và theo phòng ban
        public int PhieuVTByMonth(string PhongBan, int month)
        {
            var count = _IProvi.phieuVatTuDictionary.Where(k => k.Key == PhongBan).Select(v => v.Value[month - 1]);
            return count.Sum();
        }
        // lấy phiếu theo phòng ban in ra một danh sách các list 
        public IEnumerable<int> GetPhieuVTByPB(string pB)
        {
            var count = _IProvi.phieuVatTuDictionary.Where(k => k.Key == pB).SelectMany(v => v.Value);
            return count.ToList();
        }
        // lấy tổng các phiếu vật tư  các phòng trong phòng ban cho lãnh đạo xem
        public int CountAllPhieuVT()
        {
            List<string> Lists = new List<string> { "LanhDao", "HC&LÐ", "KT&AT", "TC&KT", "KH&VT", "Px.VH" };
            int sum = 0;
            foreach (var list in Lists)
            {
                var count = _IProvi.phieuVatTuDictionary[list];
                sum = sum + count.Sum();
            }
            return sum;
        }
        //lấy tát cả phiếu sửa theo phòng ban
        public int CountPhieuSuaByPB(string pB)
        {
            var count = _iPhieuSuaProvi.phieuSuaDictionary[pB].Sum();
            return count;
        }
        // Đếm vật tư theo tháng trong một phòng ban 
        public int CountPhieuSuaByMonth(string pB, int month)
        {
            var counts = _iPhieuSuaProvi.phieuSuaDictionary[pB];
            var count = counts[month - 1];
            return count;
        }
        //admin
        // đếm tất cả các chuc vụ gồm bao nhiêu nhân viên
        public int CountNhanVienTheoChucVu(string chucVu)
        {
            return _iPhieuSuaProvi.quanTriNguoiDungDictionary[chucVu];
        }
        // đưa ra danh sách các thành viên trong chức vụ cụ thể
        public List<KeyValuePair<string, int>> CountAllNhanVienTheoChucVu()
        {
            var list = _iPhieuSuaProvi.quanTriNguoiDungDictionary.ToList();
            return list;
        }
        // đưa ra tồng chức vụ có trong công ty
        public int CountAllChucVu()
        {
            return _iPhieuSuaProvi.quanTriNguoiDungTheoPBDictionary["TongChucVu"];
        }
        // đếm tổng tất cả các nhân viên có trong công ty
        public int CountTatCaNhanVien()
        {
            var counts = _iPhieuSuaProvi.quanTriNguoiDungTheoPBDictionary["TongThanhVien"];
            return counts;
        }
        // đếm các nhân viên có trong  phòng ban 
        public int CountNhanVienTheoPB(string phongBan)
        {
            return _iPhieuSuaProvi.quanTriNguoiDungTheoPBDictionary[phongBan];
        }
        // phần của vật tư
        // đến tất cả vật tư có trong phòng ban
        public int CountVatTuTrongPhong(string pb)
        {

            return _iPhieuSuaProvi.quanTriVatTuDictionary[pb];
        }
        // đém tổng các vật tư được sủ dụng ở trong coongty 
        public int CountAllVatTuSD()
        {
            return _iPhieuSuaProvi.quanTriVatTuDictionary["TongVatTuSuDung"];
        }
        // phần của thủ kho
        // lấy tất cả các vật tư được từng phòng ban sử dụng
        public List<KeyValuePair<string, int>> CountAllVatTuSuDungTheoPhong()
        {
            var list = _iPhieuSuaProvi.quanTriVatTuDictionary.ToList();
            return list;
        }
        // đém tổng các vật tư đang được sủ dụng
        public int CountTongVatTuSuDung()
        {
            var tong = _iPhieuSuaProvi.quanTriVatTuDictionary.Select(x => x.Value).ToList();
            return tong.Sum();
        }
        // đếm tổng vật tư đang có trong kho
        public int CoutTongVatTu()
        {
            return _iPhieuSuaProvi.quanTriNguoiDungTheoPBDictionary["TongVatTu"];
        }
        // quản trị tiền 
        public List<KeyValuePair<int, double>> TienTheoThang()
        {
            var tong= _iPhieuSuaProvi.quanTriTienDictionary.ToList();
            return tong;
        }
        public double TongTienTrongNam()
        {
            var tong = _iPhieuSuaProvi.quanTriTienDictionary.Select(x => x.Value).Sum();
            return tong;
        }

    }
}
