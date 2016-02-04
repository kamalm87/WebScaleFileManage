using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Angular.Models;

namespace Angular
{
    public class QueryFilesController : ApiController
    {

        public IHttpActionResult Query(dynamic d)
        {
            var wtf = d;
            return null;
        }

        private List<string> blah()
        {
            return null;
        }
    

        public IHttpActionResult Get(dynamic d)
        {
            return Ok<dynamic>(d);
        }
    }
}
