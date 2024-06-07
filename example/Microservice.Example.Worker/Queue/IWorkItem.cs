namespace Microservice.Example.Worker.Queue;

public interface IWorkItem
{
    ValueTask Execute(CancellationToken token);
}