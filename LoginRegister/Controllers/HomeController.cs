using LoginRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Collections.Specialized.BitVector32;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Net;
using System.Configuration;

namespace LoginRegister.Controllers
{
    public class HomeController : Controller
    {
        private DB_Entities _db = new DB_Entities();
        private object rdr;
        
        
        // GET: Home
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        protected string GetConnectionString()
        {
            var datasource = @"Saloni";//your server
            var database = "DatabaseMVC5"; //your database name
           
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + ";Password=" ;

            return connString;
        }
        //AccountDetails
        public ActionResult AccountDetails()
        {

            return View();
        }
        


    
    //Dashboard

    public ActionResult Dashboard()
        {
            return View();
        }

        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Users.Add(_user);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();


        }

        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = _db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["UserId"] = data.FirstOrDefault().UserId;
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }


        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]

        public ActionResult AdminLogin(Admin login)
        {
            if (ModelState.IsValid)
            {
                var lg = _db.Admins.Where(m => m.AdminId == login.AdminId && m.Password == login.Password).SingleOrDefault();
                if (lg != null)
                {
                    Session["AdminId"] = login.AdminId;
                    Session["password"] = login.Password;

                    return RedirectToAction("Admin");
                }
                else
                {
                    ViewBag.msg = "Invalid Adminid or Password";
                }
            }
            return View();
        }


    }
}

          