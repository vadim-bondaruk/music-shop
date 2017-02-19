﻿using System;
using System.Collections.Generic;

namespace Shop.Common.Models
{
    public class Song
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TimeSpan Duration { get; set; }

        public byte[] TrackFile { get; set; }

        public string Genre { get; set; }

        public DateTime? Birthday { get; set; }

        public byte[] Image { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual Album Album { get; set; }

        public virtual IEnumerable<SongPrice> SongPrices { get; set; }

        public virtual IEnumerable<Feedback> Feedbacks { get; set; }

        public virtual IEnumerable<Vote> Votes { get; set; }
    }
}