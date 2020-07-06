using System;
using SpeakerMeet.Core.Interfaces.Utilities;

namespace SpeakerMeet.Infrastructure.Utilities
{
    public class TimeManager : ITimeManager
    {
        public DateTime Now() => DateTime.Now;

        public DateTime UtcNow() => DateTime.UtcNow;
    }
}