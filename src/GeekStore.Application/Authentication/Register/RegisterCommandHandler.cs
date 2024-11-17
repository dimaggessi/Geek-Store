using GeekStore.Domain.Entities;
using GeekStore.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GeekStore.Application.Authentication.Register;
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<ApplicationUser>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<Result<ApplicationUser>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (_userManager.Users.Any(u => u.Email == request.Email))
        {
            return Result.Failure<ApplicationUser>(new Error(
                ResourceErrorMessages.AUTH_DEFAULT_ERROR,
                ResourceErrorMessages.AUTH_EMAIL_ALREADY_EXISTS));
        }

        var user = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password!);

        if (result.Succeeded)
            return Result.Success(user);

        return Result.Failure<ApplicationUser>(new Error(
            ResourceErrorMessages.AUTH_DEFAULT_ERROR,
            result.Errors.ToString() ?? ResourceErrorMessages.AUTH_UNEXPECTED_CREATE_USER_ERROR));
    }
}
