using Persistence.Dtos;
using Persistence.Interfaces;

namespace Persistence.Repositories
{
    // For simplicity  It will return DTO's instead of Domain Models
    public class DepositPlanRepository : IDepositPlanRepository
    {
        public IReadOnlyCollection<DepositPlanDtoDomain> GetRepositoryPlans()
        {
            return new List<DepositPlanDtoDomain>
            {
                new DepositPlanDtoDomain()
                {
                     Id = Guid.NewGuid(),
                     MaxSum = 50000,
                     MaxTerm = 12,
                     MinSum = 1000,
                     MinTerm = 6,
                     InterestRate = 12,
                     Name = "Basic",
                     AvailableCurrencies = new List<Shared.Enums.Currencies>
                     {
                         Shared.Enums.Currencies.USD,
                         Shared.Enums.Currencies.UAH
                     }
                },
                 new DepositPlanDtoDomain()
                {
                     Id = Guid.NewGuid(),
                     MaxSum = 500000,
                     MaxTerm = 24,
                     MinSum = 10000,
                     MinTerm = 3,
                     InterestRate = 18,
                     Name = "Premium",
                     AvailableCurrencies = new List<Shared.Enums.Currencies>
                     {
                         Shared.Enums.Currencies.USD,
                         Shared.Enums.Currencies.EUR,
                         Shared.Enums.Currencies.UAH,
                         Shared.Enums.Currencies.CHF
                     }
                },
            }.AsReadOnly();
        }
    }
}
