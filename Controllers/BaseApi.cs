using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChurchApi.Helpers;
using ChurchApi.Services;

namespace ChurchApi.Controllers
{
    [ApiController,Route("api/[controller]"),Authorize]
    public class BaseApi<TService,TDto> : ControllerBase where TService:IModelService<TDto> where TDto: class
    {
        protected readonly TService Service;
        //private IPositionInChurchService service;

        public BaseApi(TService service) => Service = service;

        //public BaseApi(IPositionInChurchService service)
        //{
        //    this.service = service;
        //}

        [HttpGet,Route("dontusethisfind")]
        public virtual async Task<ActionResult<TDto>> GetById(long id)
        {
            try
            {
                var data = await Service.FindAsync(id);
                if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessException(ex));
            }
        }

        [HttpGet]
        public virtual async Task<ActionResult<List<TDto>>> Get()
        {
            try { return Ok(await Service.FetchAllAsync()); }
            catch (Exception ex) { return BadRequest(ExceptionHelper.ProcessException(ex)); }
        }

        [HttpPost]
        public virtual async Task<ActionResult> Create(TDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ExceptionHelper.ProcessException(ModelState));

            try { return StatusCode(201, await Service.Save(SetUsername(model))); }
            catch (Exception ex) { return BadRequest(ExceptionHelper.ProcessException(ex)); }
        }

        [HttpPut]
        public virtual async Task<ActionResult> UpdateAsync(TDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ExceptionHelper.ProcessException(ModelState));

            try { return Ok(await Service.Update(SetUsername(model))); }
            catch (Exception ex) { return BadRequest(ExceptionHelper.ProcessException(ex)); }
        }

        [Route("{id}"), HttpDelete]
        public virtual async Task<ActionResult> Delete(long id)
        {
            try { return Ok(await Service.Delete(id)); }
            catch (Exception ex) { return BadRequest(ExceptionHelper.ProcessException(ex)); }
        }

        protected TDto SetUsername(TDto record)
        {

            if (typeof(TDto).GetProperty("Usernamex") != null)
                typeof(TDto).GetProperty("Usernamex").SetValue(record, GetUsername());

            return record;
        }

        protected string GetUsername() => User.FindFirst("username")?.Value ?? "";

    }
}
