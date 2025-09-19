# Test scenarios:

- **Import CSV test cases:**

  1. Prevent importing empty CSV file [PASSES]
  2. Prevent importing CSV with invalid data structure [PASSES]
  3. Prevent importing CSV with only headers provided [PASSES]
  4. Import valid CSV [PASSES]
  5. Prevent importing already imported CSV [PASSED]
  6. Prevent importing CSV with wrong filename [PASSED]
  7. Prevent importing non CSV files [PASSED]

- **Tractor telemetry test cases:**

  - Filter by SerialNumber:

    1. Filter by exist SerialNumber and allowed operation [PASSES]
    2. Prevent filtering by existing SerialNumber with invalid operation [PASSES]
    3. Prevent filtering by existint SerialNumber with NULL value [PASSES]
    4. Filter all data by existint SerialNumber with empty string and contains operation [PASSES] (PostgreSQL works in the same way - returns all data)

  - Filter by Date

    1. Filter by existing Date and allowed operation [PASSES]
    2. Prevent filter by duplicated Date filter and allowed operation [PASSES]
    3. Filter by data range [PASSES]
    4. Filter by NULL value [PASSES]
    5. Throw exception for empty string value [PASSES]

  - Filter by bool? (ActualStatusOfCreeper):

    1. Filter by true value [PASSES]
    2. Filter by false value [PASSES]
    3. Filter by null value [PASSES]
    4. Throw exception for empty string value [PASSES]
    5. Prevent filtering by wrong operation [PASSES]

  - Filter by Enum (AllWheelDriveStatus)

    1. Filter by valid status [PASSES]
    2. Prevent filtering by invalid status [PASSES]
    3. Prevent filtering by invalid operation [PASSES]
    4. Prevent filtering by NULL value [PASSES]
    5. Prevent filtering by emtpy string value [PASSES]

  - Filter by short? (CurrentGearShift)

    1. Filter by equals operator [PASSES]
    2. Filter by greater than operator [PASSES]
    3. Filter by less than operator [PASSES]
    4. Filter by data range [PASSES]
    5. Filter by null [PASSES]

  - Filter by double (AmbientTemperatureInCelsius)

    1. Filter by valid value [PASSES]
    2. Filter by invalid value [PASSES]
    3. Filter by range [PASSES]
    4. Prevent filtering by null [PASSES]
    5. Prevent filtering by invalid operation [PASSES]

- Combine telemetry test cases:

  - Filter by Enum (TypeOfCrop)

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
