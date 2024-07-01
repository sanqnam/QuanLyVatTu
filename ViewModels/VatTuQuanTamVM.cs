using QLVT_BE.Data;

namespace QLVT_BE.ViewModels
{
    public class VatTuQuanTamVM: VatTuQuanTamMD
    {
        public int IdQuanTam { get; set; }
        public User User { get; set; }
        public VatTu VatTu { get; set; }
    }
    public class VatTuQuanTamMD
    {
        public int IdUser { get; set; }

        public int IdVatTu { get; set; }
    }
}
