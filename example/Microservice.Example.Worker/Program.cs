using Microservice.Example.Infrastructure.Extensions;
using Microservice.Example.UseCases.Extensions;
using Microservice.Example.Worker.Database;
using Microservice.Example.Worker.Queue;

var builder = Host.CreateApplicationBuilder(args);

builder.AddApplicationServices();
builder.AddInfrastructureServices();

builder.Services.AddSingleton<IWorkItem, UsersInitiator>();

builder.Services.AddSingleton<IWorkItem, StoppingItem>();

builder.Services.AddSingleton<MonitorLoop>();
builder.Services.AddHostedService<QueuedHostedService>();
builder.Services.AddSingleton<IBackgroundTaskQueue>(_ => 

{
    if (int.TryParse(builder.Configuration["QueueCapacity"], out var queueCapacity))
    {
        queueCapacity = 30;
    }

    return new DefaultBackgroundTaskQueue(queueCapacity);
});

var host = builder.Build();

var monitorLoop = host.Services.GetRequiredService<MonitorLoop>();
monitorLoop.StartMonitorLoop();

host.Run();