# Test scenarios:

- **Import CSV test cases:**

  1. Prevent importing empty CSV file <span style="color: green;">[PASSES]</span>
  2. Prevent importing CSV with invalid data structure <span style="color: green;">[PASSES]</span>
  3. Prevent importing CSV with only headers provided <span style="color: green;">[PASSES]</span>
  4. Import valid CSV <span style="color: green;">[PASSES]</span>
  5. Prevent importing already imported CSV <span style="color: green;">[PASSES]</span>
  6. Prevent importing CSV with wrong filename <span style="color: green;">[PASSES]</span>
  7. Prevent importing non CSV files <span style="color: green;">[PASSES]</span>

- **Tractor telemetry test cases:**

  - Filter by SerialNumber:

    1. Filter by exist SerialNumber and allowed operation <span style="color: green;">[PASSES]</span>
    2. Prevent filtering by existing SerialNumber with invalid operation <span style="color: green;">[PASSES]</span>
    3. Prevent filtering by existint SerialNumber with NULL value <span style="color: green;">[PASSES]</span>
    4. Filter all data by existint SerialNumber with empty string and contains operation <span style="color: green;">[PASSES]</span>

       (PostgreSQL works in the same way - returns all data)

  - Filter by Date

    1. Filter by existing Date and allowed operation <span style="color: green;">[PASSES]</span>
    2. Prevent filter by duplicated Date filter and allowed operation <span style="color: green;">[PASSES]</span>
    3. Filter by data range <span style="color: green;">[PASSES]</span>
    4. Filter by NULL value <span style="color: green;">[PASSES]</span>
    5. Throw exception for empty string value <span style="color: green;">[PASSES]</span>

  - Filter by bool? (ActualStatusOfCreeper):

    1. Filter by true value <span style="color: green;">[PASSES]</span>
    2. Filter by false value <span style="color: green;">[PASSES]</span>
    3. Filter by null value <span style="color: green;">[PASSES]</span>
    4. Throw exception for empty string value <span style="color: green;">[PASSES]</span>
    5. Prevent filtering by wrong operation <span style="color: green;">[PASSES]</span>

  - Filter by Enum (AllWheelDriveStatus)

    1. Filter by valid status <span style="color: green;">[PASSES]</span>
    2. Prevent filtering by invalid status <span style="color: green;">[PASSES]</span>
    3. Prevent filtering by invalid operation <span style="color: green;">[PASSES]</span>
    4. Prevent filtering by NULL value <span style="color: green;">[PASSES]</span>
    5. Prevent filtering by emtpy string value <span style="color: green;">[PASSES]</span>

  - Filter by short? (CurrentGearShift)

    1. Filter by equals operator <span style="color: green;">[PASSES]</span>
    2. Filter by greater than operator <span style="color: green;">[PASSES]</span>
    3. Filter by less than operator <span style="color: green;">[PASSES]</span>
    4. Filter by data range <span style="color: green;">[PASSES]</span>
    5. Filter by null <span style="color: green;">[PASSES]</span>

  - Filter by double (AmbientTemperatureInCelsius)

    1. Filter by valid value <span style="color: green;">[PASSES]</span>
    2. Filter by invalid value <span style="color: green;">[PASSES]</span>
    3. Filter by range <span style="color: green;">[PASSES]</span>
    4. Prevent filtering by null <span style="color: green;">[PASSES]</span>
    5. Prevent filtering by invalid operation <span style="color: green;">[PASSES]</span>

- Combine telemetry test cases:

  - Filter by Enum (TypeOfCrop)

    1. Filter by correct value <span style="color: green;">[PASSES]</span>
    2. Prevent filtering by incorrect value <span style="color: green;">[PASSES]</span>
    3. Prevent filtering by null value <span style="color: green;">[PASSES]</span>
    4. Prevent filtering by empty string <span style="color: green;">[PASSES]</span>
    5. Prevent filtering by incorrect operation <span style="color: green;">[PASSES]</span>
