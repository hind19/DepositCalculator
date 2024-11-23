using Application.Dtos;
using Application.Interfaces;

namespace Application.Services;

public class DepositCalculatorService : IDepositCalculatorService
{
    public long CalculateDepositIncome(DepositDto depositDto)
    {
        throw new NotImplementedException();
    }

    private long CalculateMonthlyPayout(DepositDto depositDto)
    {
        throw new NotImplementedException();
    }
    
    private long CalculateCapitalizedPayout(DepositDto depositDto)
    {
        throw new NotImplementedException();
    }
}