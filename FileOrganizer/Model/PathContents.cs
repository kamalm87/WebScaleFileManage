using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using FileOrganizer.Extension;
using log4net;
using log4net.Config;

namespace FileOrganizer.Directory
{
    public class PathContents
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PathContents));



        public static FileOrganizer.Parser.Parser Parser = new FileOrganizer.Parser.Parser(@"C:\Users\Kamal\Desktop\ExtensionMap.json");

        public PathContents(string basePath)
        {
            BaseDirectory = new DirectoryInfo(basePath);
            var children = BaseDirectory.GetDirectories().Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden) && !f.Attributes.HasFlag(FileAttributes.ReadOnly)).ToList();
            SubLevelDirectores[0] = new HashSet<DirectoryInfo>(children);
            while (children.Any())
            {
                var nextChildLevel = new List<DirectoryInfo>();
                var allFiles = new List<FileInfo>();
                children.ForEach(c => allFiles.AddRange(c.GetFiles()));
                foreach(var f in allFiles)
                {
                    InsertExtension(f);
                    Parser.ParseFile(f);
                }

                foreach (var child in children.Where( f => !f.Attributes.HasFlag(FileAttributes.Hidden) && !f.Attributes.HasFlag(FileAttributes.ReadOnly)))
                {
                    nextChildLevel.AddRange(child.GetDirectories());
                }
                if (nextChildLevel.Any())
                {
                    SubLevelDirectores[SubLevelDirectores.Count] = new HashSet<DirectoryInfo>(nextChildLevel);
                }
                children = nextChildLevel;
            }
        }
        
        private void InsertExtension(FileInfo file)
        {
            if (!ExtensionCount.ContainsKey(file.Extension))
            {
                ExtensionCount[file.Extension] = new ExtensionMetaData(file);
                
            } 
            else
            {
                ExtensionCount[file.Extension].AddFile(file);
            }
        }

        public void Serialize(string filePath, Formatting format = Formatting.Indented)
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(SubLevelDirectores, format));
        }
        
        public Dictionary<string, ExtensionMetaData> ExtensionCount = new Dictionary<string, ExtensionMetaData>();
        public DirectoryInfo BaseDirectory { get; set; }
        public Dictionary<int, HashSet<DirectoryInfo>> SubLevelDirectores = new Dictionary<int, HashSet<DirectoryInfo>>();
    }
}
