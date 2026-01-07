💰 ExpensesTracker API



ExpensesTracker is a secure RESTful API built with ASP.NET Core (.NET 8) that allows users to manage their personal expenses.

It enables authenticated users to organize expenses by category, define budgets, and track their financial data securely.



This project was developed following real-world enterprise best practices in terms of architecture, validation, logging, and error handling.



🚀 Key Features



🔐 Authentication \& Authorization using ASP.NET Identity + JWT



👤 Multi-user support (data isolated per user)



🗂️ Category management



💸 Expense management (CRUD)



📊 Budget management per category



✅ Input validation with FluentValidation



🧩 Layered architecture (Controllers / Services / Repositories)



🧠 Global exception handling (custom middleware)



🪵 Structured logging with Serilog



📘 API documentation with Swagger / OpenAPI



🧪 Unit testing with xUnit (business logic covered)



🏗️ Project Architecture



The project follows a clean and maintainable layered architecture:



ExpensesTracker

│

├── Controllers        → API Controllers

├── Services           → Business logic layer

├── Repositories       → Data access layer (EF Core)

├── DTOs               → Data Transfer Objects

├── Models             → Domain entities

├── Validators         → FluentValidation rules

├── Middlewares        → Global exception handling

├── Data               → DbContext \& EF Core configuration





This structure ensures:



Separation of concerns



Testability



Maintainability



Scalability



🛠️ Tech Stack



ASP.NET Core 8



Entity Framework Core



SQL Server



ASP.NET Identity



JWT (JSON Web Tokens)



AutoMapper



FluentValidation



Serilog



Swagger / OpenAPI



🧪 Validation \& Error Handling



All incoming requests are validated using FluentValidation



Errors are handled centrally via a global exception middleware



API error responses follow the ProblemDetails standard



All unhandled exceptions are logged using Serilog



🪵 Logging (Serilog)



The application uses Serilog for structured and centralized logging:



Informational logs for business actions



Warning logs for unexpected business cases



Error logs captured globally



Logs written to console and rolling log files



📘 API Documentation (Swagger)



Swagger is enabled for API exploration and testing.



After running the application:



https://localhost:{port}/swagger



▶️ Running the Project Locally

Prerequisites



.NET SDK 8



SQL Server



Visual Studio or VS Code



Steps



Clone the repository



git clone https://github.com/YOUR-USERNAME/ExpensesTracker.git





Configure the database connection

Update appsettings.json



Apply database migrations



dotnet ef database update





Run the application



dotnet run



🎯 Project Purpose



This project was built to:



Practice professional backend development with .NET



Apply enterprise-level architecture patterns



Demonstrate backend skills for a Junior .NET Developer role



👨‍💻 Author



Developed by \[Your Name]

Backend .NET Developer

📍 Spain

GitHub: https://github.com/YOUR-USERNAME



📌 Notes



This project intentionally follows enterprise-grade practices:



Clean architecture



Business logic separation



Secure authentication



Centralized error handling



Structured logging

