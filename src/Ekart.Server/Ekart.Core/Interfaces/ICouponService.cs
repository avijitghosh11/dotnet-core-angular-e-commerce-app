using Ekart.Core.Entites;

namespace Ekart.Core.Interfaces
{
    public interface ICouponService
    {
        Task<AppCoupon?> GetCouponFromPromoCode(string code);
    }
}
