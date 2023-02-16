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

    public interface ITitlesService : IModelService<TitlesDto>
    {
        Task<List<TitlesDto>> GetTitles(int ChurchID);
    }


    public class TitlesService : BaseService<TitlesDto, Titles>, ITitlesService
    {
        public TitlesService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<TitlesDto>> GetTitles(int ChurchID)
        {
            var data = await _context.Titles_Tbl.Where(q => q.ChurchID == ChurchID).ToListAsync();
            if (data == null) throw new Exception("Record not found");
            return data.Select(q=>new TitlesDto
            {
                Name=q?.Name
            }).ToList();
        }

    }

}

