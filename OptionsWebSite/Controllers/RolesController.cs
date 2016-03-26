using Microsoft.AspNet.Identity.EntityFramework;
using OptionsWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OptionsWebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Roles
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // create
        public ActionResult Create()
        {
            return View();
        }

        // create post
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                db.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // delete & post
        public ActionResult Delete(string RoleName)
        {
            if (RoleName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole role = db.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (role == null)
            {
                return HttpNotFound();
            }
            db.Roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}