using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Data.Sql;
using System.Web.Mvc;
using ProjektHermods.Models;

namespace ProjektHermods.Controllers
{
    [Serializable]
    public class SearchController : Controller
    {

        public ActionResult Index()
        {

           
            return View();
        }



        public JsonResult AutoCompleteIngrediens(string term)
        {
            ReceptTipsContext context = new ReceptTipsContext();

            if (term.Contains("*"))
            {


                var result = (from r in context.Ingrediens
                              orderby r.Name
                              select new { r.Name }).Distinct();
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var result = (from r in context.Ingrediens
                              orderby r.Name
                              where r.Name.ToLower().StartsWith(term.ToLower())
                              select new { r.Name }).Distinct();
                return Json(result, JsonRequestBehavior.AllowGet);
            }


        }
    }
}