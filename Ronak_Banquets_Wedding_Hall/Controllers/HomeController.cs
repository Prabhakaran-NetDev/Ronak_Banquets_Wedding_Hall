using Ronak_Banquets_Wedding_Hall.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.ComponentModel.DataAnnotations;

namespace Ronak_Banquets_Wedding_Hall.Controllers
{
    public class HomeController : Controller
    {
        private string NewsqlConn = ConfigurationManager.ConnectionStrings[@"MysqlConn"].ConnectionString;
        // GET: tbl_events
        public ActionResult Index()
        {
            try
            {
                if (Session["UserRole"] == null)
                {
                    List<tbl_events> EventsObj = new List<tbl_events>();
                    var viewModel = new adminViewModel
                    {
                        Events = EventsObj
                    };
                    using (SqlConnection DbCon = new SqlConnection(NewsqlConn))
                    {
                        DbCon.Open();
                        SqlCommand sqlCmd = new SqlCommand("SP_events_home_select", DbCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader SdR1 = sqlCmd.ExecuteReader();

                        while (SdR1.Read())
                        {
                            EventsObj.Add(new tbl_events
                            {
                                event_name = SdR1[0].ToString(),
                                event_date = Convert.ToDateTime(SdR1[1]),
                                event_venue = SdR1[2].ToString(),
                            });
                        }
                        DbCon.Close();
                    }
                    return View(viewModel);
                }
                else if (Session["UserRole"].ToString() == "admin" && Session["UserRole"].ToString() == "manager")
                {
                    return View("../Admin/Admin_Dashboard");
                }
                else
                {
                    return View("../Admin/User_Dashboard");
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View("../Home/Error");
            }
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}