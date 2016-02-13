using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using log4net;
using log4net.Config;

namespace FileOrganizer.Parser
{

    public class BookMark
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(BookMark));

        public BookMark() { }
        public string Page { get; set; }
        public Dictionary<string, BookMark> Children { get; set; }
    }


    public class Parser
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Parser));
        public Parser(string filePath)
        {
            initContainers();
            foreach (var mdGrouping in JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(File.ReadAllText(filePath)))
            {
                foreach(var extension in mdGrouping.Value)
                {
                    ExtensionToProcedureMap[extension] = mdGrouping.Key;
                }
            }
        }

        private void initContainers()
        {
            ExtensionToProcedureMap = new Dictionary<string, string>();
        }
        public Dictionary<string,string> ExtensionToProcedureMap { get; set; }



        public object ParseFile(FileInfo file)
        {
            
            var procedure = ExtensionToProcedureMap.ContainsKey(file.Extension) ? ExtensionToProcedureMap[file.Extension] : null;
            if (procedure != null && !file.Attributes.HasFlag(FileAttributes.Hidden))
            {
                return ProcedureMap[procedure](file);
            }
            else
            {
                return null;
            }
        }

        static private Dictionary<string, Func<FileInfo, object>> ProcedureMap = new Dictionary<string, Func<FileInfo, object>>
        {
            {"Media", ParseMediaFile },
            {"Pdf", ParsePdfFile }
        };

        static private object ParseMediaFile(FileInfo file)
        {
            try
            {
                var mf = TagLib.File.Create(file.FullName);
                if (mf.Tag.Pictures.Any())
                {
                    var pics = mf.Tag.Pictures.ToList();
                }
                log.Info("Parsed: " +  file.Name);
                return new FileOrganizer.Media.MediaFile(mf, file);
            }
            catch(Exception e)
            {
                Exceptions[file] = e;
                log.Fatal("Parsed: " + file.Name);
            }
            return null;
            
        }
        private static Dictionary<FileInfo, Exception> Exceptions = new Dictionary<FileInfo, Exception>();

        static public object ParsePdfFile(FileInfo file)
        {
            /*
            var pdf = new PdfReader(file.FullName);
            var catalog = pdf.Catalog;
            var info = pdf.Info;
            */
            return new FileOrganizer.Media.PdfFile(file);    
 
            //return null;
        }

        public void AddMetaDataToPdfFile(FileInfo file, Dictionary<string,string> metaData)
        {
            PdfReader pdf;
            var ms = new MemoryStream();
            using (var fs = File.Open(file.FullName, FileMode.Open))
            {
                fs.CopyTo(ms);
                pdf = new PdfReader(ms.ToArray());
            }
            PdfStamper pdfStamper = new PdfStamper(pdf, new System.IO.FileStream(file.FullName, System.IO.FileMode.Create));
            pdfStamper.FormFlattening = true;
            pdfStamper.MoreInfo = metaData;
            pdfStamper.Close();
            pdf.Close();
        }


        public void AddBooksToPdfFile(FileInfo file, Dictionary<string,BookMark> bookMarks)
        {
            var pdfBookMarks = new List<Dictionary<string, object>>();
            foreach (var kvp in bookMarks)
            {
                pdfBookMarks.Add(CreateBookMark(kvp));
            }

            //var pdf = new PdfReader(file.FullName);
            PdfReader pdf;
            var ms = new MemoryStream();
            using (var fs = File.Open(file.FullName, FileMode.Open))
            {
                fs.CopyTo(ms);
                pdf = new PdfReader(ms.ToArray());
            }
            
            PdfStamper pdfStamper = new PdfStamper(pdf, new System.IO.FileStream(file.FullName, System.IO.FileMode.Create));
            pdfStamper.FormFlattening = true;
            pdfStamper.Outlines = pdfBookMarks;
            pdfStamper.Close();
            pdf.Close();
        }

        private Dictionary<string,object> CreateBookMark(KeyValuePair<string,BookMark> bookMark)
        {
            var bm = new Dictionary<string, object>();
            bm["Action"] = "GoTo";
            bm["Title"] = bookMark.Key;
            bm["Page"] = string.Format("{0} XYZ 0 0 0", bookMark.Value.Page);
            if(bookMark.Value.Children != null)
            {
                List<Dictionary<string, object>> kids = new List<Dictionary<string, object>>();
                foreach(var kvp in bookMark.Value.Children)
                {
                    kids.Add(CreateBookMark(kvp));
                }
                if (kids.Any())
                {
                    bm["children"] = kids;
                }
            }
            return bm;
        }

        static void ProcessBookMark(Dictionary<string,object> bm)
        {
            
            if (bm.ContainsKey("Kids")){
                foreach(var sBM in bm["Kids"] as IEnumerable<Dictionary<string,object>>)
                {
                    ProcessBookMark(sBM);
                }
            }
        }

    }
}