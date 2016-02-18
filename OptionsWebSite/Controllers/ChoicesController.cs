using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomaDataModel.Models;

namespace OptionsWebSite.Controllers
{
    public class ChoicesController : Controller
    {
        private DiplomaContext db = new DiplomaContext();

        // GET: Choices
        public ActionResult Index()
        {
            var choices = db.Choices.Include(c => c.FirstOption).Include(c => c.FourthOption).Include(c => c.SecondOption).Include(c => c.ThirdOption).Include(c => c.YearTerm);
            return View(choices.ToList());
        }

        // GET: Choices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // GET: Choices/Create
        public ActionResult Create()
        {
            // get only active options from the database
            var options = db.Options.Where(c => c.IsActive == true);

            ViewBag.FirstChoiceOptionId = new SelectList(options, "OptionId", "Title");
            ViewBag.FourthChoiceOptionId = new SelectList(options, "OptionId", "Title");
            ViewBag.SecondChoiceOptionId = new SelectList(options, "OptionId", "Title");
            ViewBag.ThirdChoiceOptionId = new SelectList(options, "OptionId", "Title");

            var defaultYear = db.YearTerms.Where(y => y.IsDefault == true);
            ViewBag.YearTermId = new SelectList(defaultYear, "YearTermId", "YearTermId");
            return View();
        }

        // POST: Choices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChoiceId,YearTermId,StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId,SelectionDate")] Choice choice)
        {
            if (ModelState.IsValid)
            {
                // prevent duplicate selection for a user
                var exists = db.Choices.Where(c => c.StudentId == choice.StudentId && c.YearTermId == choice.YearTermId);
                if(exists.Count() > 0)
                {
                    // record already exists, return to create
                    return RedirectToAction("Create");
                } else
                {
                    db.Choices.Add(choice);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            var options = db.Options.Where(c => c.IsActive == true);
            ViewBag.FirstChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.ThirdChoiceOptionId);

            var defaultYear = db.YearTerms.Where(y => y.IsDefault == true);
            ViewBag.YearTermId = new SelectList(defaultYear, "YearTermId", "YearTermId", choice.YearTermId);
            return View(choice);
        }

        // GET: Choices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            var options = db.Options.Where(c => c.IsActive == true);
            ViewBag.FirstChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.ThirdChoiceOptionId);

            var defaultYear = db.YearTerms.Where(y => y.IsDefault == true);
            ViewBag.YearTermId = new SelectList(defaultYear, "YearTermId", "YearTermId", choice.YearTermId);
            return View(choice);
        }

        // POST: Choices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChoiceId,YearTermId,StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId,SelectionDate")] Choice choice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(choice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var options = db.Options.Where(c => c.IsActive == true);
            ViewBag.FirstChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.ThirdChoiceOptionId);

            var defaultYear = db.YearTerms.Where(y => y.IsDefault == true);
            ViewBag.YearTermId = new SelectList(defaultYear, "YearTermId", "YearTermId", choice.YearTermId);
            return View(choice);
        }

        // GET: Choices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // POST: Choices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Choice choice = db.Choices.Find(id);
            db.Choices.Remove(choice);
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
