﻿using ChurchApi.DTOs;
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
    public class MinistriesController :  BaseApi<IMinistriesService, MinistriesDto> {

        public MinistriesController(IMinistriesService service) : base(service)
        {
        }

        [HttpGet, Route("GetGroup")]
        public async Task<ActionResult> GetBibleStudyGroup(int churchID)
        {
            try
            {
                var data = await Service.GetGroup(churchID);
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
