using System;

namespace SpeakerMeet.Core.DTOs;

public record ConferencePatch
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public string Slug { get; init; } = null!;

    public string Location { get; init; } = null!;

    public string Description { get; init; } = null!;
}