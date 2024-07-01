namespace QLVT_BE.ViewModels
{
    public class NotificationMD : NotificationVM
    {
        public int IdNoti { get; set; }

    }
    public class NotificationVM
    {
        public string NameGui { get; set; }
        public string NameNhan { get; set; }
        public string Mess { get; set; }
        public string? Url { get; set; }
        public DateTime? TimeTao { get; set; }
        public bool? DaDoc { get; set; }
    }
}
