using System.Net;
using CurrencyConverter.Application.Model;
using CurrencyConverter.Application.Quotes.Queries.GetQuotes;
using CurrencyConverter.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers;

public class ExchangeRatesController : ApiControllerBase
{
    public ExchangeRatesController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicyKeys.CanRead)]
    [ProducesResponseType(typeof(QuotesDto),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Get(string symbol, CancellationToken cancellation)
    {
        var currency = Currency.Create(symbol);
        if (currency.IsFailure)
            return Problem(currency.Error, null, (int)HttpStatusCode.BadRequest);

        var quotes = await Sender.Send(new GetQuotesQuery(currency.Value), cancellation);

        return quotes.IsSuccess 
            ? Ok(quotes.Value)
            : Problem(quotes.Error, null, (int)HttpStatusCode.UnprocessableEntity);
    }
}