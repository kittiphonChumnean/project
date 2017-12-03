using KonKhayHoi.Models;
using System;
using System.Linq;
using System.Web.Mvc;



namespace KonKhayHoi.Controllers
{
    public class farmController : Controller
    {
        private KonKhayHoiEntities db = new KonKhayHoiEntities();
        // GET: farm
        public ActionResult Startnewinvast()
        {

            return View();
        }

        public ActionResult ConFStartnewinvast()
        {
            var CAID = Request.Cookies["AID"].Value;
            var checkA = db.Farms.Where(a => a.AID.Equals(CAID)).FirstOrDefault();
            var checkI = checkA.Invest.Value;
            if (checkA != null)
            {
                checkA.Invest = checkI + 1;
                checkA.MonthOfSale = null;
                checkA.dateStart = null;


                db.SaveChanges();
            }
            return View();
        }

        public ActionResult dataFarm()
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
        public ActionResult DataFarm(admin pro)
        {
            var CAID = Request.Cookies["AID"].Value;
            var checkC = db.Farms.Where(a => a.AID.Equals(CAID)).FirstOrDefault<Farm>();


            Farm farm = new Farm();


            farm.Invest = 1;
            farm.F_name = Request.Form["name"];
            farm.F_tel = Request.Form["tel"];
            farm.dateStart = Request.Form["dateStart"];
            farm.MonthOfSale = Request.Form["MonthOfSale"];
            farm.F_no = Request.Form["no"];
            farm.F_subD = Request.Form["subD"];
            farm.F_sub = Request.Form["sub"];
            farm.F_ProV = Request.Form["proV"];
            farm.AID = CAID;

            db.Farms.Add(farm);
            db.SaveChanges();
            ViewBag.Message = "บันทึกสำเร็จ";


            return View();
        }


        public ActionResult ViewdataFarm()
        {
            if (ModelState.IsValid)
            {

                var CAID = Request.Cookies["AID"].Value;
                var checkW = db.Farms.Where(a => a.AID.Equals(CAID)).FirstOrDefault();


                if (checkW != null)
                {


                    Session["F_name"] = checkW.F_name;

                    Session["F_tel"] = checkW.F_tel;

                    Session["F_no"] = checkW.F_no;

                    Session["F_subD"] = checkW.F_subD;

                    Session["F_sub"] = checkW.F_sub;

                    Session["F_ProV"] = checkW.F_ProV;

                    Session["dateStart"] = checkW.dateStart;

                    Session["MonthOfSale"] = checkW.MonthOfSale;


                }




                string F_noUP = Request.Form["F_no"];
                string dateStart_update = Request.Form["dateStart"];
                string MonthOfSale_update = Request.Form["MonthOfSale"];


                if (F_noUP != null)
                {
                    Farm farm = new Farm();

                    var checkUP = db.Farms.Where(a => a.F_no.Equals(F_noUP)).FirstOrDefault();


                    checkUP.dateStart = dateStart_update;
                    checkUP.MonthOfSale = MonthOfSale_update;

                    db.Farms.Add(farm);
                    db.SaveChanges();
                    ViewBag.Message = "บันทึกสำเร็จ";

                    Session["F_name"] = checkUP.F_name;

                    Session["F_tel"] = checkUP.F_tel;

                    Session["F_no"] = checkUP.F_no;

                    Session["F_subD"] = checkUP.F_subD;

                    Session["F_sub"] = checkUP.F_sub;

                    Session["F_ProV"] = checkUP.F_ProV;

                    Session["dateStart"] = checkUP.dateStart;

                    Session["MonthOfSale"] = checkUP.MonthOfSale;



                }
            }

            return View();

        }

       

        

        public ActionResult addRevenue()
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
        public ActionResult addRevenue(int weight,int amount)
        {

            var tem_AID = Request.Cookies["AID"].Value;
            var checkA = db.Farms.Where(a => a.AID.Equals(tem_AID)).FirstOrDefault();
            int FID = Convert.ToInt32(checkA.FID);
            var invest = checkA.Invest;
            var checkC = db.Circulations.Where(a => a.FID == (FID) && a.Invest == (invest)).FirstOrDefault<Circulation>();

            Revenue revenue = new Revenue();

            revenue.Weight = weight;
            revenue.amount = amount;
            revenue.R_date = Request.Form["date"];
            revenue.R_list = Request.Form["list"];
            revenue.Shop = Request.Form["shop"];
            revenue.payee = Request.Form["payee"];
           


            if (ModelState.IsValid)
            {
               
                if ((checkC != null))
                {
                    var amount_c = checkC.Circulation1;
                    var CID = checkC.CID;
                    if (CID != null)
                    {
                        var checkupdate = db.Circulations.Where(a => a.CID.Equals(CID)).FirstOrDefault();
                        checkupdate.Circulation1 = amount_c + amount;
                        db.SaveChanges();
                        revenue.CID = checkC.CID;
                    }

                }
                else if ((checkC == null))
                {
                    Circulation circulation = new Circulation();
                    circulation.Invest = invest;
                    circulation.Circulation1 = amount;
                    circulation.FID = FID;

                    db.Circulations.Add(circulation);
                    db.SaveChanges();
                    var check = db.Circulations.Where(a => a.Invest == (invest) && a.FID == (FID)).FirstOrDefault();
                    revenue.CID = check.CID;
                    db.SaveChanges();

                }

                db.Revenues.Add(revenue);
                db.SaveChanges();
                ViewBag.Message = "บันทึกสำเร็จ";

                

            }
            
            return View();

        }

        public ActionResult addFarm()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addFarm( int investment)
        {

            admin addfarm = new admin();


            var checkNo = db.lasts.Where(a => a.no == 1).FirstOrDefault();

            var A = checkNo.lastA.Value;
            String B = Convert.ToString(A);
            if (A < 10)
            {
                B = "A000" + B;
            }
            else if (A < 100)
            {
                B = "A00" + B;
            }
            else if (A < 1000)
            {
                B = "A0" + B;
            }
            else if (A < 10000)
            {
                B = "A" + B;
            }



            var checkUP = db.lasts.Where(a => a.no == 1).FirstOrDefault();


            checkUP.lastA = A + 1;



            db.SaveChanges();




            addfarm.AID = B;
            addfarm.A_Investment = investment;
            addfarm.A_name = Request.Form["name"];
            addfarm.A_lastname = Request.Form["lastname"];
            addfarm.A_no = Request.Form["no"];
            addfarm.A_subD = Request.Form["subD"];
            addfarm.A_sub = Request.Form["sub"];
            addfarm.A_ProV = Request.Form["proV"];
            addfarm.A_tel = Request.Form["tel"];
            addfarm.A_pass = Request.Form["pass"];

            if (Request.Form["Pass"] == Request.Form["Pass2"])
            {



                if (ModelState.IsValid)
                {
                    db.admins.Add(addfarm);
                    db.SaveChanges();
                    ViewBag.Message = "บันทึกสำเร็จ ชื่อผู้ใช้ของคุณคือ " + B;
                    ViewBag.Message1 = "กรุณาจำชื่อผู้ใช้นี้เพื่อเข้าสู้ระบบครั้งต่อไป";


                }
            }
            else
            {
                ViewBag.Message2 = "รหัสผ่านไม่ตรงกัน";
            }

            return View();

        }
       


        

        public ActionResult addExpenditure()
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
        public ActionResult addExpenditure(int amount)
        {
            var tem_AID = Request.Cookies["AID"].Value;
            var checkA = db.Farms.Where(a => a.AID.Equals(tem_AID)).FirstOrDefault();
            int FID = Convert.ToInt32(checkA.FID);
            var invest = checkA.Invest;
            var checkC = db.Circulations.Where(a => a.FID == (FID) && a.Invest == (invest)).FirstOrDefault<Circulation>();
            var CID = checkC.CID;
            var amount_c = checkC.Circulation1;

            Expenditure expenditure = new Expenditure();
            expenditure.amount = amount;
            expenditure.E_date = Request.Form["date"];
            expenditure.E_list = Request.Form["list"];
            expenditure.payor = Request.Form["payor"];
            expenditure.note = Request.Form["note"];

           
            
            
            

            if (ModelState.IsValid)
            {
                

                if ((checkC != null))
                {
                    
                    
                    if (CID != null) {
                        var checkupdate = db.Circulations.Where(a => a.CID.Equals(CID) ).FirstOrDefault();
                        checkupdate.Circulation1 = amount_c - amount;
                        db.SaveChanges();
                        expenditure.CID = checkC.CID;
                    }

                }
                else if ((checkC == null))
                {
                    Circulation circulation = new Circulation();
                    circulation.Invest = invest;
                    circulation.Circulation1 = -amount;
                    circulation.FID = FID;
                    

                    db.Circulations.Add(circulation);
                    db.SaveChanges();
                    var check = db.Circulations.Where(a => a.Invest== (invest) && a.FID == (FID)).FirstOrDefault();
                    expenditure.CID = check.CID;
                    db.SaveChanges();

                }
                db.Expenditures.Add(expenditure);
                db.SaveChanges();
                ViewBag.Message = "บันทึกสำเร็จ";


            }

            return View();

        }



        public ActionResult viewExpenditure()
        {
            Expenditure listiview = new Expenditure();
               
            return View(listiview.ToString());
        }
    }
}