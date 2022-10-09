# Technical questions

## How long did you spend on the coding assignment? What would you add to your solution if you had more time? If you didn't spend much time on the coding assignment then use this as an opportunity to explain what you would add.

* I've spent a few evenings, approximately 8-10 hours. 
* Frontend, containerization, telemetry (metrics and tracing), integration with secrets store, more generic validation (validation behaviour), higher test coverage.

## What was the most useful feature that was added to the latest version of your language of choice? Please include a snippet of code that shows how you've used it.

C# 10 hasn't brought too many new dramatic changes. Although, one of the features that I started to use everywhere is file scoped mamespaces. They make code a bit cleaner and more readable. You can see the use of the feature in every single cs file in the test assessment. For example: 
https://github.com/divanchyshyn/CurrencyConverter/blob/main/src/CurrencyConverter.ExchanGeratesApi.Client/IExchanGeratesApiClient.cs
```
using CurrencyConverter.ExchangeRatesApi.Client.Model;
using Refit;

namespace CurrencyConverter.ExchangeRatesApi.Client;

public interface IExchangeRatesApiApiClient
{
    ...
```