using Shared.Enums;

namespace Persistence.Dtos;

public class DepositPlanDtoDomain
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public decimal InterestRate { get; init; }

    public int MinSum { get; init; }

    public int MaxSum { get; init; }

    public int MinTerm { get; init; }

    public int MaxTerm { get; init; }

    public List<Currencies> AvailableCurrencies { get; init; }
}