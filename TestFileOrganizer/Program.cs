using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileOrganizer;
using Newtonsoft.Json;
using System.IO;

namespace TestFileOrganizer
{




    class Program
    {
        static void Main(string[] args)
        {

            var books = new Dictionary<string, PdfFile>();
            foreach(var f in Directory.GetFiles(@"D:\OneDrive\Books\PDF", "*pdf", SearchOption.AllDirectories))
            {
                var p = new PdfFile(new FileInfo(f));
                Console.WriteLine("{0}: {1}", f, p.NumberOfPages);
                books[f] = p;
            }

            var bookMarks = JsonConvert.DeserializeObject<Dictionary<string, BookMark>>(System.IO.File.ReadAllText(@"C:\users\Kamal\desktop\pdfbookmarks.json"));

            PathContents.Parser = new Parser(@"C:\Users\Kamal\Desktop\ExtensionMap.json");
            var x = PathContents.Parser;
            var files = new Dictionary<string, HashSet<MediaFile>>();
            var t = new MediaFile(TagLib.File.Create(@"D:\Music\Alternative\blink-182\MP3\Enema of the State\All the Small Things.mp3"));

                foreach (var d in Directory.GetDirectories(@"D:\Files\Music\Rap\Kendrick Lamar"))
                {
                    files[d] = new HashSet<MediaFile>();

                    foreach (var f in Directory.GetFiles(d))
                    {
                        var ext = new FileInfo(f).Extension;
                        if(x.ExtensionToProcedureMap.ContainsKey(ext) && x.ExtensionToProcedureMap[ext] == "Media")
                        {
                            try
                            {
                                files[d].Add(new MediaFile(TagLib.File.Create(f)));
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine("{0}: {1}", f, e.Message);
                            }
                        }
                            
                    }
                    
                }

            var map = MediaFile.ArtworkHashMap;
            var HS = new HashSet<byte[]>();
            
            
            var tf = new System.IO.FileInfo(@"C:\testing\All of Statistics.pdf");
            var pdf = new PdfFile(tf);
            var md = new Dictionary<string, string>
            {
                {"Title","All of Statistics" },
                {"Author","Larry A. Wasserman" },
                {"Subject","Statistics"},
                {"Keywords","Statistics,Mathematics"},
                {"ISBN-13","978-0387402727" },
                {"ISBN-10", "0387402721"}
            };
            
            x.AddMetaDataToPdfFile(tf, md);
            x.ParseFile(tf);
            var wut = new PathContents(@"D:\");
//            wut.Serialize();
            var csvList = new List<string>();
            csvList.Add("Extension,AggregateSize,Directory,File,Size");
            foreach(var e in wut.ExtensionCount)
            {
                foreach(var f in e.Value.Files)
                {
                    csvList.Add(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"", e.Key, e.Value.AggregateFileSize.ToString("N"), f.Directory.FullName, f.Name, f.Length.ToString("N")));
                }
            }
            System.IO.File.WriteAllLines(@"D:\data.csv", csvList);
        }
    }
}
