using GeekStore.Application.Authentication.Register;
using GeekStore.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.API.Controllers;
public class AuthController(ISender _mediator) : ApiController
{
    [HttpGet]
    public ActionResult GetAuthState()
    {
        return Ok(new { IsAuthenticated = User.Identity?.IsAuthenticated ?? false });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand request)
    {
        var result = await _mediator.Send(request);

        if (result.IsSuccess)
        {
            return Ok();
        }

        var failureResponse = new
        {
            Error = result.Error,
            ValidationErrors = result.ValidationErrors
        };

        return BadRequest(failureResponse);
    }
}
