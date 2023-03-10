using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ChurchApi.Data;
using ChurchApi.Helpers;
using ChurchApi.Services;

namespace ChurchApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminController(IUserService userService, IWebHostEnvironment environment)
        {
            _userService = userService;
            _hostingEnvironment = environment;
        }


        [HttpPost, Authorize(Roles = Privileges.UserCreate)]
        public async Task<ActionResult> CreateUser(RegisterUserModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ExceptionHelper.ProcessException(ModelState));

            model.RootPath = _hostingEnvironment.ContentRootPath;
            try { return StatusCode(201, await _userService.CreateUser(model)); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }


        //[HttpPut, Authorize(Roles = Privileges.UserUpdate)]
        [HttpPut,Authorize]
        public async Task<ActionResult> UpdateUser(UpdateUserModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ExceptionHelper.ProcessException(ModelState));
            model.RootPath = _hostingEnvironment.ContentRootPath; 
            try { return Ok(await _userService.UpdateUser(model)); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }


        [HttpDelete, Authorize(Roles = Privileges.UserDelete)]
        public async Task<ActionResult> DeleteUser(string username)
        {
            try { return Ok(await _userService.DeleteUser(username)); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }


        [HttpGet, Authorize(Roles = Privileges.UserRead)]
        public async Task<ActionResult> GetUsers()
        {
            try { return Ok(await _userService.GetAllUsers()); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpGet, Authorize(Roles = Privileges.RoleRead)]
        public async Task<ActionResult> GetPrivileges()
        {
            try { return Ok(await _userService.GetPrivileges()); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpGet, Authorize(Roles = Privileges.UserRead)]
        public async Task<ActionResult> QueryUsers([FromQuery] UserFilter filter)
        {
            try
            {
                var (data, total) = await _userService.Query(filter);
                Response.Headers.Add("X-Total-Records", total.ToString());
                return Ok(data);
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
    }
}
