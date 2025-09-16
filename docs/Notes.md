# Notes:

- Bool field valid value: null, "true" or "false"
- Bool field invalid values: "1", "0"

- Enum field valid value: "Active", "Inactive", "0", "1", "2" - DB throws error when enum value is not supported (try to fix this)
- Date time field example: 2022-10-07 10:13:04

- If you apply CONTAINS operation with null value, service will return bad request.
- If you apply eqauls operation with null, empty string,
  GET Tractor tests:

Tractor:

- string:
  - SerialNumber
  - "A5304997"
- datetime:
  - Date
  - "2023-03-31 14:34:38"
- bool?:
  - ActualStatusOfCreeper
  - true/false/null
- Enum:
  - TransverseDifferentialLockStatus
  - "STATUS_2"/0
