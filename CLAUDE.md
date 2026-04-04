# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run

```bash
# Build the solution
dotnet build DepositCalculator.sln

# Build in release mode
dotnet build DepositCalculator.sln -c Release

# Run the WPF application
dotnet run --project WpfApp2/WPFClient.csproj
```

There are no automated tests in this project.

## Technology Stack

- **UI Framework**: WPF (Windows Presentation Foundation), .NET
- **Pattern**: MVVM
- **IoC Container**: Castle.Windsor
- **Object Mapping**: AutoMapper

## Architecture

This is a WPF desktop application for calculating bank deposit income, structured in four projects:

- **WPFClient** (`WpfApp2/`) — WPF UI layer. Uses MVVM pattern with `MainWindowViewModel`. Castle.Windsor is the IoC container, bootstrapped in `App.xaml.cs` via `MainInstaller.cs`.
- **Application** (`Application/`) — Business logic layer. Contains `DepositCalculatorService` (income calculation) and `DataService` (data retrieval). Uses AutoMapper profiles in `Profiles/`.
- **Persistence** (`Persistence/`) — Data layer. Currently returns hardcoded in-memory data from `DepositPlanRepository`. `CurrencyRepository` is a stub not yet in use.
- **Shared** (`Shared/`) — Enums shared across all layers (`Currencies`, `PaymentMethod`).

### Data Flow

```
WPFClient (Models) → AutoMapper → Application (Dtos) → AutoMapper → Persistence (DtoDomain)
```

There are two separate DTO layers: `Application.Dtos` (used by the application service layer) and `Persistence.Dtos` (used by repositories). AutoMapper profiles handle mapping between them.

### Key Design Notes

- **DI**: Castle.Windsor is used only in `WPFClient`. The `Application` layer uses a custom `DependencyResolver<T>` static class (not Windsor) to resolve `IDepositPlanRepository` — this is intentional per its comment ("Simplified solution instead of DI container").
- **Validation**: Input validation happens in two places: `MainWindowViewModel.ValidateDeposit()` (UI-side, sets error text properties) and `DepositCalculatorService.ValidateInput()` (service-side, throws on invalid input).
- **Calculation**: Two payout methods — `MonthlyPayout` (simple interest: `sum * rate * days / 365`) and `CapitalizedPayout` (compound interest, iterated monthly). Both assume 30 days/month.
- **Currencies displayed per plan**: When the user selects a deposit plan, `SelectedDepositPlan` setter in the ViewModel directly rebuilds the `Currencies` collection from `plan.AvailableCurrencies`.
- **`IDataService.GetCurrencies()`** is not yet implemented (throws `NotImplementedException`).

## Workflow Rules
- NEVER push commits automatically
- When I ask you 'create commit' or 'prepare commit' you should stage elements and add the commit message to the appropriate indow of IDE.
