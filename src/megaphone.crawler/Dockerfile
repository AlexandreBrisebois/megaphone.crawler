#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-functions/dotnet:3.0 AS base
WORKDIR /home/site/wwwroot
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["megaphone.crawler/megaphone.crawler.csproj", "megaphone.crawler/"]
COPY ["megaphone.crawler.core/megaphone.crawler.core.csproj", "megaphone.crawler.core/"]
RUN dotnet restore "megaphone.crawler/megaphone.crawler.csproj"
COPY . .
WORKDIR "/src/megaphone.crawler"
RUN dotnet build "megaphone.crawler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "megaphone.crawler.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true \
    MEGAPHONE_RESOURCE_PUSH=true \
    MEGAPHONE_RESOURCE_API_URL=http://HD:2002