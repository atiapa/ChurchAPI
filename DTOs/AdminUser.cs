using ChurchApi.Models;

namespace ChurchApi.DTOs
{
    public class AdminUserDto : AdminUser
    {

    }
    public class AdminUserResponse
    {
        public string Username { get; set; }
        //public string View { get; set; }
        public string Token { get; set; }
    }
}
