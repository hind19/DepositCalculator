using Application.Interfaces;
using Application.Services;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WPFClient.ViewModels;

namespace WpfApp2
{
    internal class MainInstaller : IWindsorInstaller
    {
        public MainInstaller()
        {
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<MainWindowViewModel>());
            container.Register(
                Component.For<IDataService>().ImplementedBy<DataService>());
            container.Register(
                Component.For<IDepositCalculatorService>().ImplementedBy<DepositCalculatorService>());
        }
    }
}