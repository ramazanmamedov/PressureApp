namespace PressureApp.Core.Interfaces;

public interface IExecutable
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}