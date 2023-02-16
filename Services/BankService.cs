using AutoMapper;
using ChurchApi.Data;
using ChurchApi.DTOs;
using ChurchApi.Models;

namespace ChurchApi.Services
{
    public interface IBankService : IModelService<BankDto> { }

    public class BankService : BaseService<BankDto, Bank>, IBankService
    {
        public BankService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}

