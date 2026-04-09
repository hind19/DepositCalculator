using Application.Dtos;
using Persistence.Dtos;
using Shared.Enums;
using System.ComponentModel;
using WpfApp2.Models;

namespace DepositCalculator.Tests;

public class DepositModelValidationTests
{
    #region Helpers

    private static DepositPlanModel CreatePlanModel(
        int minSum = 1000, int maxSum = 50000,
        int minTerm = 1,   int maxTerm = 24)
    {
        var domain = new DepositPlanDtoDomain
        {
            Id = Guid.NewGuid(),
            Name = "Test Plan",
            InterestRate = 12m,
            MinSum = minSum,
            MaxSum = maxSum,
            MinTerm = minTerm,
            MaxTerm = maxTerm,
            AvailableCurrencies = [Currencies.USD]
        };
        return new DepositPlanModel(new DepositPlanDto(domain));
    }

    // Access explicit interface implementation
    private static string? GetError(DepositModel model, string propertyName)
        => ((IDataErrorInfo)model)[propertyName];

    #endregion

    #region Sum — no error before user interaction

    [Fact]
    public void Sum_NotEdited_ReturnsNoError()
    {
        var model = new DepositModel();

        Assert.Null(GetError(model, nameof(DepositModel.Sum)));
    }

    #endregion

    #region Sum — validation after edit

    [Fact]
    public void Sum_EditedWithZero_ReturnsError()
    {
        var model = new DepositModel { Sum = 0 };
        model.MarkAsEdited();

        Assert.NotNull(GetError(model, nameof(DepositModel.Sum)));
    }

    [Fact]
    public void Sum_EditedBelowMinimum_ReturnsError()
    {
        var model = new DepositModel
        {
            DepositPlan = CreatePlanModel(minSum: 1000),
            Sum = 500
        };

        Assert.NotNull(GetError(model, nameof(DepositModel.Sum)));
    }

    [Fact]
    public void Sum_EditedAboveMaximum_ReturnsError()
    {
        var model = new DepositModel
        {
            DepositPlan = CreatePlanModel(maxSum: 50000),
            Sum = 100000
        };

        Assert.NotNull(GetError(model, nameof(DepositModel.Sum)));
    }

    [Fact]
    public void Sum_EditedWithValidValue_ReturnsNoError()
    {
        var model = new DepositModel
        {
            DepositPlan = CreatePlanModel(minSum: 1000, maxSum: 50000),
            Sum = 10000
        };

        Assert.Null(GetError(model, nameof(DepositModel.Sum)));
    }

    #endregion

    #region Term — no error before user interaction

    [Fact]
    public void Term_NotEdited_ReturnsNoError()
    {
        var model = new DepositModel();

        Assert.Null(GetError(model, nameof(DepositModel.Term)));
    }

    #endregion

    #region Term — validation after edit

    [Fact]
    public void Term_EditedWithZero_ReturnsError()
    {
        var model = new DepositModel { Term = 0 };
        model.MarkAsEdited();

        Assert.NotNull(GetError(model, nameof(DepositModel.Term)));
    }

    [Fact]
    public void Term_EditedBelowMinimum_ReturnsError()
    {
        var model = new DepositModel
        {
            DepositPlan = CreatePlanModel(minTerm: 6),
            Term = 3
        };

        Assert.NotNull(GetError(model, nameof(DepositModel.Term)));
    }

    [Fact]
    public void Term_EditedAboveMaximum_ReturnsError()
    {
        var model = new DepositModel
        {
            DepositPlan = CreatePlanModel(maxTerm: 12),
            Term = 24
        };

        Assert.NotNull(GetError(model, nameof(DepositModel.Term)));
    }

    [Fact]
    public void Term_EditedWithValidValue_ReturnsNoError()
    {
        var model = new DepositModel
        {
            DepositPlan = CreatePlanModel(minTerm: 1, maxTerm: 24),
            Term = 12
        };

        Assert.Null(GetError(model, nameof(DepositModel.Term)));
    }

    #endregion

    #region MarkAsEdited

    [Fact]
    public void MarkAsEdited_WithZeroValues_TriggersBothValidations()
    {
        var model = new DepositModel();
        model.MarkAsEdited();

        Assert.NotNull(GetError(model, nameof(DepositModel.Sum)));
        Assert.NotNull(GetError(model, nameof(DepositModel.Term)));
    }

    #endregion

    #region Error property

    [Fact]
    public void Error_BothInvalid_ReturnsSumErrorFirst()
    {
        var model = new DepositModel();
        model.MarkAsEdited();

        var error = ((IDataErrorInfo)model).Error;

        Assert.NotNull(error);
        Assert.Contains("Sum", error);
    }

    [Fact]
    public void Error_OnlySumValid_ReturnsTermError()
    {
        var model = new DepositModel
        {
            DepositPlan = CreatePlanModel(minSum: 1000, maxSum: 50000, minTerm: 1, maxTerm: 24),
            Sum = 10000
        };
        model.MarkAsEdited();

        var error = ((IDataErrorInfo)model).Error;

        Assert.NotNull(error);
        Assert.Contains("Term", error);
    }

    [Fact]
    public void Error_BothValid_ReturnsNull()
    {
        var model = new DepositModel
        {
            DepositPlan = CreatePlanModel(minSum: 1000, maxSum: 50000, minTerm: 1, maxTerm: 24),
            Sum = 10000,
            Term = 12
        };

        Assert.Null(((IDataErrorInfo)model).Error);
    }

    #endregion

    #region Unknown property

    [Fact]
    public void Indexer_UnknownProperty_ReturnsNull()
    {
        var model = new DepositModel();
        model.MarkAsEdited();

        Assert.Null(GetError(model, "NonExistentProperty"));
    }

    #endregion
}
