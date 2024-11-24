using Application.Dtos;
using Application.Interfaces;

namespace Application.Services;

public class DepositCalculatorService : IDepositCalculatorService
{
    public double CalculateDepositIncome(DepositDto depositDto)
    {
        if (depositDto is null || !ValidateInput(depositDto))
        {
            throw new InvalidOperationException("Incorrect Data");
        }

        return depositDto.PaymentMethod == Shared.Enums.PaymentMethod.MonthlyPayout
            ? CalculateMonthlyPayout(depositDto)
            : CalculateCapitalizedPayout(depositDto);
    }

    private bool ValidateInput(DepositDto depositDto)
    {
        return depositDto.DepositPlan is not null
            && depositDto.Sum != 0
            && depositDto.Term != 0
            && (depositDto.PaymentMethod == Shared.Enums.PaymentMethod.CapitalizedPayout || depositDto.PaymentMethod == Shared.Enums.PaymentMethod.MonthlyPayout);
    }

    private double CalculateMonthlyPayout(DepositDto depositDto)
    {
        var income = depositDto.Sum * depositDto.DepositPlan.InterestRate / 100 * depositDto.Term * 30 / 365; 
        return Math.Round(income, 2);
    }
    
    private double CalculateCapitalizedPayout(DepositDto depositDto)
    {
        double income = 0.0;
        double currentSum = depositDto.Sum;
        double monthIncome = 0.0;
        for (var i = 1; i <= depositDto.Term; i++) 
        {
            monthIncome = currentSum * depositDto.DepositPlan.InterestRate / 100 * 30 /365;
            income += monthIncome;
            currentSum += monthIncome;
        }

        return Math.Round(income, 2);

        // test changes

    }
}