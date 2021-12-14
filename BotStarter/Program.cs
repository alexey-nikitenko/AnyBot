using Autofac;
using BotStarter;

var container = ContainerConfig.Configure();

using (var scope = container.BeginLifetimeScope())
{
    var app = scope.Resolve<IRunApp>();
    app.Run();
}