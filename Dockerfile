FROM mcr.microsoft.com/dotnet/sdk:5.0 AS test

WORKDIR /src
COPY . .

WORKDIR PlutoRover.Tests
ENTRYPOINT ["/bin/bash", "test-entrypoint.sh"]