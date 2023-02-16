using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChurchApi.Services;

namespace ChurchApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController,AllowAnonymous]
    public class EnumsController : ControllerBase
    {
        private readonly IEnumService _enumService;

        public EnumsController(IEnumService enumService) => _enumService = enumService;

        [HttpGet]
        public ActionResult GetList(string name)
        {
            try { return Ok(_enumService.GetList(name)); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
    }
}
