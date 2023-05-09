using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using MVCpresentation.Models;

namespace MVCpresentation.Controllers
{
    public class ScheduleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private LogicLayer.VenueManager _venueManager = new LogicLayer.VenueManager();
        private LogicLayer.VenueUseManager _venueUseManager = new LogicLayer.VenueUseManager();
        private LogicLayer.AdCampaignManager _adCampaignManager = new LogicLayer.AdCampaignManager();

        // GET: Schedule
        public ActionResult Index()
        {
            try
            {
                return View(_venueUseManager.RetreiveVenueUses());
            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }

        // GET: Schedule/Create
        [Authorize(Roles = "Tour Planner")]
        public ActionResult Create()
        {
            List<VenueVM> venues = _venueManager.RetreiveVenues();
            ViewBag.Venues = venues;
            return View();
        }

        // POST: Schedule/Create        
        [HttpPost]
        [Authorize(Roles = "Tour Planner")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VenueUseVM venueUseVM)
        {
            if (ModelState.IsValid)
            {
                if(venueUseVM.StartDate >= venueUseVM.EndDate)
                {
                    ViewBag.EndDateError = "The end date can not be the same or before the start date.";
                }
                else
                {
                    try
                    {
                        _venueUseManager.AddVenueUse(venueUseVM);
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        return View("Error");
                    }
                    
                }
                
            }

            List<VenueVM> venues = _venueManager.RetreiveVenues();
            ViewBag.Venues = venues;
            return View(venueUseVM);
        }


        // GET: Schedule/UseCampaign/5
        [Authorize(Roles = "Ad Planner")]
        public ActionResult AssignAdCampaign(int? id)
        {
            VenueUseVM venueUse = null;
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                venueUse = _venueUseManager.RetreiveVenueUseByUseId(id.Value);
                List<AdCampaignVM> campaigns = _adCampaignManager.RetreiveAdCampaigns();
                ViewBag.AdCampaigns = campaigns;
            }
            catch (Exception)
            {
                return View("Error");
            }
            
            if (venueUse == null)
            {
                return HttpNotFound();
            }
            return View(venueUse);
        }

        // POST: Schedule/UseCampaign/5
        [HttpPost]
        [Authorize(Roles = "Ad Planner")]
        [ValidateAntiForgeryToken]
        public ActionResult AssignAdCampaign(VenueUseVM use)
        {
            _venueUseManager.UpdateVenueUseAdCampaign(use.VenueUseId, use.AdCampain.Value);
            return RedirectToAction("Index");
        }

        // GET: Schedule/RecordDay/5
        [Authorize(Roles = "Tour Planner")]
        public ActionResult RecordDay(int? id)
        {
            VenueUseVM venueUse = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                venueUse = _venueUseManager.RetreiveVenueUseByUseId(id.Value);
                List<AdCampaignVM> campaigns = _adCampaignManager.RetreiveAdCampaigns();
                ViewBag.AdCampaigns = campaigns;
            }
            catch (Exception)
            {
                return View("Error");
            }

            if (venueUse == null)
            {
                return HttpNotFound();
            }
            return View(new UseDay() { UseId = venueUse.VenueUseId });
        }

        // POST: Schedule/RecordDay/5
        [HttpPost]
        [Authorize(Roles = "Tour Planner")]
        [ValidateAntiForgeryToken]
        public ActionResult RecordDay(UseDay useDay)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _venueUseManager.AddUseDay(useDay);


                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("Error");
                }
            }
            else
            {                
                return View(); // returns server-side validation messages
            }
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
