using Application.Services;

namespace DepositCalculator.Tests;

public class DataServiceTests
{
    private readonly DataService _service = new();

    [Fact]
    public void GetDepositPlans_ReturnsNonEmptyCollection()
    {
        var plans = _service.GetDepositPlans();

        Assert.NotEmpty(plans);
    }

    [Fact]
    public void GetDepositPlans_AllPlansHaveRequiredFields()
    {
        var plans = _service.GetDepositPlans();

        foreach (var plan in plans)
        {
            Assert.NotEqual(Guid.Empty, plan.Id);
            Assert.False(string.IsNullOrWhiteSpace(plan.Name));
            Assert.True(plan.InterestRate > 0);
            Assert.True(plan.MinSum > 0);
            Assert.True(plan.MaxSum > plan.MinSum);
            Assert.True(plan.MinTerm > 0);
            Assert.True(plan.MaxTerm >= plan.MinTerm);
            Assert.NotEmpty(plan.AvailableCurrencies);
        }
    }

    [Fact]
    public void GetCurrencies_ThrowsNotImplementedException()
    {
        Assert.Throws<NotImplementedException>(() => _service.GetCurrencies());
    }
}
