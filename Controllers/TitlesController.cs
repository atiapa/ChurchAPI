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
    public class TitlesController : BaseApi<ITitlesService, TitlesDto>
       
    {
        public TitlesController(ITitlesService service) : base(service)

        {
        }

        [HttpGet, Route("GetTitles")]
        public async Task<ActionResult> GetTitles(int ChurchID)
        {
            try
            {
                var data = await Service.GetTitles(ChurchID);
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
