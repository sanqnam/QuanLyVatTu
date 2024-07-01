using System;
using System.Collections.Generic;

namespace QLVT_BE.Data;

public partial class User
{
    public int IdUser { get; set; }

    public string Username { get; set; } = null!;

    public string? MatKhau { get; set; }

    public string HoTen { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? MaBiMat { get; set; }

    public int IdChucVu { get; set; }

    public int IdPhongBan { get; set; }

    public int? CapNhat { get; set; }

    public string? DiaChi { get; set; }

    public string? DienThoai { get; set; }

    public string? HinhDaiDien { get; set; }

    public string? AboutMe { get; set; }

    public int? IsActive { get; set; }

    public string? ConnectionId { get; set; }

    public virtual ICollection<ChiTietPhieuSuaChua> ChiTietPhieuSuaChuas { get; set; } = new List<ChiTietPhieuSuaChua>();

    public virtual ICollection<ChiTietVatTu> ChiTietVatTus { get; set; } = new List<ChiTietVatTu>();

    public virtual ICollection<DeNghiNhapKho> DeNghiNhapKhoIdThuKhoNavigations { get; set; } = new List<DeNghiNhapKho>();

    public virtual ICollection<DeNghiNhapKho> DeNghiNhapKhoNguoiKiemPhieuNavigations { get; set; } = new List<DeNghiNhapKho>();

    public virtual ICollection<DeNghiNhapKho> DeNghiNhapKhoNguoiTaoPhieuNavigations { get; set; } = new List<DeNghiNhapKho>();

    public virtual ICollection<HinhAnhVatTu> HinhAnhVatTus { get; set; } = new List<HinhAnhVatTu>();

    public virtual ChucVu IdChucVuNavigation { get; set; } = null!;

    public virtual PhongBan IdPhongBanNavigation { get; set; } = null!;

    public virtual ICollection<Notification> NotificationNguoiGuiNavigations { get; set; } = new List<Notification>();

    public virtual ICollection<Notification> NotificationNguoiNhanNavigations { get; set; } = new List<Notification>();

    public virtual ICollection<PhieuDeNghiSuaChua> PhieuDeNghiSuaChuas { get; set; } = new List<PhieuDeNghiSuaChua>();

    public virtual ICollection<PhieuDeNghiVatTu> PhieuDeNghiVatTuIdLanhDaoNavigations { get; set; } = new List<PhieuDeNghiVatTu>();

    public virtual ICollection<PhieuDeNghiVatTu> PhieuDeNghiVatTuIdThuTruongNavigations { get; set; } = new List<PhieuDeNghiVatTu>();

    public virtual ICollection<PhieuDeNghiVatTu> PhieuDeNghiVatTuIdUserNavigations { get; set; } = new List<PhieuDeNghiVatTu>();

    public virtual ICollection<PhieuTrinhMua> PhieuTrinhMuaIdLanhDaoNavigations { get; set; } = new List<PhieuTrinhMua>();

    public virtual ICollection<PhieuTrinhMua> PhieuTrinhMuaIdThuTruongNavigations { get; set; } = new List<PhieuTrinhMua>();

    public virtual ICollection<PhieuTrinhMua> PhieuTrinhMuaIdUserNavigations { get; set; } = new List<PhieuTrinhMua>();

    public virtual ICollection<VatTuQuanTam> VatTuQuanTams { get; set; } = new List<VatTuQuanTam>();
}
