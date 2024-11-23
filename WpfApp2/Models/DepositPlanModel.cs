using WpfApp2.Helpers;

namespace WpfApp2.Models;

public class DepositPlanModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int MinSum { get; set; }
    
    public int MaxSum { get; set; }

    public int  MinTerm { get; set; }

    public int MaxTerm { get; set; }
    
    public List<NameValuePair<int>> AvailableCurrencies { get; set; } 
    
}