using EquillibriumERP.Core.Abstractions.MultiTenancy;
namespace EquillibriumERP.Core.Onboarding.Contracts;

public sealed record CreateTenantRequest(
    CompanyInformation Company,
    AdministratorInformation Administrator,
    SubscriptionInformation Subscription);



/*public sealed record CreateTenantRequest(
    string CompanyName,
    string AdministratorName,
    string EmailAddress,
    string Password,
    string ConfirmPassword);*/