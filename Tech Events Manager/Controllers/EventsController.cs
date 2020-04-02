using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tech_Events_Manager.Models;
using System.IO;
using System.Xml.XPath;
using System.Xml.Linq;
using Amazon.S3.Model;
using Amazon.S3;
using Amazon;
using Amazon.Runtime;
using Amazon.S3.Transfer;
using System.Configuration;

namespace Tech_Events_Manager.Controllers
{
    public class EventsController : Controller
    {

        //private const string filePath = null; 
        private static readonly string bucketName = ConfigurationManager.AppSettings["BucketName"];
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.EUWest2;
        private static readonly string accesskey = ConfigurationManager.AppSettings["AWSAccessKey"];
        private static readonly string secretkey = ConfigurationManager.AppSettings["AWSSecretKey"];

        private ApplicationDbContext db = new ApplicationDbContext();
       

        // GET: Events
        public ActionResult Index()
        {
            // return View(db.Event.ToList()); //
           
            return View(db.Event.OrderBy(a => a.Date).Take(10).ToList());
          
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Event imageDB)

        {
            // Upload event image path to database//
            string filename = Path.GetFileNameWithoutExtension(imageDB.ImageFile.FileName);
            string extension = Path.GetExtension(imageDB.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            string keyName = filename;


            string aws_s2 = "https://tech-events-uk.s3.eu-west-2.amazonaws.com/".ToString();
            imageDB.ImagePath = aws_s2 + filename;

           

            //Upload image to AWS S3 ------------//
            HttpPostedFileBase file = Request.Files[0];

               
                var s3Client = new AmazonS3Client(accesskey, secretkey, bucketRegion);
                var fileTransferUtility = new TransferUtility(s3Client);

                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = bucketName,
                    InputStream = file.InputStream,
                    StorageClass = S3StorageClass.StandardInfrequentAccess,
                    PartSize = 6291456, // 6 MB.  
                    Key = keyName,
                    CannedACL = S3CannedACL.PublicRead
                };
                fileTransferUtilityRequest.Metadata.Add("param1", "Value1");
                fileTransferUtilityRequest.Metadata.Add("param2", "Value2");
                fileTransferUtility.Upload(fileTransferUtilityRequest);
                fileTransferUtility.Dispose();


                //Coverts postcode to latitude and longitude and upload to database//
                string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={1}&address={0}&sensor=false",
               Uri.EscapeDataString(imageDB.Postcode), "AIzaSyBj8k95-RJyz0HNan_RcgS_-suLQVb7NzA");

            WebRequest request = WebRequest.Create(requestUri);
            WebResponse response = request.GetResponse();
            XDocument xdoc = XDocument.Load(response.GetResponseStream());

            XElement result = xdoc.Element("GeocodeResponse").Element("result");
            XElement locationElement = result.Element("geometry").Element("location");
            XElement lat = locationElement.Element("lat");
            XElement lng = locationElement.Element("lng");

            imageDB.Latitude = Convert.ToDouble(lat.Value);
            imageDB.Longitude = Convert.ToDouble(lng.Value);

            { 
                db.Event.Add(imageDB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Organiser,City,Date,ImageTitle,ImagePath,Website,Postcode,Latitude,Longitude")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Event.Find(id);
            db.Event.Remove(@event);
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

        // GET: Events/AdminMenu
        public ActionResult AdminMenu()
        {
            return View();
        }
    }
}
