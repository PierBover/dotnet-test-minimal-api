# Dotnet Minimal API

Download dotnet
https://dotnet.microsoft.com/en-us/download

Create repo and init.

Create .gitginore file:
`dotnet new gitignore`

Scaffold empy app:
`dotnet new web`

Add HTTPS:
`dotnet dev-certs https --trust`

Move HTTPS profile to the first position in [`Properties/launchSettings.json`](https://github.com/PierBover/dotnet-test-minimal-api/blob/main/Properties/launchSettings.json) so that `dotnet run` will use this profile by default.


## Entity Framework and database

Install EF for:
```cs
// in memory
dotnet add package Microsoft.EntityFrameworkCore.InMemory

// sqlite
dotnet add package Microsoft.EntityFrameworkCore.Sqlite

// Postgres
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

Create Postgres database.

Add db connection string into [`appsettings.json`](https://github.com/PierBover/dotnet-test-minimal-api/blob/main/appsettings.json#L2-L4).

[Create models](https://github.com/PierBover/dotnet-test-minimal-api/blob/main/Models/Fruit.cs)

[Create database context](https://github.com/PierBover/dotnet-test-minimal-api/blob/caf6bc7262637c0571474e30dcd9261e634ebc12/Data/AppDbContext.cs)

[Add db context to app builder](https://github.com/PierBover/dotnet-test-minimal-api/blob/caf6bc7262637c0571474e30dcd9261e634ebc12/Program.cs#L9-L11)

Install EF CLI globally to be able to run migrations:
`dotnet tool install --global dotnet-ef`

Install EF Design:
`dotnet add package Microsoft.EntityFrameworkCore.Design`

Create and run first migration
```
dotnet ef migrations add Init
dotnet ef database update
```

## OpenAPI and Scalar

Install OpenAPI:
`dotnet add package Microsoft.AspNetCore.OpenApi`

[Add OpenAPI to builder](https://github.com/PierBover/dotnet-test-minimal-api/blob/main/Program.cs#L13)

[Map OpenAPI endpoints](https://github.com/PierBover/dotnet-test-minimal-api/blob/main/Program.cs#L39)

Check it's working:
`https://localhost:7243/openapi/v1.json`

Install Scalar:
`dotnet add package Scalar.AspNetCore`

[Add Scalar to the app](https://github.com/PierBover/dotnet-test-minimal-api/blob/main/Program.cs#L33-L35)

Check it's working:
`https://localhost:7243/scalar/v1`

## Identity

Install Identity:
`dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore`

[Adapt DB context to use `IdentityDbContext`](https://github.com/PierBover/dotnet-test-minimal-api/blob/main/Data/AppDbContext.cs)

[Add auth to the app builder](https://github.com/PierBover/dotnet-test-minimal-api/blob/main/Program.cs#L14-L15)

[Configure password requirements](https://github.com/PierBover/dotnet-test-minimal-api/blob/main/Program.cs#L17-L24)

[Configure cookie settings](https://github.com/PierBover/dotnet-test-minimal-api/blob/main/Program.cs#L26-L29)

[Add auth middleware to the app](https://github.com/PierBover/dotnet-test-minimal-api/blob/main/Program.cs#L38)

[Map auth endpoints](https://github.com/PierBover/dotnet-test-minimal-api/blob/main/Program.cs#L40)

Create and run the auth migration
```
dotnet ef migrations add Identity
dotnet ef database update
```

## Other steps

[Configure an email provider](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm?view=aspnetcore-9.0&tabs=visual-studio#configure-an-email-provider)

