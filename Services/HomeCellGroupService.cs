using AutoMapper;
using ChurchApi.Data;
using ChurchApi.DTOs;
using ChurchApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchApi.Services
{
    public interface IHomeCellGroupService : IModelService<HomeCellGroupDto> {
        Task<List<HomeCellGroupDto>> GetHomeCellGoupByChurchID(int churchID);
    }

    public class HomeCellGroupService : BaseService<HomeCellGroupDto, HomeCellGroup>, IHomeCellGroupService
    {
        public HomeCellGroupService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<HomeCellGroupDto>> GetHomeCellGoupByChurchID(int churchID)
        {
            var data = await _context.Homecell_Tbl.Where(q => q.ChurchID == churchID && q.Groups == "Home Cell Group").ToListAsync();
            if (data == null) throw new Exception("Record not found");
            return data.Select(q => new HomeCellGroupDto
            {
                Name = q?.Name,
                Groups = q?.Groups,
                ChurchID = q.ChurchID

            }).ToList();





        }
    }
}
