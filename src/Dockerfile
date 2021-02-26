FROM mcr.microsoft.com/azure-functions/dotnet:3.0-slim AS final
EXPOSE 80
WORKDIR /home/site/wwwroot
COPY . .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logupdateging__Console__IsEnabled=true \
    MEGAPHONE_RESOURCE_PUSH=true \
    MEGAPHONE_RESOURCE_API_URL=http://resources/api/resources \
    MEGAPHONE_CRAWL_MESSAGE_API_URL=http://crawler/api/crawl 