# Test scenarios:

- Import CSV test cases:

  1. Prevent importing empty CSV file [PASSES]
  2. Prevent importing CSV with invalid data structure [PASSES]
  3. Prevent importing CSV with only headers provided [PASSES]
  4. Import valid CSV [PASSES]

- Tractor telemetry test cases:

  1. Filter by SerialNumber

  - Filter by exist SerialNumber and allowed operation [PASSES]
  - Prevent filtering by existing SerialNumber with invalid operation [PASSES]
  - Prevent filtering by existint SerialNumber with NULL value [PASSES]
  - Filter all data by existint SerialNumber with empty string and contains operation [PASSES] (PostgreSQL works in the same way - returns all data)

  2. Filter by Date

  - Filter by existing Date and allowed operation [PASSES]
  - Prevent filter by duplicated Date filter and allowed operation [PASSES]
  - Filter by data range [PASSES]
  - Filter by NULL value [PASSES]
  - Throw exception for empty string value [PASSES]

  3. Filter by bool? (ActualStatusOfCreeper):

  - Filter by true value [PASSES]
  - Filter by false value [PASSES]
  - Filter by null value [PASSES]
  - Throw exception for empty string value [PASSES]
  - Prevent filtering by wrong operation [PASSES]

  4. Filter by Enum (AllWheelDriveStatus)

  - Filter by valid status [PASSES]
  - Prevent filtering by invalid status [PASSES]
  - Prevent filtering by invalid operation [PASSES]
  - Prevent filtering by NULL value [PASSES]
  - Prevent filtering by emtpy string value [PASSES]

  5. Filter by short? (CurrentGearShift)

  - Filter by equals operator [PASSES]
  - Filter by greater than operator [PASSES]
  - Filter by less than operator [PASSES]
  - Filter by data range [PASSES]
  - Filter by null [PASSES]

6. Filter by double (AmbientTemperatureInCelsius)

- Filter by valid value
- Filter by invalid value
- Filter by range
- Prevent filtering by null
- Prevent filtering by invalid operation

# Notes:

- Bool field valid value: null, "true" or "false"
- Bool field invalid values: "1", "0"

- Enum field valid value: "Active", "Inactive", "0", "1", "2" - DB throws error when enum value is not supported (try to fix this)
- Date time field example: 2022-10-07 10:13:04

- If you apply CONTAINS operation with null value, service will return bad request.
- If you apply eqauls operation with null, empty string,
  GET Tractor tests:
- NULL check cannot be applied to non-nullable fields
  Tractor:

- string:
  - SerialNumber
  - "A5304997"
- datetime:
  - Date
  - "2023-03-31 14:34:38"
- bool?:
  - ActualStatusOfCreeper
  - true/false/null/"true"/"false"
- Enum:
  - AllWheelDriveStatus
  - "Active"/1, "Inactive/0", "Status_2"/2
- short?:
  - CurrentGearShift
  - 1/null
- double:
  - AmbientTemperatureInCelsius
  - 2/21.2

---

- Int:
  - RotorStrawWalkerSpeedInRpm
  - 80, "80"
- Double?:
  - GrainMoistureContentInPercentage
  - "10.6", 10.6, null
- Bool:
  - YieldMeasurement
  - true, "true"
- Enum:

  - CruisePilotStatus
  - STATUS_0, "Status_0", "0", 0

  - CropType
  - Maize, Sunflowers
