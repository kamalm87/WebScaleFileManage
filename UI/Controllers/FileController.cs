using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using System.Collections.Concurrent;
using System.Web.Http;
using FileOrganizer.Directory;

using log4net;
using log4net.Config;


namespace WebApiTest.Controllers
{
    public class FileController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(FileController));


        static Dictionary<string, PathContents> x = new Dictionary<string, PathContents>();



        public dynamic FileQuery(dynamic directory)
        {
            var dir = directory["Directory"].Value;
            x[dir] = new PathContents(dir);
            var files = System.IO.Directory.GetFiles(dir);
            var pdfs = new List<dynamic>();
            var mfs = new List<dynamic>();

            var mfsOptions = new Dictionary<string, HashSet<string>>
            {
                {"Artist",new HashSet<String>()},
                {"Genre",new HashSet<String>()},
                {"Album",new HashSet<String>()},
                {"Comment",new HashSet<String>()},
                {"Extension",new HashSet<String>()}
            };

            var pdfOptions = new Dictionary<string, HashSet<string>>{
                {"Author",new HashSet<string>()},
                {"Subject",new HashSet<string>()}
            };

            foreach(var f in files)
            {
                log.Info("Parsing: " + f);
                var file = PathContents.Parser.ParseFile(new System.IO.FileInfo(f));
                var mf = file as FileOrganizer.Media.MediaFile;
                var pdf = file as FileOrganizer.Media.PdfFile;
                if(mf != null)
                {
                    mfs.Add(mf.ToJson());
                    mfsOptions["Artist"].Add(mf.Artist);
                    mfsOptions["Genre"].Add(mf.Genre);
                    mfsOptions["Album"].Add(mf.Album);
                    mfsOptions["Comment"].Add(mf.Comment);
                    mfsOptions["Extension"].Add(mf.FileType.ToString());
                }
                else if ( pdf != null)
                {
                    pdfs.Add(pdf.ToJson());
                    if (pdf.MetaData != null && pdf.MetaData.ContainsKey("Author"))
                    {
                        pdfOptions["Author"].Add(pdf.MetaData["Author"]);
                    }

                    if (pdf.MetaData != null && pdf.MetaData.ContainsKey("Subject"))
                    {
                        pdfOptions["Subject"].Add(pdf.MetaData["Subject"]);
                    }
                }
            }
            
            
            return new Dictionary<string, Dictionary<string,dynamic>>()
            {
                {"Pdfs",
                    new Dictionary<string,dynamic>
                    {
                        {"List", pdfs },
                        {"Options", pdfOptions }
                    }
                },
                {"MediaFiles",  new Dictionary<string,dynamic>
                    {
                        {"List", mfs },
                        {"Options", mfsOptions }
                    }
                }
            };
            
        }
	}
}
