using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    public class MyControllerBase2 : ControllerBase
    {
    }

    [Route("[controller]")]
    public class SFTP_connController : MyControllerBase2
    {
        // GET: api/sFTP_conn
        //[HttpPost("{Users}")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "please enter compassname!" };
        }

        // GET: api/sFTP_conn/5
        [HttpGet("{IpAddress}", Name = "Get")]
        public string Get(string IpAddress)
        {
            var chk_res = SSH_NewConnection.NewConnection(IpAddress, 22);
            //var chk_res = SFTPConnection.Connection(IpAddress, 22);
            return chk_res.ToString();
        }

        // POST: api/sFTP_conn
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/sFTP_conn/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
