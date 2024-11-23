using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Persistence.Interfaces;
using Shared.Enums;

namespace Application.Services;

public class DataService : IDataService
{
    private readonly IMapper _mapper;

    public DataService(IMapper mapper)
    {
        _mapper = mapper;

    }

    public IReadOnlyCollection<Currencies> GetCurrencies()
    {
        throw new NotImplementedException();
    }

    public IReadOnlyCollection<DepositPlanDto> GetDepositPlans()
    {
        var repo = DependencyResolver<IDepositPlanRepository>.ResolveDependency();

        return _mapper.Map <IReadOnlyCollection<DepositPlanDto>>(repo.GetRepositoryPlans());
    }
}