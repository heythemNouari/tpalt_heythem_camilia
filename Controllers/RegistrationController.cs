using heythem_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace heythem_Demo.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ILogger<RegistrationController> _logger;
        private Repository Repo = new Repository();
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Register(Registration RegistrationModel)
        {
            if (Repo.Insert(RegistrationModel))
                ViewBag.Message = "success";
            
            
            return View("../Home/Index");
        }
    }
}
