namespace PressureApp.Interfaces;

public interface IExecutable
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}