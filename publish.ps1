$key ="C:\sourceCode\waterMe\key\waterMe.ppk"
dotnet publish -r linux-arm /p:ShowLinkerSizeComparison=true
pushd .\*\bin\Debug\netcoreapp3.1\linux-arm\publish
pscp -i $key -C -v -r .\* pi@raspberrypi:/home/pi/waterMe/test
popd