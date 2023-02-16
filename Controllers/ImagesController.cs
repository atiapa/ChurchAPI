using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChurchApi.Helpers;

namespace ChurchApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ImagesController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        [HttpGet]
        public ActionResult Get(string file)
        {
            try
            {
                var fileStream = new ImageHelpers(_hostingEnvironment.ContentRootPath).GetImage(file);
                return new FileStreamResult(fileStream, "image/png");
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessException(ex));
            }

        }
    }
}
