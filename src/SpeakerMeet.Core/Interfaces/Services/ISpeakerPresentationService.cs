﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpeakerMeet.Core.DTOs;

namespace SpeakerMeet.Core.Interfaces.Services
{
    public interface ISpeakerPresentationService
    {

        Task<IEnumerable<SpeakerPresentationsResult>> GetAll(Guid id);
    }
}
