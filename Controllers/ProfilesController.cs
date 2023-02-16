//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using ChurchApi.Data;
//using ChurchApi.Services;
//using System.Threading.Tasks;

//namespace ChurchApi.Controllers
//{
//    public class ProfilesController : BaseApi<IProfileService, ProfileDto>
//    {
//        public ProfilesController(IProfileService service) : base(service) { }
//    }

//    //[Authorize(Roles = Privileges.RoleRead)]
//    //public class ProfilesController : BaseApi<IProfileService, ProfileDto>
//    //{
//    //    public ProfilesController(IProfileService service) : base(service) { }

//    //    [Authorize(Roles = Privileges.RoleCreate)]
//    //    public override Task<ActionResult> Create(ProfileDto model) => base.Create(model);


//    //    [Authorize(Roles = Privileges.RoleUpdate)]
//    //    public override Task<ActionResult> UpdateAsync(ProfileDto model) => base.UpdateAsync(model);


//    //    [Authorize(Roles = Privileges.RoleDelete)]
//    //    public override Task<ActionResult> Delete(long id) => base.Delete(id);
//    //}
//}
