using Ekart.Core.Entites;
using Ekart.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ekart.Api.Controllers
{
    public class CouponsController(ICouponService couponService) : BaseApiController
    {
        [HttpGet("{code}")]
        public async Task<ActionResult<AppCoupon>> ValidateCoupon(string code)
        {
            var coupon = await couponService.GetCouponFromPromoCode(code);

            if (coupon == null) return BadRequest("Invalid voucher code");

            return coupon;
        }
    }
}
