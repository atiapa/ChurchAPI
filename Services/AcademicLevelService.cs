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
    public interface IAcademicLevelService : IModelService<AcademicLevelDto> {
        Task<List<AcademicLevelDto>> GetAcademicLevel(int ChurchID);
    }


    public class AcademicLevelService : BaseService<AcademicLevelDto, AcademicLevel>, IAcademicLevelService
    {
        public AcademicLevelService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<AcademicLevelDto>> GetAcademicLevel(int ChurchID)
        {
            var data = await _context.academicLevel_Tbl.Where(q => q.ChurchID == ChurchID).ToListAsync();
            if (data == null) throw new Exception("Record not found");
            return data.Select(q=>new AcademicLevelDto
            {
                EducationLevel = q?.EducationLevel,
                Note = q?.Note
            }).ToList();
        }

    }
}
