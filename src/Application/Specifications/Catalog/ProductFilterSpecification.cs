using ReturneeManager.Application.Specifications.Base;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Application.Specifications.Catalog
{
    public class ProductFilterSpecification : HeroSpecification<Product>
    {
        public ProductFilterSpecification(string searchString)
        {
            Includes.Add(a => a.Brand);
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Barcode != null && (p.Name.Contains(searchString) || p.Description.Contains(searchString) || p.Barcode.Contains(searchString) || p.Brand.Name.Contains(searchString) || p.IdType.Name.Contains(searchString) || p.District.Name.Contains(searchString) || p.Division.Name.Contains(searchString) || p.Upazila.Name.Contains(searchString) || p.FromCountry.Name.Contains(searchString) || p.Ward.Name.Contains(searchString));
            }
            else
            {
                Criteria = p => p.Barcode != null;
            }
        }
    }
}