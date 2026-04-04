using Application.Dtos;

namespace Application.Interfaces;

public interface IDepositCalculatorService
{
    decimal CalculateDepositIncome(DepositDto depositDto);
}