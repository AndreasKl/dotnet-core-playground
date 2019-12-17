# Demo playground for dotnet core

## Prometheus endpoint
```
https://localhost:5001/metrics
```

## Release build
```bash
../PostgresWithEFAndDapper/PostgresWithEFAndDapper
$ dotnet publish -c Release

../PostgresWithEFAndDapper/PostgresWithEFAndDapper
$ docker build -t app -f Dockerfile .
```

## Run a postgres docker container
```bash
$ docker run --name default-postgres -p5432:5432 -e POSTGRES_PASSWORD=postgres -d postgres
$ docker start default-postgres
```

## Running tests
```bash
PostgresWithEFAndDapper.Test$ dotnet test
```