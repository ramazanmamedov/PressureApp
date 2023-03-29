using PressureApp.Interfaces;

namespace PressureApp.LoadProfiles;

public class LimitedConcurrencyLoadProfile : IProfile
{
    private readonly SemaphoreSlim _semaphore;

    public LimitedConcurrencyLoadProfile(int limit)
    {
        _semaphore = new SemaphoreSlim(limit);
    }
    
    public async Task<bool> WhenNextCanBeExecutedAsync(CancellationToken cancellationToken)
    {
       await _semaphore.WaitAsync(cancellationToken);
       return cancellationToken.IsCancellationRequested;
    }

    public Task OnQueryExecutedAsync(CancellationToken cancellationToken)
    {
        _semaphore.Release();
        return Task.CompletedTask;
    }
}