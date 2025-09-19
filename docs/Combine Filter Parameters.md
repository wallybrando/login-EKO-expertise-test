# Combine Filter Parameters

| Fields                              | Data Type       | Allowed Operations            |
| ----------------------------------- | --------------- | ----------------------------- |
| SerialNumber                        | string          | Equals, Contains              |
| Date                                | DateTime        | Equals, LessThan, GreaterThan |
| GPSLongitude                        | double          | Equals, LessThan, GreaterThan |
| GPSLatitude                         | double          | Equals, LessThan, GreaterThan |
| TotalWorkingHours                   | double          | Equals, LessThan, GreaterThan |
| EngineSpeedInRpm                    | int             | Equals, LessThan, GreaterThan |
| EngineLoadInPercentage              | double          | Equals, LessThan, GreaterThan |
| GroundSpeedInKmh                    | double          | Equals, LessThan, GreaterThan |
| DrumSpeedInRpm                      | int             | Equals, LessThan, GreaterThan |
| FanSpeedInRpm                       | int             | Equals, LessThan, GreaterThan |
| RotorStrawWalkerSpeedInRpm          | int             | Equals, LessThan, GreaterThan |
| SeparationLossesInPercentage        | nullable double | Equals, LessThan, GreaterThan |
| SieveLossesInPercentage             | nullable double | Equals, LessThan, GreaterThan |
| Chopper                             | bool            | Equals                        |
| DieselTankLevelInPercentage         | double          | Equals, LessThan, GreaterThan |
| NumberOfPartialWidths               | short           | Equals, LessThan, GreaterThan |
| FrontAttachment                     | bool            | Equals                        |
| MaxNumberOfPartialWidths            | short           | Equals, LessThan, GreaterThan |
| FeedRakeSpeedInRpm                  | int             | Equals, LessThan, GreaterThan |
| WorkingPosition                     | bool            | Equals                        |
| GrainTankUnloading                  | bool            | Equals                        |
| MainDriveStatus                     | bool            | Equals                        |
| ConcavePositionInMM                 | short           | Equals, LessThan, GreaterThan |
| UpperSievePositionInMM              | short           | Equals, LessThan, GreaterThan |
| LowerSievePositionInMM              | short           | Equals, LessThan, GreaterThan |
| GrainTank70                         | bool            | Equals                        |
| GrainTank100                        | bool            | Equals                        |
| GrainMoistureContentInPercentage    | nullable double | Equals, LessThan, GreaterThan |
| ThroughputTonsPerHour               | double          | Equals, LessThan, GreaterThan |
| RadialSpreaderSpeedInRpm            | nullable int    | Equals, LessThan, GreaterThan |
| GrainInReturnsInPercentage          | double          | Equals, LessThan, GreaterThan |
| ChannelPositionInPercentage         | double          | Equals, LessThan, GreaterThan |
| YieldMeasurement                    | bool            | Equals                        |
| ReturnsAugerMeasurementInPercentage | double          | Equals, LessThan, GreaterThan |
| MoistureMeasurement                 | bool            | Equals                        |
| TypeOfCrop                          | Enum            | Equals                        |
| SpecialCropWeightInGrams            | int             | Equals, LessThan, GreaterThan |
| AutoPilotStatus                     | bool            | Equals                        |
| CruisePilotStatus                   | Enum            | Equals                        |
| RateOfWorkInHaPerHour               | double          | Equals, LessThan, GreaterThan |
| YieldInTonsPerHour                  | nullable double | Equals, LessThan, GreaterThan |
| QuantimeterCalibrationFactor        | double          | Equals, LessThan, GreaterThan |
| SeparationSensitivityInPercentage   | double          | Equals, LessThan, GreaterThan |
| SieveSensitivityInPercentage        | double          | Equals, LessThan, GreaterThan |

**Note: Values for above defined enums are:**

| Fields            | String Value      | Number Value |
| ----------------- | ----------------- | ------------ |
| TypeOfCrop        | MAIZE, SUNFLOWERS | 1, 2         |
| CruisePilotStatus | STATUS_0          | 0            |

**STATUS_0 will be converted to correct values in DB and vice versa in response model.**
