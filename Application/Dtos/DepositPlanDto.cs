using Persistence.Dtos;
using Shared.Enums;

namespace Application.Dtos;

public class DepositPlanDto(DepositPlanDtoDomain domain)
{
    public Guid Id { get; } = domain.Id;
    public string Name { get; } = domain.Name;
    public decimal InterestRate { get; } = domain.InterestRate;
    public int MinSum { get; } = domain.MinSum;
    public int MaxSum { get; } = domain.MaxSum;
    public int MinTerm { get; } = domain.MinTerm;
    public int MaxTerm { get; } = domain.MaxTerm;
    public List<Currencies> AvailableCurrencies { get; } = domain.AvailableCurrencies;
}