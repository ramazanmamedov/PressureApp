namespace PressureApp.Core.Interfaces;

public interface IProfile
{
    Task<bool> WhenNextCanBeExecutedAsync(CancellationToken cancellationToken);
    Task OnQueryExecutedAsync(CancellationToken cancellationToken);
}