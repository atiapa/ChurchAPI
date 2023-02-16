using ChurchApi.DTOs;
using ChurchApi.Helpers;
using ChurchApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChurchApi.Controllers
{
    [AllowAnonymous]
    public class BibleStudyGroupController : BaseApi<IBibleStudyGroupService, BibleStudyGroupDto>
    {
        public BibleStudyGroupController(IBibleStudyGroupService service) : base(service)
        {
        }

        [HttpGet, Route("GetBibleStudyGroup")]
        public async Task<ActionResult> GetBibleStudyGroup(int ChurchID)
        {
            try
            {
                var data = await Service.GetBibleStudyGroup(ChurchID);
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
