using Fast_To_let.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Fast_To_let.Controllers
{
    public class userController : Controller
    {
        // GET: user
        [HttpGet]
        public ActionResult registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult registration([Bind(Exclude = "IsEmailVerified_,ActivationCode ")]user user)
        {
            bool Status = false;
            string message = "";
            //model validation 
            if (ModelState.IsValid)
            {
                // var IsExist = IsEmailExist(user.email);

                #region email all ready Exist
                var IsExist = IsEmailExist(user.email);
                if (IsExist)
                {
                    ModelState.AddModelError("EmailExist", "Email is all ready Exist");
                    return View(user);
                }

                #endregion

                #region generate activation code
                user.ActivationCode = Guid.NewGuid();
                #endregion


                #region password Hasshing 
                user.password = crypto.Hash(user.password);
                user.Confirm_password = crypto.Hash(user.Confirm_password);
                #endregion
                user.IsEmailVarified_ = false;

                #region save to database
                using (fast_tolet dc = new fast_tolet())
                {
                    dc.users.Add(user);
                    dc.SaveChanges();

                    // email send


                    SendVerificationLinkEmail(user.email, user.ActivationCode.ToString());

                    message = "Reistration succesfully done.Account activation link" +
                        "has benn sent to your email id:" + user.email;
                    Status = true; 


                }
                #endregion


            }
            else
            {
                message = "Invalid request";
            }
            ViewBag.Message = message;
            ViewBag.Status = Status;
            //
            return View();
        }
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (fast_tolet dc = new fast_tolet())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;
                var active = dc.users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (active != null)
                {
                    active.IsEmailVarified_ = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = Status;
            return View();
        }
        //login
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(adminlogin login, string RetrunUrl)
        {
            string message = "";
            using (fast_tolet dc = new fast_tolet())
            {
                var v = dc.users.Where(a => a.username == login.username).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(crypto.Hash(login.password), v.password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20;
                        var ticket = new FormsAuthenticationTicket(login.username, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(RetrunUrl))
                        {
                            return Redirect(RetrunUrl);
                        }
                        else
                        {
                            return RedirectToAction("userpage", "userProfile");
                        }
                    }
                    else
                    {
                        message = "Invalid Action";
                    }



                }
                else
                {
                    message = "Invalid Action";
                }
            }
            ViewBag.Message = message;
            return View();
        }
        //admin login
        [HttpGet]
        public ActionResult adminlogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult adminlogin(adminlogin login, string RetrunUrl)
        {
            string message = "";
            using (fast_tolet dc = new fast_tolet())
            {
                var v = dc.users.Where(a => a.username == login.username).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(crypto.Hash(login.password), v.password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20;
                        var ticket = new FormsAuthenticationTicket(login.username, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(RetrunUrl))
                        {
                            return Redirect(RetrunUrl);
                        }
                        else
                        {
                            return RedirectToAction("admin", "adminpanel");
                        }
                    }
                    else
                    {
                        message = "Invalid Action";
                    }



                }
                else
                {
                    message = "Invalid Action";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        //log out
        [Authorize]
        [HttpPost]
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login", "user");
        }
       

        [NonAction]
        public bool IsEmailExist(string email)
        {
            using (fast_tolet dc = new fast_tolet())
            {
                var v = dc.users.Where(a => a.email == email).FirstOrDefault();
                return v != null;
            }
        }
        [NonAction]
        public void SendVerificationLinkEmail(string email, string activationCode)
        {
            var verifyUrl = "/User/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("fast.tolet@gmail.com");
            var toEmail = new MailAddress(email);
            var fromEmailpassword = "Fast123456";
            string subject = "your account is ssuccesfully cresated";
            string body = "<br/><br/>Hi, your fast_To-let account is " +
                "successfully created. please click on the below link to verify account" +
              "<br/><br/><a href ='" + link + "'>" + link + "</a>";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailpassword)
            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

    }
}