using PressureApp.Arguments;
using PressureApp.Core.Interfaces;

namespace PressureApp.Interfaces;

interface IProfileCreator
{
    string ProfileTypeName { get; }

    IProfile Create(ProfileArguments arguments);
}