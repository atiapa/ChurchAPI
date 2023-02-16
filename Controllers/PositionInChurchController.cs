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
    public class PositionInChurchController : BaseApi<IPositionInChurchService, PositionInChurchDto>
    {
        public PositionInChurchController(IPositionInChurchService service) : base(service)
        {
        }


        [HttpGet, Route("find")]
        public async Task<ActionResult> Find(int refno)
        {
            try
            {
                var data = await Service.Find(refno);
                if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessException(ex));
            }
        }


        [HttpGet, Route("getPosition")]
        public async Task<ActionResult> GetPosition(int churchID)
        {
            try
            {
                var data = await Service.GetPositionByChurchID(churchID);
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
