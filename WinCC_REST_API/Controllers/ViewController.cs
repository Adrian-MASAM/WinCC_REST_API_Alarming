using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinCC_REST_API.Global;

namespace WinCC_REST_API.Controllers
{
    public class ViewController : Controller
    {
        public ActionResult Alarme()
        {
            return View();
        }
    }
}