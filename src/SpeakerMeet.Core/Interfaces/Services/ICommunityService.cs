using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpeakerMeet.Core.DTOs;

namespace SpeakerMeet.Core.Interfaces.Services
{
    public interface ICommunityService
    {
        Task<CommunityResult> Get(Guid id);
        Task<CommunityResult> Get(string slug);
        Task<CommunitiesResult> GetAll(int pageIndex, int itemsPage, string? direction);
        Task<IReadOnlyCollection<CommunityFeatured>> GetFeatured();
    }
}
