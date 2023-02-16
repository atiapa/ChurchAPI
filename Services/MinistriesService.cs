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
    public interface IMinistriesService : IModelService<MinistriesDto> {
        Task<List<MinistriesDto>> GetGroup(int churchID);

    }

    public class MinistriesService : BaseService<MinistriesDto, Ministries>, IMinistriesService
    {
        public MinistriesService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<MinistriesDto>> GetGroup(int churchID)
        {
            var data = await _context.ministries_tbl.Where(q => q.ChurchID == churchID).ToListAsync();
            if (data == null) throw new Exception("Record not found");
            return data.Select(q => new MinistriesDto
            {
                Ministry = q?.Ministry,
                Note = q?.Note
            }).ToList();
        }
    }
}
