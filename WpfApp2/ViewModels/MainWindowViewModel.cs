using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfApp2.Helpers;
using WpfApp2.Models;
using WPFClient.Helpers;

namespace WPFClient.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        private readonly IDataService _dataService;
        private readonly IDepositCalculatorService _depositCalculatorService;
        private readonly IMapper _mapper;

        private ObservableCollection<DepositPlanModel> _depositPlans;
        private DepositPlanModel _depositPlan;
        private ObservableCollection<NameValuePair<int>> _currencies;
        private DepositModel _currentDeposit;
        private NameValuePair<int> _selectedCurrency;
        private bool _capitalizedPayoutChecked;
        private bool _monthlyPayoutChecked;
        private string _incomeText;
        #endregion

        #region Constructors

        public MainWindowViewModel(IDataService dataService,
            IDepositCalculatorService depositCalculatorService,
            IMapper mapper)
        {
            LoadedCommand = new RelayCommand(Loaded);
            CalculateIncomeCommand = new RelayCommand(CalculateIncome);
            ResetCommand = new RelayCommand(Reset);
            ExitCommand = new RelayCommand(Exit);
            _dataService = dataService;
            _depositCalculatorService = depositCalculatorService;
            _mapper = mapper;

            CurrentDeposit = new DepositModel();
        }

        #endregion

        #region Properties
        public ObservableCollection<DepositPlanModel> DepositPlans {
            get 
            {
                return _depositPlans;
            }
            set
            {
                _depositPlans = value;
                NotifyPropertyChanged(nameof(DepositPlans));
            }
        }
        public ObservableCollection<NameValuePair<int>> Currencies {
            get 
            {
                return _currencies;
            }
            set 
            {
                _currencies = value;
                NotifyPropertyChanged(nameof(Currencies));
            }
        }
        

        public NameValuePair<int> SelectedCurrency
        {
            get 
            { 
                return _selectedCurrency;
            }
            set 
            { 
                _selectedCurrency = value;
                NotifyPropertyChanged(nameof(SelectedCurrency));
            }
        }

        public DepositPlanModel SelectedDepositPlan
        {
            get
            {
                return _depositPlan;
            }
            set
            {
                _depositPlan = value;
                _currencies = new ObservableCollection<NameValuePair<int>>(value.AvailableCurrencies);
                NotifyPropertyChanged(nameof(SelectedDepositPlan));
                NotifyPropertyChanged(nameof(Currencies));
            }
        }

        public bool MonthlyPayoutChecked
        {
            get 
            {
                return _monthlyPayoutChecked;
            }
            set
            {
                _monthlyPayoutChecked = value;
                NotifyPropertyChanged(nameof(MonthlyPayoutChecked));
            }
        }

        public bool CapitalizedPayoutChecked
        {
            get
            {
                return _capitalizedPayoutChecked;
            }
            set
            {
                _capitalizedPayoutChecked = value;
                NotifyPropertyChanged(nameof(CapitalizedPayoutChecked));
            }
        }

        public DepositModel CurrentDeposit 
        { get
            {
                return _currentDeposit;
            }
            set
            {
                _currentDeposit = value;
                NotifyPropertyChanged(nameof(CurrentDeposit));
            }
        }

        public string IncomeText
        {
            get
            {
                return _incomeText;
            }
            set
            {
                _incomeText = value;
                NotifyPropertyChanged(nameof(IncomeText));
            }
        }

        #endregion

        #region Commands
        public ICommand LoadedCommand { get; set; }
        public ICommand CalculateIncomeCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        #endregion

        #region CommandHandlers
        public void Loaded(object parameter = null)
        {
            DepositPlans = new ObservableCollection<DepositPlanModel>(
                _mapper.Map<IReadOnlyCollection<DepositPlanModel>>(_dataService.GetDepositPlans()));
            SelectedDepositPlan = DepositPlans?.FirstOrDefault();
            if(SelectedDepositPlan is not null)
            {
                Currencies = new ObservableCollection<NameValuePair<int>>(SelectedDepositPlan?.AvailableCurrencies);
                SelectedCurrency = Currencies?.FirstOrDefault();
            }
            MonthlyPayoutChecked = true;
        }

        public void CalculateIncome(object parameter = null)
        {

            CurrentDeposit.DepositPlan = SelectedDepositPlan;
            CurrentDeposit.Currency = (Shared.Enums.Currencies)SelectedCurrency.Value;
            CurrentDeposit.PaymentMethod = MonthlyPayoutChecked
                ? Shared.Enums.PaymentMethod.MonthlyPayout
                : Shared.Enums.PaymentMethod.CapitalizedPayout;

            var income = _depositCalculatorService.CalculateDepositIncome(_mapper.Map<DepositDto>(CurrentDeposit));

            IncomeText = @$"You selected Deposit Plan '{CurrentDeposit.DepositPlan.Name}', Sum {CurrentDeposit.Sum} {CurrentDeposit.Currency} and Term {CurrentDeposit.Term} months. 
Your gross incom will be equal {income} {CurrentDeposit.Currency.ToString()}.
Note:The calculation is approximate and may vary depending on exact date of deposit agreement and actual number of deposit term's days!";
        }

        public void Reset(object parameter = null)
        {
            CurrentDeposit = null;
            IncomeText = string.Empty;
        }
        public void Exit(object parameter = null)
        {
            Environment.Exit(0);
        }
        #endregion
        
        #region Methods

        

        #endregion
    }
}
