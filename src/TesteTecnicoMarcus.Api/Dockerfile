FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os csproj (cache de restore)
COPY TesteTecnicoMarcus.Api/TesteTecnicoMarcus.Api.csproj TesteTecnicoMarcus.Api/
COPY TesteTecnicoMarcus.Application/TesteTecnicoMarcus.Application.csproj TesteTecnicoMarcus.Application/
COPY TesteTecnicoMarcus.Domain/TesteTecnicoMarcus.Domain.csproj TesteTecnicoMarcus.Domain/
COPY TesteTecnicoMarcus.Infrastructure/TesteTecnicoMarcus.Infrastructure.csproj TesteTecnicoMarcus.Infrastructure/

RUN dotnet restore TesteTecnicoMarcus.Api/TesteTecnicoMarcus.Api.csproj

# Copia todo o código
COPY . .

WORKDIR /src/TesteTecnicoMarcus.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TesteTecnicoMarcus.Api.dll"]
