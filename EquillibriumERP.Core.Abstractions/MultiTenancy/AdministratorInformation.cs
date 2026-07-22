namespace EquillibriumERP.Core.Abstractions.MultiTenancy;
public sealed record AdministratorInformation(
    string FirstName,
    string LastName,
    string EmailAddress,
    string Password,
    string ConfirmPassword);