using ChurchApi.Models;

namespace ChurchApi.DTOs
{
    public class MemberLoginDto : MemberLogin
    {

    }
    public class LoginResponse
    {
        public string Username { get; set; }
        //public string View { get; set; }
        public string Token { get; set; }
    }

}
