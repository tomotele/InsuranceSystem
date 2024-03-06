#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InsuranceSystem.API/InsuranceSystem.API.csproj", "InsuranceSystem.API/"]
COPY ["InsuranceSystem.Entities/InsuranceSystem.Entities.csproj", "InsuranceSystem.Entities/"]
COPY ["InsuranceSystem.RepositoryServices/InsuranceSystem.RepositoryServices.csproj", "InsuranceSystem.RepositoryServices/"]
COPY ["InsuranceSystem.RepositoryContracts/InsuranceSystem.RepositoryContracts.csproj", "InsuranceSystem.RepositoryContracts/"]
COPY ["InsuranceSystem.Services/InsuranceSystem.Services.csproj", "InsuranceSystem.Services/"]
COPY ["InsuranceSystem.ServiceContracts/InsuranceSystem.ServiceContracts.csproj", "InsuranceSystem.ServiceContracts/"]
RUN dotnet restore "InsuranceSystem.API/InsuranceSystem.API.csproj"
COPY . .
WORKDIR "/src/InsuranceSystem.API"
RUN dotnet build "InsuranceSystem.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InsuranceSystem.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InsuranceSystem.API.dll"]