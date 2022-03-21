FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
COPY /app/publish/KamaVerification.Core /app/
EXPOSE 80
ENTRYPOINT ["dotnet", "KamaVerification.Core.dll"]