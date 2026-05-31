using EquillibriumERP.Core.Abstractions.Validations.Exceptions;
namespace EquillibriumERP.Core.Abstractions.Validations;
public static class ValidationHelper
{
    public static void EnsureNoDuplicatesOrThrow<T>(
        IEnumerable<T> items,
        string message)
    {
        if (items is null)
            return;

        if (items.GroupBy(x => x).Any(g => g.Count() > 1))
            throw new ValidationException(message);
    }
}