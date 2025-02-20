using Ekart.Api.RequestHelpers;
using Ekart.Core.Entites;
using Ekart.Core.Interfaces;
using Ekart.Core.Models;
using Ekart.Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Ekart.Api.Controllers
{
    public class ProductsController(IGenericRepository<Product> repo) : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);
            
            return await CreatePagedResult(repo,spec,specParams.PageIndex,specParams.PageSize);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            Product product = await repo.GetByIdAsync(id);

            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            repo.Add(product);
            if(await repo.SaveAllAsync())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            return BadRequest("Problem creating product");
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id || !(repo.IsExists(id)))
                return BadRequest("Cannot update this product");

            repo.Update(product);
            if (await repo.SaveAllAsync())
            {
                return NoContent();
            }
            return BadRequest("Problem updating the product");
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            Product product = await repo.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            repo.Delete(product);
            if (await repo.SaveAllAsync())
            {
                return NoContent();
            }
            return BadRequest("Problem deleting the product");
        }

        [HttpGet("brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpecification();
            return Ok(await repo.GetAsync(spec));
        }

        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();
            return Ok(await repo.GetAsync(spec));
        }
    }
}
