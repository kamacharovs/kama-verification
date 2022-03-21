FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
COPY /app/publish/KamaVerification.Email.Core /app/
EXPOSE 80
ENTRYPOINT ["dotnet", "KamaVerification.Email.Core.dll"]