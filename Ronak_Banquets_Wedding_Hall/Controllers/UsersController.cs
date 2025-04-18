using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Ronak_Banquets_Wedding_Hall.Models;
using System.Web.UI;
using System.Web.Security;
using System.Net.Mail;
using System.Net;
using static System.Net.WebRequestMethods;
using Microsoft.Ajax.Utilities;

namespace Ronak_Banquets_Wedding_Hall.Controllers
{
    public class UsersController : Controller
    {
        string email;        
        string email_pass;
        private string NewsqlConn = ConfigurationManager.ConnectionStrings[@"MysqlConn"].ConnectionString;
        // GET: tbl_users
        public ActionResult Index()
        {
            try
            {
                List<tbl_users> UsersObj = new List<tbl_users>();
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_users_fetch", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                    while (SdR1.Read())
                    {
                        UsersObj.Add(new tbl_users
                        {
                            user_id = Convert.ToInt32(SdR1[0]),
                            user_name = SdR1[1].ToString(),
                            user_password = SdR1[2].ToString(),
                            user_email = SdR1[3].ToString(),
                            user_phone = Convert.ToInt64(SdR1[4]),
                            user_role = SdR1[5].ToString()
                        });
                    }
                    DbCon.Close();
                }
                return View(UsersObj);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

        // GET: Login/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    tbl_users UsersObj = new tbl_users();
                    using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                    {
                        DbCon.Open();
                        SqlCommand sqlCmd = new SqlCommand("SP_users_select_id", DbCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@user_id", Session["UserId"]);
                        SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                        while (SdR1.Read())
                        {
                            UsersObj = new tbl_users
                            {
                                user_id = Convert.ToInt32(SdR1[0]),
                                user_name = SdR1[1].ToString(),
                                user_password = SdR1[2].ToString(),
                                user_email = SdR1[3].ToString(),
                                user_phone = Convert.ToInt64(SdR1[4]),
                                user_role = SdR1[5].ToString()
                            };
                        }
                        DbCon.Close();
                    }
                    return View(UsersObj);
                }
                else
                {
                    tbl_users UsersObj = new tbl_users();
                    using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                    {
                        DbCon.Open();
                        SqlCommand sqlCmd = new SqlCommand("SP_users_select_id", DbCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@user_id", id);
                        SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                        while (SdR1.Read())
                        {
                            UsersObj = new tbl_users
                            {
                                user_id = Convert.ToInt32(SdR1[0]),
                                user_name = SdR1[1].ToString(),
                                user_password = SdR1[2].ToString(),
                                user_email = SdR1[3].ToString(),
                                user_phone = Convert.ToInt64(SdR1[4]),
                                user_role = SdR1[5].ToString()
                            };
                        }
                        DbCon.Close();
                    }
                    return View(UsersObj);
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

        public ActionResult Create()
        {

            List<string> Roles = new List<string> {"attendee","manager", "admin"};
            ViewBag.RoleOptions = Roles;
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(tbl_users UserObj)
        {
            try
            {
                List<string> Roles = new List<string> { "attendee", "manager", "admin" };
                ViewBag.RoleOptions = Roles;
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_users_create", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@user_name", UserObj.user_name);
                    sqlCmd.Parameters.AddWithValue("@user_email", UserObj.user_email);
                    sqlCmd.Parameters.AddWithValue("@user_password", UserObj.user_password);
                    sqlCmd.Parameters.AddWithValue("@user_phone", UserObj.user_phone);
                    sqlCmd.Parameters.AddWithValue("@user_role", UserObj.user_role);

                    sqlCmd.ExecuteNonQuery();
                    DbCon.Close();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

         // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                tbl_users UsersObj = new tbl_users();
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_users_select_id", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@user_id", id);
                    SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                    while (SdR1.Read())
                    {
                        UsersObj = new tbl_users
                        {
                            user_id = Convert.ToInt32(SdR1[0]),
                            user_name = SdR1[1].ToString(),
                            user_password = SdR1[2].ToString(),
                            user_email = SdR1[3].ToString(),
                            user_phone = Convert.ToInt64(SdR1[4]),
                            user_role = SdR1[5].ToString()
                        };
                    }
                    DbCon.Close();
                }
                return View(UsersObj);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }
        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tbl_users UserObj)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_users_update", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@user_id", id);
                    sqlCmd.Parameters.AddWithValue("@user_name", UserObj.user_name);
                    sqlCmd.Parameters.AddWithValue("@user_email", UserObj.user_email);
                    sqlCmd.Parameters.AddWithValue("@user_password", UserObj.user_password);
                    sqlCmd.Parameters.AddWithValue("@user_phone", UserObj.user_phone);
                    sqlCmd.Parameters.AddWithValue("@user_role", UserObj.user_role);

                    sqlCmd.ExecuteNonQuery();
                    DbCon.Close();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }
        public ActionResult User_Edit(int id)
        {
            try
            {
                tbl_users UsersObj = new tbl_users();
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_users_select_id", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@user_id", id);
                    SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                    while (SdR1.Read())
                    {
                        UsersObj = new tbl_users
                        {
                            user_id = Convert.ToInt32(SdR1[0]),
                            user_name = SdR1[1].ToString(),
                            user_password = SdR1[2].ToString(),
                            user_email = SdR1[3].ToString(),
                            user_phone = Convert.ToInt64(SdR1[4]),
                            user_role = SdR1[5].ToString()
                        };
                    }
                    DbCon.Close();
                }
                return View(UsersObj);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult User_EditM(int id, tbl_users UserObj)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_users_update", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@user_id", id);
                    sqlCmd.Parameters.AddWithValue("@user_name", UserObj.user_name);
                    sqlCmd.Parameters.AddWithValue("@user_phone", UserObj.user_phone);

                    sqlCmd.ExecuteNonQuery();
                    DbCon.Close();
                }
                return RedirectToAction("../Home/Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                tbl_users UsersObj = new tbl_users();
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_users_delete", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@user_id", id);

                    sqlCmd.ExecuteNonQuery();
                    DbCon.Close();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }
        // GET: Email
        public ActionResult Email()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmailM(tbl_users UserObj)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_users_fetch_admin", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = sqlCmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        email = sdr[3].ToString();
                        email_pass = sdr[6].ToString();

                    }
                    DbCon.Close();
                }
                Session["Remail"] = UserObj.user_email;

                string from, pass, messageBody, to;
                Random random = new Random();
                Session["Rrandom"] = (random.Next(999999)).ToString();
                MailMessage message = new MailMessage();
                to = (UserObj.user_email).ToString();
                from = email;
                pass = email_pass;
                messageBody = "Your Email Verification Code: " + Session["Rrandom"];
                message.To.Add(to);
                message.From = new MailAddress(from);
                message.Body = messageBody;
                message.Subject = "Verification Code:";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(from, pass);
                try
                {
                    smtp.Send(message);
                    return RedirectToAction("Register");
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    return View("Email");
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }
                   
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterM(tbl_users UserObj)
        {
            try
            {
                if (UserObj == null) 
                { 
                    TempData["Message"] = "Invalid user information."; 
                    return View("../Home/Error"); 
                }
                                
                string randomcode = Session["Rrandom"].ToString();

                if (Convert.ToString(UserObj.OTP).Trim() == randomcode.Trim())
                {
                    if (UserObj.user_password == UserObj.confirm_password)
                    {
                        using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                        {
                            DbCon.Open();
                            SqlCommand sqlCmd = new SqlCommand("SP_users_create", DbCon);
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Parameters.AddWithValue("@user_name", UserObj.user_name);
                            sqlCmd.Parameters.AddWithValue("@user_email", Session["Remail"]);
                            sqlCmd.Parameters.AddWithValue("@user_password", UserObj.user_password);
                            sqlCmd.Parameters.AddWithValue("@user_phone", UserObj.user_phone);

                            int a = sqlCmd.ExecuteNonQuery();
                            DbCon.Close();
                            Session["Remail"] = null;
                            if (a > 0)
                            {
                                TempData["Message"] = "Successfully Registered";
                                return RedirectToAction("../Home/Index");
                            }
                            else
                            {
                                TempData["Message"] = "Error";
                                return RedirectToAction("Register");
                            }
                        }
                    }
                    else
                    {
                        return View("Register");
                    }
                }
                else
                {
                    TempData["Message"] = "OTP Mismached";
                    return View("Register");
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

        // GET: Login 
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginM(tbl_users UserObj)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_users_select", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@user_email", UserObj.user_email);
                    sqlCmd.Parameters.AddWithValue("@user_password", UserObj.user_password);
                    SqlDataReader sdr = sqlCmd.ExecuteReader();
                    //string a = ((int)sqlCmd.ExecuteScalar()).ToString();
                    if (sdr.Read())
                    {
                        Session["UserId"] = Convert.ToInt32(sdr[0]);
                        Session["UserName"] = sdr[1].ToString();
                        Session["UserPwd"] = sdr[2].ToString();
                        Session["UserEmail"] = sdr[3].ToString();
                        Session["UserRole"] = sdr[5].ToString();

                        if(Session["UserRole"].ToString() == "admin" || Session["UserRole"].ToString() == "manager")
                        {
                            return RedirectToAction("../Admin/Admin_Dashboard");
                        }
                        else
                        {
                            return RedirectToAction("../Admin/User_Dashboard");
                        }                        
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }
                }
            }
            catch(Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("../Home/Index");
        }
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPasswordM(tbl_users UserObj)
        {
            try
            {
                if (UserObj == null)
                {
                    TempData["Message"] = "Invalid user information.";
                    return View("../Home/Error");
                }
                if (UserObj.user_password == UserObj.confirm_password)
                {
                    using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                    {
                        DbCon.Open();
                        SqlCommand sqlCmd = new SqlCommand("SP_users_resetpassword", DbCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@user_id", Session["UserId"]);
                        sqlCmd.Parameters.AddWithValue("@user_name", Session["UserName"]);
                        sqlCmd.Parameters.AddWithValue("@user_password", UserObj.user_password);
                        sqlCmd.Parameters.AddWithValue("@old_password", UserObj.old_password);

                        int a = sqlCmd.ExecuteNonQuery();
                        DbCon.Close();

                        if (a > 0)
                        {
                            TempData["Message"] = "Successfull";
                            return RedirectToAction("../Home/Index");
                        }
                        else
                        {
                            TempData["Message"] = "Error";
                            return RedirectToAction("ResetPassword");
                        }
                    }
                }
                else
                {
                    TempData["Message"] = "Password Mismached";
                    return View("ResetPassword");
                }                
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

        
        public ActionResult ForgotPasswordOTP(tbl_users UserObj)
        {            
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_users_fetch_admin", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = sqlCmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        email = sdr[3].ToString();
                        email_pass = sdr[6].ToString();
                    }
                    DbCon.Close();
                }

                string from, pass, messageBody, to;
                Random random = new Random();
                Session["ForgotPwdrandom"] = (random.Next(999999)).ToString();
                MailMessage message = new MailMessage();
                to = UserObj.user_email.ToString();
                from = email;
                pass = email_pass;
                messageBody = "Your Email Verification Code: " + Session["ForgotPwdrandom"];
                message.To.Add(to);
                message.From = new MailAddress(from);
                message.Body = messageBody;
                message.Subject = "Verification Code:";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(from, pass);
                try
                {
                    smtp.Send(message);
                    TempData["TimeOut"] = "OtpRequest()";
                    return RedirectToAction("ForgotPassword");
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    return RedirectToAction("ForgotPassword");
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPasswordM(tbl_users UserObj)
        {
            try
            {
                if (UserObj == null)
                {
                    TempData["Message"] = "Invalid user information.";
                    return View("../Home/Error");
                }

                string randomcode = Session["ForgotPwdrandom"].ToString();

                if (Convert.ToString(UserObj.OTP).Trim() == randomcode.Trim())
                {
                    if (UserObj.user_password == UserObj.confirm_password)
                    {
                        using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                        {
                            DbCon.Open();
                            SqlCommand sqlCmd = new SqlCommand("SP_users_forgotpassword", DbCon);
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Parameters.AddWithValue("@user_id", Session["UserId"]);
                            sqlCmd.Parameters.AddWithValue("@user_name", Session["UserName"]);
                            sqlCmd.Parameters.AddWithValue("@user_password", UserObj.user_password);

                            int a = sqlCmd.ExecuteNonQuery();
                            DbCon.Close();
                            
                            if (a > 0)
                            {
                                TempData["Message"] = "Successfull";
                                return RedirectToAction("../Home/Index");
                            }
                            else
                            {
                                TempData["Message"] = "Error";
                                return RedirectToAction("ForgotPassword");
                            }
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Password Mismached";
                        return View("ForgotPassword");
                    }
                }
                else
                {
                    TempData["Message"] = "OTP Mismached";
                    return View("ForgotPassword");
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }
    }    
}
