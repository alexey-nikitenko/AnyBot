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
            services.AddScoped<BotStarter.IManipulator, BotStarter.Manipulator>();
            services.AddScoped<BotStarter.IComPortConnector, BotStarter.ComPortConnector>();
            services.AddScoped<BotStarter.IConfiguration, BotStarter.Configuration>();
            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
