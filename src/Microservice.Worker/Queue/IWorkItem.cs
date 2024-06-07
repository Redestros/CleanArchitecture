namespace Microservice.Worker.Queue;

public interface IWorkItem
{
    ValueTask Execute(CancellationToken token);
}