using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewController : ControllerBase
    {
            [HttpPost("{Encrypts}")]

            public async Task<ActionResult<Class>> Post(Class Modify)
            {
                object encrypt = null;
                //User user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
                //Modify.
                if (Modify.Encrypts == null)
                    return NotFound();
                if (Modify.Encrypts != null)
                    //Encrypt.ToString();
                    encrypt = await Encript_data.Encript_(Modify.Encrypts);
                string path = "C:\\Temp\\employee.json.txt";
                using (TextWriter tw1 = new StreamWriter(path, true))
                {
                    tw1.WriteLine(encrypt.ToString());
                    tw1.Close();

                }

                return new ObjectResult(encrypt);
            }
    }
 }
