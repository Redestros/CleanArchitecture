using Microservice.Worker.Queue;

var builder = Host.CreateApplicationBuilder(args);

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