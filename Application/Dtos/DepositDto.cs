using Shared.Enums;

namespace Application.Dtos;

public class DepositDto
{
    public DepositPlanDto DepositPlan { get; set; }

    public int Sum { get; set; }

    public int Term { get; set; }

    public double Income { get; set; }

    public Currencies Currency { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

}