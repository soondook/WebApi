using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCApp.Models;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;


namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            string authData = $"Login: {login}   Password: {password}";
            return Content(authData);
        }

        [HttpPost]
        public string Privacy(int altitude, int height)
        {
            double square = altitude * height / 2;
            return $"Площадь треугольника с основанием {altitude} и высотой {height} равна {square}";
        }


        //[HttpGet("{IpAddress}", Name = "Get")]
        [HttpPost]
        public string Area(string altitude, string height)
        {
            string ipAddr = height;
            //string sftp_res = SSH_NewConnection.NewConnection(ipAddr, 22);
            /*bool flag = IPAddress.TryParse(ipAddr, out IPAddress IP);
           if (flag)
                Console.WriteLine("{0} is a valid IP address", ipAddr);
            else
                Console.WriteLine("{0} is not a valid IP address", ipAddr);
            Console.WriteLine(ipAddr + " myIpString");
            */
            try
            {
                var match = Regex.Match(ipAddr, @"\b(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\b");

                if (match.Success)
                {
                    Console.WriteLine(match.Captures[0] + " is a valid IP address");
                    string sftp_res = SSH_NewConnection.NewConnection(ipAddr, 22);
                    return sftp_res.ToString();
                }
                else
                {
                    Console.WriteLine(ipAddr + " is a not valid IP address");
                    //sftp_res = ipAddr + " is a not valid IP address";
                    return ipAddr.ToString() + " is a not valid IP address!";
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        
            string chk_res = DBSQLServerUtils.Connection(altitude);
            string sftp_res2 = SSH_NewConnection.NewConnection(chk_res, 22);
            //var chk_res = SFTPConnection.Connection(IpAddress, 22);
            return sftp_res2.ToString();
            }
            

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
