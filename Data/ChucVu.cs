using System;
using System.Collections.Generic;

namespace QLVT_BE.Data;

public partial class ChucVu
{
    public int IdChucVu { get; set; }

    public string TenChucVu { get; set; } = null!;

    public string MaChuVu { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
