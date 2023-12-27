# Создать миграцию
1. cd `Папка .sln`
2. `dotnet ef migrations add Name --startup-project .\Api\Api.csproj --output-dir Migrations --project .\Infrastructure.Database\Infrastructure.Database.csproj`
# Накатить миграцию
1. cd `Папка .sln`
2. `dotnet ef database update --startup-project .\Api\Api.csproj --project Infrastructure.Database\Infrastructure.Database.csproj`
# Удалить все миграции
1. cd `Папка .sln`
2. `dotnet ef database update 0 --startup-project .\Api\Api.csproj --project Infrastructure.Database\Infrastructure.Database.csproj`