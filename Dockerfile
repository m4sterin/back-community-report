FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 4100
COPY publish_output .
ENTRYPOINT ["dotnet", "back_community_report.dll"]