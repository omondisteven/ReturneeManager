using ReturneeManager.Application.Specifications.Base;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Application.Specifications.Catalog
{
    public class IdTypeFilterSpecification : HeroSpecification<IdType>
    {
        public IdTypeFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Name.Contains(searchString) || p.Description.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}
