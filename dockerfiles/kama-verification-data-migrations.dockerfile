FROM mcr.microsoft.com/dotnet/sdk:6.0

RUN dotnet tool install --global dotnet-ef --version 6.0.2
ENV PATH="${PATH}:/root/.dotnet/tools"

WORKDIR /
COPY /KamaVerification.Data/ /app/data/migrations
COPY /KamaVerification.Data.Migrations/ /app/data/migrations
WORKDIR /app/data/migrations

CMD ["dotnet", "ef", "database", "update"]