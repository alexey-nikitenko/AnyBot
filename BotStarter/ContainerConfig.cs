using Autofac;
using BotStarter.HardwareInteraction;
using ImageRecognition;

namespace BotStarter
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ComPortConnector>().As<IComPortConnector>();
            builder.RegisterType<Manipulator>().As<IManipulator>();
            builder.RegisterType<RunApp>().As<IRunApp>();
            builder.RegisterType<Configuration>().As<IConfiguration>();
            builder.RegisterType<EmguCvProcessor>().As<IEmguCvProcessor>();

            return builder.Build();
        }
    }
}
