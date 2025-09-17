namespace LoginEKO.FileProcessingService.Contracts.Responses.V1
{
    public class VehicleResponse
    {
        public string SerialNumber { get; init; }
        public string Date { get; init; }
        public string GPSLongitude { get; init; }
        public string GPSLatitude { get; init; }
        public string TotalWorkingHours { get; init; }
        public string EngineSpeedInRpm { get; init; }
    }
}
