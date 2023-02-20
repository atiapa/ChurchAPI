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
    public class AdminUserController : BaseApi<IAdminUserService, AdminUserDto>
    {
        public AdminUserController(IAdminUserService service) : base(service)
        {
        }

        [Route("adminUserLogin")]
        [HttpGet]
        public async Task<IActionResult> AdminUserLogin(string username, string password)
        {
            try { return Ok(await Service.AdminUserLogin(username, password)); }
            catch (Exception ex) { return BadRequest(ExceptionHelper.ProcessException(ex)); }
        }

        [HttpGet, Route("getAdminUsermember")]
        public async Task<ActionResult> GetMemberId(int memberId)
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

        [HttpGet, Route("GetAdminMembersList")]
        public async Task<ActionResult> GetAdminMembersList()
        {
            try
            {
                var data = await Service.GetAdminMembersList();
                //if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessException(ex));
            }
        }
    }   
    }

