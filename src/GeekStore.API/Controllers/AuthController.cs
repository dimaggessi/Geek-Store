using GeekStore.Application.Authentication.Register;
using GeekStore.Domain.Entities;
using GeekStore.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.API.Controllers;
public class AuthController(ISender _mediator, 
    SignInManager<ApplicationUser> signInManager) : ApiController
{
    [HttpGet("is-authenticated")]
    public IActionResult IsAuthenticated()
    {
        return Ok(User.Identity?.IsAuthenticated ?? false);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromQuery] bool? useCookies, 
        [FromQuery] bool? useSessionCookies, 
        [FromBody] RegisterCommand request)
    {
        var result = await _mediator.Send(request);

        if (result.IsFailure)
        {
            var failureResponse = new
            {
                Error = result.Error,
                ValidationErrors = result.ValidationErrors
            };

            return BadRequest(failureResponse);
        }

        var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
        var isPersistent = (useCookies == true) && (useSessionCookies != true);

        signInManager.AuthenticationScheme = useCookieScheme ? 
            IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

        // User login without password verification
        await signInManager.SignInAsync(result.Value!, isPersistent);

        return result.Map<IActionResult>(
            onSuccess: user =>
            {
                var successResponse = new
                {
                    User = new
                    {
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        IsEmailConfirmed = user.EmailConfirmed

                    }
                };
                return Ok(successResponse);
            },
            onFailure: (error) => BadRequest(error));
    }
}
