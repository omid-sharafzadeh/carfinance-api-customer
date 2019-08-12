FROM ubuntu:18.04 AS base

# Register Microsoft repository
RUN apt-get update
RUN apt-get install -y wget
RUN wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb

# Install dotnet-core
RUN apt-get install -y apt-transport-https
RUN apt-get update
RUN apt-get install -y dotnet-sdk-2.2=2.2.203-1

# Test dotnetcore
RUN dotnet --info

# Run tests
FROM base AS test
COPY . ./app/
WORKDIR ./app/
RUN dotnet restore
RUN dotnet test

# Publish
FROM base AS publish
COPY --from=test /app /app
RUN dotnet publish /app/CarFinance.Api.Customer/CarFinance.Api.Customer.csproj -o /publish

# Runtime
FROM base
COPY --from=publish /publish /publish
WORKDIR /publish

ENTRYPOINT ["dotnet", "CarFinance.Api.Customer.dll", "--server.urls", "http://0.0.0.0:5000"]
