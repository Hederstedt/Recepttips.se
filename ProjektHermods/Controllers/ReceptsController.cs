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
    public class ReceptsController : Controller
    {
        //skapar en context
        private ReceptTipsContext context = new ReceptTipsContext();

        // GET: Recepts
        public ActionResult Index()
        {
            //skriver ut alla recept ifrån databasen i en lista
            return View(context.Recepts.ToList());
        }

        // GET: Recepts/Details/5
        //kollar id om vilken produkt
        public ActionResult Details(int? id)
        {
            // finns de inget id så skickas vi hit
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //kollar om vi hittar ett recept med de id
            Recept recept = context.Recepts.Find(id);
            // en viebag av recept som hämtar alla recept i contexten som en lista
            ViewBag.Recepies = context.Recepts.ToList();
            //om inte så skickas vi till en annan sida
            if (recept == null)
            {
                return HttpNotFound();
            }
            //finns de ett recept med de id vi är ute efter så skickar vi det i vyn
            return View(recept);
        }
        
        
    }

}
