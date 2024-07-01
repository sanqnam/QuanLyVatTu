using System;
using System.Collections.Generic;

namespace QLVT_BE.Data;

public partial class VatTuQuanTam
{
    public int IdQuanTam { get; set; }

    public int? IdUser { get; set; }

    public int? IdVatTu { get; set; }

    public int? Status { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual VatTu? IdVatTuNavigation { get; set; }
}
