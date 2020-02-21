using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace Zires_Explorer.Player
{
    class AlbumInfo
    {
        public AlbumInfo()
        {
            
        }

        public string Name { get; set; }
        public Artist Artist { get; set; }
        public string Duration { get; set; }
        public BitmapSource AlbumArt { get; set; }
        public BitmapSource Thumbnail { get; set; }
        public SongCollection AlbumSongs { get; set; }
        public bool HasArt { get; set; }

    }
}
