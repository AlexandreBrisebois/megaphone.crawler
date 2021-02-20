#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY . .
WORKDIR /src
RUN dotnet restore "megaphone.crawler.sln"
# megaphone.standard
WORKDIR "/src/megaphone.standard"
RUN dotnet build "megaphone.standard.csproj" -c Release --no-restore
WORKDIR "/src/megaphone.standard.tests"
RUN dotnet build "megaphone.standard.tests.csproj" -c Release --no-restore
RUN dotnet test "megaphone.standard.tests.csproj" -c Release --no-build --logger trx --results-directory "../..TestResults/megaphone.standard.tests"
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
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Megaphone.Crawler.dll"]