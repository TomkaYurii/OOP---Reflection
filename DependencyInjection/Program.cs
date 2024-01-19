using DependencyInjection;

var container = new Container();
container.Register<IService, Service>();

var client = container.GetInstance<Client>();
client.RunProcess();