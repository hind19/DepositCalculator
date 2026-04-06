using Application.Dtos;
using Application.Interfaces;
using Persistence.Interfaces;
using Shared.Enums;

namespace Application.Services;

public class DataService : IDataService
{
    public IReadOnlyCollection<Currencies> GetCurrencies()
    {
        throw new NotImplementedException();
    }

    public IReadOnlyCollection<DepositPlanDto> GetDepositPlans()
    {
        var repo = DependencyResolver<IDepositPlanRepository>.ResolveDependency();

        return repo.GetRepositoryPlans().Select(d => new DepositPlanDto(d)).ToList();
    }
}