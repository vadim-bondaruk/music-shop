using System;
using System.Collections.Generic;
using Shop.Infrastructure.Models;

namespace Shop.Common.Models
{
    public class Track : BaseEntity
    {
        public string Name { get; set; }

        public TimeSpan? Duration { get; set; }

        public byte[] TrackFile { get; set; }

        public string Genre { get; set; }

        public int? Year { get; set; }

        public byte[] Image { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual Album Album { get; set; }

        public virtual IEnumerable<TrackPrice> TrackPrices { get; set; }

        public virtual IEnumerable<Feedback> Feedbacks { get; set; }

        public virtual IEnumerable<Vote> Votes { get; set; }
    }
}