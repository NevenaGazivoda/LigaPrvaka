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
    public class IgracisController : Controller
    {
        private LigaprvakaEntities db = new LigaprvakaEntities();

        // GET: Igracis
        public ActionResult Index()
        {
            var igracis = db.Igracis.Include(i => i.Timovi);
            return View(igracis.ToList());
        }

        public ActionResult IgraciTima(int TimID)
        {
            var igraci = db.Igracis.Where(k => k.FKTimID == TimID);
            return PartialView(igraci.ToList());
        }
        // GET: Igracis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Igraci igraci = db.Igracis.Find(id);
            if (igraci == null)
            {
                return HttpNotFound();
            }
            return View(igraci);
        }

        // GET: Igracis/Create
        public ActionResult Create()
        {
            ViewBag.FKTimID = new SelectList(db.Timovis, "PKTimID", "Naziv");
            return View();
        }

        // POST: Igracis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PKIgracID,Ime,FKTimID")] Igraci igraci)
        {
            if (ModelState.IsValid)
            {
                db.Igracis.Add(igraci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKTimID = new SelectList(db.Timovis, "PKTimID", "Naziv", igraci.FKTimID);
            return View(igraci);
        }

        // GET: Igracis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Igraci igraci = db.Igracis.Find(id);
            if (igraci == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKTimID = new SelectList(db.Timovis, "PKTimID", "Naziv", igraci.FKTimID);
            return View(igraci);
        }

        // POST: Igracis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKIgracID,Ime,FKTimID")] Igraci igraci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(igraci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKTimID = new SelectList(db.Timovis, "PKTimID", "Naziv", igraci.FKTimID);
            return View(igraci);
        }

        // GET: Igracis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Igraci igraci = db.Igracis.Find(id);
            if (igraci == null)
            {
                return HttpNotFound();
            }
            return View(igraci);
        }

        // POST: Igracis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Igraci igraci = db.Igracis.Find(id);
            db.Igracis.Remove(igraci);
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
