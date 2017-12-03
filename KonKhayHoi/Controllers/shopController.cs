using KonKhayHoi.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace KonKhayHoi.Controllers
{
    public class shopController : Controller
    {

        private KonKhayHoiEntities db = new KonKhayHoiEntities();

        // GET: shop
        public ActionResult STdatastore()
        {

            if (ModelState.IsValid)
            {
                var CSID = Request.Cookies["SID"].Value;
                var checkW = db.Shops.Where(a => a.SID.Equals(CSID)).FirstOrDefault();


                if (checkW != null)
                {
                    Session["Suser"] = checkW.SID;

                    Session["name"] = checkW.Name;

                    Session["Sname"] = checkW.S_name;

                    Session["Slastname"] = checkW.S_lastname;

                    Session["Stel"] = checkW.S_tel;

                    Session["Sno"] = checkW.S_no;

                    Session["SsubD"] = checkW.S_subD;

                    Session["Ssub"] = checkW.S_sub;

                    Session["SproV"] = checkW.S_ProV;

                    Session["SpurchasedAmount"] = checkW.purchasedAmount;

                    Session["Sstate"] = checkW.state;


                }

            }

            return View();
        }



        public ActionResult Viewstore()
        {
            bool result = Request.Cookies["AID"] != null;
            if (result == true)
            {

                string query2 = "SELECT * FROM Shop WHERE state='1' ";
                var test = db.Database.SqlQuery<Shop>(query2).ToList();
                return View(Tuple.Create(test));
            }
            else
            {
                return RedirectToAction("login", "login");
            }
        }



        public ActionResult PNViewstore()
        {
            bool result = Request.Cookies["PID"] != null;
            if (result == true)
            {

                string query2 = "SELECT * FROM Shop WHERE state='1' ";
                var test = db.Database.SqlQuery<Shop>(query2).ToList();
                return View(Tuple.Create(test));
            }
            else
            {
                return RedirectToAction("login", "login");
            }
        }


        public ActionResult addshop()
        {
            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addshop(admin pro, int purchasedAmount)
        {
            Shop shop = new Shop();

            var checkNo = db.lasts.Where(a => a.no == 1).FirstOrDefault();  //runเลข

            var A = checkNo.lastS.Value;
            String B = Convert.ToString(A);
            if (A < 10)
            {
                B = "S000" + B;
            }
            else if (A < 100)
            {
                B = "S00" + B;
            }
            else if (A < 1000)
            {
                B = "S0" + B;
            }
            else if (A < 10000)
            {
                B = "S" + B;
            }


            var checkUP = db.lasts.Where(a => a.no == 1).FirstOrDefault();

            checkUP.lastS = A + 1;
            db.SaveChanges();

            shop.SID = B;
            shop.Name = Request.Form["nameS"];
            shop.S_name = Request.Form["name"];
            shop.S_lastname = Request.Form["lastname"];
            shop.S_tel = Request.Form["tel"];
            shop.purchasedAmount = purchasedAmount;
            shop.S_pass = Request.Form["Pass"];
            shop.S_no = Request.Form["no"];
            shop.S_subD = Request.Form["subD"];
            shop.S_sub = Request.Form["sub"];
            shop.S_ProV = Request.Form["ProV"];
            shop.state = 1;

            if (Request.Form["Pass"] == Request.Form["Pass2"])
            {



                if (ModelState.IsValid)
                {
                    db.Shops.Add(shop);
                    db.SaveChanges();
                    ViewBag.Message = "บันทึกสำเร็จ ชื่อผู้ใช้ของคุณคือ " + B;


                }
            }
            else
            {
                ViewBag.Message2 = "รหัสผ่านไม่ตรงกัน";
            }

            return View();

        }

        
        public ActionResult STchangdatastore()

        {

            Shop shop = new Shop();

            string SID_update = Request.Form["username_update"];
            string name_update = Request.Form["name_update"];
            string S_name_update = Request.Form["names_update"];
            string S_lastname_update = Request.Form["lastname_update"];
            string S_tel_update = Request.Form["tel_update"];
            string purchasedAmount_update = Request.Form["SpurchasedAmount_update"];
            string S_no_update = Request.Form["no_update"];
            string S_subD_update = Request.Form["subD_update"];
            string S_sub_update = Request.Form["sub_update"];
            string S_ProV_update = Request.Form["proV_update"];
            string state_update = Request.Form["state_update"];

            if (ModelState.IsValid)
            {
                if (SID_update != null)
                {
                    var CSID = Request.Cookies["SID"].Value;
                    var checkUP = db.Shops.Where(a => a.SID.Equals(CSID)).FirstOrDefault();



                    int A = Convert.ToInt32(purchasedAmount_update);
                    int B = Convert.ToInt32(state_update);

                    checkUP.purchasedAmount = A;
                    checkUP.state = B;
                    checkUP.Name = name_update;
                    checkUP.S_name = S_name_update;
                    checkUP.S_lastname = S_lastname_update;
                    checkUP.S_tel = S_tel_update;
                    checkUP.S_no = S_no_update;
                    checkUP.S_subD = S_subD_update;
                    checkUP.S_sub = S_sub_update;
                    checkUP.S_ProV = S_ProV_update;


                    db.SaveChanges();
                    ViewBag.Message = "บันทึกสำเร็จ";


                    Session["username_update"] = checkUP.SID;

                    Session["name"] = checkUP.Name;

                    Session["Sname"] = checkUP.S_name;

                    Session["Slastname"] = checkUP.S_lastname;

                    Session["Stel"] = checkUP.S_tel;

                    Session["Sno"] = checkUP.S_no;

                    Session["SsubD"] = checkUP.S_subD;

                    Session["Ssub"] = checkUP.S_sub;

                    Session["SProV"] = checkUP.S_ProV;

                    Session["SpurchasedAmount"] = checkUP.purchasedAmount;

                    Session["Sstate"] = checkUP.state;


                }

            }
            return View();

        }

        //ดูข้อมูลฟาร์ม
        public ActionResult STselectdatafarm()
        {
            string query2 = "SELECT * FROM Farm ";
            var test = db.Database.SqlQuery<Farm>(query2).ToList();
            return View(Tuple.Create(test));

        }


        public ActionResult STchangPassword()
        {
            bool result = Request.Cookies["SID"] != null;
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
        public ActionResult STchangPassword(admin ad)
        {

            string password = Request.Form["password"];
            string newPass = Request.Form["Pass"];
            string cfnewPass = Request.Form["cfnewPass"];
            var CAID = Request.Cookies["SID"].Value;

            var checkeS = db.Shops.Where(a => a.SID.Equals(CAID)).FirstOrDefault();

            if (ModelState.IsValid)
            {


                if (checkeS.S_pass.Equals(password))
                {
                    if (newPass.Equals(cfnewPass))
                    {
                        checkeS.S_pass = newPass;
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


                
            }

            return View();
        }

    }
}