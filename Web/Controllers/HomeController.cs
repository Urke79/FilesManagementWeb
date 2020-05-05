using DataAccess;
using DataAccess.EntityModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FileControl.Models;

namespace FileControl.Controllers
{
    public class HomeController : Controller
    {      
        public ActionResult Index()
        {
            return View();
        }
    }
}