using Application.Dtos;

namespace Application.Interfaces;

public interface IDepositCalculatorService
{
    double CalculateDepositIncome(DepositDto depositDto);
}