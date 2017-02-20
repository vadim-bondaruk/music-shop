using System.Collections.Generic;

using Ship.Infrastructure.Models;

namespace Shop.Common.Models
{
    public class Album : BaseEntity
    {
        public string Name { get; set; }

        public byte[] Cover { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual IEnumerable<Song> Songs { get; set; }

        public virtual IEnumerable<AlbumPrice> AlbumPrices { get; set; }
    }
}