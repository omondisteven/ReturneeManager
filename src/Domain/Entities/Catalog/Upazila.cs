using ReturneeManager.Domain.Contracts;

namespace ReturneeManager.Domain.Entities.Catalog
{
    public class FromCountry : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
