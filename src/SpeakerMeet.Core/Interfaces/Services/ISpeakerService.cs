﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpeakerMeet.Core.Models.DTOs;

namespace SpeakerMeet.Core.Interfaces.Services
{
    public interface ISpeakerService
    {
        Task<SpeakerResult> Get(Guid id);
        Task<SpeakerResult> Get(string slug);
        Task<SpeakersResult> GetAll(int pageIndex, int itemsPage, string? direction);
        Task<IReadOnlyCollection<SpeakerFeatured>> GetFeatured();
    }
}
