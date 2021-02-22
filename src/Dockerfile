FROM mcr.microsoft.com/azure-functions/dotnet:3.0 AS final
EXPOSE 80
COPY src/megaphone.crawler/bin/Release/netcoreapp3.1 /home/site/wwwroot
WORKDIR /home/site/wwwroot
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logupdateging__Console__IsEnabled=true