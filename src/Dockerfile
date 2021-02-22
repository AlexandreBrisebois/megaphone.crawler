#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

ARG PAT

FROM mcr.microsoft.com/azure-functions/dotnet:3.0 AS base
WORKDIR /home/site/wwwroot
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
COPY . .
WORKDIR /src
RUN "dotnet nuget add source https://nuget.pkg.github.com/AlexandreBrisebois/index.json -n github -p "${PAT}" --store-password-in-clear-text"
RUN dotnet restore "megaphone.crawler.sln"
# megaphone.crawler.core
WORKDIR "/src/megaphone.crawler.core"
RUN dotnet build "megaphone.crawler.core.csproj" -c Release --no-restore
WORKDIR "/src/megaphone.crawler.core.tests"
RUN dotnet build "megaphone.crawler.core.tests.csproj" -c Release --no-restore
RUN dotnet test "megaphone.crawler.core.tests.csproj" -c Release --no-build --logger trx --results-directory "../../TestResults/megaphone.crawler.core.tests"
# megaphone.crawler
WORKDIR "/src/megaphone.crawler"
RUN dotnet build "megaphone.crawler.csproj" -c Release --no-restore
WORKDIR "/src/megaphone.crawler.tests"
RUN dotnet build "megaphone.crawler.tests.csproj" -c Release --no-restore
RUN dotnet test "megaphone.crawler.tests.csproj" -c Release --no-build --logger trx --results-directory "../../TestResults/megaphone.crawler.tests"

FROM build AS publish
WORKDIR "/src/megaphone.crawler"
RUN dotnet publish "megaphone.crawler.csproj" -c Release --no-build -o /app/publish

FROM base AS final
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true