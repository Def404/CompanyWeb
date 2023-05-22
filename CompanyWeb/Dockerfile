FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CompanyWeb.csproj", "./"]
RUN dotnet restore "CompanyWeb.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "CompanyWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CompanyWeb.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompanyWeb.dll"]
