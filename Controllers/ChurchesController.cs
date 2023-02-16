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
    public class ChurchesController : BaseApi<IChurchesService, ChurchesDto>
    {
        public ChurchesController(IChurchesService service) : base(service)
        {
        }

        [HttpGet, Route("GetChurchService")]
        public async Task<ActionResult> GetBibleStudyGroup(int churchID)
        {
            try
            {
                var data = await Service.GetChurches(churchID);
                if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessException(ex));
            }


        }


        [HttpGet, Route("GetChurchByID")]
        public async Task<ActionResult> GetChurchByID(int churchID)
        {
            try
            {
                var data = await Service.GetChurchByID(churchID);
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
