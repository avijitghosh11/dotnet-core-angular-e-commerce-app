using Ekart.Core.Entites;

namespace Ekart.Core.Specifications
{
    public class BrandListSpecification : BaseSpecification<Product,string>
    {
        public BrandListSpecification()
        {
            AddSelect(x => x.Brand);
            ApplyDistinct();
        }
    }
}
