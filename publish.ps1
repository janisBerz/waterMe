$key ="C:\sourceCode\waterMe\key\waterMe.ppk"
dotnet publish -r linux-arm /p:ShowLinkerSizeComparison=true
pushd .\*\bin\Debug\netcoreapp3.1\linux-arm\publish
## putty SCP client
pscp -i $key -v -r .\* pi@raspberrypi:/home/pi/waterMe/test
popd