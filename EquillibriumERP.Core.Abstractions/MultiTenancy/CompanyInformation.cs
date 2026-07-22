namespace EquillibriumERP.Core.Abstractions.MultiTenancy;
public sealed record CompanyInformation(
    string CompanyName,
    string TradingName,
    string RegistrationNumber,
    string VatNumber,
    string Industry);