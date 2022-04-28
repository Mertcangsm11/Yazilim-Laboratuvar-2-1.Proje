using CarProje.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;

namespace CarProje.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index(string Message)
        {
            return View();
        }
    }
}
