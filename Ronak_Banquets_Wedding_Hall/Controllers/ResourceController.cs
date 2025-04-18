using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Ronak_Banquets_Wedding_Hall.Models;

namespace Ronak_Banquets_Wedding_Hall.Controllers
{
    public class ResourceController : Controller
    {
        private string NewsqlConn = ConfigurationManager.ConnectionStrings[@"MysqlConn"].ConnectionString;
        // GET: Resource/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Resource/Create
        [HttpPost]
        public ActionResult Create(tbl_resources Obj)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_resources_insert", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@resource_name", Obj.resource_name);
                    sqlCmd.Parameters.AddWithValue("@resource_availability", Obj.resource_availability);

                    sqlCmd.ExecuteNonQuery();
                    DbCon.Close();
                }
                return RedirectToAction("../Admin/Admin_Dashboard");
            }
            catch
            {
                return View();
            }
        }

        // GET: Resource/Edit/5
        public ActionResult Edit(int id)
        {
            tbl_resources Obj = new tbl_resources();
            using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
            {
                DbCon.Open();
                SqlCommand sqlCmd = new SqlCommand("SP_resources_select2", DbCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@resource_id", id);
                SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                while (SdR1.Read())
                {
                    Obj = new tbl_resources
                    {
                        resource_name = SdR1[1].ToString(),
                        resource_availability = Convert.ToBoolean(SdR1[2])
                    };
                }
                DbCon.Close();
            }
            return View(Obj);
        }

        // POST: Resource/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tbl_resources Obj)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_resources_update", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@resource_name", Obj.resource_name);
                    sqlCmd.Parameters.AddWithValue("@resource_availability", Obj.resource_availability);

                    sqlCmd.ExecuteNonQuery();
                    DbCon.Close();
                }
                return RedirectToAction("../Admin/Admin_Dashboard");
            }
            catch
            {
                return View();
            }
        }

        // GET: Resource/Delete/5
        public ActionResult Delete(int id)
        {
            tbl_resources Obj = new tbl_resources();
            using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
            {
                DbCon.Open();
                SqlCommand sqlCmd = new SqlCommand("SP_resources_select2", DbCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@resource_id", id);
                SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                while (SdR1.Read())
                {
                    Obj = new tbl_resources
                    {
                        resource_name = SdR1[1].ToString(),
                        resource_availability = Convert.ToBoolean(SdR1[2])
                    };
                }
                DbCon.Close();
            }
            return View(Obj);
        }

        // POST: Resource/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, tbl_resources Obj)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_resources_delete", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@resource_id", id);

                    sqlCmd.ExecuteNonQuery();
                    DbCon.Close();
                }
                return RedirectToAction("../Admin/Admin_Dashboard");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ERCreate()
        {
            return View();
        }

        // POST: Resource/Create
        [HttpPost]
        public ActionResult ERCreateM(tbl_event_resources Obj)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_event_resources_insert", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@event_id", Obj.event_id);
                    sqlCmd.Parameters.AddWithValue("@resource_id", Obj.resource_id);

                    sqlCmd.ExecuteNonQuery();
                    DbCon.Close();
                }
                return RedirectToAction("../Admin/Admin_Dashboard");
            }
            catch
            {
                return View();
            }
        }

        // GET: Resource/Edit/5
        public ActionResult EREdit(int id)
        {
            tbl_event_resources Obj = new tbl_event_resources();
            using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
            {
                DbCon.Open();
                SqlCommand sqlCmd = new SqlCommand("SP_event_resources_select", DbCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@event_id", Obj.event_id);
                sqlCmd.Parameters.AddWithValue("@resource_id", Obj.resource_id);
                SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                while (SdR1.Read())
                { 
                    Obj = new tbl_event_resources
                    {
                        event_id = Convert.ToInt32(SdR1[0]),
                        resource_id = Convert.ToInt32(SdR1[1])
                    };
                }
                DbCon.Close();
            }
            return View(Obj);
        }

        // POST: Resource/Edit/5
        [HttpPost]
        public ActionResult EREditM(int id, tbl_event_resources Obj)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_event_resources_update", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@event_id", Obj.event_id);
                    sqlCmd.Parameters.AddWithValue("@resource_id", Obj.resource_id);

                    sqlCmd.ExecuteNonQuery();
                    DbCon.Close();
                }
                return RedirectToAction("../Admin/Admin_Dashboard");
            }
            catch
            {
                return View();
            }
        }

        // GET: Resource/Delete/5
        public ActionResult ERDelete(int id)
        {
            tbl_event_resources Obj = new tbl_event_resources();
            using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
            {
                DbCon.Open();
                SqlCommand sqlCmd = new SqlCommand("SP_event_resources_select", DbCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@event_id", Obj.event_id);
                sqlCmd.Parameters.AddWithValue("@resource_id", Obj.resource_id);
                SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                while (SdR1.Read())
                {
                    Obj = new tbl_event_resources
                    {
                        event_id = Convert.ToInt32(SdR1[0]),
                        resource_id = Convert.ToInt32(SdR1[1])
                    };
                }
                DbCon.Close();
            }
            return View(Obj);
        }

        // POST: Resource/Delete/5
        [HttpPost]
        public ActionResult ERDeleteM(int id, tbl_event_resources Obj)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                {
                    DbCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("SP_event_resources_delete", DbCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@event_id", Obj.event_id);
                    sqlCmd.Parameters.AddWithValue("@resource_id", Obj.resource_id);

                    sqlCmd.ExecuteNonQuery();
                    DbCon.Close();
                }
                return RedirectToAction("../Admin/Admin_Dashboard");
            }
            catch
            {
                return View();
            }
        }
    }
}
