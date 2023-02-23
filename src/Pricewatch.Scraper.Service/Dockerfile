#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM selenium/standalone-chrome AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . ./
RUN dotnet restore "Pricewatch.Scraper.Service/Pricewatch.Scraper.Service.csproj"
COPY . .
WORKDIR "/src/Pricewatch.Scraper.Service"
RUN dotnet build "Pricewatch.Scraper.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pricewatch.Scraper.Service.csproj" -c Release --self-contained -o /app/publish --runtime linux-x64 /p:PublishTrimmed=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./Pricewatch.Scraper.Service"]