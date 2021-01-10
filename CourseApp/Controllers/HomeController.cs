using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            CourseApp.Models.Request model = new Models.Request();
            model.Name = "Funda";
            model.Email = "funda@funda.com";
            model.Phone = "123";
            model.Message = "Hello";
            
            return View(model); 
        }
    }
}