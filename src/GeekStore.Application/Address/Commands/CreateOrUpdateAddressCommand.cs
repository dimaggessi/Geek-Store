using GeekStore.Domain.Entities;
using GeekStore.Domain.Shared;
using MediatR;
using System.Security.Claims;

namespace GeekStore.Application.Address.Commands;
public sealed record CreateOrUpdateAddressCommand : IRequest<Result<Domain.Entities.Address>>
{
    public string? Number { get; set; }
    public string? Street { get; init; }
    public string? Neighborhood { get; init; }
    public string? City { get; init; }
    public string? State { get; init; }
    public string? Country { get; init; }
    public string? PostalCode { get; init; }
    public ClaimsPrincipal? User { get; set; }
}
