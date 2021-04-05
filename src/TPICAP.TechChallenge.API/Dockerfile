#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/TPICAP.TechChallenge.API/TPICAP.TechChallenge.API.csproj", "src/TPICAP.TechChallenge.API/"]
COPY ["src/TPICAP.TechChallenge.Infrastructure/TPICAP.TechChallenge.Infrastructure.csproj", "src/TPICAP.TechChallenge.Infrastructure/"]
COPY ["src/TPICAP.TechChallenge.Data/TPICAP.TechChallenge.Data.csproj", "src/TPICAP.TechChallenge.Data/"]
COPY ["src/TPICAP.TechChallenge.Model/TPICAP.TechChallenge.Model.csproj", "src/TPICAP.TechChallenge.Model/"]
RUN dotnet restore "src/TPICAP.TechChallenge.API/TPICAP.TechChallenge.API.csproj"
COPY . .
WORKDIR "/src/src/TPICAP.TechChallenge.API"
RUN dotnet build "TPICAP.TechChallenge.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TPICAP.TechChallenge.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TPICAP.TechChallenge.API.dll"]