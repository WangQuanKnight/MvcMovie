using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        //HTTPGET
        public IActionResult Index()
        {
            return View();
        }


        //HTTPGET
        public IActionResult Welcome(string name,int  numTimes = 3)
        {
            ViewData["name"] = name;
            ViewData["numTimes"] = numTimes;

            return View();
        }
    }
}