using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LigaPrvaka.ModelEF;

namespace LigaPrvaka.Controllers
{
    public class UtakmicesController : Controller
    {
        private LigaprvakaEntities db = new LigaprvakaEntities();

        // GET: Utakmices
        public ActionResult Index()
        {
            var utakmice = db.Utakmice.Include(u => u.Timovi).Include(u => u.Timovi1);
            return View(utakmice.ToList());
        }

        // GET: Utakmices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utakmouse utakmouse = db.Utakmice.Find(id);
            if (utakmouse == null)
            {
                return HttpNotFound();
            }
            return View(utakmouse);
        }

        // GET: Utakmices/Create
        public ActionResult Create()
        {
            ViewBag.Domaci = new SelectList(db.Timovis, "PKTimID", "Naziv");
            ViewBag.Gosti = new SelectList(db.Timovis, "PKTimID", "Naziv");
            return View();
        }

        // POST: Utakmices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PKUtakmicaID,Domaci,Gosti,Br_golova_d,Br_golova_g")] Utakmouse utakmouse)
        {
            if (ModelState.IsValid)
            {
                db.Utakmice.Add(utakmouse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Domaci = new SelectList(db.Timovis, "PKTimID", "Naziv", utakmouse.Domaci);
            ViewBag.Gosti = new SelectList(db.Timovis, "PKTimID", "Naziv", utakmouse.Gosti);
            return View(utakmouse);
        }

        // GET: Utakmices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utakmouse utakmouse = db.Utakmice.Find(id);
            if (utakmouse == null)
            {
                return HttpNotFound();
            }
            ViewBag.Domaci = new SelectList(db.Timovis, "PKTimID", "Naziv", utakmouse.Domaci);
            ViewBag.Gosti = new SelectList(db.Timovis, "PKTimID", "Naziv", utakmouse.Gosti);
            return View(utakmouse);
        }

        // POST: Utakmices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKUtakmicaID,Domaci,Gosti,Br_golova_d,Br_golova_g")] Utakmouse utakmouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utakmouse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Domaci = new SelectList(db.Timovis, "PKTimID", "Naziv", utakmouse.Domaci);
            ViewBag.Gosti = new SelectList(db.Timovis, "PKTimID", "Naziv", utakmouse.Gosti);
            return View(utakmouse);
        }

        // GET: Utakmices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utakmouse utakmouse = db.Utakmice.Find(id);
            if (utakmouse == null)
            {
                return HttpNotFound();
            }
            return View(utakmouse);
        }

        // POST: Utakmices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utakmouse utakmouse = db.Utakmice.Find(id);
            db.Utakmice.Remove(utakmouse);
            db.SaveChanges();
            return RedirectToAction("Index");
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
