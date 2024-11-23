using Application.Dtos;

namespace Application.Interfaces;

public interface IDepositCalculatorService
{
    long CalculateDepositIncome(DepositDto depositDto);
}