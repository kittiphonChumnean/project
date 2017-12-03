using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KonKhayHoi.Models;
using System.Data.Entity.Validation;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace KonKhayHoi.Controllers
{
    public class AdminController : Controller
    {
        KonKhayHoiEntities db = new KonKhayHoiEntities();
        // GET: Admin


        public ActionResult profile()
        {

            if (ModelState.IsValid)
            {
                var CAID = Request.Cookies["AID"].Value;
                var checkW = db.admins.Where(a => a.AID.Equals(CAID)).FirstOrDefault();


                if (checkW != null)
                {

                    Session["ADuser"] = checkW.AID;

                    Session["ADname"] = checkW.A_name;

                    Session["ADlast"] = checkW.A_lastname;

                    Session["ADtel"] = checkW.A_tel;

                    Session["ADno"] = checkW.A_no;

                    Session["ADsubD"] = checkW.A_subD;

                    Session["ADsub"] = checkW.A_sub;

                    Session["ADproV"] = checkW.A_ProV;

                    Session["ADInvestment"] = checkW.A_Investment;

                }

            }

            return View();
        }




        public ActionResult Editprofile()

        {
            admin Admin = new admin();

            string AID_update = Request.Form["username_update"];
            string A_Investment_update = Request.Form["Investment_update"];
            string A_name_update = Request.Form["name_update"];
            string A_lastname_update = Request.Form["lastname_update"];
            string A_tel_update = Request.Form["tel_update"];
            string A_no_update = Request.Form["no_update"];
            string A_subD_update = Request.Form["subD_update"];
            string A_sub_update = Request.Form["sub_update"];
            string A_ProV_update = Request.Form["proV_update"];


            if (ModelState.IsValid)
            {
                if (AID_update != null)
                {

                    var checkUP = db.admins.Where(a => a.AID.Equals(AID_update)).FirstOrDefault();

                    int A = Convert.ToInt32(A_Investment_update);

                    checkUP.A_Investment = A;
                    checkUP.A_name = A_name_update;
                    checkUP.A_lastname = A_lastname_update;
                    checkUP.A_tel = A_tel_update;
                    checkUP.A_no = A_no_update;
                    checkUP.A_subD = A_subD_update;
                    checkUP.A_sub = A_sub_update;
                    checkUP.A_ProV = A_ProV_update;



                    db.SaveChanges();
                    ViewBag.Message = "บันทึกสำเร็จ";

                    Session["ADuser"] = checkUP.AID;

                    Session["ADname"] = checkUP.A_name;

                    Session["ADlast"] = checkUP.A_lastname;

                    Session["ADtel"] = checkUP.A_tel;

                    Session["ADno"] = checkUP.A_no;

                    Session["ADsubD"] = checkUP.A_subD;

                    Session["ADsub"] = checkUP.A_sub;

                    Session["ADproV"] = checkUP.A_ProV;

                    Session["ADInvestment"] = checkUP.A_Investment;





                }

            }
            return View();
        }


        public ActionResult Circulation()
        {
            var circulation = new List<Circulation>();
            var CAID = Request.Cookies["AID"].Value;
            var checkeA = db.admins.Where(a => a.AID.Equals(CAID)).FirstOrDefault();
            if (checkeA != null)
            {
                var Fame = db.Farms.Where(a => a.AID == (checkeA.AID)).FirstOrDefault();
                if (Fame != null)
                {
                    circulation = db.Circulations.Where(a => a.FID == (Fame.FID)).ToList();
                }
            }

            return View(circulation);

        }


        public ActionResult viewRevenue()
        {

            bool result = Request.Cookies["AID"] != null;
            if (result == true)
            {

                var tem_AID = Request.Cookies["AID"].Value;
                var checkA = db.Farms.Where(a => a.AID.Equals(tem_AID)).FirstOrDefault();
                int FID = Convert.ToInt32(checkA.FID);
                var invest = checkA.Invest;
                var checkC = db.Circulations.Where(a => a.FID == (FID) && a.Invest == (invest)).FirstOrDefault<Circulation>();
                var CID = checkC.CID;


                string query2 = "SELECT * FROM Revenues WHERE CID = '" + CID + "'";
                var test = db.Database.SqlQuery<Revenue>(query2).ToList();
                return View(Tuple.Create(test));
            }
            else
            {
                return RedirectToAction("login", "login");
            }

        }



        public ActionResult viewExpenditure()
        {
            bool result = Request.Cookies["AID"] != null;
            if (result == true)
            {

                var tem_AID = Request.Cookies["AID"].Value;
            var checkA = db.Farms.Where(a => a.AID.Equals(tem_AID)).FirstOrDefault();
            int FID = Convert.ToInt32(checkA.FID);
            var invest = checkA.Invest;
            var checkC = db.Circulations.Where(a => a.FID == (FID) && a.Invest == (invest)).FirstOrDefault<Circulation>();
            var CID = checkC.CID;


            string query2 = "SELECT * FROM Expenditure WHERE CID = '" + CID + "'";
            var test = db.Database.SqlQuery<Expenditure>(query2).ToList();
            return View(Tuple.Create(test));
            }
            else
            {
                return RedirectToAction("login", "login");
            }
        }

        public ActionResult CheckProfit()
        {
            bool result = Request.Cookies["AID"] != null;
            if (result == true)
            {

                int sum = 0;



                var tem_AID = Request.Cookies["AID"].Value;
                var checkF = db.Farms.Where(a => a.AID.Equals(tem_AID)).FirstOrDefault();
                var FID = checkF.FID;
                var invest = checkF.Invest;
                var checkC = db.Circulations.Where(a => a.FID == (FID) && a.Invest == (invest)).FirstOrDefault();
                var cir = checkC.Circulation1;
                var circul = Convert.ToDouble(cir);

                var checkA = db.admins.Where(a => a.AID.Equals(tem_AID)).FirstOrDefault();
                var checkP = db.Partners.Where(a => a.AID.Equals(tem_AID) && a.Invest == (invest)).FirstOrDefault();



                string query = "SELECT * FROM admin WHERE AID = '" + tem_AID + "'";
                var test = db.Database.SqlQuery<admin>(query).ToList();



                string query2 = "SELECT * FROM Partner WHERE AID = '" + tem_AID + "' AND Invest = '" + invest + "'";
                var test2 = db.Database.SqlQuery<Partner>(query2).ToList();


                foreach (var item in test)
                {
                    sum = sum + item.A_Investment.Value;
                }

                foreach (var item in test2)
                {
                    sum = sum + item.P_investment.Value;
                }

                foreach (var item in test)
                {
                    double A_profit1 = Convert.ToDouble(checkA.A_Investment.Value);
                    double A_profit = (A_profit1 / sum) * 100;
                    A_profit = (A_profit / 100) * circul;
                    int A_profit2 = Convert.ToInt32(A_profit);
                    if (checkA.AID != null)
                    {
                        var checkupdate = db.admins.Where(a => a.AID.Equals(checkA.AID)).FirstOrDefault();
                        checkupdate.Profit = A_profit2;
                        db.SaveChanges();
                    }
                }

                foreach (var item in test2)
                {
                    double P_profit = Convert.ToDouble(item.P_investment.Value);
                    double P_profit2 = (P_profit / sum) * 100;
                    P_profit2 = (P_profit2 / 100) * circul;
                    int P_profit3 = Convert.ToInt32(P_profit2);
                    if (checkP.PID != null)
                    {
                        var checkupdate = db.Partners.Where(a => a.PID.Equals(item.PID)).FirstOrDefault();
                        checkupdate.Profit = P_profit3;
                        db.SaveChanges();
                    }
                }


                string query3 = "SELECT * FROM admin WHERE AID = '" + tem_AID + "'";
                var test3 = db.Database.SqlQuery<admin>(query).ToList();



                string query4 = "SELECT * FROM Partner WHERE AID = '" + tem_AID + "' AND Invest = '" + invest + "'";
                var test4 = db.Database.SqlQuery<Partner>(query2).ToList();


                return View(Tuple.Create(test3, test4));
            }
            else
            {
                return RedirectToAction("login", "login");
            }
            }


        public ActionResult Index()

        {
            return View();
        }

        public ActionResult ChangPassword()
        {
            bool result = Request.Cookies["AID"] != null;
            if (result == true)
            {

                return View();
            }
            else
            {
                return RedirectToAction("login", "login");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangPassword(admin ad)
        {

            string password = Request.Form["password"];
            string newPass = Request.Form["Pass"];
            string cfnewPass = Request.Form["cfnewPass"];
            var CAID = Request.Cookies["AID"].Value;

            var checkeA = db.admins.Where(a => a.AID.Equals(CAID)).FirstOrDefault();

            if (ModelState.IsValid)
            {


                if (checkeA.A_pass.Equals(password))
                {
                    if (newPass.Equals(cfnewPass))
                    {
                        checkeA.A_pass = newPass;
                        db.SaveChanges();
                        ViewBag.save = "แก้ไขสำเร็จ";
                    }
                    else
                    {
                        ViewBag.Error1 = "รหัสผ่านไม่ตรงกัน";
                    }
                }
                else
                {
                    ViewBag.Error = "รหัสผ่านผิด";
                }


                string query2 = "SELECT * FROM Farm ";
                var test = db.Database.SqlQuery<Farm>(query2).ToList();
                return View(Tuple.Create(test));
            }

            return View();
        }







    }
}