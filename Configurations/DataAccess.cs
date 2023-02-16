using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChurchApi.Data;
using ChurchApi.DTOs;
using ChurchApi.Models;
using ChurchApi.Services;

namespace ChurchApi.Configurations
{
    public static class DataAccess
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DKAWCAppCon")));
            return services;
        }

        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            //services.AddScoped<IUserService, UserService>();
            ////services.AddScoped<IProfileService, ProfileService>();
            ////services.AddScoped<IDocumentService, DocumentService>();
            //services.AddScoped<IPictureService, PictureService>();
            //services.AddScoped<IEnumService, EnumService>();
            //services.AddScoped<IBankService, BankService>();
            services.AddScoped<IHomeCellGroupService, HomeCellGroupService>();
            services.AddScoped<IMemberLoginService, MemberLoginService>();
            services.AddScoped<IAcademicLevelService, AcademicLevelService>();
            services.AddScoped<IBibleStudyGroupService, BibleStudyGroupService>();
            services.AddScoped<IPositionInChurchService, PositionInChurchService>();
            services.AddScoped<IGroupsService, GroupsService>(); 
            services.AddScoped<IMemberRegistrationService, MemberRegistrationService>(); 
            services.AddScoped<IMinistriesService, MinistriesService>(); 
            services.AddScoped<IChurchesService, ChurchesService>();
            services.AddScoped<IChurchServiceService, ChurchServiceService>();
            services.AddScoped<ITitlesService, TitlesService>();
            services.AddScoped<IAdminUserService, AdminUserService>();

            return services;
        }

        public class DataMappingProfile : AutoMapper.Profile
        {
            public DataMappingProfile()
            {
                //CreateMap<Country, CountryDto>()
                //    .ForMember(q=>q.ISOCode,s=>s.MapFrom(a=>a.ISOAlphaTwoCode))
                //    .ReverseMap();
                //CreateMap<Document, DocumentDto>().ReverseMap();
                //CreateMap<Bank, BankDto>().ReverseMap();
                CreateMap<HomeCellGroup, HomeCellGroupDto>().ReverseMap();
                CreateMap<MemberLogin, MemberLoginDto>().ReverseMap();
                CreateMap<AcademicLevel, AcademicLevelDto>().ReverseMap();
                CreateMap<BibleStudyGroup, BibleStudyGroupDto>().ReverseMap();
                CreateMap<PositionInChurch, PositionInChurchDto>().ReverseMap();
                CreateMap<Groups, GroupsDto>().ReverseMap();
                CreateMap<MemberRegistration, MemberRegistrationDto>().ReverseMap();
                CreateMap<Ministries, MinistriesDto>().ReverseMap();
                CreateMap<Churches, ChurchesDto>().ReverseMap();
                CreateMap <ChurchService, ChurchServiceDto > ().ReverseMap();
                CreateMap <TitlesService, TitlesDto > ().ReverseMap();
                CreateMap <AdminUserService, AdminUserDto > ().ReverseMap();



            }
        }
    }
}
