using ReturneeManager.Domain.Contracts;

namespace ReturneeManager.Domain.Entities.Catalog
{
    public class Upazila : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
