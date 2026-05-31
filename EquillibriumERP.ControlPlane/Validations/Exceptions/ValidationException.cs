namespace EquillibriumERP.Core.Abstractions.Validations.Exceptions;

public sealed class ValidationException : Exception
{
    public ValidationException(string message)
        : base(message)
    {
    }
}