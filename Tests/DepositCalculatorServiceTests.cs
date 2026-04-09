using Application.Dtos;
using Application.Services;
using Persistence.Dtos;
using Shared.Enums;
using AppDepositDto = Application.Dtos.DepositDto;

namespace DepositCalculator.Tests;

public class DepositCalculatorServiceTests
{
    private readonly DepositCalculatorService _service = new();

    #region Helpers

    private static DepositPlanDto CreatePlan(
        decimal interestRate = 12m,
        int minSum = 1000, int maxSum = 50000,
        int minTerm = 1,   int maxTerm = 24)
    {
        var domain = new DepositPlanDtoDomain
        {
            Id = Guid.NewGuid(),
            Name = "Test Plan",
            InterestRate = interestRate,
            MinSum = minSum,
            MaxSum = maxSum,
            MinTerm = minTerm,
            MaxTerm = maxTerm,
            AvailableCurrencies = [Currencies.USD]
        };
        return new DepositPlanDto(domain);
    }

    private static AppDepositDto CreateDeposit(
        int sum = 10000,
        int term = 12,
        PaymentMethod method = PaymentMethod.MonthlyPayout,
        DepositPlanDto? plan = null)
    {
        return new AppDepositDto
        {
            DepositPlan = plan ?? CreatePlan(),
            Sum = sum,
            Term = term,
            Currency = Currencies.USD,
            PaymentMethod = method
        };
    }

    #endregion

    #region Validation — throws on invalid input

    [Fact]
    public void CalculateDepositIncome_NullDto_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => _service.CalculateDepositIncome(null!));
    }

    [Fact]
    public void CalculateDepositIncome_NullDepositPlan_ThrowsInvalidOperationException()
    {
        var dto = CreateDeposit();
        dto.DepositPlan = null!;

        Assert.Throws<InvalidOperationException>(() => _service.CalculateDepositIncome(dto));
    }

    [Fact]
    public void CalculateDepositIncome_ZeroSum_ThrowsInvalidOperationException()
    {
        var dto = CreateDeposit(sum: 0);

        Assert.Throws<InvalidOperationException>(() => _service.CalculateDepositIncome(dto));
    }

    [Fact]
    public void CalculateDepositIncome_ZeroTerm_ThrowsInvalidOperationException()
    {
        var dto = CreateDeposit(term: 0);

        Assert.Throws<InvalidOperationException>(() => _service.CalculateDepositIncome(dto));
    }

    #endregion

    #region Monthly payout — simple interest: sum * rate/100 * term * 30 / 365

    [Fact]
    public void CalculateDepositIncome_MonthlyPayout_ReturnsCorrectValue()
    {
        // 10000 * 12/100 * 12 * 30 / 365 = 432000 / 365 = 1183.56 (rounded)
        var dto = CreateDeposit(sum: 10000, term: 12, method: PaymentMethod.MonthlyPayout);

        var result = _service.CalculateDepositIncome(dto);

        Assert.Equal(1183.56m, result);
    }

    [Theory]
    [InlineData(10000, 6,  591.78)]  // 10000 * 12/100 * 6 * 30/365 = 216000/365 = 591.78
    [InlineData(5000,  12, 591.78)]  // 5000  * 12/100 * 12 * 30/365 = 216000/365 = 591.78
    [InlineData(10000, 1,  98.63)]   // 10000 * 12/100 * 1 * 30/365 =  36000/365 = 98.63
    public void CalculateDepositIncome_MonthlyPayout_VariousInputs(
        int sum, int term, decimal expected)
    {
        var dto = CreateDeposit(sum: sum, term: term, method: PaymentMethod.MonthlyPayout);

        var result = _service.CalculateDepositIncome(dto);

        Assert.Equal(expected, result);
    }

    #endregion

    #region Capitalized payout — compound interest iterated monthly

    [Fact]
    public void CalculateDepositIncome_CapitalizedPayout_SingleMonth_EqualsMonthlyPayout()
    {
        // For 1 month, compound == simple (no compounding effect yet)
        var monthly     = _service.CalculateDepositIncome(CreateDeposit(term: 1, method: PaymentMethod.MonthlyPayout));
        var capitalized = _service.CalculateDepositIncome(CreateDeposit(term: 1, method: PaymentMethod.CapitalizedPayout));

        Assert.Equal(monthly, capitalized);
    }

    [Fact]
    public void CalculateDepositIncome_CapitalizedPayout_MultipleMonths_GreaterThanMonthly()
    {
        // Compound interest always exceeds simple interest for term > 1
        var monthly     = _service.CalculateDepositIncome(CreateDeposit(term: 12, method: PaymentMethod.MonthlyPayout));
        var capitalized = _service.CalculateDepositIncome(CreateDeposit(term: 12, method: PaymentMethod.CapitalizedPayout));

        Assert.True(capitalized > monthly);
    }

    [Fact]
    public void CalculateDepositIncome_CapitalizedPayout_IncomeIncreasesWithTerm()
    {
        var income6  = _service.CalculateDepositIncome(CreateDeposit(term: 6,  method: PaymentMethod.CapitalizedPayout));
        var income12 = _service.CalculateDepositIncome(CreateDeposit(term: 12, method: PaymentMethod.CapitalizedPayout));

        Assert.True(income12 > income6);
    }

    #endregion
}
