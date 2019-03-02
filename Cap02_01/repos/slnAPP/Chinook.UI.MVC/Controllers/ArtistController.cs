using Chinok.Data;
using Chinook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chinook.UI.MVC.Controllers
{
    public class ArtistController : Controller
    {
        // GET: Artist
        public ActionResult Index()
        {
            var da = new ArtistaDA();
            var model = da.Gets();

            return View(model);
        }
    }
}