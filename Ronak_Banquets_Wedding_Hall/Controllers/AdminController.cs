using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Ronak_Banquets_Wedding_Hall.Models;
using System.Resources;

namespace Ronak_Banquets_Wedding_Hall.Controllers
{
    public class AdminController : Controller
    {
        private string NewsqlConn = ConfigurationManager.ConnectionStrings[@"MysqlConn"].ConnectionString;
        // GET: tbl_events
        public ActionResult Admin_Dashboard()
        {
            try
            {
                List<tbl_events> EventsObj = new List<tbl_events>();
                List<SP_events_bookings> Obj2 = new List<SP_events_bookings>();
                List<tbl_resources> Obj3 = new List<tbl_resources>();
                List<SP_event_resources> Obj4 = new List<SP_event_resources>();
                var viewModel = new adminViewModel
                {
                    Events = EventsObj,
                    Events_bookings = Obj2,
                    Resources = Obj3,
                    Events_Resources_Select = Obj4
                };
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_events_select", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                    while (SdR1.Read())
                    {
                        EventsObj.Add(new tbl_events
                        {
                            event_id = Convert.ToInt32(SdR1["event_id"]),
                            event_name = SdR1["event_name"].ToString(),
                            event_date = Convert.ToDateTime(SdR1["event_date"]),
                            event_venue = SdR1["event_venue"].ToString(),
                            created_by = Convert.ToInt32(SdR1["created_by"]),
                            payment = SdR1["payment"].ToString(),
                            event_status = SdR1["event_status"].ToString()
                        });
                    }
                    SdR1.Close();
                    SqlCommand sqlCmd2 = new SqlCommand("SP_events_booking_select", DbCon);
                    sqlCmd2.CommandType = CommandType.StoredProcedure;
                    SqlDataReader SdR2 = sqlCmd2.ExecuteReader();
                    while (SdR2.Read())
                    {
                        Obj2.Add(new SP_events_bookings
                        {
                            event_id = Convert.ToInt32(SdR2[0]),
                            event_name = SdR2[1].ToString(),
                            event_date = Convert.ToDateTime(SdR2[2]),
                            event_venue = SdR2[3].ToString(),
                            created_by = Convert.ToInt32(SdR2[4]),
                            payment = SdR2[5].ToString(),
                            event_status = SdR2[6].ToString(),
                            booking_id = Convert.ToInt32(SdR2[7]),
                            booking_date = Convert.ToDateTime(SdR2[10])
                        });
                    }
                    SdR2.Close();
                    SqlCommand sqlCmd3 = new SqlCommand("SP_resources_select", DbCon);
                    sqlCmd3.CommandType = CommandType.StoredProcedure;
                    SqlDataReader SdR3 = sqlCmd3.ExecuteReader();
                    while (SdR3.Read())
                    {
                        Obj3.Add(new tbl_resources
                        {
                            resource_id = Convert.ToInt32(SdR3[0]),
                            resource_name = SdR3[1].ToString(),
                            resource_availability = Convert.ToBoolean(SdR3[2])
                        });
                    }
                    SdR3.NextResult();
                    while (SdR3.Read())
                    {
                        Obj4.Add(new SP_event_resources
                        {
                            resource_id = Convert.ToInt32(SdR2[0]),
                            resource_name = SdR3[1].ToString(),
                            resource_availability = Convert.ToBoolean(SdR3[2]),
                            event_id = Convert.ToInt32(SdR2[3]),
                            event_name = SdR2[6].ToString(),
                            event_date = Convert.ToDateTime(SdR2[7])
                        });
                    }
                    DbCon.Close();
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }
        public ActionResult User_Dashboard()
        {
            try
            {
                List<SP_events_bookings_users> Obj = new List<SP_events_bookings_users>();
                List<tbl_events> Obj2 = new List<tbl_events>();
                List<tbl_resources> Obj3 = new List<tbl_resources>();
                List<SP_event_resources> Obj4 = new List<SP_event_resources>();
                var viewModel = new adminViewModel
                {
                    Events_bookings_users = Obj,
                    Events = Obj2,
                    Resources = Obj3,
                    Events_Resources_Select = Obj4

                };
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_events_booking_select_users", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    if(Session["UserId"] != null) 
                    { 
                        sqlCmd.Parameters.AddWithValue("@user_id", Session["UserId"]);
                    }
                    else
                    {
                        sqlCmd.Parameters.AddWithValue("@user_id", 1);
                    }
                    SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                    while (SdR1.Read())
                    {
                        Obj.Add(new SP_events_bookings_users
                        {
                            created_by = Convert.ToInt32(SdR1[6]),
                            event_name = SdR1[0].ToString(),
                            event_date = Convert.ToDateTime(SdR1[1]),
                            event_venue = SdR1[2].ToString(),
                            payment = SdR1[3].ToString(),
                            event_status = SdR1[4].ToString(),
                            booking_date = Convert.ToDateTime(SdR1[5])
                        });
                    }
                    SdR1.Close();
                    SqlCommand sqlCmd2 = new SqlCommand("SP_events_home_select", DbCon);
                    sqlCmd2.CommandType = CommandType.StoredProcedure;
                    SqlDataReader SdR2 = sqlCmd2.ExecuteReader();

                    while (SdR2.Read())
                    {
                        Obj2.Add(new tbl_events
                        {
                            event_name = SdR2[0].ToString(),
                            event_date = Convert.ToDateTime(SdR2[1]),
                            event_venue = SdR2[2].ToString(),
                        });
                    }
                    SdR2.Close();
                    SqlCommand sqlCmd3 = new SqlCommand("SP_resources_select", DbCon);
                    sqlCmd3.CommandType = CommandType.StoredProcedure;
                    SqlDataReader SdR3 = sqlCmd3.ExecuteReader();
                    while (SdR3.Read())
                    {
                        Obj3.Add(new tbl_resources
                        {
                            resource_name = SdR3[1].ToString(),
                            resource_availability = Convert.ToBoolean(SdR3[2])
                        });
                    }
                    SdR3.NextResult();
                    while (SdR3.Read())
                    {
                        Obj4.Add(new SP_event_resources
                        {
                            resource_id = Convert.ToInt32(SdR2[0]),
                            resource_name = SdR3[1].ToString(),
                            resource_availability = Convert.ToBoolean(SdR3[2]),
                            event_id = Convert.ToInt32(SdR2[3]),
                            event_name = SdR2[6].ToString(),
                            event_date = Convert.ToDateTime(SdR2[7])
                        });
                    }
                    DbCon.Close();
                    DbCon.Close();
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }
        public ActionResult Details(int id)
        {
            try
            {
                tbl_events EventsObj = new tbl_events();
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_events_select", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                    while (SdR1.Read())
                    {
                        EventsObj = new tbl_events
                        {
                            event_id = Convert.ToInt32(SdR1["event_id"]),
                            event_name = SdR1["event_name"].ToString(),
                            event_date = Convert.ToDateTime(SdR1["event_date"]),
                            event_venue = SdR1["event_venue"].ToString(),
                            created_by = Convert.ToInt32(SdR1["created_by"]),
                            payment = SdR1["payment"].ToString(),
                            event_status = SdR1["event_status"].ToString()
                        };
                    }
                    SdR1.Close();
                    DbCon.Close();
                }
                return View(EventsObj);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

        // GET: tbl_events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbl_events/Create
        [HttpPost]
        public ActionResult Create(tbl_events EventsObj)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_events_insert", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@event_name", EventsObj.event_name);
                    sqlCmd.Parameters.AddWithValue("@event_date", EventsObj.event_date);
                    sqlCmd.Parameters.AddWithValue("@event_venue", EventsObj.event_venue);
                    sqlCmd.Parameters.AddWithValue("@created_by", Session["UserId"]);

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

        // GET: tbl_events/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                tbl_events EventsObj = new tbl_events();
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_events_select", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                    while (SdR1.Read())
                    {
                        if (Convert.ToInt32(SdR1["event_id"]) == id)
                        {
                            EventsObj = new tbl_events
                            {
                                event_id = Convert.ToInt32(SdR1["event_id"]),
                                event_name = SdR1["event_name"].ToString(),
                                event_date = Convert.ToDateTime(SdR1["event_date"]),
                                event_venue = SdR1["event_venue"].ToString(),
                                created_by = Convert.ToInt32(SdR1["created_by"]),
                                payment = SdR1["payment"].ToString(),
                                event_status = SdR1["event_status"].ToString()
                            };
                        }
                    }
                    DbCon.Close();
                }
                return View(EventsObj);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

        // POST: tbl_events/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tbl_events EventsObj)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_events_update", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@event_id", id);
                    sqlCmd.Parameters.AddWithValue("@event_name", EventsObj.event_name);
                    sqlCmd.Parameters.AddWithValue("@event_date", EventsObj.event_date);
                    sqlCmd.Parameters.AddWithValue("@event_venue", EventsObj.event_venue);
                    sqlCmd.Parameters.AddWithValue("@created_by", EventsObj.created_by);
                    sqlCmd.Parameters.AddWithValue("@payment", EventsObj.payment);
                    sqlCmd.Parameters.AddWithValue("@event_status", EventsObj.event_status);

                    sqlCmd.ExecuteNonQuery();
                    DbCon.Close();
                }
                return RedirectToAction("Admin_Dashboard");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

        // GET: tbl_events/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                tbl_events EventsObj = new tbl_events();
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_events_select", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                    while (SdR1.Read())
                    {
                        EventsObj = new tbl_events
                        {
                            event_id = Convert.ToInt32(SdR1["event_id"]),
                            event_name = SdR1["event_name"].ToString(),
                            event_date = Convert.ToDateTime(SdR1["event_date"]),
                            event_venue = SdR1["event_venue"].ToString(),
                            created_by = Convert.ToInt32(SdR1["created_by"]),
                            payment = SdR1["payment"].ToString(),
                            event_status = SdR1["event_status"].ToString()
                        };
                    }
                    SdR1.Close();
                    DbCon.Close();
                }
                return View(EventsObj);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

        // POST: tbl_events/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, tbl_events EventsObj)
        {
            try
            {                
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_events_delete", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@event_id", id);

                    sqlCmd.ExecuteNonQuery();
                    DbCon.Close();
                }
                return RedirectToAction("Admin_Dashboard");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }
    }
}
