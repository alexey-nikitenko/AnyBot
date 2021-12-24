using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace WpfUi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddScoped<BotStarter.IRunApp, BotStarter.RunApp>();
            services.AddScoped<BotStarter.HardwareInteraction.IManipulator, BotStarter.HardwareInteraction.Manipulator>();
            services.AddScoped<BotStarter.HardwareInteraction.IComPortConnector, BotStarter.HardwareInteraction.ComPortConnector>();
            services.AddScoped<BotStarter.IConfiguration, BotStarter.Configuration>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<BotStarter.Models.CoordinatesModel>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
