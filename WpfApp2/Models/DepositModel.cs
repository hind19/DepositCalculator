using Shared.Enums;

namespace WpfApp2.Models;

public class DepositModel
{
    public DepositPlanModel DepositPlan { get; set; }
    
    public int Sum { get; set; }
    
    public int Term { get; set; }
    
    public double Income { get; set; }
    
    public Currencies Currency { get; set; }
    
    public PaymentMethod PaymentMethod { get; set; }
}