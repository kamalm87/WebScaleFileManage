using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace FileOrganizer
{
    // For media files:
    // * album art will be SHA-1 hashed on a per-file basis to save space on duplicated art (albums, compiliations, etc...)
    // * there is a static container storing: <SHA-1 hash, contents of art>
    // * the following class is required to override the hashing functionality of byte[]
    //   so that it hashes based on the contents each byte[], rather than the reference
    public class ByteArrayComparer : IEqualityComparer<byte[]>
    {
        public bool Equals(byte[] l, byte[]r)
        {
            if (l == null || r == null)
            {
                return l == r;
            }
            return l.SequenceEqual(r);
        }

        public int GetHashCode(byte[] key)
        {
            if (key == null)
            {
                throw new ArgumentException("key");
            }
            else
            {
                return key.Sum(b => b);
            }
        }
    }
    


    public enum FileType
    {
        Media = 0,
        Pdf = 1
    }

    public abstract class FileMetaData
    {
        public FileInfo FileInfo { get; set; }
        public FileType FileType { get; set; }
    }

    public class MediaFile : FileMetaData
    {
        public MediaFile(TagLib.File file)
        {
            FileType = FileType.Media;
            var tag = file.Tag;
            Title = tag.Title;
            Album = tag.Album;
            Artist = tag.Artists.FirstOrDefault();
            AlbumArtist = tag.AlbumArtists.FirstOrDefault();
            TrackNumber = tag.Track;
            TrackCount = tag.TrackCount;
            DiscNumber = tag.Disc;
            DiscCount = tag.DiscCount;
            Genre = tag.Genres.FirstOrDefault();
            Comment = tag.Comment;
            Lyrics = tag.Lyrics;
            Year = tag.Year;
            BitRate = file.Properties.AudioBitrate;
            Duration = file.Properties.Duration;
            ArtworkHash = setArtIfAny(tag.Pictures);
        }



        public byte[] GetArtwork()
        {
            if(ArtworkHash != null)
            {
                return ArtworkHashMap.Value[ArtworkHash];
            }
            else
            {
                return null;
            }
        }


        private byte[] setArtIfAny(TagLib.IPicture[] pictures)
        {
            if(pictures != null && pictures.Any())
            {
                var picture = pictures.First();
                return HashArt(picture.Data.Data);
            }
            else
            {
                return null;
            }
        }

        private byte[]HashArt(byte[] data)
        {
            if (_sha1 == null) _sha1 = new Lazy<SHA1>( () => new SHA1CryptoServiceProvider());
            byte[] hashCode = _sha1.Value.ComputeHash(data);
            
            if (!ArtworkHashMap.Value.ContainsKey(hashCode))
            {
                ArtworkHashMap.Value[hashCode] = data;
            }
            return hashCode;
        }

        private static Lazy<SHA1> _sha1;

        

        public int? BitRate { get; set; }
        public TimeSpan Duration { get; set; }

        public string Title { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public string AlbumArtist { get; set; }
        public uint? TrackNumber { get; set; }
        public uint? DiscNumber { get; set; }
        public uint? TrackCount{ get; set; }
        public uint? DiscCount { get; set; }
        public uint? Year   { get; set; }
        public string Genre { get; set; }
        public string Comment { get; set; }
        public string Lyrics { get; set; }
        public byte[] ArtworkHash { get; set; }


        private static byte[] GetArtwork(byte[] hash)
        {
            if (ArtworkHashMap.Value.ContainsKey(hash))
            {
                return ArtworkHashMap.Value[hash];
            }
            else
            {
                return null;
            }
        }

        public  static Lazy<Dictionary<byte[], byte[]>> ArtworkHashMap = new Lazy<Dictionary<byte[], byte[]>>( () => new Dictionary<byte[], byte[]>(new ByteArrayComparer()));

    }




    public class PdfFile : FileMetaData
    {
        public PdfFile(FileInfo file)
        {
            FileInfo = file;
            try
            {
                iTextSharp.text.pdf.PdfReader pdf = new iTextSharp.text.pdf.PdfReader(file.FullName);
                NumberOfPages = pdf.NumberOfPages;
                var _bm = iTextSharp.text.pdf.SimpleBookmark.GetBookmark(pdf);
                BookMarks = _bm != null ? _bm.ToList() : null;
                NamedDestinations = pdf.GetNamedDestination();
                MetaData = pdf.Info;
            }
            catch(Exception e)
            {
                NumberOfPages = -1;
                Exception = e;
                CanParse = false;
            }
        }

        public bool CanParse { get; set; }
        public int                              NumberOfPages { get; set; } 
        public Dictionary<string,string>        MetaData { get; set; }
        public List<Dictionary<String,object>>  BookMarks { get; set; }
        public Dictionary<Object,iTextSharp.text.pdf.PdfObject> NamedDestinations { get; set; }
        public List<Object> Links { get; set; }
        public Exception Exception { get; set; }
    }


}
