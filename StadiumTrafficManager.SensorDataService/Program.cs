
 using StadiumTrafficManager.SensorDataService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<SensorDataServiceWorker>();
    })
    .Build();

await host.RunAsync();
