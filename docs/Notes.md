Notes:

- If you apply CONTAINS operation with null value, service will return bad request.

GET Tractor tests:

- SerialNumber (string):

  - Equals with explicit null: returns emtpy collection [PASSES]
  - Equals with implicit null: 400 HTTP error - value field is requried [PASSES]
  - Equals with wrong serial number returns emtpy collection [PASSES]
  - Equals with correct serial number: returns telemetry data [PASSES]
  - Equals with empty string: returns empty collection [PASSES]
  - Equals with number: returns emtpy collection [PASSES AND FAILS] - should it throw exception
  - Equals with date: returns emtpy collection [PASSES AND FAILS] - should it throw exception

  - Contains with explicit null: throw exception [PASSES] \*\*\*\*
  - Contains with implicit null: 400 HTTP error - value field is requried [PASSES]
  - Contains with wrong serial number returns emtpy collection [PASSES]
  - Contains with correct serial number: returns telemetry data [PASSES]
  - Contains with empty string: returns empty collection [PASSES AND FAILS] -DB returns data????
  - Contains with number: returns emtpy collection [PASSES AND FAILS] - should it throw exception
  - Contains with date: returns emtpy collection [PASSES AND FAILS] - should it throw exception
