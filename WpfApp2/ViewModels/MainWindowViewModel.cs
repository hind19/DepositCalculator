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
            }
        }

        public void CalculateIncome(object parameter = null)
        {
            
        }

        public void Reset(object parameter = null)
        {
            
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
