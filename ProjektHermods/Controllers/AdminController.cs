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
    
    public class AdminController : Controller
    {
     private ReceptTipsContext db = new ReceptTipsContext();
        
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["username"] == null || Session["username"].ToString() != "Admin")
            {
                return Redirect("/Admin/AjaBaja");
            }
            else
            {
               foreach (var item in db.UserModels)
                {              
                    return View(db.Recepts.ToList());              
                }          
            }
            return Redirect("/"); 
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["username"] == null || Session["username"].ToString() != "Admin")
            {
                return Redirect("/Admin/AjaBaja");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Recept recept = db.Recepts.Find(id);
                if (recept == null)
                {
                    return HttpNotFound();
                }
                return View(recept);
            }
           
        }

        // GET: Admin/Create
        public ActionResult Create()
        {

            if (Session["username"] == null || Session["username"].ToString() != "Admin")
            {
                return Redirect("/Admin/AjaBaja");
            }
            else
            {
                 return View();
            }
                  
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Info,OkRecept,Tid,Picture")] Recept recept)
        {

            if (Session["username"] == null || Session["username"].ToString() != "Admin")
            {
                return Redirect("/Admin/AjaBaja");
            }
            else
            {
                 if (ModelState.IsValid)
                 {
                    db.Recepts.Add(recept);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                 }

                    return View(recept);
            }         
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["username"] == null || Session["username"].ToString() != "Admin")
            {
                return Redirect("/Admin/AjaBaja");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Recept recept = db.Recepts.Find(id);
                if (recept == null)
                {
                    return HttpNotFound();
                }
                return View(recept);
            }      
  
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Info,OkRecept,Tid,Picture")] Recept recept)
        {
            if (Session["username"] == null || Session["username"].ToString() != "Admin")
            {
                return Redirect("/Admin/AjaBaja");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(recept).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                    return View(recept);
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["username"] == null || Session["username"].ToString() != "Admin")
            {
                return Redirect("/Admin/AjaBaja");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Recept recept = db.Recepts.Find(id);
                if (recept == null)
                {
                    return HttpNotFound();
                }
                return View(recept);
            }
       
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["username"] == null || Session["username"].ToString() != "Admin")
            {
                return Redirect("/Admin/AjaBaja");
            }
            else
            {
                Recept recept = db.Recepts.Find(id);
                db.Recepts.Remove(recept);
                db.SaveChanges();
                return RedirectToAction("Index");
            }         
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult AjaBaja()
        {
            return View();
        }
    }
    
}
