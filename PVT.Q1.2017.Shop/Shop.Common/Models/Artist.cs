using System;
using System.Collections.Generic;

namespace Shop.Common.Models
{
    public class Artist
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public byte[] Photo { get; set; }

        public string Biography { get; set; }

        public virtual IEnumerable<Song> Songs { get; set; }

        public virtual IEnumerable<Album> Albums { get; set;
        }
    }
}