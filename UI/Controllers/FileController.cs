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



namespace WebApiTest.Controllers
{
	public class FileController : ApiController
	{
        static Dictionary<string, PathContents> x = new Dictionary<string, PathContents>();
        public dynamic FileQuery(dynamic directory)
        {
            var dir = directory["Directory"].Value;
            x[dir] = new PathContents(dir);
            var files = System.IO.Directory.GetFiles(dir);
            var wtf = new List<dynamic>();
            foreach(var f in files)
            {
                wtf.Add(PathContents.Parser.ParseFile(new System.IO.FileInfo(f)));
            }
            return null;
        }
	}
}
