using PressureApp.Core.Interfaces;
using PressureApp.Interfaces;

namespace PressureApp.Factories;

internal class LoadProfilesFactory
{
    private readonly IDictionary<string, IProfileCreator> _creators;
    public LoadProfilesFactory(IEnumerable<IProfileCreator> creators)
    {
        _creators = creators.ToDictionary(x => x.ProfileTypeName);
    }
    
    public IProfile CreateProfile(Arguments.Arguments arguments)
    {
        var profile = arguments.Profile;
        if (!_creators.TryGetValue(profile.Type, out var creator))
            throw new ApplicationException($"No profile with name of {profile.Type}");

        return creator.Create(profile);
    }
}