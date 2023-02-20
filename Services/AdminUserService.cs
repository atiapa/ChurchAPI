using System.Threading.Tasks;
using AutoMapper;
using ChurchApi.Data;
using ChurchApi.DTOs;
using ChurchApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ChurchApi.Services
{
    public interface IAdminUserService : IModelService<AdminUserDto>

    {
        Task<AdminUserDto> AdminUserLogin(string username, string password);
        
        Task<List<AdminUserDto>> GetAdminMembersList();
    }

    public class AdminUserService : BaseService<AdminUserDto, AdminUser>, IAdminUserService

    {
        public AdminUserService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {

        }
   
        public async Task<AdminUserDto> AdminUserLogin(string username, string password)
    {
        var user = await _context.User_Tbl.Where(q => q.username == username && q.password == password).FirstOrDefaultAsync();
        if (user == null) throw new Exception("Invalid username or password");

        return new AdminUserDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            username = user.username,
            password = user.password,
            MemberID = user.MemberID,
            ChurchID = user.ChurchID,
            Access_level = user.Access_level
        };
        
    }
          public async Task<List<AdminUserDto>> GetAdminMembersList()
        {
            var members = await _context.User_Tbl.ToListAsync();
            // return _mapper.Map<List<MemberRegistration>,List<GetMembersListDto>>(members);
            return members.Select(q => new AdminUserDto()
            {
                MemberID = q.MemberID,
                FirstName = q?.FirstName,
                //MiddleName = q?.MiddleName,
                LastName = q?.LastName,
               
                //DigitalAddress = q?.DigitalAddress,
                //BibleStudyGroup = q?.BibleStudyGroup,
               // Title = q?.Title,
             //  PositionInChurch = q?.PositionInChurch,
                ChurchID = (int)q?.ChurchID,
                
                //PostalAddress = q?.PostalAddress,
                //MemberStatus = q?.MemberStatus,
                //EntryDate = q?.EntryDate,
                //HomeCellGroup = q?.HomeCellGroup,
                //Deceased = q?.Deceased,
                //MarriageDate = q?.MarriageDate,
                //Status = q?.Status,
                //Remarks = q?.Remarks,
                //Inactive_Reason = q?.Inactive_Reason,
                //HolySpiritBaptism = q?.HolySpiritBaptism,
                //BaptizmaDate = q?.BaptizmaDate,
                //Service = q?.Service,
                //Communicant = q?.Communicant,
                //Transfered = q?.Transfered,
                //TransferedDate = q?.TransferedDate,
                //TransferedToFrom = q?.TransferedToFrom,
                //Officer = q?.Officer,
                //DependantID = q?.DependantID,
                //Relation = q?.Relation,
                //Photo = q?.Photo
            }).ToList();
        }


    }


}
