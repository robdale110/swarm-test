# escape=`
FROM microsoft/dotnet:2.0.4-sdk-2.1.3-nanoserver-sac2016 AS builder

WORKDIR /src
COPY src/App.Api/App.Api.csproj .
RUN dotnet restore

COPY src/App.Api .
RUN dotnet publish

# app image
FROM microsoft/aspnetcore:2.0.4-nanoserver-sac2016

EXPOSE 5001
CMD ["dotnet", "App.Api.dll"]

HEALTHCHECK --interval=5s --start-period=15s --retries=3 `
 CMD powershell -command `  
    try { `
     $response = iwr http://localhost:5001/password; `
     if ($response.StatusCode -eq 200) { return 0} `
     else {return 1}; `
    } catch { return 1 }

WORKDIR /dotnetapp
COPY --from=builder /src/bin/Debug/netcoreapp2.0/publish .