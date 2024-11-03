namespace GeekStore.API.DTOs;

public sealed record AddressDto
{
    public string? Street { get; init; }
    public string? Neighborhood { get; init; }
    public string? City { get; init; }
    public string? State { get; init; }
    public string? Country { get; init; }
    public string? PostalCode { get; init; }
}