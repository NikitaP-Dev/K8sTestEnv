FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["SampleApp/SampleApp.csproj", "SampleApp/"]
RUN dotnet restore "./SampleApp/SampleApp.csproj"
COPY . .
WORKDIR "/src/SampleApp"
RUN dotnet build "./SampleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./SampleApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SampleApp.dll"]

