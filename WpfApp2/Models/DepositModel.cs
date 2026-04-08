using System.ComponentModel;
using Shared.Enums;

namespace WpfApp2.Models;

public class DepositModel : IDataErrorInfo
{
    public DepositPlanModel? DepositPlan { get; set; }

    private int _sum;
    private bool _sumEdited;
    public int Sum
    {
        get => _sum;
        set { _sum = value; _sumEdited = true; }
    }

    private int _term;
    private bool _termEdited;
    public int Term
    {
        get => _term;
        set { _term = value; _termEdited = true; }
    }

    public Currencies Currency { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public void MarkAsEdited()
    {
        _sumEdited = true;
        _termEdited = true;
    }

    string IDataErrorInfo.Error =>
        ((IDataErrorInfo)this)[nameof(Sum)] ?? ((IDataErrorInfo)this)[nameof(Term)];

    string IDataErrorInfo.this[string columnName] => columnName switch
    {
        nameof(Sum)  => _sumEdited ? ValidateSum() : null,
        nameof(Term) => _termEdited ? ValidateTerm() : null,
        _            => null
    };

    private string ValidateSum()
    {
        if (Sum == 0)
            return "Sum is Required or entered incorrectly";
        if (DepositPlan is not null && (Sum < DepositPlan.MinSum || Sum > DepositPlan.MaxSum))
            return "Entered Sum is not in the range allowed for this deposit plan";
        return null;
    }

    private string ValidateTerm()
    {
        if (Term == 0)
            return "Term is Required or entered incorrectly";
        if (DepositPlan is not null && (Term < DepositPlan.MinTerm || Term > DepositPlan.MaxTerm))
            return "Entered Term is not in the range allowed for this deposit plan";
        return null;
    }
}
