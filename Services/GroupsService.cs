using AutoMapper;
using ChurchApi.Data;
using ChurchApi.DTOs;
using ChurchApi.Models;
using ChurchApi.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchApi.Services
{
    public interface IGroupsService : IModelService<GroupsDto> {
        Task<GroupsDto> Find(int churchID);
    }

    public class GroupsService : BaseService<GroupsDto, Groups>, IGroupsService
    {
        public GroupsService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<GroupsDto> Find(int churchID)
        {
            var data = await _context.groups_Tbl.Where(q => q.ChurchID == churchID).FirstOrDefaultAsync();
            if (data == null) throw new Exception("Record not found");
            return new GroupsDto
            {
                groups = data?.groups,
                Note = data?.Note

            };
        }
    }
}
