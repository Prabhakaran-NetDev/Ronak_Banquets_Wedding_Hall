using Ronak_Banquets_Wedding_Hall.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ronak_Banquets_Wedding_Hall.Controllers
{
    public class ReportsController : Controller
    {
        private string NewsqlConn = ConfigurationManager.ConnectionStrings[@"MysqlConn"].ConnectionString;
        // GET: Reports
        public ActionResult Index()
        {
            try
            {
                List<tbl_reports> Obj = new List<tbl_reports>();
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_reports_fetch", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                    while (SdR1.Read())
                    {
                        Obj.Add(new tbl_reports
                        {
                            report_id = Convert.ToInt32(SdR1[0]),
                            event_id = Convert.ToInt32(SdR1[1]),
                            generated_at = Convert.ToDateTime(SdR1[2]),
                            details = SdR1[3].ToString(),
                            attentess_count = Convert.ToInt32(SdR1[4])
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

        // GET: Reports/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    tbl_reports Obj = new tbl_reports();
                    using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                    {
                        DbCon.Open();
                        SqlCommand sqlCmd = new SqlCommand("SP_users_reports_select", DbCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@event_id", Session["reports_select"]);
                        SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                        while (SdR1.Read())
                        {
                            Obj = new tbl_reports
                            {
                                report_id = Convert.ToInt32(SdR1[0]),
                                event_id = Convert.ToInt32(SdR1[1]),
                                generated_at = Convert.ToDateTime(SdR1[2]),
                                details = SdR1[3].ToString(),
                                attentess_count = Convert.ToInt32(SdR1[4])
                            };
                        }
                        DbCon.Close();
                    }
                    return View(Obj);
                }
                else
                {
                    tbl_reports Obj = new tbl_reports();
                    using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                    {
                        DbCon.Open();
                        SqlCommand sqlCmd = new SqlCommand("SP_reports_select", DbCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@report_id", id);
                        SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                        while (SdR1.Read())
                        {
                            Obj = new tbl_reports
                            {
                                report_id = Convert.ToInt32(SdR1[0]),
                                event_id = Convert.ToInt32(SdR1[1]),
                                generated_at = Convert.ToDateTime(SdR1[2]),
                                details = SdR1[3].ToString(),
                                attentess_count = Convert.ToInt32(SdR1[4])
                            };
                        }
                        DbCon.Close();
                    }
                    return View(Obj);
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

        // GET: Reports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reports/Create
        [HttpPost]
        public ActionResult Create(tbl_reports Obj)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_reports_insert", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@event_id", Obj.event_id);
                    sqlCmd.Parameters.AddWithValue("@details", Obj.details);
                    sqlCmd.Parameters.AddWithValue("@user_role", Obj.attentess_count);

                    sqlCmd.ExecuteNonQuery();
                    DbCon.Close();
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

        // GET: Reports/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                tbl_reports Obj = new tbl_reports();
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_reports_select", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@report_id", id);
                    SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                    while (SdR1.Read())
                    {
                        Obj = new tbl_reports
                        {
                            report_id = Convert.ToInt32(SdR1[0]),
                            event_id = Convert.ToInt32(SdR1[1]),
                            generated_at = Convert.ToDateTime(SdR1[2]),
                            details = SdR1[3].ToString(),
                            attentess_count = Convert.ToInt32(SdR1[4])
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

        // POST: Reports/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tbl_reports Obj)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_reports_update", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@report_id", id);
                    sqlCmd.Parameters.AddWithValue("@details", Obj.details);
                    sqlCmd.Parameters.AddWithValue("@user_role", Obj.attentess_count);

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

        // GET: Reports/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                tbl_reports Obj = new tbl_reports();
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_reports_select", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@report_id", id);
                    SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                    while (SdR1.Read())
                    {
                        Obj = new tbl_reports
                        {
                            report_id = Convert.ToInt32(SdR1[0]),
                            event_id = Convert.ToInt32(SdR1[1]),
                            generated_at = Convert.ToDateTime(SdR1[2]),
                            details = SdR1[3].ToString(),
                            attentess_count = Convert.ToInt32(SdR1[4])
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

        // POST: Reports/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, tbl_reports Obj)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_reports_delete", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@report_id", id);

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
