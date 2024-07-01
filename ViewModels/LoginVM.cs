namespace QLVT_BE.ViewModels
{
    public class LoginVM
    {
        public string Username { get; set; }
        public string MatKhau { get; set; }
    }
    public class LoginResponse
    {
        public string? Token { get; set; }
    }
}
