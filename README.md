 
# UniProjectHub_BackEnd

This repository contains the backend services for UniProjectHub, a platform for managing schedules and courses.

## Technologies Used

- Framework: ASP.NET Core 8.0
- Database: Microsoft SQL Server with Entity Framework Core
- Authentication: JWT Bearer Authentication
- API Documentation: Swagger (OpenAPI)
- Mapping: AutoMapper
- Validation: FluentValidation

## Dependencies

- Microsoft.EntityFrameworkCore.SqlServer - 8.0.0
- AutoMapper - 12.0.0
- FluentValidation - 11.9.1
- Microsoft.AspNetCore.Authentication.JwtBearer - 8.0.5
- Swashbuckle.AspNetCore - 6.4.0

## Prerequisites

Before running the application, ensure you have the following installed:

- .NET SDK 8.0 or higher
- SQL Server (or use Docker for containerized development)

## Getting Started

### Clone the repository:

```bash
git clone https://github.com/your-username/UniProjectHub_BackEnd.git
cd UniProjectHub_BackEnd

##### Database Setup:
Create a SQL Server database and update the connection string in appsettings.json:

json
 
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=UniProjectHubDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
##### Run Migrations:
Apply database migrations using Entity Framework Core:

bash
 
dotnet ef database update
## Start the Application:
Run the backend application locally:

bash
 
dotnet run
## Explore APIs:
Open your browser and navigate to http://localhost:5275/swagger to view and test the APIs using Swagger.

## Contributing
We welcome contributions to UniProjectHub! To contribute, follow these steps:

1.Fork the repository.
2.Create your feature branch: git checkout -b feature/AmazingFeature.
3.Commit your changes: git commit -m 'Add some AmazingFeature'.
4.Push to the branch: git push origin feature/AmazingFeature.
5.Submit a pull request.
## License
This project is licensed under the MIT License - see the LICENSE file for details.

Feel free to adjust any details or add more specific instructions as per your project's requirements!

sql
Copy code

Copy and paste this section into your `README.md` file and adjust any placeholders (`your-username`, specific URLs, etc.) according to your project's setup. Let me know if you need further assistance!







## Packages Used
- **Microsoft.EntityFrameworkCore.SqlServer:** 5.0.17
- **AutoMapper:** 12.0.0
- **FluentValidation:** 11.9.1
- **Microsoft.AspNetCore.Http:** 2.1.34
- **Microsoft.Data.SqlClient:** 5.1.5
- **Microsoft.EntityFrameworkCore.Design:** 8.0.0
- **Microsoft.EntityFrameworkCore.Tools:** 8.0.0
- **Microsoft.Extensions.Configuration:** 8.0.0
- **Microsoft.Extensions.Configuration.Json:** 8.0.0
- **payOS:** 1.0.5
- **Microsoft.AspNetCore.Identity.EntityFrameworkCore:** 8.0.0
- **Microsoft.AspNetCore.Authentication.Google:** 8.0.5
- **Microsoft.AspNetCore.OpenApi:** 8.0.5
- **Microsoft.Data.SqlClient:** 5.2.0
- **Microsoft.EntityFrameworkCore.Analyzers:** 8.0.0
- **Microsoft.EntityFrameworkCore.Design:** 8.0.0
- **Microsoft.EntityFrameworkCore.SqlServer:** 8.0.0
- **Microsoft.EntityFrameworkCore.Tools:** 8.0.0
- **Microsoft.Extensions.Identity.Core:** 8.0.5
- **RestSharp:** 110.2.0
- **Swashbuckle.AspNetCore:** 6.4.0
- **AutoMapper:** 12.0.0
- **AutoMapper.Extensions.Microsoft.DependencyInjection:** 12.0.0
- **Firebase.Auth:** 1.0.0
- **FirebaseStorage.net:** 1.0.3
- **FluentValidation:** 11.9.1
- **FluentValidation.AspNetCore:** 11.3.0
- **FluentValidation.DependencyInjectionExtensions:** 11.9.1
- **MailKit:** 4.5.0
- **Microsoft.AspNetCore.Authentication.JwtBearer:** 8.0.5
- **Microsoft.AspNetCore.Cors:** 2.2.0
- **Microsoft.AspNetCore.Identity.EntityFrameworkCore:** 8.0.0
- **Microsoft.AspNetCore.Mvc.NewtonsoftJson:** 8.0.0
- **Microsoft.Extensions.Identity.Core:** 8.0.5
- **Microsoft.IdentityModel.JsonWebTokens:** 7.5.2
- **Microsoft.IdentityModel.Protocols.OpenIdConnect:** 7.5.2
- **MimeKit:** 4.5.0
- **Newtonsoft.Json:** 13.0.3
- **payOS:** 1.0.5

Detected automatically on Jun 17, 2024 (NuGet)
