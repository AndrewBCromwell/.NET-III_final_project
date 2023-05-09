using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MVCpresentation.Models;

namespace MVCpresentation.Controllers
{
    public class AdminController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager userManager;

        // GET: Admin
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            //return View(db.ApplicationUsers.ToList());
            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return View(userManager.Users.OrderBy(n => n.FamilyName).ToList());
        }

        // GET: Admin/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser applicationUser = userManager.FindById(id);

            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            // get a list of roles the user has and put them into a viewbag as roles
            // along with a list of roles the user doesn't have as no Roles
            var usrMgr = new LogicLayer.EmployeeManager();
            var allRoles = usrMgr.RetreiveRoles();

            var roles = userManager.GetRoles(id);
            var noRoles = allRoles.Except(roles);

            ViewBag.Roles = roles;
            ViewBag.NoRoles = noRoles;

            return View(applicationUser);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult RemoveRole(string id, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            if (role == "Administrator")
            {
                var adminUsers = userManager.Users.ToList()
                    .Where(u => userManager.IsInRole(u.Id, "Administrator"))
                    .ToList().Count();
                if (adminUsers < 2)
                {
                    ViewBag.Error = "Cannot remove last administrator.";
                    return RedirectToAction("Details", "Admin", new { id = user.Id });
                }                
            }
            userManager.RemoveFromRole(id, role);

            if(user.employee_id != null)
            {
                try
                {
                    var empMgr = new LogicLayer.EmployeeManager();
                    empMgr.RemoveEmployeeRole((int)user.employee_id, role);
                }
                catch (Exception)
                {
                    // nothing to do
                }
            }
            return RedirectToAction("Details", "Admin", new { id = user.Id });
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AddRole(string id, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            userManager.AddToRole(id, role);

            if (user.employee_id != null)
            {
                try
                {
                    var empMgr = new LogicLayer.EmployeeManager();
                    empMgr.AddEmployeeRole((int)user.employee_id, role);
                }
                catch (Exception)
                {
                    // nothing to do
                }
            }
            return RedirectToAction("Details", "Admin", new { id = user.Id });
        }
    }
}
