using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Angular.Models;

namespace Angular
{
    public class PersonController : ApiController
    {
        private static readonly List<Person> people = new List<Person>
        {
            new Person
            {
                Id = 1,
                FirstName = "Homer",
                LastName = "Simpson"
            },
            new Person
            {
                Id = 2,
                FirstName = "Marge",
                LastName = "Simpson"
            },
            new Person
            {
                Id = 3,
                FirstName = "Bart",
                LastName = "Simpson"
            },
        };

        public IHttpActionResult Get()
        {
            return Ok(people);
        }
    }
}
