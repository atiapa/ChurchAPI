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
    public interface IBibleStudyGroupService : IModelService<BibleStudyGroupDto> { 
        Task<List<BibleStudyGroupDto>> GetBibleStudyGroup(int churchID);
    }

    public class BibleStudyGroupService : BaseService<BibleStudyGroupDto, BibleStudyGroup>, IBibleStudyGroupService
    {
        public BibleStudyGroupService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<BibleStudyGroupDto>> GetBibleStudyGroup(int churchID)
        {
            var data = await _context.Homecell_Tbl.Where(q => q.ChurchID == churchID && q.Groups == "Bible Study Group").ToListAsync();
            if (data == null) throw new Exception("Record not found");
            return data.Select(q => new BibleStudyGroupDto
            {
                Name = q?.Name,
                Groups = q?.Groups,
                ChurchID = q.ChurchID
            }).ToList();
        }
    }
}
