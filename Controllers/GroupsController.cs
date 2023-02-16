using ChurchApi.DTOs;
using ChurchApi.Helpers;
using ChurchApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace ChurchApi.Controllers
{
    [AllowAnonymous]
    public class GroupsController : BaseApi<IGroupsService, GroupsDto>
    {
        public GroupsController(IGroupsService service) : base(service)
        {
        }
        [HttpGet, Route("find")]
        public async Task<ActionResult> Find(int churchID)
        {
            try
            {
                var data = await Service.Find(churchID);
                if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessException(ex));
            }
        }
    }
}
