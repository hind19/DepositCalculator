using Shared.Enums;

namespace Persistence.Dtos;

public class DepositPlanDtoDomain
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int MinSum { get; set; }

    public int MaxSum { get; set; }

    public int MinTerm { get; set; }

    public int MaxTerm { get; set; }

    public List<Currencies> AvailableCurrencies { get; set; }
}