using GeekStore.API.DTOs;
using GeekStore.Application.Address.Commands;
using GeekStore.Application.Address.Queries;
using GeekStore.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.API.Controllers;
public class AddressController(ISender mediator) : ApiController
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateOrUpdateAddress([FromBody] AddressDto addressDto)
    {
        var user = User;

        if (user is null)
            return NotFound(ResourceErrorMessages.AUTH_USER_NOT_FOUND);

        var request = new CreateOrUpdateAddressCommand
        {
            Number = addressDto.Number,
            Street = addressDto.Street,
            Neighborhood = addressDto.Neighborhood,
            City = addressDto.City,
            State = addressDto.State,
            Country = addressDto.Country,
            PostalCode = addressDto.PostalCode,
            User = user
        };

        var result = await mediator.Send(request);

        return result.Map<IActionResult>(
            onSuccess: address => Ok(address),
            onFailure: error =>
            {
                var errorResponse = new
                {
                    Error = result.Error,
                    ValidationErrors = result.ValidationErrors
                };
                return BadRequest(errorResponse);
            }
        );
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUserAddress()
    {
        var user = User;

        if (user is null)
            return NotFound(ResourceErrorMessages.AUTH_USER_NOT_FOUND);

        var request = new GetAddressQuery { User = user };

        var result = await mediator.Send(request);

        return result.Map<IActionResult>(
            onSuccess: address => Ok(address),
            onFailure: err => NoContent()
        );
    }
}
