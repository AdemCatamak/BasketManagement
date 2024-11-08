FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy everything else and build
COPY . ./
RUN dotnet publish ./Src/BasketManagement.WebApi/BasketManagement.WebApi.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0

WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 80
HEALTHCHECK --interval=5s --timeout=3s --retries=3 CMD curl -f / http://localhost:80/health-check || exit 1 

ENTRYPOINT ["./BasketManagement.WebApi"]
