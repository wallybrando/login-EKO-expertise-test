# How to...

## Containers

**1. How to build and run Docker image for .Net Web API?**

- Navigate to `dotnet-rest-api` directory,
- Execute following command:
  ```ps
  > docker build -t loginapi .
  ```
  where:
  - `loginapi` is custom name for Docker image

**2. How to run Docker image for .NET Web API?**

- Previous command must be executed.
- Execute following command:
  ```ps
  > docker run -p 8080:8080 loginapi
  ```
  where:
  - -p maps container ports to host ports ({container port}:{host port})
  - `loginapi` is Docker image name

**3. How to spin up whole system with docker compose?**

- Navigate to root location (`/login-EKO-expertise-test`),
- Execute following command:
  ```ps
  > docker compose up --build -d
  ```
  where:
  - --build force to rebuild image before create and run container(s)
  - -d indicates to spin up services in background (to see logs omit this parameter)
    _Note: This command will spin up API and Postgres DB._
