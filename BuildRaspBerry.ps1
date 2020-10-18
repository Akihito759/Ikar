cd .\DataClient
dotnet build
dotnet publish -c Release -r linux-arm --self-contained
Copy-Item -Path ".\bin\Release\netcoreapp3.1\linux-arm\publish" -Destination $PSScriptRoot -Recurse -Force
cd ..\