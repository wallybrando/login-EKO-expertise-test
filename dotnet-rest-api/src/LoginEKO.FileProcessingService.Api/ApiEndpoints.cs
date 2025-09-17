namespace LoginEKO.FileProcessingService.Api
{
    public static class ApiEndpoints
    {
        private const string ApiBase = "api";

        public static class V1
        {
            private const string VersionBase = $"{ApiBase}/v1";
            public static class Files
            {
                private const string Base = $"{VersionBase}/files";

                public const string Get = Base;
                public const string Import = $"{Base}/import-vehicle-telemetry";
            }

            public static class Telemetries
            {
                private const string Base = $"{VersionBase}/telemetries";

                public const string All = Base;
                public const string Tractors = $"{Base}/tractors";
                public const string Combines = $"{Base}/combines";

            }
        }
    }
}
