dotnet restore
dotnet publish -c Release
gcloud app deploy bin\Release\netcoreapp3.1\publish\app.yaml