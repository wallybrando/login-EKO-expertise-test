namespace LoginEKO.FileProcessingService.Contracts.Responses.V1
{
    public class TractorTelemetryResponse : VehicleResponse
    {
        public string EngineLoadInPercentage { get; init; }
        public string FuelConsumptionPerHour { get; init; }
        public string GroundSpeedGearboxInKmh { get; init; }
        public string GroundSpeedRadarInKmh { get; init; }
        public string CoolantTemperatureInCelsius { get; init; }
        public string SpeedFrontPtoInRpm { get; init; }
        public string SpeedRearPtoInRpm { get; init; }
        public string CurrentGearShift { get; init; }
        public string AmbientTemperatureInCelsius { get; init; }
        public string ParkingBreakStatus { get; init; }
        public string TransverseDifferentialLockStatus { get; init; }
        public string AllWheelDriveStatus { get; init; }
        public string ActualStatusOfCreeper { get; init; }
    }
}
