Write-Output "Starting server"
cd .\Ikar\
dotnet build
$server = Start-Process powershell -Argument "dotnet run --urls http://0.0.0.0:5000"
Write-Output "Starting angular"
cd .\ClientApp\
Start-Process powershell -ArgumentList "-NoExit npm run host"

cd ..\..\