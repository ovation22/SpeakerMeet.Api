using System;

namespace SpeakerMeet.Core.Interfaces.Utilities
{
    public interface ITimeManager
    {
        DateTime Now();
        DateTime UtcNow();
    }
}
