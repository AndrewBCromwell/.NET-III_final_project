using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using DataObjects;

namespace MVCpresentation.Controllers
{
    [Authorize(Roles = "Ad Planner")]
    public class AdCompaniesController : Controller
    {
        private AdCompanyManager _adCompanyManager = new AdCompanyManager();

        // GET: AdCompanies
        public ActionResult Index()
        {
            List<AdCompanyVM> companies = _adCompanyManager.RetreiveAdCompanies();
            return View(companies);
        }

        // GET: AdCompanies/Create/
        public ActionResult Create()
        {
            return View();
        }

        // POST: Venue/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdCompanyVM adCompany)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _adCompanyManager.AddAdCompany(adCompany);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View("Error");
                }
            }
            else
            {
                return View();
            }
        }
    }
}