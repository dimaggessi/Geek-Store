using GeekStore.Application.Extensions;
using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekStore.Application.Address.Queries;

public sealed record GetAddressQuery: IRequest<Result<Domain.Entities.Address>>
{
    public ClaimsPrincipal? User { get; set; }
}
public class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, Result<Domain.Entities.Address>>
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    public GetAddressQueryHandler(IUnityOfWork unityOfWork, UserManager<ApplicationUser> userManager)
    {
        _unityOfWork = unityOfWork;
        _userManager = userManager;
    }
    public async Task<Result<Domain.Entities.Address>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserWithAddressByEmail(request.User!);

        if (user is null)
            return Result.Failure<Domain.Entities.Address>(new Error(
                ResourceErrorMessages.AUTH_DEFAULT_ERROR,
                ResourceErrorMessages.AUTH_USER_NOT_FOUND));

        if (user.Address is not null)
        {
            return Result.Success(user.Address);
        }

        return Result.Failure<Domain.Entities.Address>(new Error(
            ResourceErrorMessages.DEFAULT_ERROR,
            ResourceErrorMessages.ERROR_ADDRESS_NULL));
    }
}
