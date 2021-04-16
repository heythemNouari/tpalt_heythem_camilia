using heythem_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Net.Http.Headers;

namespace heythem_Demo.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILogger<LocationController> _logger;
        private Repository Repo = new Repository();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Ajout()
        {
            ViewBag.Message = "ajout";
            return View("Index");
        }
        public IActionResult Recherche()
        {
            ViewBag.Message = "recherche";
            return View("Index");
        }
        public IActionResult getBien(Bien bien)
        {
            List<Bien> res = Repo.getBien(bien);
            ViewBag.Message = res;
            return View("Bien");
        }
        [HttpPost]
        public IActionResult addBien(Bien bien )
        {

            
            string id = Request.Cookies["id"] ;
            if ( id != null)
            {
                int addedBien = Repo.addBien(bien, int.Parse(id));

                // le traitement pour lire les fichier et puis les stocker dans le cloud
                //try
                //{
                //   // var postedFile = Request.Form.Files[0];
                //    var postedFile = Request.Form.Files["photo"];
                   
                    
                    
                //    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");  
                //    if (postedFile.Length > 0)
                //    {
                       
                //        var fileName = ContentDispositionHeaderValue.Parse(postedFile.ContentDisposition)
                //            .FileName.Trim('"');
                        
                //        var finalPath = Path.Combine(uploadFolder, fileName);
                //        using (var fileStream = new FileStream(finalPath, FileMode.Create))
                //        {
                //            postedFile.CopyTo(fileStream);
                //        }
                        
                //    }

                //}
                //catch (Exception ex)
                //{
                //    return StatusCode(500, $"Some Error Occcured while uploading File {ex.Message}");
                //}

            }
            else
            {
                return View("../Home/Index");

            }
                ViewBag.Message = "added";
            return View("Index");
        }
    }
}
