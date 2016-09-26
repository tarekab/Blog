using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.Web.Entity;

namespace Blog.Web.Controllers
{
    public class AdministratorsController : Controller
    {
        private BlogModel db = new BlogModel();
      
        public ActionResult Login()
        {
            ModelState.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult Login(Administrators administrators)
        {
            var usr = db.Administrators.
                Where(u => u.Name == administrators.Name  && 
                u.Password == administrators.Password).FirstOrDefault();
            if(usr!= null)
            {
                Session["UserId"] = usr.ID.ToString();
                Session["Username"] = usr.Name.ToString();              
                return RedirectToAction("Index","Posts");

            }
            else
            {
                ModelState.AddModelError("","Username or Password is invalid .");
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Registration()
        {
            return View();
        }

        // GET: Administrators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrators administrators = db.Administrators.Find(id);
            if (administrators == null)
            {
                return HttpNotFound();
            }
            return View(administrators);
        }

        // GET: Administrators/Create
        

        // POST: Administrators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Include = "ID,Name,Password,Email,ConfirmPassword")] Administrators administrators)
        {
            if (ModelState.IsValid)
            {
                
                db.Administrators.Add(administrators);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(administrators);
        }

        // GET: Administrators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrators administrators = db.Administrators.Find(id);
            if (administrators == null)
            {
                return HttpNotFound();
            }
            return View(administrators);
        }

        // POST: Administrators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Password")] Administrators administrators)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administrators).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(administrators);
        }

        // GET: Administrators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrators administrators = db.Administrators.Find(id);
            if (administrators == null)
            {
                return HttpNotFound();
            }
            return View(administrators);
        }

        // POST: Administrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Administrators administrators = db.Administrators.Find(id);
            db.Administrators.Remove(administrators);
            db.SaveChanges();
            return RedirectToAction("Login");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
