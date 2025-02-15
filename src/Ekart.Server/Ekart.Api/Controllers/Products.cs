using Ekart.Core.Entites;
using Ekart.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ekart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Products : ControllerBase
    {
        private readonly StoreContext _storeContext;

        public Products(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _storeContext.Products.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            Product product = await _storeContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return NotFound();
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _storeContext.Products.AddAsync(product);
            await _storeContext.SaveChangesAsync();
            return product;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id || !(await ProductExists(id)))
                return BadRequest("Cannot update this product");

            _storeContext.Entry(product).State = EntityState.Modified;
            await _storeContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            Product product = await _storeContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
                return NotFound();
            _storeContext.Products.Remove(product);
            await _storeContext.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> ProductExists(int id)
        {
            return await _storeContext.Products.AnyAsync(x => x.Id == id);
        }
    }
}
