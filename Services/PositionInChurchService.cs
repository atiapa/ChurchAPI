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
    public interface IPositionInChurchService : IModelService<PositionInChurchDto>
    {
        Task<PositionInChurchDto> Find(int refno);
        Task<List<PositionInChurchDto>> GetPositionByChurchID(int churchID);
    }

    public class PositionInChurchService : BaseService<PositionInChurchDto, PositionInChurch>, IPositionInChurchService
    {
        public PositionInChurchService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<PositionInChurchDto> Find(int refno)
        {
            var data = await _context.position_Tbl.Where(q => q.Refno == refno).FirstOrDefaultAsync();
            if (data == null) throw new Exception("Record not found");
            return new PositionInChurchDto
            {
                Positions = data?.Positions,
                Note = data?.Note

             };
        }

        public async Task<List<PositionInChurchDto>> GetPositionByChurchID(int churchID)
        {
            var data = await _context.position_Tbl.Where(q => q.ChurchID == churchID).ToListAsync();
            if (data == null) throw new Exception("Record not found");
            return data.Select(q=> new PositionInChurchDto
            {
                Positions = q?.Positions,
                Note = q?.Note
            }).ToList();
        }
    }
}
