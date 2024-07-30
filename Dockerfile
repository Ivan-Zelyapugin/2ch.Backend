FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["2ch.WebApi/2ch.WebApi.csproj", "2ch.WebApi/"]
COPY ["2ch.Application/2ch.Application.csproj", "2ch.Application/"]
COPY ["2ch.Domain/2ch.Domain.csproj", "2ch.Domain/"]
COPY ["2ch.Persistence/2ch.Persistence.csproj", "2ch.Persistence/"]
RUN dotnet restore "2ch.WebApi/2ch.WebApi.csproj"
COPY . .
WORKDIR /src/2ch.WebApi
RUN dotnet build "2ch.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "2ch.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "2ch.WebApi.dll"]
