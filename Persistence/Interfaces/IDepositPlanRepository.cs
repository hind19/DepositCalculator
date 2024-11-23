using Persistence.Dtos;

namespace Persistence.Interfaces
{
    public interface IDepositPlanRepository :IRepository
    {
        IReadOnlyCollection<DepositPlanDtoDomain> GetRepositoryPlans();
    }
}
