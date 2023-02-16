using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChurchApi.Data;
using ChurchApi.DTOs;
using ChurchApi.Models;

namespace ChurchApi.Services
{

    public interface IPictureService : IModelService<PictureDto> { }
    public class PictureService : BaseService<PictureDto, Picture>, IPictureService
    {
        public PictureService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
