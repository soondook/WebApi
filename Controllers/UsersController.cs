using Microsoft.AspNetCore.Mvc;
using System;
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
    public class UserController : MyControllerBase1

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
            //Console.WriteLine(buffer.GetValue(0));
            //Console.WriteLine(buffer.GetValue(1));

            Employee.JSONDeserilaize(ms);
            return new ObjectResult(ms);
        }

     }
}
