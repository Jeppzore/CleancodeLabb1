# ASP.NET Web API – Clean Code & SOLID

This project is a refactored ASP.NET Web API for a simple WebShop application.

The main focus of the refactoring was to apply:

- Clean Code principles
- SOLID principles
- Repository Pattern
- Dependency Injection
- Unit testing with xUnit and Moq

The project was created as part of a school assignment with the goal of
identifying design and architectural issues and resolving them through
refactoring.

---

## Technologies & Tools

- ASP.NET Core Web API
- C#
- xUnit
- Moq
- Dependency Injection

---

## Testing

Business logic is primarily tested at the **service layer**, where most of the
application rules reside.

Mocks, stubs, and fakes are used to isolate dependencies and verify behavior
without relying on external infrastructure.

---

## Documentation

A more detailed explanation of:

- identified design and architectural problems
- SOLID violations
- refactoring decisions
- test strategy

can be found in [`DOCUMENTATION.md`](DOCUMENTATION.md).
DOCUMENTATION.md is written in Swedish and is mainly intended for 
the school project and personal development.
