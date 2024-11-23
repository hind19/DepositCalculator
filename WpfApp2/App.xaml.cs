using Castle.Windsor;
using System.Windows;
using WPFClient.ViewModels;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = new WindsorContainer();

            container.Install(new MainInstaller());

            var viewModel = container.Resolve<MainWindowViewModel>();

            var mainWindow = new MainWindow();
            mainWindow.DataContext = viewModel;
            mainWindow.Show();
        }
    }

}
