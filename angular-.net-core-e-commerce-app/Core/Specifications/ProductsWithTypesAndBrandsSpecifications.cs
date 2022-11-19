using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecifications : BaseSpecifications<Product>
    {
        public ProductsWithTypesAndBrandsSpecifications()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        public ProductsWithTypesAndBrandsSpecifications(int id)
            :base(x=>x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
