namespace ReturneeManager.Application.Requests.Catalog
{
    public class GetAllPagedPersonsRequest : PagedRequest
    {
        public string SearchString { get; set; }
    }
}