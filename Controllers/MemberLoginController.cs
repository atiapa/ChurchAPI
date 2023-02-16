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
    public class MemberLoginController : BaseApi<IMemberLoginService, MemberLoginDto>
    {
        public MemberLoginController(IMemberLoginService service) : base(service)
        {
        }

        [Route("membersignin")]
        [HttpGet]
        public async Task<IActionResult> Login(string username, string password)
        {
            try { return Ok(await Service.Login(username, password)); }
            catch (Exception ex) { return BadRequest(ExceptionHelper.ProcessException(ex)); }
        }

        [HttpGet, Route("getmember")]
        public  async Task<ActionResult> GetMemberId(int memberId)
        {
            try
            {
                var data = await Service.FindAsync(memberId);
                if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessException(ex));
            }
        }

        [HttpGet, Route("recoverpassword")]
        public async Task<ActionResult> GetMemberId(string email)
        {
            try
            {
                var data = await Service.RecoverPassword(email);
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
