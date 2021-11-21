# BikeShop
Оnline store
A small website for an online store with goods for bicycles and accessories.

Technologies: C#, ASP.Net Core MVC, EF Core (MS SQL Server), Bootstrap 5.

---

Main features:
- filtering and pagination goods
- add goods and manage cart (work with sessions)
- register and log into account
- authentication, authorization (work with cookies)
- different access to the resource depending on user roles
- using admin panel for CRUD-operations in db 

## Beginning of work
1. Change connection string to db in [appsettings.json](/BikeShop.WebUI/appsettings.json)
2. Change administrator user details in [InitializerDb.cs](/BikeShop.WebUI/Infrastructure/InitializerDb.cs)
3. Run application

---

## Application example

![Watch the video](/doc/exampleApp.gif)