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
    public class ChurchServiceController : BaseApi<IChurchServiceService, ChurchServiceDto>
    {
        public ChurchServiceController(IChurchServiceService service) : base(service)
        {
        }

    

        [HttpGet, Route("GetServiceByID")]
        public async Task<ActionResult> GetServiceByID(int churchID)
        {
            try
            {
                var data = await Service.GetServiceByID(churchID);
                if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessException(ex));
            }
        }
    }

    public interface IChurchService
    {
    }
}

