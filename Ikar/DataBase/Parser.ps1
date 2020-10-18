
$jqPath = "C:\Users\AkiSoft\source\repos\Ikar\Ikar\ClientApp\node_modules\node-jq\bin\jq.exe"
Get-ChildItem -Filter *.json | Foreach-Object {
& $jqPath -c ".[]" $_.FullName > (-join($_.BaseName,".azure.json"))
Write-Output $_.BaseName
}