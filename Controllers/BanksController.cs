using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChurchApi.Data;
using ChurchApi.DTOs;
using ChurchApi.Services;
using System.Threading.Tasks;

namespace ChurchApi.Controllers
{
    public class BanksController : BaseApi<IBankService, BankDto>
    {
        public BanksController(IBankService service) : base(service)
        {
        }
        [Authorize(Roles = Privileges.Setting)]
        public override Task<ActionResult> Create(BankDto model) => base.Create(model);

        [Authorize(Roles = Privileges.Setting)]
        public override Task<ActionResult> UpdateAsync(BankDto model) => base.UpdateAsync(model);

        [Authorize(Roles = Privileges.Setting)]
        public override Task<ActionResult> Delete(long id) => base.Delete(id);
    }
}
