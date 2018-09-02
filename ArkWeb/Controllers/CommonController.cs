using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Domain;
using ArkWeb.Common;

namespace ArkWeb.Controllers
{
    public class CommonController : Csla.Web.Mvc.MyController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}