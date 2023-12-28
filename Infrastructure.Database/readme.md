# Создать миграцию
```cli 
cd Папка .sln
```
```cli
dotnet ef migrations add Name --startup-project .\Api\Api.csproj --output-dir Migrations --project .\Infrastructure.Database\Infrastructure.Database.csproj
```
# Накатить миграцию
```cli 
cd Папка .sln
```
```cli
dotnet ef database update --startup-project .\Api\Api.csproj --project Infrastructure.Database\Infrastructure.Database.csproj
```
# Удалить все миграции
```cli 
cd Папка .sln
```
```cli
dotnet ef database update 0 --startup-project .\Api\Api.csproj --project Infrastructure.Database\Infrastructure.Database.csproj
```