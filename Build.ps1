dotnet restore
dotnet publish -c Release -o ./publish -r win7-x64
Remove-Item ./publish/Frontend -Recurse -Force
pushd ./Frontend
npm install
npm run build
popd