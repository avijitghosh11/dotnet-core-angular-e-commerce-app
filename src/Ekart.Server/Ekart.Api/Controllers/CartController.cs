using Ekart.Core.Entites;
using Ekart.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ekart.Api.Controllers
{
    public class CartController(ICartService cartService) : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCart>> GetCartById(string Id)
        {
            var cart = await cartService.GetCartAsync(Id);
            return Ok(cart ?? new ShoppingCart { Id = Id });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
        {
            var updateCart = await cartService.SetCartAsync(cart);
            if (updateCart == null)
                return BadRequest("Problem with cart.");
            return Ok(updateCart);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCart(string Id)
        {
            var result = await cartService.DeleteCartAsync(Id);
            if(!result) return BadRequest("Problem deleting cart.");
            return Ok();
        }
    }
}
