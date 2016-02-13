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
            foreach(var f in files)
            {
                log.Info("Parsing: " + f);
                var file = PathContents.Parser.ParseFile(new System.IO.FileInfo(f));
                var mf = file as FileOrganizer.Media.MediaFile;
                var pdf = file as FileOrganizer.Media.PdfFile;
                if(mf != null)
                {
                    mfs.Add(mf.ToJson());
                }
                else if ( pdf != null)
                {
                    pdfs.Add(pdf.ToJson());
                }
            }
            
            
            return new Dictionary<string, List<dynamic>>()
            {
                {"Pdfs", pdfs },
                {"MediaFiles", mfs }
            };
            
        }
	}
}
