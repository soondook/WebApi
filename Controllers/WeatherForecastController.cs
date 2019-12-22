using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace WebApi.Controllers
{
    [ApiController]
    public class MyControllerBase : ControllerBase
    {
    }


    [Route("[controller]")]
    public class WeatherForecastController : MyControllerBase
    {
        /*
           [HttpPost]
           public IEnumerable<WeatherForecast> Post()
           {
               var rng = new Random();
               return Enumerable.Range(1, 5).Select(index => new WeatherForecast
               {
                   Date = DateTime.Now.AddDays(index),
                   TemperatureC = rng.Next(-20, 55),
                   //Summary = Summaries[rng.Next(Summaries.Length)],

               })
               .ToArray();
           }
           */
        [HttpPost("{Encrypt}")]
        public async Task<ActionResult<WeatherForecast>> Post(WeatherForecast Modify)
        {
            object encrypt = null;
            string ms = "";
            Console.WriteLine(Modify.Encrypt);
            //User user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            //Modify.
            if (Modify.Encrypt == null)
                return NotFound();
            if (Modify.Encrypt != null)
                //Encrypt.ToString();
                encrypt = await Encript_data.Encript_(Modify.Encrypt);
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






