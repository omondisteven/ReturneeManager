using ReturneeManager.Domain.Contracts;

namespace ReturneeManager.Domain.Entities.Catalog
{
    public class Gender : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
