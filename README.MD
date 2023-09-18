## STFU - Short This Freaking URL

Micro-Service Web API designed to generate short redirection URL from the provided long one.

Project can be extended in the future for enterprise or commercial use.

This project was created to fulfil my personal need in this kind of service and 
to show-case my skills and knowledge.  
I've kept in mind performance, so it should be pretty fast.

#### Planned features:
- Health checks.
- Rule expiration by max usages or passed time.
- Generation rate limits.

#### Tech stack
- .NET 8.0
- Logs
  - Serilog
  - Sentry
- Data Base
  - EntityFramework
  - PostgreSQL
- Performance Tests - NBomber
- Integration Tests - NUnit
- OpenAPI - Swagger