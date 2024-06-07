namespace Microservice.Example.Worker.Queue;

public class StoppingItem : IWorkItem
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;

    public StoppingItem(IHostApplicationLifetime hostApplicationLifetime)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
    }

    public ValueTask Execute(CancellationToken token)
    {
        _hostApplicationLifetime.StopApplication();
        return  ValueTask.CompletedTask;
    }
    
}