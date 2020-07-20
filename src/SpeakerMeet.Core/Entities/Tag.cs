﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpeakerMeet.Core.Entities
{
    public class Tag
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsActive { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime Updated { get; set; }

        public IEnumerable<SpeakerTag>? SpeakerTags { get; set; }
    }
}
