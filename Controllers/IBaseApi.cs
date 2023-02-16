using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChurchApi.Controllers
{
    public interface IBaseApi<TDto> where TDto : class
    {
        Task<ActionResult> Create(TDto model);
        Task<ActionResult> Delete(long id);
        Task<ActionResult<List<TDto>>> Get();
        Task<ActionResult<TDto>> GetById(long id);
        Task<ActionResult> UpdateAsync(TDto model);
    }
}