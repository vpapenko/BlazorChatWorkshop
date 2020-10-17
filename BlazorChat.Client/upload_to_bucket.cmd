dotnet restore
dotnet publish -c Release
gsutil rsync -R bin\Release\netstandard2.1\publish\wwwroot gs://blazor-chat-client
gsutil web set -m index.html -e index.html gs://blazor-chat-client
gsutil setmeta -r -h "Cache-control:no-cache" gs://blazor-chat-client