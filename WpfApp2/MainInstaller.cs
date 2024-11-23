using Application.Interfaces;
using Application.Services;
using AutoMapper;
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

            //// Register Automapper
            var profileType = typeof(Profile);

            container.Register(Component.For<IMapper>().UsingFactoryMethod(factory =>
            {
                return new MapperConfiguration(cfg =>
                cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => profileType.IsAssignableFrom(p)))).CreateMapper();
            }));
            container.Register(
                Component.For<IDataService>().ImplementedBy<DataService>());
            container.Register(
                Component.For<IDepositCalculatorService>().ImplementedBy<DepositCalculatorService>());
        }
    }
}