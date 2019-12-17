FROM  mcr.microsoft.com/dotnet/core/aspnet:3.1.0-buster-slim
WORKDIR /app
COPY bin/Release/netcoreapp3.1/publish/ .
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "PostgresWithEFAndDapper.dll"]