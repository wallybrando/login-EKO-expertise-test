namespace LoginEKO.FileProcessingService.Api
{
    public static class ApiEndpoints
    {
        public const string ApiBase = "api";

        public static class Files
        {
            public const string Base = $"{ApiBase}/files";

            public const string Import = $"{Base}/import-vehicle-telemetry";
        }

        public static class Telemetries
        {
            public const string Base = $"{ApiBase}/telemetries";

            public const string Tractors = $"{Base}/tractors";
            public const string Combines = $"{Base}/combines";
        }
    }
}
