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
    public class MemberRegistrationController : BaseApi<IMemberRegistrationService, MemberRegistrationDto>
    {

        public MemberRegistrationController(IMemberRegistrationService service) : base(service)
        {
        }

        [HttpGet, Route("find")]
        public async Task<ActionResult> Find(int MemberID)
        {
            try
            {
                var data = await Service.Find(MemberID);
                if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessException(ex));
            }
        }

        [HttpGet, Route("FindMember")]
        public async Task<ActionResult> FindMember(int MemberID)
        {
            try
            {
                var data = await Service.Find(MemberID);
                if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessException(ex));
            }
        }



        [HttpGet, Route("LastMemberID")]
        public async Task<ActionResult> LastMember(int ChurchID)
        {
            try
            {
                var data = await Service.LastMember(ChurchID);
                //if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessException(ex));
            }
        }


        //       [HttpPut, Route("updatemember")]
        //public async Task<ActionResult> UpdateMember(MemberRegistrationDto member)
        //       {
        //           try
        //           {
        //               //member.Photo=member.File.CopyToAsync()
        //               //new FileStreamResult(fileStream, "image/png");
        //               var data = await Service.UpdateMember(member);
        //               //if (data == null) return NotFound();
        //               return Ok(data);
        //           }
        //           catch (Exception ex)
        //           {
        //               return BadRequest(ExceptionHelper.ProcessException(ex));
        //           }
        //       }

        [HttpPut, Route("updatemember"), Consumes("multipart/form-data")]
        public async Task<ActionResult> UpdateMember([FromForm] MemberRegistrationDto member)
        {
            try
            {
                var data = await Service.UpdateMember(member);
                //if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessException(ex));
            }
        }


        [HttpGet, Route("searchmember")]
        public async Task<ActionResult> SearchMember([FromQuery] SearchFilter member)
        {
            try
            {
                var data = await Service.SearchMember(member);
                //if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessException(ex));
            }
        }

        [HttpGet, Route("DependantList")]
        public async Task<ActionResult> DependantList([FromQuery] string dependant)
        {
            try
            {
                var data = await Service.DependantList(dependant);
                //if (data == null) return NotFound();
                return Ok(data);  
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessException(ex));
            }
        }


       


    [HttpGet, Route("GetMembersList")]
    public async Task<ActionResult> GetMembersList()
    {
        try
        {
            var data = await Service.GetMembersList();
            //if (data == null) return NotFound();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.ProcessException(ex));
        }
    }


    [HttpGet, Route("GetActiveMembers")]
    public async Task<ActionResult> GetActiveMembers()
    {
        try
        {
            var data = await Service.GetActiveMembers();
            if (data == null) return NotFound();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.ProcessException(ex));
        }
    }



    [HttpGet, Route("GetInActiveMembers")]
    public async Task<ActionResult> GetInActiveMembers()
    {
        try
        {
            var data = await Service.GetInActiveMembers();
            if (data == null) return NotFound();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.ProcessException(ex));
        }
    }


    /*[HttpGet, Route("GetConfirmedMembers")]
    public async Task<ActionResult> GetConfirmedMembers([FromQuery] string approved)
    {
        try    
        {
            var data = await Service.GetConfirmedMembers(approved);
            if (data == null) return NotFound();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.ProcessException(ex));
        }
    }*/

    [HttpGet, Route("GetUnConfirmedMembers")]
    public async Task<ActionResult> GetUnConfirmedMembers()
    {
        try
        {
            var data = await Service.GetUnConfirmedMembers();
            if (data == null) return NotFound();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.ProcessException(ex));
        }
    }



    [HttpGet, Route("GetDeceasedMembers")]
    public async Task<ActionResult> GetDeceasedMembers()
    {
        try
        {
            var data = await Service.GetDeceasedMembers();
            if (data == null) return NotFound();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.ProcessException(ex));
        }
    }


    [HttpGet, Route("GetAdultMembers")]
    public async Task<ActionResult> GetAdultMembers()
    {
        try
        {
            var data = await Service.GetAdultMembers();
            if (data == null) return NotFound();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.ProcessException(ex));
        }
    }



    [HttpGet, Route("GetChildMembers")]
    public async Task<ActionResult> GetChildMembers()
    {
        try
        {
            var data = await Service.GetChildMembers();
            if (data == null) return NotFound();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.ProcessException(ex));
        }
    }


    [HttpGet, Route("TransferedMembersOut")]
    public async Task<ActionResult> TransferedMembersOut()
    {
        try
        {
            var data = await Service.TransferedMembersOut();
            if (data == null) return NotFound();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.ProcessException(ex));
        }
    }

    [HttpGet, Route("TransferedMembersIn")]
    public async Task<ActionResult> TransferedMembersIn()
    {
        try
        {
            var data = await Service.TransferedMembersIn();
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
