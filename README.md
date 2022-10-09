# Currency Converter
[![.NET](https://github.com/divanchyshyn/CurrencyConverter/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/divanchyshyn/CurrencyConverter/actions/workflows/dotnet.yml)
[![CodeQL](https://github.com/divanchyshyn/CurrencyConverter/actions/workflows/codeql-analysis.yml/badge.svg?branch=main)](https://github.com/divanchyshyn/CurrencyConverter/actions/workflows/codeql-analysis.yml)

.NET 6 based service that provides that accepts a cryptocurrency code as input. The application displaies its
current quote in the following currencies:
* USD
* EUR
* BRL
* GBP
* AUD

## Technologies

* [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [CSharpFunctionalExtensions](https://github.com/vkhorikov/CSharpFunctionalExtensions) 
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [xUnit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq)

## Getting Started

1. Install the latest [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
2. Navigate to `src/CurrencyConverter` and run `dotnet run` to launch the API
3. Check `https://localhost:7274/swagger/index.html` for API documentation
4. For authorization use locally generated JWT token from the Swagger authorization dialog
5.  <details><summary>BTC example</summary><img src="https://user-images.githubusercontent.com/9357531/194728216-2707516b-e305-41f5-ad58-29550f245ede.png"></details>
6. [Answers to the tech questions](https://github.com/divanchyshyn/CurrencyConverter/blob/main/Answers%20to%20technical%20questions.md)