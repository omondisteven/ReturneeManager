namespace ReturneeManager.Client.Infrastructure.Routes
{
    public static class IdTypesEndpoints
    {
        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/v1/idTypes/export";

        public static string GetAll = "api/v1/idTypes";
        public static string Delete = "api/v1/idTypes";
        public static string Save = "api/v1/idTypes";
        public static string GetCount = "api/v1/idTypes/count";
    }
}