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
    public class partnerController : Controller
    {
        KonKhayHoiEntities db = new KonKhayHoiEntities();
        // GET: partner

        public ActionResult PNcirculation()
        {
            var circulation = new List<Circulation>();
            var CPID = Request.Cookies["PID"].Value;
            var checkeA = db.Partners.Where(a => a.PID.Equals(CPID)).FirstOrDefault();


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

        public ActionResult PNexpenditure()
        {
            bool result = Request.Cookies["PID"] != null;
            if (result == true)
            {

                var tem_PID = Request.Cookies["PID"].Value;
            var checkP = db.Partners.Where(a => a.PID.Equals(tem_PID)).FirstOrDefault();
            var tem_AID = checkP.AID;
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

        public ActionResult PNrevenue()
        {
            bool result = Request.Cookies["PID"] != null;
            if (result == true)
            {

                var tem_PID = Request.Cookies["PID"].Value;
            var checkP = db.Partners.Where(a => a.PID.Equals(tem_PID)).FirstOrDefault();
            var tem_AID = checkP.AID;
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

        public ActionResult PNcheckProfit()
        {
            bool result = Request.Cookies["PID"] != null;
            if (result == true)
            {

                int sum = 0;



                var tem_PID = Request.Cookies["PID"].Value;
                var check = db.Partners.Where(a => a.PID.Equals(tem_PID)).FirstOrDefault();
                var tem_AID = check.AID;
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

        public ActionResult ViewPN()
        {
            bool result = Request.Cookies["AID"] != null;
            if (result == true)
            {
                var AID = Request.Cookies["AID"].Value;
                var checkF = db.Farms.Where(a => a.AID.Equals(AID)).FirstOrDefault();
                var invest = checkF.Invest.Value;
                string query2 = "SELECT * FROM Partner WHERE AID='" + AID + "' AND Invest = '" + invest + "'";
                var test = db.Database.SqlQuery<Partner>(query2).ToList();
                return View(Tuple.Create(test));
            }
            else
            {
                return RedirectToAction("login", "login");
            }

        }

        public ActionResult addPN()
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
        public ActionResult addPN(int investment)
        {
            Partner partner = new Partner();

            var checkNo = db.lasts.Where(a => a.no == 1).FirstOrDefault();

            var A = checkNo.lastP.Value;
            String B = Convert.ToString(A);
            if (A < 10)
            {
                B = "P000" + B;
            }
            else if (A < 100)
            {
                B = "P00" + B;
            }
            else if (A < 1000)
            {
                B = "P0" + B;
            }
            else if (A < 10000)
            {
                B = "P" + B;
            }


            var checkUP = db.lasts.Where(a => a.no == 1).FirstOrDefault();
            checkUP.lastP = A + 1;

            var AID = Request.Cookies["AID"].Value;
            var checkF = db.Farms.Where(a => a.AID.Equals(AID)).FirstOrDefault();
            var invest = checkF.Invest.Value;

            partner.PID = B;
            partner.P_name = Request.Form["name"];
            partner.P_lastname = Request.Form["lastname"];
            partner.P_tel = Request.Form["tel"];
            partner.P_investment = investment;
            partner.P_no = Request.Form["no"];
            partner.P_pass = Request.Form["pass"];
            partner.P_sub = Request.Form["sub"];
            partner.P_subD = Request.Form["subD"];
            partner.P_ProV = Request.Form["ProV"];
            partner.AID = AID;
            partner.Invest = invest;


            if (ModelState.IsValid)
            {
                db.Partners.Add(partner);
                db.SaveChanges();
                ViewBag.Message = "บันทึกสำเร็จ ชื่อผู้ใช้ของหุ้นส่วนคือ " + B;
            }

            return View();

        }

        public ActionResult PNChangPassword()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PNChangPassword(admin ad)
        {

            string password = Request.Form["password"];
            string newPass = Request.Form["newPass"];
            string cfnewPass = Request.Form["cfnewPass"];
            var CPID = Request.Cookies["PID"].Value;

            var checkeA = db.Partners.Where(a => a.PID.Equals(CPID)).FirstOrDefault();

            if (password != null)
            {
                var check_username = db.Partners.Where(a => a.PID.Equals(CPID)).FirstOrDefault<Partner>();
                if (check_username.P_pass == password)
                {
                    if (newPass == cfnewPass)
                    {
                        check_username.P_pass = newPass;
                        db.SaveChanges();
                        ViewBag.Error3 = "เปลี่ยนรหัสผ่านสำเร็จ";
                        return View();
                    }
                    else
                    {
                        ViewBag.Error1 = "กรอกรหัสผ่านไม่ตรงกัน";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error2 = "รหัสผ่านผิด";
                    return View();
                }
            }

            return View();
        }

    }
}