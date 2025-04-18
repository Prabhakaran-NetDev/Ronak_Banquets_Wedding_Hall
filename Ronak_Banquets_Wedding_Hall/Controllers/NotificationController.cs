using Ronak_Banquets_Wedding_Hall.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace Ronak_Banquets_Wedding_Hall.Controllers
{
    public class NotificationController : Controller
    {
        private string NewsqlConn = ConfigurationManager.ConnectionStrings[@"MysqlConn"].ConnectionString;
        // GET: Notification
        public ActionResult Index()
        {
            try
            {
                List<tbl_notifications> Obj = new List<tbl_notifications>();
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_notification_fetch", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                    while (SdR1.Read())
                    {
                        Obj.Add(new tbl_notifications
                        {
                            notification_id = Convert.ToInt32(SdR1[0]),
                            user_id = Convert.ToInt32(SdR1[1]),
                            message = SdR1[2].ToString(),
                            is_read = Convert.ToBoolean(SdR1[3])
                        });
                    }
                    DbCon.Close();
                }
                return View(Obj);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

        // GET: Notification/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                tbl_notifications Obj = new tbl_notifications();
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_notification_select", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@notification_id", id);
                    SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                    while (SdR1.Read())
                    {
                        Obj = new tbl_notifications
                        {
                            notification_id = Convert.ToInt32(SdR1[0]),
                            user_id = Convert.ToInt32(SdR1[1]),
                            message = SdR1[2].ToString(),
                            is_read = Convert.ToBoolean(SdR1[3])
                        };
                    }
                    DbCon.Close();
                }
                return View(Obj);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

        public ActionResult U_Details()
        {
            try
            {
                List<tbl_notifications> Obj = new List<tbl_notifications>();
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_notification_user_select", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@user_id", Session["UserId"]);
                    SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                    while (SdR1.Read())
                    {
                        Obj.Add(new tbl_notifications
                        {
                            notification_id = Convert.ToInt32(SdR1[0]),
                            user_id = Convert.ToInt32(SdR1[1]),
                            message = SdR1[2].ToString(),
                            is_read = Convert.ToBoolean(SdR1[3])
                        });
                    }
                    DbCon.Close();
                }
                return View(Obj);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

            // GET: Notification/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notification/Create
        [HttpPost]
        public ActionResult Create(tbl_notifications Obj)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_notification_insert", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@user_id", Obj.user_id);
                    sqlCmd.Parameters.AddWithValue("@message", Obj.message);
                    sqlCmd.Parameters.AddWithValue("@is_read", Obj.is_read);

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

        // GET: Notification/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                tbl_notifications Obj = new tbl_notifications();
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_notification_select", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@notification_id", id);
                    SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                    while (SdR1.Read())
                    {
                        Obj = new tbl_notifications
                        {
                            notification_id = Convert.ToInt32(SdR1[0]),
                            user_id = Convert.ToInt32(SdR1[1]),
                            message = SdR1[2].ToString(),
                            is_read = Convert.ToBoolean(SdR1[3])
                        };
                    }
                    DbCon.Close();
                }
                return View(Obj);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

        // POST: Notification/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tbl_notifications Obj)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_notification_update", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@notification_id", id);
                    sqlCmd.Parameters.AddWithValue("@user_id", Obj.notification_id);
                    sqlCmd.Parameters.AddWithValue("@message", Obj.message);
                    sqlCmd.Parameters.AddWithValue("@is_read", Obj.is_read);

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

        // GET: Notification/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                tbl_notifications Obj = new tbl_notifications();
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_notification_select", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@notification_id", id);
                    SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                    while (SdR1.Read())
                    {
                        Obj = new tbl_notifications
                        {
                            notification_id = Convert.ToInt32(SdR1[0]),
                            user_id = Convert.ToInt32(SdR1[1]),
                            message = SdR1[2].ToString(),
                            is_read = Convert.ToBoolean(SdR1[3])
                        };
                    }
                    DbCon.Close();
                }
                return View(Obj);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

        // POST: Notification/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, tbl_notifications Obj)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_notification_delete", DbCon);
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
    }
}
