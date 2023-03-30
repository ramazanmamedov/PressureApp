using PressureApp.Core.LoadProfiles;

namespace PressureApp.Tests;

public class LimitedConcurrencyLoadProfileTest
{
    [Fact]
    public async Task WhenNextCanBeExecutedAsync_ReturnsCompletedTasks_BeforeLimitExceeded()
    {
        var profile = new LimitedConcurrencyLoadProfile(2);
        var task1 = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);
        var task2 = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);
        var task3 = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);
        
        Assert.True(task1.IsCompletedSuccessfully);
        Assert.True(task2.IsCompletedSuccessfully);
        Assert.False(task3.IsCompleted);
    }
    
    [Fact]
    public async Task WhenNextCanBeExecutedAsync_TheNextAfter_LimitExceedTaskCompleted_After_OnQueryExecutedAsync_Called()
    {
        var profile = new LimitedConcurrencyLoadProfile(2);
        var task1 = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);
        var task2 = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);
        var task3 = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);
        
        Assert.True(task1.IsCompletedSuccessfully);
        Assert.True(task2.IsCompletedSuccessfully);
        
        await Task.Delay(10);
        Assert.False(task3.IsCompleted);

        await profile.OnQueryExecutedAsync(CancellationToken.None);
        await Task.Delay(10);
        Assert.True(task3.IsCompletedSuccessfully);
    }
}