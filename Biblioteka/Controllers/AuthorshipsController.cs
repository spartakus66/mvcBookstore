﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteka.Models;

namespace Biblioteka.Controllers
{
    public class AuthorshipsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Authorships
        public ActionResult Index()
        {
            var authorships = db.Authorships.Include(a => a.Author).Include(a => a.Book);
            return View(authorships.ToList());
        }

        // GET: Authorships/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authorship authorship = db.Authorships.Find(id);
            if (authorship == null)
            {
                return HttpNotFound();
            }
            return View(authorship);
        }

        // GET: Authorships/Create
        public ActionResult Create()
        {
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName");
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title");
            return View();
        }

        // POST: Authorships/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AuthorshipID,AuthorID,BookID")] Authorship authorship)
        {
            if (ModelState.IsValid)
            {
                db.Authorships.Add(authorship);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", authorship.AuthorID);
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", authorship.BookID);
            return View(authorship);
        }

        // GET: Authorships/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authorship authorship = db.Authorships.Find(id);
            if (authorship == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", authorship.AuthorID);
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", authorship.BookID);
            return View(authorship);
        }

        // POST: Authorships/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AuthorshipID,AuthorID,BookID")] Authorship authorship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(authorship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", authorship.AuthorID);
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", authorship.BookID);
            return View(authorship);
        }

        // GET: Authorships/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authorship authorship = db.Authorships.Find(id);
            if (authorship == null)
            {
                return HttpNotFound();
            }
            return View(authorship);
        }

        // POST: Authorships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Authorship authorship = db.Authorships.Find(id);
            db.Authorships.Remove(authorship);
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
