dotnet restore
pushd ./Frontend
npm install
npm run build
popd
dotnet publish -c Release -o ./publish