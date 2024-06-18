namespace ReturneeManager.Client.Infrastructure.Routes
{
    public static class WardsEndpoints
    {
        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/v1/wards/export";

        public static string GetAll = "api/v1/wards";
        public static string Delete = "api/v1/wards";
        public static string Save = "api/v1/wards";
        public static string GetCount = "api/v1/wards/count";
    }
}