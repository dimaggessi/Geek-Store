using GeekStore.Application.Extensions;
using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GeekStore.Application.Address;
public class CreateOrUpdateAddressCommandHandler : IRequestHandler<CreateOrUpdateAddressCommand, 
    Result<Domain.Entities.Address>>
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly UserManager<ApplicationUser> _userManager;

    public CreateOrUpdateAddressCommandHandler(IUnityOfWork unityOfWork, 
        UserManager<ApplicationUser> userManager)
    {
        _unityOfWork = unityOfWork;
        _userManager = userManager;
    }

    public async Task<Result<Domain.Entities.Address>> Handle(CreateOrUpdateAddressCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserWithAddressByEmail(request.User!);

        if (user is null)
            return Result.Failure<Domain.Entities.Address>(new Error(
                ResourceErrorMessages.AUTH_DEFAULT_ERROR, 
                ResourceErrorMessages.AUTH_USER_NOT_FOUND));

        if (user.Address is not null)
        {
            user = user.UpdateUserAddress(request);
            _unityOfWork.GetRepository<Domain.Entities.Address>().Update(user.Address!);
        }
        else
        {
            user = user.CreateUserAddress(request);
            _unityOfWork.GetRepository<Domain.Entities.Address>().Add(user.Address!);
        }
        
        var commited = await _unityOfWork.CommitAsync(cancellationToken);

        var result = await _userManager.UpdateAsync(user);

        return  (commited && result.Succeeded) ? Result.Success(user.Address)! : 
            Result.Failure<Domain.Entities.Address>(new Error(
            ResourceErrorMessages.DEFAULT_ERROR,
            ResourceErrorMessages.ADDRESS_ERROR));
    }
}
