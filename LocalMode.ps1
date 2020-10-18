Write-Output "Starting angular"
cd  .\Ikar\ClientApp
Start-Process powershell -Argument "npm run start"

Write-Output "Starting server - Locally"
cd ..\
dotnet build
$server = Start-Process powershell -Argument "dotnet run"

Write-Output "Starting mock"
cd ..\DataClient
$mock = Start-Process powershell -Argument "dotnet run"

cd ..\
