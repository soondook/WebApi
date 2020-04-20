using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace WebApi
{
    [ApiController]
    public class MyControllerBase3 : ControllerBase
    {
    }

    [Route("[controller]")]

    public class ReturnIPController : MyControllerBase3
    {
        [HttpPost("{Edit}")]
        public async Task<ActionResult<ReturnIP>> Post(ReturnIP Modifys)
        {
            string chk_res1 = "";
            string ms;
            //string compassname ="";
            //string compassname = HttpContext.Connection.RemoteIpAddress.ToString();
            IPAddress ipAddress = HttpContext.Connection.RemoteIpAddress;
            //Console.WriteLine(compassname);
            Console.WriteLine(Modifys.CompassName);
            //User user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            //Modify.
            if (Modifys.CompassName == null)
                return NotFound();
            if (Modifys.CompassName != null) {
                string CompassName = Modifys.CompassName;
                chk_res1 = DBSQLServerUtils.Connection2(CompassName);
                Console.WriteLine(chk_res1);
                //return new ObjectResult(chk_res1);
                //EmployeeIP.JSONDeserilaize(chk_res1);
                return new ObjectResult(chk_res1);
            }
            //encrypt = await Encript_data.Encript_(Modifys.FirstNames);

            
            /*
ms = Employee.Json_pars(FirstNames: encrypt.ToString(), LastName: Modifys.LastName, EmployeeID: Modifys.EmployeeID, Designation: Modifys.Designation);
byte[] byteArray = Encoding.UTF8.GetBytes(ms);
MemoryStream stream = new MemoryStream(byteArray);
byte[] buffer = stream.ToArray();
Console.WriteLine(buffer.GetValue(0));
//Console.WriteLine(ms);
string ResultJson = "[" + ms + "]";
JArray a = JArray.Parse(ResultJson);
Console.WriteLine(a.Children());
foreach (JObject o in a.Children<JObject>())
{
foreach (JProperty p in o.Properties())
{
string name = p.Name;
string value = (string)p.Value;
Console.WriteLine(name + " -- " + value);
}
}
//MyClass ss = JsonConvert.DeserializeObject<MyClass>(ms);
//Console.WriteLine(ss.data[1].LastName);
Employee.JSONDeserilaize(ms);
return new ObjectResult(ms);
*/
            return new ObjectResult(chk_res1);
        }

    }
    /*
    public class MyClass3
    {
        public List<Item> data;
    }
    public class Item3
    {
        public string FirstName;
        public string LastName;
        public int employeeID;
        public string designation;
    }
    */
}