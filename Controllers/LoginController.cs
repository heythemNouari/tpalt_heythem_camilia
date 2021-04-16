using heythem_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace heythem_Demo.Controllers
{
    
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private Repository Repo = new Repository();
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login(Login LoginModel)
        {
            int id = Repo.IsMember(LoginModel);
            if ( id != -1)
            {
                Response.Cookies.Append("id", id.ToString());

                return View("../Location/Index");
            }
            else
            {
                ViewBag.Message = null;
                return View();
            }
        }
    }
}
