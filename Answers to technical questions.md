# Technical questions

## How long did you spend on the coding assignment? What would you add to your solution if you had more time? If you didn't spend much time on the coding assignment then use this as an opportunity to explain what you would add.

* I've spent a few evenings, approximately 8-10 hours. 
* Frontend, containerization, telemetry (metrics and tracing), integration with secrets store, more generic validation (validation behaviour), higher test coverage.

## What was the most useful feature that was added to the latest version of your language of choice? Please include a snippet of code that shows how you've used it.

C# 10 hasn't brought too many new dramatic changes. Although, one of the features that I'm using everywhere is file scoped mamespaces. They make code a bit cleaner and more readable. You can see the use of the feature in every single cs file in the test assessment. For example: 
https://github.com/divanchyshyn/CurrencyConverter/blob/main/src/CurrencyConverter.ExchanGeratesApi.Client/IExchanGeratesApiClient.cs
```
using CurrencyConverter.ExchangeRatesApi.Client.Model;
using Refit;

namespace CurrencyConverter.ExchangeRatesApi.Client;

public interface IExchangeRatesApiApiClient
{
    ...
```

## How would you track down a performance issue in production? Have you ever had to do this?

  It depends on available resources:
  1. check logs
  2. check traces and metrics within existing monitoring solutions (think about adding if not available). Possible options are OpenTelemetry, Prometheus, AWS X-Ray etc.
  3. perform local profiling with visual studio diagnostics tools. Some issues could be detected on local environment and don't require additional investigation on upper environments. 

## What was the latest technical book you have read or tech conference you have been to? What did you learn?

Code That Fits in Your Head: Heuristics for Software Engineering by Mark Seemann (https://www.oreilly.com/library/view/code-that-fits/9780137464302/)

We spend much much more time on code reading than writing. Code complexity and readability are two of the most important metrics in large enterprise projects:

> The more you produce, the more you have to read. As Martin Fowler writes about low code quality: “Even small changes require programmers to understand large areas of code, code that’s difficult to understand.” Code that’s difficult to understand slows you down. On the other hand, every minute you invest in making the code easier to understand pays itself back tenfold.

## What do you think about this technical assessment?

It's a good assignment. It's not too big. It could be implemented in multiple different ways. There is plenty to discuss during the interview. 

## Please, describe yourself using JSON

oh no, there was a connection issue. The details were not loaded properly! You have the chance to learn more during the interview :) 
```
{
	"person": {
		"name": "Dima",
		"details": null,
		"links": {
			"linkedIn": "https://www.linkedin.com/in/dmytro-ivanchyshyn-830444b1/"
		}
	}
}
```