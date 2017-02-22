using System;
using System.Collections.Generic;
using Shop.Infrastructure.Models;

namespace Shop.Common.Models
{
    public class Artist : BaseEntity
    {
        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public byte[] Photo { get; set; }

        public string Biography { get; set; }

        public virtual IEnumerable<Track> Tracks { get; set; }

        public virtual IEnumerable<Album> Albums { get; set; }
    }
}