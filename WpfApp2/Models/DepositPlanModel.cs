using Application.Dtos;
using WpfApp2.Helpers;

namespace WpfApp2.Models;

public class DepositPlanModel(DepositPlanDto dto)
{
    public Guid Id { get; set; } = dto.Id;

    public string Name { get; set; } = dto.Name;

    public int MinSum { get; set; } = dto.MinSum;

    public int MaxSum { get; set; } = dto.MaxSum;

    public int MinTerm { get; set; } = dto.MinTerm;

    public int MaxTerm { get; set; } = dto.MaxTerm;

    public decimal InterestRate { get; set; } = dto.InterestRate;

    public List<NameValuePair<int>> AvailableCurrencies { get; set; } = dto.AvailableCurrencies
        .Select(c => new NameValuePair<int>(c.ToString(), (int)c))
        .ToList();
}
