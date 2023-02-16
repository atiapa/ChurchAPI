
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
    public interface IChurchServiceService: IModelService<ChurchServiceDto> {

        Task<List<ChurchServiceDto>> GetServiceByID(int churchID);
    }

public class ChurchServiceService : BaseService<ChurchServiceDto, IChurchServiceService>, IChurchServiceService
    {
    public ChurchServiceService(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }


        public async Task<List<ChurchServiceDto>> GetServiceByID(int churchID)
        {
            var data = await _context.Services_Tbl.Where(q => q.ChurchID == churchID).ToListAsync();
            if (data == null) throw new Exception("Record not found");
            return data.Select(q => new ChurchServiceDto
            {
                Name = q.Name,

            }).ToList();
        }

    }

    //internal interface IChurchServiceService
    //{
    //}
}
