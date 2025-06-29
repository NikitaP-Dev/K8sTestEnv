FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5099

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ExampleApp/Sample.Web.csproj", "Sample.Web/"]
RUN dotnet restore 
COPY . .
WORKDIR "/src/Sample.Web"
RUN dotnet build "./Sample.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Sample.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.Web.dll"]
