FROM mcr.microsoft.com/azure-functions/dotnet:3.0 AS final
EXPOSE 80
WORKDIR /home/site/wwwroot
COPY /home/runner/work/crawler/crawler/src/app/publish/ .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logupdateging__Console__IsEnabled=true