namespace LoginEKO.FileProcessingService.Api
{
    public static class ApiEndpoints
    {
        public const string ApiBase = "api";

        public static class Vehicles
        {
            public const string Base = $"{ApiBase}/vehicles";

            public const string Import = $"{Base}/telemetry-file";
        }
    }
}
