using SEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Net.Mime;

namespace SEP.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminIndex()
        {
            return View();
        }

        public ActionResult ViewUsers(int ?page)
        {
            UserLayer blayer = new UserLayer();
           // List<editUser> users = blayer.Users.ToList().ToPagedList(page??1,3);
            return View(blayer.Users.ToList().ToPagedList(page ?? 1,10));
           
        }

        [HttpPost]
        public ActionResult ViewUsers(int id)
        {
            UserLayer blayer = new UserLayer();
            if (blayer.DeleteCustomer(id))
            {
                TempData["Success"] = "Deleted Successfully";
                return RedirectToAction("ViewUsers");
            }

            TempData["Exception"] = "Cannot Delete";
            return RedirectToAction("ViewUsers");


        }


        public ActionResult ViewRestaurants(int? page)
        {
            RestaurantLayer rlayer = new RestaurantLayer();
            return View(rlayer.Restaurants.ToList().ToPagedList(page ?? 1,4));
        }

        [HttpPost]
        public ActionResult ViewRestaurants(int id)
        {
            RestaurantLayer rlayer = new RestaurantLayer();
            if (rlayer.DeleteResaturants(id))
            {
                TempData["Success"] = "Deleted Successfully";
                return RedirectToAction("ViewRestaurants");
            }

            TempData["Exception"] = "Cannot Delete";
            return RedirectToAction("ViewRestaurants");


        }
        public ActionResult Backup()
        {
            BackupDB db = new BackupDB();

            if (db.BackupDatabase())
            {
                ViewData["Success"] = "Backup is completed Successfully";
                Download("09062016_225024.Bak");
                return View("AdminIndex");
            }

            ViewData["Exception"] = "Backup Failed";
            return View("AdminIndex");
            
        }
        public FileResult Download(string ImageName)
        {
            return File("‪C:\\Backups\\" + ImageName, System.Net.Mime.MediaTypeNames.Application.Octet);
        }

        public ActionResult ViewLoginhistory(int ?page)
        {
            UserLayer ulayer = new UserLayer();
            return View(ulayer.Loginhistory.ToList().ToPagedList(page ?? 1, 15));
        }
      
    }
}