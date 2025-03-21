﻿using GeekStore.Domain.Entities;
using GeekStore.Domain.Shared;
using MediatR;

namespace GeekStore.Application.Authentication.Register;
public sealed record RegisterCommand : IRequest<Result<ApplicationUser>>
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public string? Password { get; init; }
}
