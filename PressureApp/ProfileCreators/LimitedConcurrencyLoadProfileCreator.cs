using PressureApp.Arguments;
using PressureApp.Core.Interfaces;
using PressureApp.Core.LoadProfiles;
using PressureApp.Interfaces;

namespace PressureApp.ProfileCreators;

internal class LimitedConcurrencyLoadProfileCreator : IProfileCreator
{
    public string ProfileTypeName => "limitedConcurrency";
    public IProfile Create(ProfileArguments profile)
    {
        return new LimitedConcurrencyLoadProfile(int.Parse(profile.Arguments["limit"]));
    }
}