# HR Platform Project

Technologies Used:

Backend (.NET 8):

Framework: ASP .NET Core Web API

ORM: Entity Framework Core

Database: MySQL

Testing: NUnit & Moq

Frontend:

Library: React

Language: JavaScript

Environment: Vite

The project follows Clean Architecture and SOLID principles.


The most challenging part was to implement search candidate by name and skill(s) because of many-to-many database relationship. I solved this by building a dynamic query (using IQueryable) step-by-step. Writing Unit test was also challenging because they had to cover many cases.
