namespace ReturneeManager.Client.Infrastructure.Routes
{
    public static class DivisionsEndpoints
    {
        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/v1/divisions/export";

        public static string GetAll = "api/v1/divisions";
        public static string Delete = "api/v1/divisions";
        public static string Save = "api/v1/divisions";
        public static string GetCount = "api/v1/divisions/count";
    }
}