FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./src/BLTodoist/BLTodoist.csproj", "./BLTodoist/"]
COPY ["./src/BLTodoist.Contracts/BLTodoist.Contracts.csproj", "./BLTodoist.Contracts/"]
RUN dotnet nuget add source https://baget.dev.gbms.site/v3/index.json -n gbms
RUN dotnet restore "./BLTodoist/BLTodoist.csproj"
RUN dotnet restore "./BLTodoist.Contracts/BLTodoist.Contracts.csproj"
COPY ["./src/BLTodoist/", "./BLTodoist/"]
COPY ["./src/BLTodoist.Contracts/", "./BLTodoist.Contracts/"]
WORKDIR "./BLTodoist"
RUN dotnet build "BLTodoist.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BLTodoist.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BLTodoist.dll"]
