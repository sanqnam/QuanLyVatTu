using System;
using System.Collections.Generic;

namespace QLVT_BE.Data;

public partial class TinhTrangPhieu
{
    public int IdTrinhTrangPhieu { get; set; }

    public string? TenTinhTrangDuyet { get; set; }

    public virtual ICollection<PhieuDeNghiSuaChua> PhieuDeNghiSuaChuas { get; set; } = new List<PhieuDeNghiSuaChua>();

    public virtual ICollection<PhieuDeNghiVatTu> PhieuDeNghiVatTus { get; set; } = new List<PhieuDeNghiVatTu>();

    public virtual ICollection<PhieuTrinhMua> PhieuTrinhMuas { get; set; } = new List<PhieuTrinhMua>();
}
