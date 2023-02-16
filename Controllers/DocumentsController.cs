//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using ChurchApi.DTOs;
//using ChurchApi.Helpers;
//using ChurchApi.Services;
//using System;
//using System.Threading.Tasks;

//namespace ChurchApi.Controllers
//{
//    public class DocumentsController : BaseApi<IDocumentService, DocumentDto>
//    {
//        private readonly IWebHostEnvironment _hostingEnvironment;
//        public DocumentsController(IDocumentService service, IWebHostEnvironment environment)
//            : base(service) { _hostingEnvironment = environment; }

//        [HttpGet, Route("referencedocuments")]
//        public async Task<IActionResult> ReferenceDocuments(long referenceId)
//        {
//            try { return Ok(await Service.GetDocuments(referenceId, GetUsername())); }
//            catch (Exception ex) { return BadRequest(ExceptionHelper.ProcessException(ex)); }
//        }

//        [HttpGet, Route("processeddocuments")]
//        public async Task<IActionResult> ProcessedDocuments([FromQuery] long id)
//        {
//            try { return Ok(await Service.GetProcessedDocuments(id, GetUsername())); }
//            catch (Exception ex) { return BadRequest(ExceptionHelper.ProcessException(ex)); }
//        }

//        [HttpGet, Route("stats")]
//        public async Task<IActionResult> FetchStats()
//        {
//            try { return Ok(await Service.FetchStats()); }
//            catch (Exception ex) { return BadRequest(ExceptionHelper.ProcessException(ex)); }
//        }

//        [HttpGet, Route("query")]
//        public async Task<IActionResult> Query([FromQuery] DocumentFilter filter)
//        {
//            try
//            {
//                var (data, total) = await Service.Query(filter, GetUsername());
//                Response.Headers.Add("X-Total-Records", total.ToString());
//                return Ok(data);
//            }
//            catch (Exception ex) { return BadRequest(ExceptionHelper.ProcessException(ex)); }
//        }


//        [HttpGet, Route("template")]
//        public async Task<IActionResult> Template([FromQuery] DocumentFilter filter)
//        {
//            try { return Ok(await Service.GetTemplate()); }
//            catch (Exception ex) { return BadRequest(ExceptionHelper.ProcessException(ex)); }
//        }

//        [HttpGet, Route("download")]
//        public async Task<IActionResult> Download(long id)
//        {
//            try
//            {
//                var (file, name) = await Service.GetFile(id, _hostingEnvironment.ContentRootPath);
//                return File(file, "application/pdf", name);
//            }
//            catch (Exception ex) { return BadRequest(ExceptionHelper.ProcessException(ex)); }
//        }

//        [HttpGet, Route("downloadtemplate")]
//        public async Task<IActionResult> DownloadTemplate()
//        {
//            try
//            {
//                var (file, name) = await Service.DownloadTemplate(_hostingEnvironment.ContentRootPath);
//                return File(file, "application/pdf", name);
//            }
//            catch (Exception ex) { return BadRequest(ExceptionHelper.ProcessException(ex)); }
//        }

//        [HttpPost, Route("upload"), Authorize(Roles = "NOTXASTTWBWII")]
//        public async Task<ActionResult> Upload()
//        {
//            try
//            {
//                return Ok(await Service.UploadFile(HttpContext.Request.Form.Files[0],
//                    _hostingEnvironment.ContentRootPath));
//            }
//            catch (Exception ex) { return BadRequest(ExceptionHelper.ProcessException(ex)); }
//        }

//        [HttpDelete, Route("template/{id}")]
//        public async Task<ActionResult> DeleteDocument(long id)
//        {
//            try
//            {
//                return Ok(await Service.DeleteDocument(id,
//                    _hostingEnvironment.ContentRootPath));
//            }
//            catch (Exception ex) { return BadRequest(ExceptionHelper.ProcessException(ex)); }
//        }

//        [Consumes("multipart/form-data")]
//        public override Task<ActionResult> Create([FromForm] DocumentDto model)
//        {
//            model.RootPath = _hostingEnvironment.ContentRootPath;
//            model.Username = GetUsername();
//            return base.Create(model);
//        }

//        [Consumes("multipart/form-data")]
//        public override Task<ActionResult> UpdateAsync([FromForm] DocumentDto model)
//        {
//            model.RootPath = _hostingEnvironment.ContentRootPath;
//            model.Username = GetUsername();
//            return base.UpdateAsync(model);
//        }
//    }
//}
