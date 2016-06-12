using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEP.Models;

namespace SEP.Controllers
{
    public class EmpController : Controller
    {
        // GET: Emp
        public ActionResult Index()
        {
            UserLayer blayer = new UserLayer();



             List<editUser> users = blayer.Users.ToList();



            return View(users);
        }



        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create_Get()
        {

            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post()
        {
            User user = new User();
            TryUpdateModel(user);

            if (ModelState.IsValid)
            {
                UserLayer usr = new UserLayer();
                usr.AddUser(user);
                return RedirectToAction("Index");
            }


            return View();
        }
        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    UserLayer blayer = new UserLayer();
        //   // User user = blayer.Users.Single(us => us.UserId == id);
        //    //return View(user);

        //}
       //// [HttpPost]
       // public ActionResult Edit(User user)
       // {
       //     if (ModelState.IsValid)
       //     {
       //         UserLayer blayer = new UserLayer();
       //         blayer.SaveUser(user);

       //         return RedirectToAction("Index");
       //     }
       //     return View(user);
                        

       // }

    }
}