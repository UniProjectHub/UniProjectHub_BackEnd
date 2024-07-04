# UniProjectHub_BackEnd
UniProjectHub_BackEnd
This repository contains the backend services for UniProjectHub, a platform for managing schedules and courses.

Technologies Used
Framework: ASP.NET Core 8.0
Database: Microsoft SQL Server with Entity Framework Core
Authentication: JWT Bearer Authentication
API Documentation: Swagger (OpenAPI)
Mapping: AutoMapper
Validation: FluentValidation
Dependencies
Microsoft.EntityFrameworkCore.SqlServer - 8.0.0
AutoMapper - 12.0.0
FluentValidation - 11.9.1
Microsoft.AspNetCore.Authentication.JwtBearer - 8.0.5
Swashbuckle.AspNetCore - 6.4.0
Prerequisites
Before running the application, ensure you have the following installed:

.NET SDK 8.0 or higher
SQL Server (or use Docker for containerized development)
Getting Started
Clone the repository:

bash
Copy code
git clone https://github.com/your-username/UniProjectHub_BackEnd.git
cd UniProjectHub_BackEnd
Database Setup:

Create a SQL Server database and update the connection string in appsettings.json.
json
Copy code
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=UniProjectHubDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
Run Migrations:

bash
Copy code
dotnet ef database update
Start the Application:

bash
Copy code
dotnet run
Explore APIs:

Open your browser and navigate to http://localhost:5275/swagger to view and test the APIs using Swagger.
Contributing
We welcome contributions to UniProjectHub! To contribute, follow these steps:

Fork the repository.
Create your feature branch: git checkout -b feature/AmazingFeature.
Commit your changes: git commit -m 'Add some AmazingFeature'.
Push to the branch: git push origin feature/AmazingFeature.
Submit a pull request.
License
This project is licensed under the MIT License - see the LICENSE file for details.
