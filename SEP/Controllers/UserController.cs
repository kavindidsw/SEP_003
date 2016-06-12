using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEP.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.IO;
using System.Globalization;
using System.Dynamic;

namespace SEP.Controllers
{
    public class UserController : Controller
    {


        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

       

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Signup(User user)
        {
            if (ModelState.IsValid)
            {
                UserLayer userBusinessLayer = new UserLayer();
                userBusinessLayer.AddUser(user);
                var message = await EmailTemplate("WelcomeEmail");
                message = message.Replace("@ViewBag.Name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase

                    (user.Fname));


                await MessageServices.SendEmailAsync(user.Email, "Welcome", message);
                return RedirectToAction("Index","Home");



                ModelState.Clear();


            }
            return View();

            
        }

        public ActionResult SignIn()
        {
            userlogin user = new userlogin();
            user = Checkcookie();


            return View();
        }

        public userlogin Checkcookie()
        {
            userlogin usrl = null;
            string username = string.Empty, password = string.Empty;
            if (Request.Cookies["Username"] != null)
                username = Request.Cookies["Username"].Value;
            if (Request.Cookies["Password"] != null)
                password = Request.Cookies["Password"].Value;
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
                usrl = new userlogin { Username = username, Password = password };
            return usrl; 


        }

        [HttpPost]
        public ActionResult SignIn(userlogin user)
        {
            
      
                if (user.IsValid(user.Username, user.Password)) {
                Session["UserId"] = user.UserId.ToString();
                Session["Username"] = user.Username.ToString();

                if (user.Rememberme) {

                    HttpCookie cookieusername = new HttpCookie("Username");
                    cookieusername.Expires = DateTime.Now.AddSeconds(3600);
                    cookieusername.Value = user.Username;
                    Response.Cookies.Add(cookieusername);
                    HttpCookie cookiepassword = new HttpCookie("Password");
                    cookiepassword.Expires = DateTime.Now.AddSeconds(3600);
                    cookiepassword.Value = user.Password;
                    Response.Cookies.Add(cookiepassword);


                }


                if (user.Username == "admin"|| user.Username == "ADMIN")
                {
                    return RedirectToAction("AdminIndex", "Admin");
                }


                UserLayer ulayer = new UserLayer();
                ulayer.saveLoginhistory(user);
                return RedirectToAction("Index", "Home");


                }
                else
                {
                ViewData["Name"] = "Credentials does not match our records";
                return View();
                }
            

           
            
            /*
            if (user.IsValid(user.Username, user.Password))
            {
                //  FormsAuthentication.SetAuthCookie(user.Email, model.RememberMe);
               // ViewData["IDV"] = user.UserId;
                
                
                Session["UserId"] = user.UserId.ToString();
                Session["Username"] = user.Username.ToString();

                return RedirectToAction("Index", "Home");

            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");

            }

            ViewData["Name"] = "Credentials does not match our records";

            return View();
            */
        }

        public ActionResult PasswordRecovery()
        {

            return View();

        }
        [HttpPost]
        public async Task<ActionResult> PasswordRecovery(editUser user)
        {
           
            UserLayer ulayer = new UserLayer();
            Random r = new Random(1001);
            int code = new Random().Next(1001, 9999);
            try
            {
               var result = ulayer.Users.Single(u => u.Email == user.Email);


                if (result != null)
                {
                    ulayer.Updatecode(code, user.Email);

                    var message = await EmailTemplate("RecoveryEmail");
                    message = message.Replace("@ViewBag.Code", CultureInfo.CurrentCulture.TextInfo.ToTitleCase

                        (code.ToString()));


                    await MessageServices.SendEmailAsync(user.Email, "Password Recovery", message);

                    TempData["Email"] = "Email has been Sent successfully to " + user.Email + ". Please enter the recovery code. ";
                    return View("Recoverychange");


                }
            }
            catch(Exception e)
            {
                ViewData["Exception"] = "This account does not exist. Please Sign Up";
            }
            

            return View();

        }
        public ActionResult Recoverychange()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Recoverychange(RecoveryPasswordchange user)
        {
            UserLayer ulayer = new UserLayer();
            try
            {
                var result = ulayer.recoverypasswordchange.Single(u => u.Code == user.Code);
                if (result != null)
                {
                    if (ulayer.RecoveryUserPassword(user))
                    {
                        ViewData["Success"] = "Password changed Successfully";
                        return View("Recoverychange");

                    }
                    else
                    {
                        ViewData["Exception"] = "Password Not saved" + user.Code;
                        return View("Recoverychange");
                    }



                }
            }
            catch (Exception e)
            {
                ViewData["Exception"] = "Code doesnot matched "+e.Message;
            }


                return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }



        public ActionResult Edituserinfo(int id)
        {

            UserLayer blayer = new UserLayer();
           
            editUser user = blayer.Users.Single(us => us.UserId == id);


            return View(user); 
         

        }


        [HttpPost]
        public ActionResult Edituserinfo(editUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserLayer blayer = new UserLayer();
                    TryUpdateModel(user);
                    blayer.SaveUser(user);
                    TempData["Sucess"] = "Profile Successfully updated";

                    return RedirectToAction("Edituserinfo","User",user);
                    
                    //return View(user);
                }
            }

            catch(Exception ex)
            {
                ViewData["Exception"] = "Error :" + ex.Message;

            }
            ViewData["Exception"] = "Fill All required Fields";
            return View(user);


        }

        
        public ActionResult editpassword(int id){

            UserLayer blayer = new UserLayer();
            clsUserPasswordedit user = blayer.Usersl.Single(u => u.UserId == id);
            

            return View(user);
        
        }
        [HttpPost]
        public ActionResult editpassword( clsUserPasswordedit user)
        {
            try
            {
                UserLayer ul = new UserLayer();


                var usr = ul.Usersl.Where(u => u.OldPassword == user.OldPassword).Where(i=> i.UserId==user.UserId) ;
                if (usr != null)
                {
                    ul.SaveUserPassword(user);
                    return RedirectToAction("Index", "Home");
                }




               
            }
            catch (Exception ex)
            {
                ViewData["Exception"] = "Password error :"+ex.Message;

            }
            return View(user);

        }



        public ActionResult SendFeedbackEmail()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SendFeedbackEmail(clsSendFeedback model)
        {
         


            if (model.Rate!=null && model.Feedbacktype!=null && model.feedbacktext!=null)
            {

                var message = await EmailTemplate("feedbackemail");
                message = message.Replace("@ViewBag.Name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase

                    (model.Feedbacktype));
                message = message.Replace("@ViewBag.Description", CultureInfo.CurrentCulture.TextInfo.ToTitleCase

                     (model.feedbacktext));
                message = message.Replace("@ViewBag.rate", CultureInfo.CurrentCulture.TextInfo.ToTitleCase

                    (model.Rate));

                await MessageServices.SendEmailAsync("newhasakelum@gmail.com", "Welcome", message);
                ViewData["Suceess"] = "Feedback sent";
                return View();
            }
            ViewData["Exception"] = "All Fields Required";
            return View();
        }


        [HttpGet]
        public ActionResult Sent()
        {
            return View();
        }

        public static async Task <string> EmailTemplate(string Template)
        {
            var templatePath = HostingEnvironment.MapPath("~/Content/templates/")+Template+".cshtml";
            StreamReader objstream = new StreamReader(templatePath);
            var body = await objstream.ReadToEndAsync();
            objstream.Close();
            return body;
        }
        public ActionResult saveimage(int id)
        {
            UserImage user=new UserImage();
            UserLayer blayer = new UserLayer();
            try
            {
                user = blayer.images.Single(us => us.UserId == id);
                return View(user);

            }
            catch (Exception ex) {

            }

            UserImage u1 = new UserImage();
            return View(u1);

            




        }
        [HttpPost]
        public ActionResult saveimage(UserImage user)
        {
            // Apply Validation Here



            if (user.File.ContentLength > (2 * 1024 * 1024))
            {
                //ModelState.AddModelError("CustomError", "File size must be less than 2 MB");
                ViewData["Exception"] = "File size must be less than 2 MB";
                return View(user);
            }
            if (!(user.File.ContentType == "image/jpeg" || user.File.ContentType == "image/gif"))
            {
                //ModelState.AddModelError("CustomError", "File type allowed : jpeg and gif");
                ViewData["Exception"] = "File type allowed : jpeg and gif";
                return View(user);
            }




            byte[] data = new byte[user.File.ContentLength];
            user.File.InputStream.Read(data, 0, user.File.ContentLength);

            user.ProfPic = data;


            UserLayer layer = new UserLayer();
            if (layer.Saveimage(user))
            {
                @ViewData["Sucess"] = "Successfully Updated";


            }
            return View(user);

        }

        public ActionResult SendAskQuestionEmail()
        {

            return View();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SendAskQuestionEmail(clsAskquestion model)
        {
            var message = await EmailTemplate("faqemail");
            message = message.Replace("@ViewBag.Email", CultureInfo.CurrentCulture.TextInfo.ToTitleCase

                (model.emailaddress));
            message = message.Replace("@ViewBag.Ordernum", CultureInfo.CurrentCulture.TextInfo.ToTitleCase

                 (model.order));
            message = message.Replace("@ViewBag.Reason", CultureInfo.CurrentCulture.TextInfo.ToTitleCase

                (model.reason));

            message = message.Replace("@ViewBag.Description", CultureInfo.CurrentCulture.TextInfo.ToTitleCase

                (model.questionText));


            await MessageServices.SendEmailAsync("newhasakelum@gmail.com", "Welcome", message);
            return View();
        }

        public ActionResult viewOrders()
        {
            UserLayer ulayer = new UserLayer();
            List<Order> orders = ulayer.Orders.ToList();
            return View(orders);
        }

        public ActionResult viewBlackListUsers()
        {
            UserLayer ulayer = new UserLayer();
            List<BlacklistUser> blUsers = ulayer.BlackListUsers.ToList();
            return View(blUsers);
        }
        public ActionResult Delete_BlackListCustomer(int id)
        {
            UserLayer ulayer = new UserLayer();
            ulayer.DeleteBLCustomer(id);
            return View("Index");
        }



    }
    
}