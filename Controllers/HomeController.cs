using Fitness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Controllers
{
    public class HomeController : Controller
    {
        private readonly ModelContext _context;

        //private readonly ILogger<HomeController> _logger;

        public HomeController(ModelContext context)
        {
            //  _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestDietSystem([Bind("Height,Weight")] FitnessUserInformation fitnessUserInformation)
        {
            var userID = TempData["UserID"];
            
            if (ModelState.IsValid)
            {
                if (userID == null)
                {
                    //go to login page
                    return RedirectToAction("Index", "FitnessUserLogins");

                    //return View("../UserDashboard/Index");
                   // return View("FitnessUserLogins");
                }
                else
                {
                    //proceed
                }
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
