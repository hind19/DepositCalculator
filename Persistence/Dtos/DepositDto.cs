using Shared.Enums;

namespace Persistence.Dtos;

public class DepositDto
{
    public DepositPlanDtoDomain DepositPlan { get; set; }

    public int Sum { get; set; }

    public int Term { get; set; }

    public double Income { get; set; }

    public Currencies Currency { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

}