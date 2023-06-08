## Basic Data Access Example

## Set Up

### Database
#### 1. Create database structure
```
dotnet ef migrations add Initial --context DAL/SQLiteContextOld --output-dir Migrations/SqlServer
dotnet ef database update --context SqlServerContext
```