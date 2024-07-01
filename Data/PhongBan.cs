using System;
using System.Collections.Generic;

namespace QLVT_BE.Data;

public partial class PhongBan
{
    public int IdPhongBan { get; set; }

    public string TenPhongBan { get; set; } = null!;

    public string MaPhongBan { get; set; } = null!;

    public virtual ICollection<ChiTietPhieuSuaChua> ChiTietPhieuSuaChuas { get; set; } = new List<ChiTietPhieuSuaChua>();

    public virtual ICollection<PhieuDeNghiSuaChua> PhieuDeNghiSuaChuas { get; set; } = new List<PhieuDeNghiSuaChua>();

    public virtual ICollection<PhieuDeNghiVatTu> PhieuDeNghiVatTus { get; set; } = new List<PhieuDeNghiVatTu>();

    public virtual ICollection<PhieuTrinhMua> PhieuTrinhMuas { get; set; } = new List<PhieuTrinhMua>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
