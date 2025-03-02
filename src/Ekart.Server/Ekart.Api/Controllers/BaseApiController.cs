using Ekart.Api.RequestHelpers;
using Ekart.Core.Entites;
using Ekart.Core.Interfaces;
using Ekart.Core.Specifications.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Ekart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> repo,
            ISpecification<T> spec, int pageIndex, int pageSize) where T : BaseEntity
        {
            var items = await repo.GetAsync(spec);
            var count = await repo.CountAsync(spec);
            var pagination = new Pagination<T>(pageIndex, pageSize, count, items);
            return Ok(pagination);
        }
    }
}
