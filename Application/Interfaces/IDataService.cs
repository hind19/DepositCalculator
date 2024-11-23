using Application.Dtos;
using Shared.Enums;

namespace Application.Interfaces;

public interface IDataService
{
    IReadOnlyCollection<Currencies> GetCurrencies();
    IReadOnlyCollection<DepositPlanDto> GetDepositPlans();
}

