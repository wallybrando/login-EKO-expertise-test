namespace LoginEKO.FileProcessingService.Api
{
    public static class ApiEndpoints
    {
        public const string ApiBase = "api";

        public static class Files
        {
            public const string Base = $"{ApiBase}/files";

            public const string Upload = Base;
            public const string Get = $"{Base}/{{id}}";
        }
    }
}
