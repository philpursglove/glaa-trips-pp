using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace glaa_trips_pp.Models
{
    public class Album : IPaginator
    {
        private AlbumCollection _ac;

        public Album(string absolutePath, AlbumCollection ac)
        {
            _ac = ac;
            AbsolutePath = absolutePath;
            Name = new DirectoryInfo(AbsolutePath).Name;
            Photos = new List<Photo>();
        }

        public string Name { get; }

        public string UrlName
        {
            get
            {
                return Name.Replace(" ", "%20").ToLowerInvariant();
            }
        }

        public string Link
        {
            get
            {
                return $"/album/{UrlName}/";
            }
        }

        public string AbsolutePath { get; }

        public List<Photo> Photos { get; private set; }

        public Photo CoverPhoto
        {
            get
            {
                return Photos?.FirstOrDefault();
            }
        }

        public IPaginator Next
        {
            get
            {
                int index = _ac.Albums.IndexOf(this);

                if (index < _ac.Albums.Count - 1)
                {
                    return _ac.Albums[index + 1];
                }

                return null;
            }
        }

        public IPaginator Previous
        {
            get
            {
                int index = _ac.Albums.IndexOf(this);

                if (index > 0)
                {
                    return _ac.Albums[index - 1];
                }

                return null;
            }
        }

        /// <summary>
        /// Sorts the photos in the album.
        /// </summary>
        public void Sort()
        {
            Photos = Photos.OrderBy(p => p.DisplayName).ToList();
        }

    }
}