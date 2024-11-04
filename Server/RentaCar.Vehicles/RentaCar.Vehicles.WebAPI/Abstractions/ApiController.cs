using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RentaCar.Vehicles.WebAPI.Abstractions;
[Route("api/[controller]")]
[ApiController]
public abstract class ApiController : ControllerBase
{
    public readonly IMediator mediator;
    protected ApiController(IMediator mediator)
    {
        this.mediator = mediator;
    }
}
