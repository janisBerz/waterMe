$key = "C:\sourceCode\waterMe\key\waterMe.ppk"
dotnet publish -c debug -r linux-arm /p:ShowLinkerSizeComparison=true  WaterMe\WaterMe.csproj

if (Test-Path -Path .\*\bin\Debug\netcoreapp3.1\linux-arm\publish) {
    pushd .\WaterMe\bin\Debug\netcoreapp3.1\linux-arm\publish
    ## putty SCP client
    pscp -i $key -v -r .\* pi@raspberrypi:/home/pi/waterMe/test
    popd
}
