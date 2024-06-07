namespace Microservice.Example.Worker.Queue;

public class MonitorLoop
{
    private readonly IBackgroundTaskQueue _taskQueue;
    private readonly ILogger<MonitorLoop> _logger;
    private readonly CancellationToken _cancellationToken;
    private readonly IEnumerable<IWorkItem> _workItems;

    public MonitorLoop(IBackgroundTaskQueue taskQueue, ILogger<MonitorLoop> logger,
        IHostApplicationLifetime applicationLifetime, IEnumerable<IWorkItem> workItems)
    {
        _taskQueue = taskQueue;
        _logger = logger;
        _cancellationToken = applicationLifetime.ApplicationStopping;
        _workItems = workItems;
    }

    public void StartMonitorLoop()
    {
        _logger.LogInformation("MonitorAsync loop is starting.");

        Task.Run(async () => await MonitorAsync(), _cancellationToken);
    }

    private async ValueTask MonitorAsync()
    {
        _logger.LogInformation("Processing {workItemsCount} items", _workItems.Count());

        foreach (var workItem in _workItems)
        {
            await _taskQueue.QueueBackgroundWorkItemAsync(token => workItem.Execute(token));
        }

        _logger.LogInformation("Done processing");
    }
}