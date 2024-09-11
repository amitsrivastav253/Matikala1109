using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Web;
using System.Web.Mvc;
using Matikala.Models;
using System.Data;
using Matikala.DBLayer;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
namespace Matikala.Controllers
{
    public class LoginController : Controller
    {
        DB_Layer DB = new DB_Layer();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult UserLogin()
        //{
        //    UserLoginModel Model = new UserLoginModel();
        //    ViewBag.ButtonName = "ओटीपी भेजें";
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult UserLogin(UserLoginModel Model, string Command)
        //{
        //    if (Command != "" || Command != null)
        //    {
        //        try
        //        {
        //            if (Command == "ओटीपी भेजें")
        //            {
        //                if (Model.MobileNo != null)
        //                {
        //                    Random generator = new Random();
        //                    String num = generator.Next(0, 999999).ToString("D6");
        //                    //num = "123456";
        //                    var strNumber = Model.MobileNo;
        //                    var code = num;
        //                    Session["OTP"] = num;
        //                    if (Model.MobileNo != null)
        //                    {
        //                        DB.SendSMS(Model.MobileNo, "B-PACS सा0स0समिति का सदस्य बनने हेतु OTP-" + num + " MECTOI");
        //                        //DB.SendSMS(Model.MobileNo, "UPPACS बी-पैक्स साधन सहकारी समिति का सदस्य बनने हेतु ओटीपी - "+ num +" है | Powered By Mectoi Technologies");
        //                        //DB.SendOtpSMS(Model.MobileNo, "MECTOI बी-पैक्स साधन सहकारी समिति का सदस्य बनने हेतु ओटीपी - " + num + " है |");
        //                    }
        //                    TempData["Message"] = "1";
        //                    //TempData["OTP"] = num;
        //                    //Model.OTP = num;
        //                    ViewBag.Status = "Verify";
        //                    ViewBag.ButtonName = "ओटीपी सत्यापित करें";
        //                    Session["MobileNo"] = Model.MobileNo;

        //                }
        //            }

        //            if (Command == "ओटीपी सत्यापित करें")
        //            {
        //                string test = Session["OTP"].ToString();
        //                if (Model.OTP == test)
        //                {
        //                    Model.ProcId = 2;
        //                    var list = DB.Proc_MemberRegister(Model).ToList();
        //                    if (list.Count > 0)
        //                    {
        //                        TempData["Message"] = "4";
        //                        ViewBag.Status = "Verified";
        //                        ViewBag.ButtonName = "सत्यापित";
        //                        Session["DetailId"] = list[0].DetailId;
        //                        Session["UserId"] = list[0].UserId;
        //                        Session["MobileNo"] = list[0].MobileNo;
        //                        Session["PaymentStatus"] = list[0].PaymentStatus;
        //                        Session["UserType"] = "0";
        //                        //if (list[0].DetailsStatus == 0)
        //                        //{
        //                        //    return RedirectToAction("Dashboard", "Sahkarita", new { UserId = list[0].UserId });
        //                        //}
        //                        if (list[0].DetailsStatus == 1)
        //                        {
        //                            return RedirectToAction("MemberDetailsSummary", "Sahkarita");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Model.ProcId = 1;
        //                        var list1 = DB.Proc_MemberRegister(Model).ToList();
        //                        if (list1.Count > 0)
        //                        {
        //                            TempData["Message"] = "4";
        //                            ViewBag.Status = "Verified";
        //                            ViewBag.ButtonName = "सत्यापित";
        //                            Session["UserType"] = "0";
        //                            Session["DetailId"] = list[0].DetailId;
        //                            Session["UserId"] = list1[0].UserId;
        //                            Session["PaymentStatus"] = "FAILED";
        //                            return RedirectToAction("Dashboard", "Sahkarita", new { UserId = list1[0].UserId });
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    ViewBag.Status = "Verify";
        //                    ViewBag.ButtonName = "ओटीपी सत्यापित करें";
        //                    TempData["Message"] = "2";
        //                }
        //            }
        //            if (Command == "Already have OTP")
        //            {
        //                Model.Action = "Register";
        //                ViewBag.Status = "Verify";
        //                ViewBag.ButtonName = "ओटीपी सत्यापित करें";
        //            }
        //            if (Command == "ओटीपी दोबारा भेजें")
        //            {
        //                if (Model.MobileNo != null)
        //                {
        //                    Random generator = new Random();
        //                    String num = generator.Next(0, 999999).ToString("D6");
        //                    var strNumber = Model.MobileNo;
        //                    var code = num;
        //                    Session["OTP"] = num;
        //                    if (Model.MobileNo != null)
        //                    {
        //                        DB.SendSMS(Model.MobileNo, "B-PACS सा0स0समिति का सदस्य बनने हेतु OTP-" + num + " MECTOI");
        //                        //DB.SendSMS(Model.MobileNo, "UPPACS बी-पैक्स साधन सहकारी समिति का सदस्य बनने हेतु ओटीपी - " + num + " है | Powered By Mectoi Technologies");
        //                        //DB.SendSMS(Model.MobileNo, "Mectoi- Your One time password (OTP) is: " + num);
        //                    }
        //                    ViewBag.Status = "Verify";
        //                    ViewBag.ButtonName = "ओटीपी सत्यापित करें";
        //                    TempData["Message"] = "1";
        //                    TempData["OTP"] = num;
        //                    Model.OTP = num;
        //                    //if (Model.Email != null)
        //                    //{
        //                    //    string subject = "OTP For Registration Of Grievence Redresal Portal";
        //                    //    string body = "This Is Your One Time Password (OTP) For Registration Of Grivence Registration :- " + code;
        //                    //    SendMailManual(Model.Email, code, subject, body);
        //                    //}
        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            Model.Action = "Register";
        //            TempData["Message"] = "5";
        //            ViewBag.ButtonName = "ओटीपी भेजें";
        //            ViewBag.Status = "sendotp";
        //            return View(e.Message.ToString());
        //        }
        //    }
        //    return View(Model);
        //}

        public ActionResult UserRegister()
        {
            UserLoginModel Model = new UserLoginModel();
            return View();
        }
        public ActionResult UserRegisterLogin()
        {
            UserLoginModel Model = new UserLoginModel();
            return RedirectToAction("MemberDash", "Home");
        }

        public ActionResult FirstChangePassword()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            DepartmentLogin Model = new DepartmentLogin();
            Model.UserId = Convert.ToInt32(Session["UserId"].ToString());
            Model.UserType = Convert.ToInt32(Session["UserType"].ToString());
            return View();
        }

        [HttpPost]
        public ActionResult FirstChangePassword(DepartmentLogin Model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    Model.UserId = Convert.ToInt32(Session["UserId"].ToString());
                    Model.UserType = Convert.ToInt32(Session["UserType"].ToString());
                    if (Model.UserType == 3)
                    {
                        Model.ProcId = 1;
                    }
                    if (Model.UserType == 4)
                    {
                        Model.ProcId = 2;
                    }
                    if (Model.UserType == 2)
                    {
                        Model.ProcId = 3;
                    }
                    if (Model.UserType == 5)
                    {
                        Model.ProcId = 4;
                    }
                    if (Model.UserType == 6)
                    {
                        Model.ProcId = 6;
                    }
                    if (Model.UserType == 1)
                    {
                        Model.ProcId = 7;
                    }

                    var list = DB.Proc_ChangeFirstPassword(Model).ToList();
                    if (list[0].msg == "success")
                    {
                        TempData["Message"] = "Password Changed Successfully. Please Login again with new Password.";
                        ModelState.Clear();
                        return RedirectToAction("UserLogin", "Login");
                    }
                    if (list[0].msg == "not match")
                    {
                        TempData["Message"] = "Old Password is Incorrect!";
                    }
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    return RedirectToAction("FirstChangePassword", "Login");
                }
            }
            catch (Exception e)
            {
                TempData["Message"] = "Something went wrong! Try Again Later.";
                return View();
            }
            return View();
        }





        public ActionResult UserLoginRegister()
        {
            UserLoginModel Model = new UserLoginModel();
            return View();
        }


        public ActionResult DeparmentLogin()
        {
            DepartmentLogin Model = new DepartmentLogin();
            // Model.UserType = Convert.ToInt32(Session["UserType"].ToString());
            return View();
        }
        //[HttpPost]
        //public ActionResult DeparmentLogin(DepartmentLogin Model)
        //{
        //    if (Model.UserType == 1)
        //    {
        //        return RedirectToAction("DashboardAdmin", "Admin");
        //    }
        //    else if (Model.UserType == 2)
        //    {
        //        return RedirectToAction("DashboardDCB", "Admin");
        //    }
        //    else if (Model.UserType == 3)
        //    {
        //        return RedirectToAction("DashboardDistrict", "Admin");
        //    }
        //    else if (Model.UserType == 4)
        //    {
        //        return RedirectToAction("DashboardPacs", "Admin");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}
        [HttpPost]
        public ActionResult DeparmentLogin(DepartmentLogin Model)
        {
            Model.ProcId = Model.UserType;
            Model.IULogin = DB.GetPasswrdDetails(Model).ToList();
            if (Model.IULogin.Count > 0)
            {
                if (Model.IULogin[0].msg == "success")
                {
                    Session["FinancialId"] = Model.IULogin[0].FinancialId;
                    Session["UserId"] = Model.IULogin[0].UserId;
                    Session["UserName"] = Model.IULogin[0].UserName;
                    Session["UserType"] = Model.IULogin[0].UserType;
                    Session["BlockId"] = Model.IULogin[0].BlockId;
                    Session["DistrictCode"] = Model.IULogin[0].DistCode;
                    Session["DivisionCode"] = Model.IULogin[0].DivCode;
                    Session["TraningId"] = Model.IULogin[0].TraningId;
                    Session["IsFirstLogin"] = Model.IULogin[0].IsFirstLogin;
                    Session["BranchCode"] = Model.IULogin[0].BranchCode;
                    Model.IsFirstLogin = Model.IULogin[0].IsFirstLogin;
                    Model.UserType = Model.IULogin[0].UserType;

                    if (Model.UserType == 1)
                    {
                        if (Model.IsFirstLogin == "F")
                        {
                            return RedirectToAction("FirstChangePassword", "Login");
                        }
                        else
                        {
                            return RedirectToAction("MatikalaDash", "Login");
                        }
                    }
                    else if (Model.UserType == 2)
                    {
                        if (Model.IsFirstLogin == "F")
                        {
                            return RedirectToAction("FirstChangePassword", "Login");
                        }
                        else
                        {
                            return RedirectToAction("MatikalaDash", "Login");
                        }
                    }
                    else if (Model.UserType == 3)
                    {
                        if (Model.IsFirstLogin == "F")
                        {
                            return RedirectToAction("FirstChangePassword", "Login");
                        }
                        else
                        {
                            return RedirectToAction("MatikalaDash", "Login");
                        }
                    }
                    else if (Model.UserType == 4)
                    {
                        if (Model.IsFirstLogin == "F")
                        {
                            return RedirectToAction("FirstChangePassword", "Login");
                        }
                        else
                        {


                            return RedirectToAction("MatikalaDash", "Login");
                        }
                    }
                    else if (Model.UserType == 5)
                    {
                        if (Model.IsFirstLogin == "F")
                        {
                            return RedirectToAction("FirstChangePassword", "Login");
                        }
                        else
                        {
                            return RedirectToAction("ApplicantDash", "Login");
                        }
                    }
                    else if (Model.UserType == 6)
                    {
                        if (Model.IsFirstLogin == "F")
                        {
                            return RedirectToAction("FirstChangePassword", "Login");
                        }
                        else
                        {
                            return RedirectToAction("MatikalaDash", "Login");
                        }
                    }

                    else
                    {
                        TempData["Message"] = "2";
                        ModelState.Clear();
                    }
                }
                if (Model.IULogin[0].msg == "fail")
                {
                    TempData["Message"] = "1";
                    Model.Password = "";
                }
                if (Model.IULogin[0].msg == "Locked")
                {
                    TempData["Message"] = "5";
                    Model.Password = "";
                }
            }
            else
            {
                TempData["Message"] = "1";
                Model.Password = "";
            }

            return View();

        }
        public ActionResult MemberLogout()
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Clear();
            return RedirectToAction("UserLogin", "Login");
        }
        public ActionResult DepartmentLogout()
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Clear();
            return RedirectToAction("UserLogin", "Login");
        }

        public ActionResult Dashboard()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            return View();
        }
        //public ActionResult MatiKalaDash()
        //{
        //    if (Session["UserId"] == null)
        //    {
        //        return RedirectToAction("UserLogin", "Login");
        //    }

        //    DashboardDataModel Model = new DashboardDataModel();
        //    Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
        //    Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
        //    //Model.BranchId = Convert.ToInt32(Session["BranchId"].ToString());



        //    //Model = DB.proc_Registration(Model).FirstOrDefault();
        //    // For UserType Admin

        //    return View();
        //}


        public ActionResult MatiKalaDash()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            DashboardDataModel Model = new DashboardDataModel();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            Model.Id = Convert.ToInt32(Session["UserId"].ToString());
            Model.ProcId = 1;
            Model = DB.Proc_JobDashboardData(Model).FirstOrDefault();
            Model.ProcId = 2;
            var list1 = DB.Proc_JobDashboardData(Model).ToList();
            Model.ProcId = 3;
            var list2 = DB.Proc_JobDashboardData(Model).ToList();
            Model.ProcId = 4;
            var list3 = DB.Proc_JobDashboardData(Model).ToList();
            Model.ProcId = 5;
            var list4 = DB.Proc_JobDashboardData(Model).ToList();
            if (list1.Count > 0)
            {
                ViewBag.ListData1 = list1;
                ViewBag.Status1 = "Show";
            }
            else
            {
                ViewBag.ListData1 = null;
                TempData["Message1"] = "No Record Found!";
            }
            if (list2.Count > 0)
            {
                ViewBag.ListData2 = list2;
                ViewBag.Status2 = "Show";
            }
            else
            {
                ViewBag.ListData2 = null;
                TempData["Message2"] = "No Record Found!";
            }
            if (list3.Count > 0)
            {
                ViewBag.ListData3 = list3;
                ViewBag.Status3 = "Show";
            }
            else
            {
                ViewBag.ListData3 = null;
                TempData["Message3"] = "No Record Found!";
            }
            if (list4.Count > 0)
            {
                ViewBag.ListData4 = list4;
                ViewBag.Status4 = "Show";
            }
            else
            {
                ViewBag.ListData4 = null;
                TempData["Message4"] = "No Record Found!";
            }

            TempData["msg"] = "00001";
            return View(Model);
        }

        public ActionResult ApplicantDash(string SchemeCode = "")
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Registration Model = new Registration();
            Model.Id = Convert.ToInt32(Session["UserId"].ToString());
            Model.ProcId = 6;
            Model.SchemeCode = SchemeCode;
            var list = DB.proc_Registration(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.SchemeCode = list[0].SchemeCode;
                Model.SchemeCode = SchemeCode;
                ViewBag.listdata = list;
            }
            else
            {
                ViewBag.listdata = null;
            }
            return View();
        }

        public JsonResult getFinancialYear(int Procid, string FinYear,string SchemeCode, int UserTypeId, int DistrictId, int BranchId, int DivCode, int TraningId)
        {
            DashboardDataModel Model = new DashboardDataModel();
            Model.UserTypeId = UserTypeId;
            Model.DistrictId = DistrictId;
            Model.BranchId = BranchId;
            Model.DivCode = DivCode;
            Model.TraningId = TraningId;
            Model.ProcId = Procid;
            Model.FinYear = FinYear;
            Model.SchemeCode = SchemeCode;
            var list = DB.Proc_JobDashboardData(Model).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getSchemeCode(int Procid, string FinYear,string SchemeCode, int UserTypeId, int DistrictId, int BranchId, int DivCode, int TraningId)
        {
            DashboardDataModel Model = new DashboardDataModel();
            Model.UserTypeId = UserTypeId;
            Model.DistrictId = DistrictId;
            Model.BranchId = BranchId;
            Model.DivCode = DivCode;
            Model.TraningId = TraningId;
            Model.ProcId = Procid;
            Model.FinYear = FinYear;
            Model.SchemeCode = SchemeCode;
            var list = DB.Proc_JobDashboardData(Model).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGraph(int Procid, string FinYear, string SchemeCode, int UserTypeId, int DistrictId, int BranchId)
        {
            DashboardDataModel Model = new DashboardDataModel();
            Model.UserTypeId = UserTypeId;
            Model.DistrictId = DistrictId;
            Model.BranchId = BranchId;
            Model.ProcId = Procid;
            //Model.FinYear = FinYear;
            //Model.TotalGeneral = TotalGeneral;
            //Model.TotalMinority = TotalMinority;
            //Model.TotalOBC = TotalOBC;
            //Model.TotalSC = TotalSC;
            //Model.TotalST = TotalST;
            //Model.ProcId = Procid;
            Model.FinYear = FinYear;
            Model.SchemeCode = SchemeCode;
            var list = DB.Proc_JobDashboardData(Model).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TraningEmp(int Procid=0, string FinancialYear="", string SchemeCode="", int DistrictId=0, int UserTypeId=0,int DivisionId=0,int ToolkitId=0)
        {
            ProgressMaster Model = new ProgressMaster();
            Model.ToolkitId = ToolkitId;
            Model.DistrictId = DistrictId;
            Model.DivisionId = DivisionId;
            Model.FinancialYear = FinancialYear;
            Model.ProcId = Procid;
            Model.SchemeCode = SchemeCode;
            Model.UserTypeId = UserTypeId;
            var list = DB.Proc_UploadSuchi(Model).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SheduledTraning(int Procid=0, string FinancialYear="", string SchemeCode="", int TrainingOfficeId = 0,string FromDate="",string ToDate = "")
        {
            ProgressMaster Model = new ProgressMaster();
            Model.TrainingOfficeId = TrainingOfficeId;
            Model.FinancialYear = FinancialYear;
            Model.FromDate = FromDate;
            Model.ToDate = ToDate;
            Model.ProcId = Procid;
            Model.SchemeCode = SchemeCode;
            var list = DB.Proc_TraningSheduled(Model).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TrainingCompletedPro(int id, int Fid, string TrainingCompletionDate="")
        {
            ProgressMaster Model = new ProgressMaster();
            Model.ProcId = 8;
           
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.TrainingCompletionDate = TrainingCompletionDate;
            Model.Id = id;
            Model.TraningStatus = Fid;
            var data = DB.Proc_TraningSheduled(Model).FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SwapFinYear(int Procid = 0, string FinYear = "", string SchemeCode = "", int DistrictId = 0)
        {
            Registration Model = new Registration();
            Model.DistrictId = DistrictId;
            Model.FinYear = FinYear;
            Model.ProcId = Procid;
            Model.SchemeCode = SchemeCode;
            var list = DB.proc_Registration(Model).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Userlogin()
        {
            return View();
        }

        public JsonResult ReturnForm(int Procid = 0, string FinYear = "", string SchemeCode = "", int DistrictId = 0)
        {
            Closure Model = new Closure();
            Model.DistrictId = DistrictId;
            Model.FinYear = FinYear;
            Model.ProcId = Procid;
            Model.SchemeCode = SchemeCode;
            var list = DB.Proc_Closure(Model).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}