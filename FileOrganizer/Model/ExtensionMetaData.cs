using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileOrganizer.Extension
{
    public class ExtensionMetaData
    {
        public ExtensionMetaData()
        {
            AggregateFileSize = 0;
            Files = new HashSet<FileInfo>();
        }
        public ExtensionMetaData(FileInfo file)
        {
            AggregateFileSize = 0;
            Files = new HashSet<FileInfo>();
            AddFile(file);
        }
        public void AddFile(FileInfo file)
        {
            Files.Add(file);
            AggregateFileSize += file.Length;
        }
        public long AggregateFileSize { get; set; }
        public HashSet<FileInfo> Files { get; set; }

    }
}
