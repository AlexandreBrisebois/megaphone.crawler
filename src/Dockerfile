FROM mcr.microsoft.com/azure-functions/dotnet:3.0 AS final
EXPOSE 80
WORKDIR /home/site/wwwroot
COPY . .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logupdateging__Console__IsEnabled=true