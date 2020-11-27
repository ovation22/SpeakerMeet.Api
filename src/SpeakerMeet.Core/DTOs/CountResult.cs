﻿namespace SpeakerMeet.Core.DTOs
{
    public record CountResult
    {
        public int SpeakerCount { get; init; }

        public int ConferenceCount { get; init; }

        public int CommunityCount { get; init; }
    }
}
