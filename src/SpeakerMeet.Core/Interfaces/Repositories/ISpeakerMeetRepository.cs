using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeakerMeet.Core.Interfaces.Repositories
{
    public interface ISpeakerMeetRepository : IEFRepository
    {
        Task<IEnumerable<T>> GetRandom<T>(int count) where T : class;
    }
}
