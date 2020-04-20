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

namespace WebApi
{

    [ApiController]
    public class MyControllerBase1 : ControllerBase
    {
    }

    [Route("[controller]")]
        
    public class UsersController : MyControllerBase1
    {
        [HttpPost("{Users}")]
        public async Task<ActionResult<User>> Post(User Modifys)
        {
            object encrypt = "";
            string ms;
            Console.WriteLine(Modifys.FirstNames);
            //User user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            //Modify.
            if (Modifys.FirstNames == null)
                return NotFound();
            if (Modifys.FirstNames != null)
                encrypt = await Encript_data.Encript_(Modifys.FirstNames);

            ms = Employee.Json_pars(FirstNames: encrypt.ToString(), LastName: Modifys.LastName, EmployeeID: Modifys.EmployeeID, Designation: Modifys.Designation);
            byte[] byteArray = Encoding.UTF8.GetBytes(ms);
            MemoryStream stream = new MemoryStream(byteArray);
            byte[] buffer = stream.ToArray();
            Console.WriteLine(buffer.GetValue(0));
            Console.WriteLine(ms);
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
        }

     }

    public class MyClass
    {
        public List<Item> data;
    }
    public class Item
    {
        public string FirstName;
        public string LastName;
        public int employeeID;
        public string designation;
    }
}
