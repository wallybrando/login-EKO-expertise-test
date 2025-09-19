namespace LoginEKO.FileProcessingService.Contracts.Responses.V1.Telemetries
{
    public class CombineTelemetryResponse : AgroVehicleTelemetryResponse
    {
        public string? GroundSpeedInKmh { get; init; }
        public string? EngineLoadInPercentage { get; init; }
        public string? DrumSpeedInRpm { get; init; }
        public string? FanSpeedInRpm { get; init; }
        public string? RotorStrawWalkerSpeedInRpm { get; init; }
        public string? SeparationLossesInPercentage { get; init; }
        public string? SieveLossesInPercentage { get; init; }
        public string? Chopper { get; init; }
        public string? DieselTankLevelInPercentage { get; init; }
        public string? NumberOfPartialWidths { get; init; }
        public string? FrontAttachment { get; init; }
        public string? MaxNumberOfPartialWidths { get; init; }
        public string? FeedRakeSpeedInRpm { get; init; }
        public string? WorkingPosition { get; init; }
        public string? GrainTankUnloading { get; init; }
        public string? MainDriveStatus { get; init; }
        public string? ConcavePositionInMM { get; init; }
        public string? UpperSievePositionInMM { get; init; }
        public string? LowerSievePositionInMM { get; init; }
        public string? GrainTank70 { get; init; }
        public string? GrainTank100 { get; init; }
        public string? GrainMoistureContentInPercentage { get; init; }
        public string? ThroughputTonsPerHour { get; init; }
        public string? RadialSpreaderSpeedInRpm { get; init; }
        public string? GrainInReturnsInPercentage { get; init; }
        public string? ChannelPositionInPercentage { get; init; }
        public string? YieldMeasurement { get; init; }
        public string? ReturnsAugerMeasurementInPercentage { get; init; }
        public string? MoistureMeasurement { get; init; }
        public string? TypeOfCrop { get; init; }
        public string? SpecialCropWeightInGrams { get; init; }
        public string? AutoPilotStatus { get; init; }
        public string? CruisePilotStatus { get; init; }
        public string? RateOfWorkInHaPerHour { get; init; }
        public string? YieldInTonsPerHour { get; init; }
        public string? QuantimeterCalibrationFactor { get; init; }
        public string? SeparationSensitivityInPercentage { get; init; }
        public string? SieveSensitivityInPercentage { get; init; }
    }
}
