using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektHermods;
using ProjektHermods.Models;

namespace ProjektHermods.Controllers
{
    public class ReceptsMatController : Controller
    {
        private ReceptTipsContext db = new ReceptTipsContext();

        // GET: ReceptsMat
        public ActionResult Index()
        {
            return View(db.Recepts.ToList());
        }

        // GET: ReceptsMat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recept recept = db.Recepts.Find(id);
            ViewBag.Recepies = db.Recepts.ToList();
            if (recept == null)
            {
                return HttpNotFound();
            }
            return View(recept);
        }

       
    }
}
