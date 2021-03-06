﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tech_Events_Manager.Models;
using Tech_Events_Manager.ViewModel;
using System.IO;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Net;

namespace Tech_Events_Manager.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        

        // GET: /Home/Index
        public ActionResult Index()
        {
            var model = new CustomerViewModel
            {
              
               Event = db.Event.OrderBy(a => a.Date).ToList()
              
            };
            
           
            return View(model);
        }
        

        // POST: /Home/Index
        [HttpPost]
        public PartialViewResult Index(UserLocation location)
        {
            if (ModelState.IsValid)
            {
           
            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={1}&address={0}&sensor=false",
              Uri.EscapeDataString(location.UserPostcode), "ENTER_KEY_HERE");

            WebRequest request = WebRequest.Create(requestUri);
            WebResponse response = request.GetResponse();
            XDocument xdoc = XDocument.Load(response.GetResponseStream());

            XElement result = xdoc.Element("GeocodeResponse").Element("result");
            XElement locationElement = result.Element("geometry").Element("location");
            XElement lat = locationElement.Element("lat");
            XElement lng = locationElement.Element("lng");

            location.UserLat = Convert.ToDouble(lat.Value);
            location.UserLng = Convert.ToDouble(lng.Value);
            }

            var viewModel = new CustomerViewModel
            {
                UserLat = location.UserLat,
                UserLng = location.UserLng,
                Distance = location.Distance,
                Event = db.Event.OrderBy(a => a.Date).Take(10).ToList()

            };

            return PartialView("_GoogleMap", viewModel);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}
