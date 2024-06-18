namespace ReturneeManager.Client.Infrastructure.Routes
{
    public static class FromCountriesEndpoints
    {
        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/v1/fromCountries/export";

        public static string GetAll = "api/v1/fromCountries";
        public static string Delete = "api/v1/fromCountries";
        public static string Save = "api/v1/fromCountries";
        public static string GetCount = "api/v1/fromCountries/count";
    }
}