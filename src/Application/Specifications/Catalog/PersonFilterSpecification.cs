using ReturneeManager.Application.Specifications.Base;
using ReturneeManager.Domain.Entities.Catalog;

namespace ReturneeManager.Application.Specifications.Catalog
{
    public class PersonFilterSpecification : HeroSpecification<Person>
    {
        public PersonFilterSpecification(string searchString)
        {
            Includes.Add(a => a.IdType);
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.IdNumber != null && (p.Name.Contains(searchString) || p.IdType.Name.Contains(searchString) || p.IdType.Name.Contains(searchString) || p.District.Name.Contains(searchString) || p.Division.Name.Contains(searchString) || p.Upazila.Name.Contains(searchString) || p.FromCountry.Name.Contains(searchString) || p.Ward.Name.Contains(searchString));
            }
            else
            {
                Criteria = p => p.IdNumber != null;
            }
        }
    }
}