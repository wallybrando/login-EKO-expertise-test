# LoginEKO File Processing Service

# Overview

**File Processing API** provides endpoints to import telemetry files and to query (filter) telemetry data. It supports API and Database health check mehanisms.

# Directory structure

- _docs_ contains test CSV files and other resources like images for documentation.
- _dotnet-rest-api_ contains API service implemented in .NET 8
- _quarkus-rest-api_ **not imeplemented yet\***
- _README.MD_ represents service documentation
- _docker-compose.yml_ represents script to spin up service in containerized environment ([more information here](#how-to-run-system))

**\* It was planned to implement service also in Java with Quarkus framework. However, after limited time this implementation is ON HOLD.**

# Components:

- .NET Web API service,
- PostgreSQL database

# How to run system?

Clone this repository to local machine and use **_main_** branch to spin up system and review code.

Prerequisites:

- **Required: Docker Desktop or Docker CLI must be installed on host machine to spin up system.**
- Optional: _Visual Studio 2022_ to inspect and debug code
- Optional: _pgAdmin_ or _Visual Studio Code_ (with PostgreSQL extension installed) to query data from PostgreSQL database.

System can be run in two ways:

1.  In Docker containerized environment - recommended
2.  In "Hybrid" environment - only during development phase

**In both cases, database must run in Docker container.**

## 1. Docker Containerized environment (docker compose)

Both API service and PostgreSQL database run in Docker containers. Steps to spin up system are following:

1. Navigate to repo root location (`/login-EKO-expertise-test`),
2. Open terminal,
3. Execute following command:
   ```ps
   > docker compose up --build --force-recreate
   ```
   use **-d** flag to spin up system in the background if you prefere it.
4. After execution, you should see container logs like this:
   ![](/docs/resources/img/container-env-log.png)
   In _Docker client_ there will be two containers that are up and running:
   ![](/docs/resources/img/docker-client-containers.png)
5. Now open browser and enter in address bar this address:
   ```
   http://localhost:8080/swagger/index.html
   ```
   **Note**: Use **HTTP** instead of HTTPS, because self-signed certificate is not added to container. **TBD**
6. _Swagger UI_ should load:
   ![](/docs/resources/img/container-swagger-ui.png)
7. You are ready to consume API endpoints.

## 2. Hybrid environment

In hybrid environment, PostgreSQL **must** be ran in Docker Container and API can be ran from IDE (like _Visual Studio_). Use this approach during development phase. Steps to spin up system in hybrid environment are:

1. Navigate to repo root location (`/login-EKO-expertise-test`),
2. Open terminal,
3. Execute following command:
   ```ps
   > docker compose up --build --force-recreate db
   ```
   - `db` is name of PostgreSQL service defined in docker compose file.
   - use **-d** flag to spin up system in the background if you prefere it.
4. After execution, database will be up and running:
   ![](/docs/resources/img/container-env-log-db.png)
5. Open API project in IDE like _Visual Studio_ and run API.
6. Swagger UI will be loaded automatically
7. You are ready to use service or inspect and debug code.

# How to use system?

On page [API Endpoint documentation](./docs/API%20Endpoint%20documentation.md), you will find brief explanation how to consume endpoints.

Also, on page [Testing](./docs/Testing.md), you can find some test cases that were being tested during development phase.
