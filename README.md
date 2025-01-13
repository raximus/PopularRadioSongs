# PopularRadioSongs
Popular radio songs on Polish radio stations. Contains the three largest radio stations in Poland:
- Radio RMF FM
- Radio ZET
- Radio Eska

The application was created as a portfolio of Michał Krysiński, presenting his acquired skills and experience. Focus was more on functionality and code than on graphical appearance.

# Working version
A working version of this project can be found at:
[https://popularradiosongs.azurewebsites.net](https://popularradiosongs.azurewebsites.net)

Its hosted on MS Azure, as App Service. The database contains current data starting from December 31st 2024.

# Technology
Technologies used in this project:
- ASP.NET Core MVC
- ASP.NET Core Minimal API
- Entity Framework Core
- MS SQL Server
- MediatR
- FluentValidation
- AutoMapper
- Serilog
- Hangfire (background tasks)

The approaches used to create code:
- SOLID
- Clean Code
- Clean Architecture
- CQRS
- Result pattern

# Usage
Run project in VS2022. Choose a startup project(Mvc or Api).

Use the migration mechanism to build a database containing a small sample dataset.

In the Development environment, you can use manual data import by selecting the appropriate buttons on the main screen, importing data for the last full hour, or the last 24 hours.

Additionally, once an hour, a background task will automatically import data for the last full hour. By default background task is started only for Mvc, if you want, uncomment app.StartBackgroundTasks() in Program.cs file in Api part of the app.
