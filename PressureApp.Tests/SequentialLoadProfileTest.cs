using PressureApp.LoadProfiles;

namespace PressureApp.Tests;

public class SequentialLoadProfileTest
{
    [Fact]
    public void WhenNextCanBeExecutedAsync_FirstCall_ReturnsCompletedTask()
    {
        var profile = new SequentialLoadProfile();
        var task = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);
        Assert.True(task.IsCompletedSuccessfully);
    }
    
    [Fact]
    public async Task WhenNextCanBeExecutedAsync_SecondCall_CompleteOnly_AfterWhen_QueryExecutedCalled()
    {
        var profile = new SequentialLoadProfile();
        var _ = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);
        var task = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);

        await Task.Delay(10);
        Assert.False(task.IsCompleted);
        
        await profile.OnQueryExecutedAsync(CancellationToken.None);
        
        await Task.Delay(10);
        Assert.True(task.IsCompletedSuccessfully);
    }
}