﻿using Shared.Enums;

namespace Application.Dtos;

public class DepositPlanDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public double InterestRate { get; set; }

    public int MinSum { get; set; }

    public int MaxSum { get; set; }

    public int MinTerm { get; set; }

    public int MaxTerm { get; set; }

    public List<Currencies> AvailableCurrencies { get; set; }
}