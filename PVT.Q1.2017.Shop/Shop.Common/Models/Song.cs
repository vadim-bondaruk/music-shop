using System.Collections.Generic;

namespace Shop.Common.Models
{
    public class Song
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual Album Album { get; set; }

        public virtual IEnumerable<SongPrice> SongPrices { get; set; }

        public virtual IEnumerable<Feedback> Feedbacks { get; set; }

        public virtual IEnumerable<Vote> Votes { get; set;
        }
    }
}