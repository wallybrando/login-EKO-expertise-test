# API Documentation

API service exposes two endpoints to end-user:

1. `POST /api/v1/files/import-vehicle-telemetry` - used to import telemetry files into PostgreSQL database.
2. `POST api/v1/telemetries?pageNumber={pageNumer}&pageSize={pageSize}` - used to query/filter telemetry data by some criteria

To consume endpoint, you can use Swagger UI from Web browser or with Postman (collection is available at this location under name _LoginEKO .NET API Endpoints.postman.collection_)

## Import file endpoint

`POST /api/v1/files/import-vehicle-telemetry` enables user to import one file per request. Actually, user will be able to send request with multiple files for import but service will import only first file.

Validations:

- Service will prevent file import if filename does not start with "LD_A" or "LD_C".
- Service will prevent file import if file type is not allowed
- Service will prevent already imported file to be imported again
- Service will prevent importing file with no data

How to consume endpoint:

- If you use Swagger UI, just click on Chose file button, select file for import and send retquest
  ![](/docs/resources/img/import-endpoint-swagger.png)
- If you use Postman, go to Body section, select form-data option, enter File for Key column and select from dropdown File option, for Value column select file from local machine and send request.
  ![](/docs/resources/img/import-endpoint-postman.png)

On `/CSVs for testing` you have few valid and invalid files for testing.

## Query telemetry endpoint

`POST api/v1/telemetries?pageNumber={pageNumer}&pageSize={pageSize}` enables user to query telemetry by some filter criteria for both Tractors and Combines.

Filters:

- Filters are grouped by uniqueness which is defined by field name and operation. For example this filter is not valid because there are two filters with 'Data' field and 'LessThan' operation

  ```
      [
          {
              "field": "Date",
              "operation": "LessThan",
              "value": ""2023-03-31 14:34:38""
          },
          {
              "field": "Date",
              "operation": "LessThan",
              "value": ""2024-02-20 15:30:18""
          }
      ]
  ```

  In this example, filters are valid:

  ```
      [
          {
              "field": "Date",
              "operation": "GreaterThan",
              "value": ""2023-03-31 14:34:38""
          },
          {
              "field": "Date",
              "operation": "LessThan",
              "value": ""2024-02-20 15:30:18""
          }
      ]
  ```

  **However, there is one field for which uniqueness is not applied. That field is SerialNumber. SerialNumber is unique identifier for agro vehicles and we can apply multiple time same operation on this field. Example:**

  ```
  [
      {
          "field": "SerialNumber",
          "operation": "Equals",
          "value": "C7502627"
      },
      {
          "field": "SerialNumber",
          "operation": "Equals",
          "value": "A5304997"
      },
  ]
  ```

Validations:

- Service will prevent quering telemetries if filters are not unique (except for SerialNumber)
- Service will prevent quering telemetries if filter field does not exist
- Service will prevent quering telemetries if filter operation does not exist or not allowed
- Service will prevent quering telemetries if filter value is not in right format

On page [Tractor Filter Parameters](./Tractor%20Filter%20Parameters.md) you can find valid parameters for tractor's field.

On page [Combine Filter Parameters](./Combine%20Filter%20Parameters.md) you can find valid parameters for combine's fields.

How to consume endpoing?

- Whether you use Swagger UI or Postman, you just need to provide filters in following format through request body:
  ```
  [
      {
          "field": "..."
          "operation": "..."
          "value": "..."
      },
      {
         "field": "..."
          "operation": "..."
          "value": "..."
      },
      ....
  ]
  ```
