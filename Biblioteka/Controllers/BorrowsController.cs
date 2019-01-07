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
    public class BorrowsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Borrows
        public ActionResult Index()
        {
            var borrows = db.Borrows.Include(b => b.BookCopy).Include(b => b.Employee).Include(b => b.Reader);
            return View(borrows.ToList());
        }

        // GET: Borrows/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrow borrow = db.Borrows.Find(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            return View(borrow);
        }

        // GET: Borrows/Create
        public ActionResult Create()
        {
            ViewBag.BookCopyID = new SelectList(db.BookCopies, "BookCopyID", "BookCopyID");
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Pesel");
            ViewBag.ReaderID = new SelectList(db.Readers, "ReaderID", "Pesel");
            return View();
        }

        // POST: Borrows/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BorrowId,BorrowDate,ReturnDate,BookCopyID,ReaderID,EmployeeID")] Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                db.Borrows.Add(borrow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookCopyID = new SelectList(db.BookCopies, "BookCopyID", "BookCopyID", borrow.BookCopyID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Pesel", borrow.EmployeeID);
            ViewBag.ReaderID = new SelectList(db.Readers, "ReaderID", "Pesel", borrow.ReaderID);
            return View(borrow);
        }

        // GET: Borrows/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrow borrow = db.Borrows.Find(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookCopyID = new SelectList(db.BookCopies, "BookCopyID", "BookCopyID", borrow.BookCopyID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Pesel", borrow.EmployeeID);
            ViewBag.ReaderID = new SelectList(db.Readers, "ReaderID", "Pesel", borrow.ReaderID);
            return View(borrow);
        }

        // POST: Borrows/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BorrowId,BorrowDate,ReturnDate,BookCopyID,ReaderID,EmployeeID")] Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookCopyID = new SelectList(db.BookCopies, "BookCopyID", "BookCopyID", borrow.BookCopyID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Pesel", borrow.EmployeeID);
            ViewBag.ReaderID = new SelectList(db.Readers, "ReaderID", "Pesel", borrow.ReaderID);
            return View(borrow);
        }

        // GET: Borrows/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrow borrow = db.Borrows.Find(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            return View(borrow);
        }

        // POST: Borrows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Borrow borrow = db.Borrows.Find(id);
            db.Borrows.Remove(borrow);
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
