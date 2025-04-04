using Ekart.Core.Entites;
using Ekart.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Ekart.Infrastructure.Services
{
    public class CouponService : ICouponService
    {
        public CouponService(IConfiguration config)
        {
            var isDevelopment = string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                "development", StringComparison.InvariantCultureIgnoreCase);

            StripeConfiguration.ApiKey = config["StripeSettings:SecretKey"];
            
            if (isDevelopment)
            {
                StripeConfiguration.ApiKey = Environment.GetEnvironmentVariable("StripeSettings_SecretKey");
            }
        }

        public async Task<AppCoupon?> GetCouponFromPromoCode(string code)
        {
            var promotionService = new PromotionCodeService();

            var options = new PromotionCodeListOptions
            {
                Code = code
            };

            var promotionCodes = await promotionService.ListAsync(options);

            var promotionCode = promotionCodes.FirstOrDefault();

            if (promotionCode != null && promotionCode.Coupon != null)
            {
                return new AppCoupon
                {
                    Name = promotionCode.Coupon.Name,
                    AmountOff = promotionCode.Coupon.AmountOff,
                    PercentOff = promotionCode.Coupon.PercentOff,
                    CouponId = promotionCode.Coupon.Id,
                    PromotionCode = promotionCode.Code
                };
            }

            return null;
        }
    }
}
