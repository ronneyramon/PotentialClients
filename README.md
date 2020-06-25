# PotentialClients

A Simple API that allows:

- Inserting a person's LinkedIn public profile info
- Find the top N people with the highest chance of becoming clients
- Find, for a given personId, the position on the priority potential clients
list

## Installation

- Clone this repository
- Open the solution inside "PotentialClients" folder with Visual Studio and run the project PotentialClients.API. This will create and seed the database automacally, and open a browser with the swagger UI for the API

## Concepts and Technologies used

- ASP.NET Core
- Filters for global exception handling
- EF Core with SQL Server
- Dependency Injection
- Repository Pattern
- Services
- Swagger
- DDD
- TDD

## Future Improvements

[ ] The score system implemented is a very simple one, based only on the properties NumberOfRecommendations and NumberOfConnections. The architecture allows the replacement of the IScoreCalculator implementation to improve the score system. One good solution would be implementing the score based on scores for the other properties (like CurrentRole, Country and Industry). An even better implementation would use concepts of Machine Learning.
[ ] Improve the DI implementation using a more consolidated IoC container (like Autofac) that allows registration by convention, modularization and assembly scanning.
[ ] Improve parallelism of the potential clients import process
[ ] Enable EF Migrations and remove the startup database initialization (for demo porpouse only)
[ ] More Unit Tests and implement integration and end-to-end tests
