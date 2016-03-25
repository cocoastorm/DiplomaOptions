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

        [Authorize(Roles = "Admin")]
        // GET: Choices
        public ActionResult Index()
        {
            var choices = db.Choices.Include(c => c.FirstOption).Include(c => c.FourthOption).Include(c => c.SecondOption).Include(c => c.ThirdOption).Include(c => c.YearTerm);

            // YearTermSelects
            var yearterms = db.YearTerms.ToArray();
            var yearterms_default = db.YearTerms.Where(c => c.IsDefault == true).First();
            List<SelectListItem> yearterms_selects = new List<SelectListItem>();
            foreach(YearTerm term in yearterms)
            {
                if (!term.IsDefault)
                {
                    yearterms_selects.Add(new SelectListItem { Text = term.Year.ToString() + " - " + term.getTermString, Value = term.Year.ToString() + "-" + term.Term.ToString(), Selected = false});
                }
                else
                {
                    yearterms_selects.Add(new SelectListItem { Text = term.Year.ToString() + " - " + term.getTermString, Value = term.Year.ToString() + "-" + term.Term.ToString(), Selected = true });
                }
            }
            ViewBag.YearTermSelects = yearterms_selects;

            // TypeReports
            List<SelectListItem> typereports = new List<SelectListItem>()
            {
                new SelectListItem {Text = "Details Report", Value = "details" },
                new SelectListItem {Text = "Chart", Value = "chart" }
            };
            ViewBag.TypeReportSelects = typereports;

            return View(choices.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: Choices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Include(c => c.FirstOption).Include(c => c.FourthOption).Include(c => c.SecondOption).Include(c => c.ThirdOption).Include(c => c.YearTerm).SingleOrDefault(c => c.ChoiceId == id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        [Authorize(Roles = "Admin, Student")]
        // GET: Choices/Create
        public ActionResult Create()
        {
            // get only active options from the database
            var options = db.Options.Where(c => c.IsActive == true);

            var StudentId = User.Identity.Name;

            Session["StudentId"] = StudentId;

            ViewBag.FirstChoiceOptionId = new SelectList(options, "OptionId", "Title");
            ViewBag.FourthChoiceOptionId = new SelectList(options, "OptionId", "Title");
            ViewBag.SecondChoiceOptionId = new SelectList(options, "OptionId", "Title");
            ViewBag.ThirdChoiceOptionId = new SelectList(options, "OptionId", "Title");

            var defaultYear = db.YearTerms.Where(y => y.IsDefault == true);
            ViewBag.DefaultYear = defaultYear.FirstOrDefault().getTermString;
            ViewBag.YearTermId = new SelectList(defaultYear, "YearTermId", "YearTermId");
            return View();
        }

        [Authorize(Roles = "Admin, Student")]
        // POST: Choices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChoiceId,YearTermId,StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId,SelectionDate")] Choice choice)
        {
            if (ModelState.IsValid)
            {
                //prevent duplicate selection for a user

               var exists = db.Choices.Where(c => c.StudentId == choice.StudentId && c.YearTermId == choice.YearTermId);
                if (exists.Count() > 0)
                {
                    var StudentId = User.Identity.Name;

                    Session["StudentId"] = StudentId;
                    TempData["Success"] = "false";
                    // record already exists, return to create
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    db.Choices.Add(choice);
                    db.SaveChanges();
                    TempData["Success"] = "true";
                    return RedirectToAction("Index", "Home");
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

        [Authorize(Roles = "Admin")]
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

            var StudentId = User.Identity.Name;
            Session["StudentId"] = StudentId;

            var options = db.Options.Where(c => c.IsActive == true);
            ViewBag.FirstChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.ThirdChoiceOptionId);

            var defaultYear = db.YearTerms.Where(y => y.IsDefault == true);
            ViewBag.YearTermId = new SelectList(defaultYear, "YearTermId", "YearTermId", choice.YearTermId);
            ViewBag.DefaultYear = defaultYear.FirstOrDefault().getTermString;
            return View(choice);
        }

        [Authorize(Roles = "Admin")]
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
            ViewBag.DefaultYear = defaultYear.FirstOrDefault().getTermString;
            return View(choice);
        }

        [Authorize(Roles = "Admin")]
        // GET: Choices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Include(c => c.FirstOption).Include(c => c.FourthOption).Include(c => c.SecondOption).Include(c => c.ThirdOption).Include(c => c.YearTerm).SingleOrDefault(c => c.ChoiceId == id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        [Authorize(Roles = "Admin")]
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
