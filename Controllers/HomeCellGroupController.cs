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
    public class HomeCellGroupController : BaseApi<IHomeCellGroupService, HomeCellGroupDto>
    {
        public HomeCellGroupController(IHomeCellGroupService service) : base(service)
        {
        }


        [HttpGet, Route("getHomCellGroup")]
        public async Task<ActionResult> GetHomeCellGroup(int churchID)
        {
            try
            {
                var data = await Service.GetHomeCellGoupByChurchID(churchID);
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
