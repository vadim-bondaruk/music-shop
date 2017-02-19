using System.Collections.Generic;

namespace Shop.Common.Models
{
    public class Album
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Cover { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual IEnumerable<Song> Songs { get; set; }

        public virtual IEnumerable<AlbumPrice> AlbumPrices { get; set; }
    }
}