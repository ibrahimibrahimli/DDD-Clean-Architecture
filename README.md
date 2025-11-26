🏗️  DDD + Clean Architecture + Onion Architecture

⚠️ Layihə hazırda inkişaf mərhələsindədir və tamamlanmayıb

Bu layihə Domain-Driven Design (DDD), Clean Architecture və Onion Architecture prinsiplərinə uyğun olaraq hazırlanmış kiçik bir nümunə proyektdir. Məqsəd bu arxitektura pattern-lərini öyrənmək və praktikada tətbiq etməkdir.

📋 Layihə Haqqında

Bu layihə məhsul kataloqu (Product Catalog) domenində sadə CRUD əməliyyatlarını göstərir. Layihə enterprise-level arxitektura pattern-lərinin necə tətbiq olunacağını nümayiş etdirir.


🎯 Məqsəd

✅ Domain-Driven Design (DDD) prinsiplərini öyrənmək
✅ Clean Architecture pattern-ini tətbiq etmək
✅ Onion Architecture strukturunu başa düşmək
✅ CQRS pattern-i ilə tanış olmaq
✅ Repository və Unit of Work pattern-lərini istifadə etmək
✅ Domain Events konseptini həyata keçirmək



🛠️ Texnologiyalar

Framework: .NET 8.0
Dil: C# 12
ORM: Entity Framework Core 
API: ASP.NET Core Web API 
Database: SQL Server
Testing: xUnit, FluentAssertions


📚 Tətbiq olunan Pattern-lər
Hazırda tətbiq olunanlar (✅):

✅ Domain-Driven Design (DDD)

Rich Domain Model
Value Objects
Domain Events
Entities


✅ Clean Architecture

Dependency Inversion
Separation of Concerns
Independent layers


⏳ CQRS (Command Query Responsibility Segregation)
⏳ Repository Pattern
⏳ Unit of Work Pattern
⏳ MediatR (Mediator pattern)
⏳ Specification Pattern


📦 Funksionallıq

 Product yaratma (Create)
 Product yeniləmə (Update)
 Product silmə (Soft Delete)
 Bütün məhsulları siyahıya alma (List)
 ID-yə görə məhsul əldə etmə (Get by ID)
 Stock management (artırma/azaltma)
 Product aktivləşdirmə/deaktivləşdirmə
 Filtrasiya və axtarış
