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
    public interface IChurchesService  : IModelService<ChurchesDto> {
        Task<List<ChurchesDto>> GetChurches(int churchID);
        Task<List<ChurchesDto>> GetChurchByID(int churchID);
    }

public class ChurchesService : BaseService<ChurchesDto, Churches>, IChurchesService
    {
    public ChurchesService(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

        public async Task<List<ChurchesDto>> GetChurches(int churchID)
        {
            var data = await _context.churchDetail_tbl.Where(q => q.ChurchID == churchID).ToListAsync();
            if (data == null) throw new Exception("Record not found");
            return data.Select(q => new ChurchesDto
            {
                name = q.name,
                branchName = q?.branchName,
            }).ToList();
        }


        public async Task<List<ChurchesDto>> GetChurchByID(int churchID)
        {
            var data = await _context.churchDetail_tbl.Where(q => q.ChurchID == churchID).ToListAsync();
            if (data == null) throw new Exception("Record not found");
            return data.Select(q => new ChurchesDto
            {
                name = q.name,
                branchName = q?.branchName,
            }).ToList();
        }

    }
}
