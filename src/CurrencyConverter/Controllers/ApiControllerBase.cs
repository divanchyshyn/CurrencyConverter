using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected ISender Sender { get; }
    protected ApiControllerBase(ISender sender) => Sender = sender;
}