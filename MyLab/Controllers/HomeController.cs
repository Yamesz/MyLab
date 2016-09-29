using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyLab.Controllers
{
    public class HomeController : ApiController
    {
        // GET: api/Home/5
        public IHttpActionResult Get(int id)
        {
            if (id == 4)
            {
                int a = 0;
                int b = 1 / a;
            }
            return Ok(
                new
                {
                    code = 200,
                    id = id
                });
        }
    }
}
