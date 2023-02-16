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
    public class AcademicLevelController : BaseApi<IAcademicLevelService, AcademicLevelDto>
    {
        public AcademicLevelController(IAcademicLevelService service) : base(service)
        {
        }

        [HttpGet, Route("GetAcademicLevel")]
        public async Task<ActionResult> GetAcademicLevel(int ChurchID)
        {
            try
            {
                var data = await Service.GetAcademicLevel(ChurchID);
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
