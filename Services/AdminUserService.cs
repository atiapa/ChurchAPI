using System.Threading.Tasks;
using AutoMapper;
using ChurchApi.Data;
using ChurchApi.DTOs;
using ChurchApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ChurchApi.Services
{
    public interface IAdminUserService : IModelService<AdminUserDto>

    {
        Task<AdminUserDto> AdminUserLogin(string username, string password);
    }

    public class AdminUserService : BaseService<AdminUserDto, AdminUser>, IAdminUserService

    {
        public AdminUserService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {

        }
   
        public async Task<AdminUserDto> AdminUserLogin(string username, string password)
    {
        var user = await _context.User_Tbl.Where(q => q.Username == username && q.Password == password).FirstOrDefaultAsync();
        if (user == null) throw new Exception("Invalid username or password");

        return new AdminUserDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
            MemberID = user.MemberID,
            ChurchID = user.ChurchID,
            Access_level = user.Access_level
        };



    }

    }

}
