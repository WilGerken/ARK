using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Domain;
using ArkWeb.Common;
using ArkWeb.Models;

namespace ArkWeb.Controllers
{
    public class ProjectController : Csla.Web.Mvc.MyController
    {
        public IActionResult Index()
        {
            return View();

        }

        public IActionResult Project_List()
        {
            ViewData["Message"] = "Your project list page.";

            return View();
        }

        public IActionResult Location_List()
        {
            ViewData["Message"] = "Your location list page.";

            return View();
        }
    }
}