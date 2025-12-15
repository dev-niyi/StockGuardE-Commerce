# StockGuardE-Commerce
StockGuard API
A production-grade e-commerce Web API built with .NET 9 that supports product catalog management and order processing with stock control and concurrency protection.

Features

Product Management: Full CRUD operations for products
Order Processing: Place orders with multiple products
Stock Management: Automatic stock deduction with overselling prevention
Concurrency Control: Transaction-based locking prevents race conditions
Clean Architecture: Separation of concerns with distinct layers
Input Validation: FluentValidation for request validation
Error Handling: Graceful error responses with detailed messages

Tech Stack

.NET 9 - Latest LTS framework
ASP.NET Core Web API - RESTful API framework
Entity Framework Core 9.0.9 - ORM for data access
SQL Server - Relational database
MediatR 14.0 - CQRS implementation
FluentValidation - Input validation
Serilog - Structured logging
Swashbuckle - API documentation (Swagger)
