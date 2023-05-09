using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;

namespace MVCpresentation.Controllers
{
    [Authorize(Roles = "Tour Planner")]
    public class VenueController : Controller
    {
        private LogicLayer.VenueManager _venueManager = new LogicLayer.VenueManager();

        // GET: Venue        
        public ActionResult Index()
        {
            List<VenueVM> venues = _venueManager.RetreiveVenues();
            return View(venues);
        }

        // POST: Venue
        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            decimal lowRevenue;
            if(formCollection["filter"] != null)
            {
                if(!decimal.TryParse(formCollection["filter"], out lowRevenue))
                {
                    lowRevenue = 0;
                }
            }
            else
            {
                lowRevenue = 0;
            }

            try
            {
                List<VenueVM> venues = _venueManager.RetreiveVenues().Where(v => v.AverageRevenue >= lowRevenue).ToList();
                return View(venues);
            } catch(Exception ex)
            {
                return View("Error");
            }
        }


        // GET: Venue/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                List<VenueVM> venues = _venueManager.RetreiveVenues().Where(v => v.VenueID == id).ToList();
                return View(venues[0]);
            } catch(Exception ex)
            {
                return View("Error");
            }
            
        }

        // GET: Venue/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Venue/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VenueVM venue)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _venueManager.AddVenue(venue);
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

        // GET: Venue/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Venue/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Venue/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Venue/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
