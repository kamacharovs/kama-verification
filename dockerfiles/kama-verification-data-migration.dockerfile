FROM mcr.microsoft.com/dotnet/sdk:6.0
RUN dotnet tool install --global dotnet-ef --version 6.0.2
ENV PATH="${PATH}:/root/.dotnet/tools"
COPY ./KamaVerification.Data.Migrations/ ./
CMD ["dotnet", "ef", "database", "update"]