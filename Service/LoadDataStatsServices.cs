using Microsoft.EntityFrameworkCore;
using QLVT_BE.Data;
using QLVT_BE.Providers;
using QLVT_BE.Repositorys;
using QLVT_BE.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QLVT_BE.Service
{
    public class LoadDataStatsServices : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IPhieuVTProviders _phieuVTProviders;
        private readonly IPhieuSuaProviders _phieuSuaProvi;

        public LoadDataStatsServices(IServiceScopeFactory scopeFactory, IPhieuVTProviders phieuVTProviders, IPhieuSuaProviders phieuSuaProvi)
        {
            _scopeFactory = scopeFactory;
            _phieuVTProviders = phieuVTProviders;
            _phieuSuaProvi = phieuSuaProvi;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<QuanLyVatTuContext>();
                var phongBans = new List<string> { "LanhDao", "HC&LÐ", "KT&AT", "TC&KT", "KH&VT", "Px.VH" };
                var phongBanVatTus = new List<string> { "GD", "P.GD", "HC&LÐ", "KT&AT", "TC&KT", "KH&VT", "Px.VH" };
                await GetPhieuVatTuData(dbContext, phongBans);
                await GetPhieuSuaChuaData(dbContext, phongBans);
                var chucVus = dbContext.ChucVus.Select(cv => cv.MaChuVu).ToList();
                await GetNguoiDungTheoChucVuData(dbContext, chucVus);
                await GetNguoiDungTheoPhongBanData(dbContext, phongBanVatTus);
                await GetVatTuTheoPhongData(dbContext, phongBanVatTus);
                await GetTienMuaTheoThang( dbContext);
            }
        }
        // lấy dữ liệu phiếu đề nghị vật tư
        private async Task GetPhieuVatTuData(QuanLyVatTuContext dbContext, List<string> phongBans)
        {
            int i = 0;
            foreach (var phong in phongBans)
            {
                if (i == 0) i = 2;
                var data = new List<int>();
                for (int j = 1; j <= 12; j++)
                {
                    if (phong == "LanhDao")
                    {
                        data.Add(await dbContext.PhieuDeNghiVatTus
                            .Where(p => p.IdPhieuChinhThuc != 0 && p.TimeDuyetPhieu.Value.Month == j)
                            .CountAsync());
                    }
                    else
                    {
                        data.Add(await dbContext.PhieuDeNghiVatTus
                            .Where(p => p.IdPhieuChinhThuc != 0 && p.IdPhongBan == i && p.TimeDuyetPhieu.Value.Month == j)
                            .CountAsync());
                    }
                }
                _phieuVTProviders.phieuVatTuDictionary.TryAdd(phong, data);
                i++;
            }
        }
        // lấy dữ liệu phiếu sửa chửa
        private async Task GetPhieuSuaChuaData(QuanLyVatTuContext dbContext, List<string> phongBans)
        {
            int i = 0;
            foreach (var phong in phongBans)
            {
                if (i == 0) i = 2;
                var data = new List<int>();
                for (int j = 1; j <= 12; j++)
                {
                    if (phong == "LanhDao")
                    {
                        data.Add(await dbContext.PhieuDeNghiSuaChuas
                            .Where(p => p.IdTinhTrangPhieu ==2 && p.NgayTaoPhieu.Value.Month == j)
                            .CountAsync());
                    }
                    else
                    {
                        data.Add(await dbContext.PhieuDeNghiSuaChuas
                            .Where(p => p.IdPhongBan == i && p.IdTinhTrangPhieu==2 && p.NgayTaoPhieu.Value.Month == j)
                            .CountAsync());
                    }
                }
                _phieuSuaProvi.phieuSuaDictionary.TryAdd(phong, data);
                i++;
            }
        }

        // lấy dữ liệu người dùng theo chức vụ 
        private async Task GetNguoiDungTheoChucVuData(QuanLyVatTuContext dbContext, List<string> chucVus)
        {

            foreach (var chucVu in chucVus)
            {
                var id = await dbContext.ChucVus.Where(cv => cv.MaChuVu == chucVu).Select(cv => cv.IdChucVu).FirstOrDefaultAsync();

                var data = await dbContext.Users.Where(u => u.IdChucVu == id).CountAsync();

                _phieuSuaProvi.quanTriNguoiDungDictionary.TryAdd(chucVu, data);
            }

        }
        // đếm dữ liệu người dung theo phòng ban và theo những dữ liệu không cân phỉa logic
        private async Task GetNguoiDungTheoPhongBanData(QuanLyVatTuContext dbContext, List<string> phongBanVatTus)
        {

            foreach (var phongBan in phongBanVatTus)
            {
                var id = await dbContext.PhongBans.Where(cv => cv.MaPhongBan == phongBan).Select(cv => cv.IdPhongBan).FirstOrDefaultAsync();

                var data = await dbContext.Users.Where(u => u.IdPhongBan == id).CountAsync();

                _phieuSuaProvi.quanTriNguoiDungTheoPBDictionary.TryAdd(phongBan, data);
            }
            string tong = "TongThanhVien";
            var datas = await dbContext.Users.CountAsync();
            _phieuSuaProvi.quanTriNguoiDungTheoPBDictionary.TryAdd(tong, datas);
            // đếm chức tồng chức vụ 
            var dataCV = await dbContext.ChucVus.CountAsync();
            _phieuSuaProvi.quanTriNguoiDungTheoPBDictionary.TryAdd("TongChucVu", dataCV);
            // đểm tổng vật tư có trong kho
            var tongAll = await dbContext.VatTus.CountAsync();
            _phieuSuaProvi.quanTriNguoiDungTheoPBDictionary.TryAdd("TongVatTu", tongAll);
        }
        // đếm dữ liệu vật tư của mõi phòng ban
        private async Task GetVatTuTheoPhongData(QuanLyVatTuContext dbConText, List<string> phongBan)
        {
            foreach (var pb in phongBan)
            {
                var search = await dbConText.PhongBans.FirstOrDefaultAsync(p => p.MaPhongBan == pb);
                var idpb = search.IdPhongBan;
                var data = await dbConText.ChiTietVatTus.Where(vt => vt.IdUserNavigation.IdPhongBan == idpb).CountAsync();
                _phieuSuaProvi.quanTriVatTuDictionary.TryAdd(pb, data);
            }
        }

        private async Task GetTienMuaTheoThang(QuanLyVatTuContext dbContext)
        {
            for (int j = 1; j <= 12; j++)
            {

                var phieus = await dbContext.PhieuTrinhMuas.Where(p => p.IdTinhTrangPhieu == 7 && p.TimeTaoPhieu.Month ==j).ToListAsync();
                double tongTien = 0;
                foreach (var ph in phieus)
                {
                    var tien = ph.TongTien;
                    tongTien += tien;
                }
                _phieuSuaProvi.quanTriTienDictionary.TryAdd(j, tongTien);

            }
        }
    }
}
