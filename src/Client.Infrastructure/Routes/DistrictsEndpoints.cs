namespace ReturneeManager.Client.Infrastructure.Routes
{
    public static class DistrictsEndpoints
    {
        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/v1/districts/export";

        public static string GetAll = "api/v1/districts";
        public static string Delete = "api/v1/districts";
        public static string Save = "api/v1/districts";
        public static string GetCount = "api/v1/districts/count";
    }
}