using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tech_Events_Manager.Models;
using Tech_Events_Manager.ViewModel;


namespace Tech_Events_Manager.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Events
       /* public ActionResult Index()
        {
            
            return View(db.Event.OrderBy(a => a.Date).ToList());
           
        }*/
      
       public ActionResult Index(UserLocation location, string postcode)
        {
            location.UserPostcode = postcode;
           

            var viewModel = new CustomerViewModel
            {
                UserPostcode = postcode,
                Event = db.Event.OrderBy(a => a.Date).ToList()

            };

           
           System.Diagnostics.Debug.WriteLine(postcode);
            return View(viewModel);
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