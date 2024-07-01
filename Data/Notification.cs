using System;
using System.Collections.Generic;

namespace QLVT_BE.Data;

public partial class Notification
{
    public int IdNoti { get; set; }

    public int? NguoiGui { get; set; }

    public int NguoiNhan { get; set; }

    public string? Mess { get; set; }

    public string? Url { get; set; }

    public int? IdTb { get; set; }

    public DateTime? TimeTao { get; set; }

    public bool? DaDoc { get; set; }

    public int? IdPhieu { get; set; }

    public virtual User? NguoiGuiNavigation { get; set; }

    public virtual User? NguoiNhanNavigation { get; set; }
}
