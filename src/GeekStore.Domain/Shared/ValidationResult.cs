﻿using GeekStore.Domain.Interfaces;

namespace GeekStore.Domain.Shared;
public class ValidationResult : Result, IValidationResult
{
    private ValidationResult(Error[] errors)
        : base(IValidationResult.ValidationError, errors) { }

    public static ValidationResult WithErrors(Error[] errors) => new(errors);
}
