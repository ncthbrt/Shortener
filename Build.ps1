dotnet restore
pushd ./Frontend
npm install
npm run build
popd
dotnet publish -c Release -o ./publish -r win10-x64