# Bank Project

Our bank-solution has 3 Projects:
- RabbitEyeBank is a library with all business logic.
- LoginDemo (rename asap) has the UI built in Spectre.Console.
- RabbitEyeTests contains the unit tests covering the business logic.

## RabbitEyeBank-Project

The library is built around a much simplified adaptation of the _Controller-Service-Repository_ pattern. Following this pattern means you have _Repository_ classes that stores and fetches one kind of entity. A UserRepository would handle User classes and only that. A _Service_ class manages the business logic, that is the creation and manipulation of the data classes and communicates with the outside world via controller classes. A service class might be wired into one or more repository classes depending on purpose. An EmailService could use a UserRepository and a AddressRepository for example.
The service classes acts as both Service, repository and controller squashed into one. The idea is that these
