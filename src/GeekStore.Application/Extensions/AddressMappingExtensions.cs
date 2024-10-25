using GeekStore.Application.Address;
using GeekStore.Domain.Entities;

namespace GeekStore.Application.Extensions;
public static class AddressMappingExtensions
{
    public static ApplicationUser UpdateUserAddress(this ApplicationUser user, CreateOrUpdateAddressCommand request)
    {
        user.Address!.Street = request.Street!;
        user.Address.Neighborhood = request.Neighborhood;
        user.Address.City = request.City!;
        user.Address.State = request.State!;
        user.Address.Country = request.Country!;
        user.Address.PostalCode = request.PostalCode!;

        return user;
    }

    public static ApplicationUser CreateUserAddress(this ApplicationUser user, CreateOrUpdateAddressCommand request)
    {
        user.Address = new Domain.Entities.Address
        {
            Street = request.Street!,
            Neighborhood = request.Neighborhood,
            City = request.City!,
            State = request.State!,
            Country = request.Country!,
            PostalCode = request.PostalCode!
        };

        return user;
    }
}
