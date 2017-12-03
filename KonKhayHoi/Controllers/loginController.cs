using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KonKhayHoi.Models;
using System.Data.Entity.Validation;

namespace KonKhayHoi.Controllers
{

    public class loginController : Controller
    {
        KonKhayHoiEntities db = new KonKhayHoiEntities();
        // GET: login


        public ActionResult login()
        {

            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(admin Pro)
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            if (ModelState.IsValid)
            {
                var checkA = db.admins.Where(a => a.AID.Equals(username) && a.A_pass.Equals(password)).FirstOrDefault();
                var checkP = db.Partners.Where(a => a.PID.Equals(username) && a.P_pass.Equals(password)).FirstOrDefault();
                var checkS = db.Shops.Where(a => a.SID.Equals(username) && a.S_pass.Equals(password)).FirstOrDefault();
                if (checkA != null)
                {
                    var cookie_AID = new HttpCookie("AID");
                    cookie_AID.Value = checkA.AID;
                    Response.Cookies.Add(cookie_AID);

                    Session["A_name"] = checkA.A_name;
                    Session["A_lastname"] = checkA.A_lastname;


                    var AID = Request.Cookies["AID"].Value;
                    var checkF = db.Farms.Where(a => a.AID.Equals(AID)).FirstOrDefault();
                    bool result = checkF == null;

                    if (result == true)
                    {
                        return RedirectToAction("dataFarm", "farm");
                    }
                    else
                    {
                        return RedirectToAction("ViewdataFarm", "farm");
                    }

                }
                else if (checkP != null)
                {
                    var cookie_PID = new HttpCookie("PID");
                    cookie_PID.Value = checkP.PID;
                    Response.Cookies.Add(cookie_PID);

                    Session["P_name"] = checkP.P_name;
                    Session["P_lastname"] = checkP.P_lastname;

                    return RedirectToAction("PNcheckProfit", "partner");
                }
                else if (checkS != null)
                {
                    var cookie_SID = new HttpCookie("SID");
                    cookie_SID.Value = checkS.SID;
                    Response.Cookies.Add(cookie_SID);

                    Session["S_name"] = checkS.S_name;
                    Session["S_lastname"] = checkS.S_lastname;

                    return RedirectToAction("STdatastore", "shop");
                }
                else
                {
                    ViewBag.Message = "ชื่อผู้ใช้ หรือรหัสผ่านผิด";
                }


            }
           
            return View();
        }

    }

        
        
    
}