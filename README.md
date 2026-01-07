## 💰 ExpensesTracker – ASP.NET Core Backend Project

## 📌 Overview

ExpensesTracker is a backend application built with ASP.NET Core (.NET 8) designed to help users track, categorize, and analyze their personal expenses.

The project focuses on real-world backend development practices, including clean architecture, authentication, validation, logging, and unit testing.

This application was developed as a portfolio and learning project to demonstrate readiness for a Junior .NET Backend Developer position.

## 🛠️ Tech Stack

ASP.NET Core (.NET 8)

Entity Framework Core

ASP.NET Identity

JWT Authentication

SQL Server

AutoMapper

FluentValidation

Serilog

Swagger (OpenAPI)

xUnit (Unit Testing)

## 🧱 Architecture & Design

The application follows a **layered architecture** commonly used in enterprise environments:
...
Controllers (API)
Services (Business Logic)
Repositories (Data Access)
DTOs
Validators
Middlewares
...
## Key Principles:

Separation of Concerns

Dependency Injection

SOLID principles

Clean and testable code

## 🔐 Authentication & Authorization

Authentication implemented using ASP.NET Identity + JWT

Secure login and registration endpoints

Each resource is linked to the authenticated user

API endpoints protected using [Authorize]

## 💳 Core Features
**Expenses**

Create, update, delete expenses

Each expense is linked to a category

Expenses belong to a specific user

Expense statistics by date and category

**Categories**

User-defined expense categories

Each category belongs to a single user

Categories used to organize expenses

**Budgets**

Budget management per category

Track spending against defined budgets

Designed for future monthly statistics and reporting

## 🔄 API Design

RESTful API design

Clear resource-based endpoints

Proper HTTP status codes (200, 201, 400, 401, 404, 500)

Fully documented with Swagger

Example endpoints:

POST   /api/expenses
GET    /api/expenses
GET    /api/expenses/{id}
PUT    /api/expenses/{id}
DELETE /api/expenses/{id}

## ✅ Validation

Validation implemented using FluentValidation

DTO-based validation

Automatic validation for API requests

Centralized error responses via global exception middleware

Examples:

Amount must be greater than zero

Required fields validation

Date validation

## 🧠 Business Logic (Services)

Business logic isolated in the Service layer

Controllers remain thin

Logging added at critical business steps

Domain rules enforced in services

## 🧾 Logging & Error Handling

Centralized Global Exception Middleware

Logging implemented with Serilog

Logs include:

Business operations

Warnings

Errors

Designed to work in both development and production environments

## 🧪 Unit Testing

Unit tests implemented using xUnit

Service layer tested independently

Repository dependencies mocked

Tests executed using:

dotnet test

## 🚀 How to Run the Project

Clone the repository

Configure the database connection string

Apply migrations:

dotnet ef database update


Run the application:

dotnet run


Open Swagger UI:

https://localhost:{port}/swagger

## 🎯 Why This Project?

This project was built to:

Practice professional backend development with .NET

Apply enterprise-level architectural patterns

Demonstrate secure API development

Strengthen backend-focused portfolio

👤 Author

**Redouan Aachboun**
	
Junior .NET Backend Developer
📍 Spain

🔗 GitHub: https://github.com/aachboun

🔗 LinkedIn: https://www.linkedin.com/in/redouan-aachboun-a798a32ab/

⭐ Feedback and suggestions are welcome!