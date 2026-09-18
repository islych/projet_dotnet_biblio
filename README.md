# Library Management — ASP.NET Core

Academic web application for managing a digital library with ASP.NET Core MVC, Razor views, Entity Framework Core and SQL Server.

## Features

- User registration, login and session-based access
- Administrator and user roles
- Searchable book catalog with category filtering
- Book details, favorites and PDF downloads
- Administration workflows for books, categories and users
- Download history and dashboard statistics
- Cover-image and PDF upload support

## Architecture

The application follows a layered MVC structure:

```text
Razor views
    |
MVC controllers
    |
Services
    |
Repositories
    |
Entity Framework Core
    |
SQL Server
```

The domain includes accounts, users, administrators, books, categories, favorites and download records. Entity Framework Core manages the relational model using a code-first approach.

## Technology stack

- .NET 8 and ASP.NET Core MVC
- Razor views
- Entity Framework Core 8
- SQL Server
- Bootstrap and jQuery

## Local setup

### Prerequisites

- .NET 8 SDK
- SQL Server LocalDB, Express or another compatible SQL Server instance

### Run the project

```bash
git clone https://github.com/islych/projet_dotnet_biblio.git
cd projet_dotnet_biblio
dotnet restore
dotnet run
```

Before starting, set `ConnectionStrings:DefaultConnection` in `appsettings.json` or with an environment-specific configuration file. Do not commit production credentials.

The development database is initialized on first launch by `DbInitializer`. See [MIGRATIONS.md](MIGRATIONS.md) for the Entity Framework migration workflow.

## Project structure

```text
Controllers/    MVC request handling
Data/           DbContext and database initialization
Models/         Domain entities
Repositories/   Data-access abstractions
Services/       Business logic
Views/          Razor views
wwwroot/        Styles, scripts and uploaded assets
```

## Security note

The current authentication flow is intended for an academic demonstration. A production version should use ASP.NET Core Identity, strong password hashing, managed secrets and stricter file-upload validation.

