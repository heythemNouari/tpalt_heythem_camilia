using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace heythem_Demo.Models
{
    public class Bien
    {
        public int id { get; set; } = 0;
        public string Type { get; set; }
        public string Adresse { get; set; }
        public string PostalCode { get; set; }
        public string OwnerTel { get; set; }
        public string PhotoUrl { get; set; }

        [BindProperty]
        public IFormFile Upload { get; set; }



    }
}
