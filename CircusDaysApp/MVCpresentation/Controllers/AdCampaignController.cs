using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;

namespace MVCpresentation.Controllers
{
    [Authorize(Roles = "Ad Planner")]
    public class AdCampaignController : Controller
    {
        private AdCampaignManager _adCampaignManager = new AdCampaignManager();
        private AdCompanyManager _adCompanyManager = new AdCompanyManager();

        // GET: AdCampaign
        public ActionResult Index()
        {
            List<AdCampaignVM> campaigns = _adCampaignManager.RetreiveAdCampaigns();
            return View(campaigns);
        }

        // GET: AdCampaign/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                    List<AdCampaignVM> campaign = _adCampaignManager.RetreiveAdCampaigns().Where(c => c.AdCampaignId == id).ToList();
                return View(campaign[0]);
            }
            catch ( Exception ex)
            {
                return View("Error");
            }
            
        }

        // GET: AdCampaign/Create
        public ActionResult Create()
        {
            ViewBag.AdCompanies = _adCompanyManager.RetreiveAdCompanies();
            return View();
        }

        // POST: AdCampaign/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdCampaignVM adCampaign)
        {
            if (ModelState.IsValid)
            {
                adCampaign.TotalCost = 0;
                adCampaign.AdItems = new List<AdItem>();
                Session["adCampaign"] = adCampaign;
                return RedirectToAction("AddAdItem");
            }

            ViewBag.AdCompanies = _adCompanyManager.RetreiveAdCompanies();
            return View(adCampaign);
        }

        // GET: AdCampaign/AddAdCampaign/5
        public ActionResult AddAdItem()
        {
            if(Session["adCampaign"] == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                AdCampaignVM adCampaign = (AdCampaignVM)Session["adCampaign"];
                List<string> adTypes = _adCampaignManager.RetreiveAdTypes();
                List<string> acts = _adCampaignManager.RetreiveActs();
                foreach (AdItem item in adCampaign.AdItems)
                {
                    adTypes.Remove(item.AdType);
                }
                if(adTypes.Count == 0)
                {
                    return RedirectToAction("UsedAllAdTypes");
                }
                ViewBag.adTypes = adTypes;
                acts.Add("None");
                ViewBag.acts = acts;
                return View();
            }
        }

        // POST: AdCampaign/AddAdItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAdItem(AdItem item)
        {
            if (ModelState.IsValid)
            {
                AdCampaignVM adCampaign = (AdCampaignVM)Session["adCampaign"];
                if(item.FocusAct == "None")
                {
                    item.FocusAct = null;
                }
                adCampaign.AdItems.Add(item);
                adCampaign.TotalCost += item.Cost;
                Session["adCampaign"] = adCampaign;
                if (item.IsLastItemInCampaign)
                {
                    try
                    {
                        _adCampaignManager.AddAdCampaign(adCampaign);
                        Session["adCampaign"] = null;
                        return RedirectToAction("index");
                    }
                    catch (Exception)
                    {
                        return View("Error");
                    }
                }
                else
                {
                    return RedirectToAction("AddAdItem");
                }
                
            }

            ViewBag.adTypes = _adCampaignManager.RetreiveAdTypes();
            List<string> acts = _adCampaignManager.RetreiveActs();
            acts.Add("None");
            ViewBag.acts = acts;
            ViewBag.AdCompanies = _adCompanyManager.RetreiveAdCompanies();
            return View(item);
        }

        public ActionResult UsedAllAdTypes()
        {
            AdCampaignVM adCampaign = (AdCampaignVM)Session["adCampaign"];
            try
            {
                _adCampaignManager.AddAdCampaign(adCampaign);
                Session["adCampaign"] = null;
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

    }
}
