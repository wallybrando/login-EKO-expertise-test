# Tractor Filter Parameters

| Fields                           | Data Type       | Allowed Operations            |
| -------------------------------- | --------------- | ----------------------------- |
| SerialNumber                     | string          | Equals, Contains              |
| Date                             | DateTime        | Equals, LessThan, GreaterThan |
| GPSLongitude                     | double          | Equals, LessThan, GreaterThan |
| GPSLatitude                      | double          | Equals, LessThan, GreaterThan |
| TotalWorkingHours                | double          | Equals, LessThan, GreaterThan |
| EngineSpeedInRpm                 | int             | Equals, LessThan, GreaterThan |
| EngineLoadInPercentage           | double          | Equals, LessThan, GreaterThan |
| FuelConsumptionPerHour           | nullable double | Equals, LessThan, GreaterThan |
| GroundSpeedGearboxInKmh          | double          | Equals, LessThan, GreaterThan |
| GroundSpeedRadarInKmh            | nullable int    | Equals, LessThan, GreaterThan |
| CoolantTemperatureInCelsius      | int             | Equals, LessThan, GreaterThan |
| SpeedFrontPtoInRpm               | int             | Equals, LessThan, GreaterThan |
| SpeedRearPtoInRpm                | int             | Equals, LessThan, GreaterThan |
| CurrentGearShift                 | nullable short  | Equals, LessThan, GreaterThan |
| AmbientTemperatureInCelsius      | double          | Equals, LessThan, GreaterThan |
| ParkingBreakStatus               | Enum            | Equals                        |
| TransverseDifferentialLockStatus | Enum            | Equals                        |
| AllWheelDriveStatus              | Enum            | Equals                        |
| ActualStatusOfCreeper            | bool            | Equals                        |

**Note: Values for above defined enums are:**

| Fields                           | String Value               | Number Value |
| -------------------------------- | -------------------------- | ------------ |
| ParkingBreakStatus               | STATUS_3                   | 3            |
| TransverseDifferentialLockStatus | STATUS_0                   | 0            |
| AllWheelDriveStatus              | Inactive, Active, STATUS_2 | 0, 1, 2      |

**STATUS_3, STATUS_2, STATUS_0 will be converted to correct values in DB and vice versa in response model.**
