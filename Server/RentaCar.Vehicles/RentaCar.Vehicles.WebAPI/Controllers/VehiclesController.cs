using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentaCar.Vehicles.Application.Features.Vehicles;
using RentaCar.Vehicles.WebAPI.Abstractions;
using RentaCar.Vehicles.WebAPI.AOP;

namespace RentaCar.Vehicles.WebAPI.Controllers;
public sealed class VehiclesController : ApiController
{
    public VehiclesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [EnableQueryWithMetadata]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAllVehicleQuery(), cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        DeleteVehicleByIdCommand request = new(id);
        var response = await mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
