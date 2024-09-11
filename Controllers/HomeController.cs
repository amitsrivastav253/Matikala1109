using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Matikala.DBLayer;
using Matikala.Models;
using System.Drawing;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace Matikala.Controllers
{
    public class HomeController : Controller
    {
        DB_Layer DB = new DB_Layer();
        public ActionResult Index()
        {
            ProgressMaster Model = new ProgressMaster();
            Model.ProcId = 6;
            var list = DB.Proc_Suchna(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
                ViewBag.Message = "No Record";
            }

            TempData["msg"] = "5";
            return View(Model);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //public ActionResult Registration()
        //{
        //    Registration Model = new Registration();
        //    Model.ProcId = 2;
        //    Model = DB.proc_Registration(Model).FirstOrDefault();
        //    ModelState.Clear();
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Registration(Registration Model, string command)
        //{
        //    if (command == "Submit")
        //    {
        //        Model.ProcId = 1;
        //    }
        //    var list = DB.proc_Registration(Model).ToList();
        //    if (list.Count >= 0)
        //    {
        //        TempData["Message"] = "1";
        //        TempData["regn"] = list[0].ApplicationId;
        //        if (list[0].ApplicationId != "")
        //        {
        //            DB.SendSMS(Model.MobileNo1, "Your Application No. is " + list[0].ApplicationId +  " and Passwors is Your Mobile No. Submitted for UP-MMRY" );
        //        }
        //    }
        //    else
        //    {
        //        TempData["Message"] = "3";
        //    }

        //    return RedirectToAction("Registration", "Home");
        //}


        public JsonResult IsLifeInsurance()
        {
            Registration Model = new Registration();
            var option = DB.Proc_InsurancePolicy(1, Model).ToList();
            return Json(option, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Registration()
        //{

        //    Registration Model = new Registration();

        //    if (Request.QueryString["sid"] != null)
        //    {
        //        Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
        //        Model.ProcId = 6;
        //        Model = DB.proc_Registration(Model).FirstOrDefault();

        //        ViewBag.ButtonName = "Update";

        //        ViewBag.BankId = Model.BankId;
        //        ViewBag.BranchId = Model.BranchId;
        //        ViewBag.BranchAddressId = Model.BranchAddressId;
        //        ViewBag.IFSCCode = Model.IFSCCode;
        //    }
        //    else
        //    {
        //        ViewBag.ButtonName = "Submit";
        //        Model.ProcId = 2;
        //        var list = DB.proc_Registration(Model).ToList();
        //        if (list.Count > 0)
        //        {
        //            ViewBag.ListData = list;
        //        }
        //        else
        //        {
        //            ViewBag.ListData = null;
        //        }

        //        //return RedirectToAction("Registration", "Home");
        //    }
        //    return View(Model);

        //}
        //[HttpPost]
        //public ActionResult Registration(Registration Model, string command)
        //{
        //    if (command == "Submit")
        //    {
        //        Model.ProcId = 1;
        //        TempData["Message"] = 1;
        //    }
        //    if (command == "Update")
        //    {
        //        Model.ProcId = 5;
        //        TempData["Message"] = 2;
        //    }

        //    List<Registration> ExpSet = JsonConvert.DeserializeObject<List<Registration>>(Model.ListJson);
        //    //List<Registration> EduSet = JsonConvert.DeserializeObject<List<Registration>>(Model.IsLifePolicy);

        //    for (int i = 0; i < ExpSet.Count; i++)
        //    {
        //        Model.IsLifePolicy += (ExpSet[i].InsuranceId.ToString());
        //        if (i < ExpSet.Count - 1)
        //        {
        //            Model.IsLifePolicy += ",";
        //        }
        //    }

        //    var list = DB.proc_Registration(Model).ToList();
        //    if (list.Count >= 0)
        //    {

        //        if (Model.ProcId == 1)
        //        {
        //            ViewBag.ListData = list;
        //            TempData["Message"] = "1";
        //            TempData["regn"] = list[0].ApplicationId;
        //            TempData["Score"] = list[0].TotalScoreNumber;
        //            //if (list[0].ApplicationId != "")
        //            //{
        //            //    DB.SendSMS(Model.MobileNo1, "Your Application No. is " + list[0].ApplicationId + " and Passwors is Your Mobile No. Submitted for UP-MMRY");
        //            //}

        //        }
        //        if (Model.ProcId == 5)
        //        {

        //            TempData["Message"] = 2;
        //        }

        //    }
        //    else
        //    {
        //        ViewBag.ListData = null;
        //    }

        //    //for (int i = 0; i < ExpSet.Count; i++)
        //    //{
        //    //    if (Model.IsLifePolicy != "")
        //    //    {
        //    //        Model.IsLifePolicy += ",";
        //    //    }
        //    //    else
        //    //    {
        //    //        Model.IsLifePolicy += (ExpSet[i].InsuranceId.ToString());
        //    //    }
        //    //}

        //    //if (Model.IsLifePolicy != null)
        //    //{
        //    //    var scm = String.Join(",", Model.IsLifePolicy);
        //    //    Model.IsLifePolicy = scm; 
        //    //}

        //    //var list = DB.proc_Registration(Model).ToList();
        //    //if (list.Count >= 0)
        //    //{
        //    //    ViewBag.ListData = list;
        //    //    TempData["Message"] = "1";
        //    //    TempData["regn"] = list[0].ApplicationId;
        //    //    TempData["Score"] = list[0].TotalScoreNumber;
        //    //    //if (list[0].ApplicationId != "")
        //    //    //{
        //    //    //    DB.SendSMS(Model.MobileNo1, "Your Application No. is " + list[0].ApplicationId + " and Passwors is Your Mobile No. Submitted for UP-MMRY");
        //    //    //}

        //    //    return RedirectToAction("Registration", "Home");
        //    //}
        //    //else
        //    //{
        //    //    ViewBag.ListData = null;
        //    //}
        //    ViewBag.ButtonName = "Submit";
        //    ModelState.Clear();
        //    return View();
        //}

        public ActionResult Registration()
        {

            Registration Model = new Registration();

            if (Request.QueryString["sid"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                Model.ProcId = 2;
                Model = DB.proc_Registration(Model).FirstOrDefault();
                Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                ViewBag.buttonname = "Update";
                Session["DocAadharCardReport"] = Model.DocAadharCardReport;
                Session["DocJatiReport"] = Model.DocJatiReport;
                Session["DocProjectReport"] = Model.DocProjectReport;
                Session["DocNiwasReport"] = Model.DocNiwasReport;
                Session["DocQualificationReport"] = Model.DocQualificationReport;
                Session["DocPhoto"] = Model.DocPhoto;
                Session["DocSign"] = Model.DocSign;

                ViewBag.CADistrict = Model.CADistrict;
                ViewBag.CATehsil = Model.HCATehsil;
                ViewBag.CABlock = Model.HCABlock;
                ViewBag.PADistrictId = Model.PADistrictId;
                ViewBag.PATehsil = Model.HPATehsil;
                ViewBag.PABlock = Model.HPABlock;
                ViewBag.FBDistrict = Model.HFBDistrict;
                ViewBag.BankId = Model.BankId;
                ViewBag.BranchId = Model.HBranchId;
                ViewBag.IsLifePolicy = Model.IsLifePolicy;
            }
            else

            {
                ViewBag.buttonname = "Submit";
                Model.ProcId = 4;
                Model.UserTypeId = 0;
                var list = DB.proc_Registration(Model).ToList();
                if (list.Count > 0)
                {
                    ViewBag.listdata = list;
                }
                else
                {
                    ViewBag.listdata = null;
                }
                Session["DocAadharCardReport"] = "";
                Session["DocJatiReport"] = "";
                Session["DocProjectReport"] = "";
                Session["DocNiwasReport"] = "";
                Session["DocQualificationReport"] = "";
                Session["DocPhoto"] = "";
                Session["DocSign"] = "";

            }
            ModelState.Clear();
            return View(Model);
        }
        [HttpPost]
        public ActionResult Registration(Registration Model, string command, HttpPostedFileBase DocAadharCardReport,
        HttpPostedFileBase DocJatiReport, HttpPostedFileBase DocProjectReport,
        HttpPostedFileBase DocNiwasReport, HttpPostedFileBase DocQualificationReport, HttpPostedFileBase DocPhoto, HttpPostedFileBase DocSign)
        {



            if (command == "Submit")
            {
                Model.ProcId = 1;
                //TempData["Message"] = 1;
            }
            if (command == "Update")
            {

                Model.ProcId = 5;
                //Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                //TempData["Message"] = 2;
            }

            string prepath = "~/Content/Document";
            string path = "";
            var uploadUrl = Server.MapPath(prepath);
            //Random r = new Random(6);
            //string number = r.ToString();
            string extensionPdf1 = System.IO.Path.GetExtension(Request.Files["DocAadharCardReport"].FileName);
            if (DocAadharCardReport != null)
            {
                if (extensionPdf1.ToLower() == ".pdf" || extensionPdf1.ToLower() == ".png" || extensionPdf1.ToLower() == ".jpeg")
                {
                    if (DocAadharCardReport.ContentLength > 0)
                    {
                        DocAadharCardReport = Request.Files["DocAadharCardReport"];
                        string Name = DateTime.Now.Ticks + "_AD" + extensionPdf1.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocAadharCardReport = pathtosave;
                        DocAadharCardReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocAadharCardReport = Session["DocAadharCardReport"].ToString();
            }

            string extensionPdf2 = System.IO.Path.GetExtension(Request.Files["DocJatiReport"].FileName);
            if (DocJatiReport != null)
            {
                if (extensionPdf2.ToLower() == ".pdf" || extensionPdf2.ToLower() == ".png" || extensionPdf2.ToLower() == ".jpeg")
                {
                    if (DocJatiReport.ContentLength > 0)
                    {
                        DocJatiReport = Request.Files["DocJatiReport"];
                        string Name = DateTime.Now.Ticks + "_Cast" + extensionPdf2.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocJatiReport = pathtosave;
                        DocJatiReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocJatiReport = Session["DocJatiReport"].ToString();
            }
            string extensionPdf3 = System.IO.Path.GetExtension(Request.Files["DocProjectReport"].FileName);
            if (DocProjectReport != null)
            {
                if (extensionPdf3.ToLower() == ".pdf" || extensionPdf3.ToLower() == ".png" || extensionPdf3.ToLower() == ".jpeg")
                {
                    if (DocProjectReport.ContentLength > 0)
                    {
                        DocProjectReport = Request.Files["DocProjectReport"];
                        string Name = DateTime.Now.Ticks + "_PR" + extensionPdf3.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocProjectReport = pathtosave;
                        DocProjectReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocProjectReport = Session["DocProjectReport"].ToString();
            }
            string extensionPdf4 = System.IO.Path.GetExtension(Request.Files["DocNiwasReport"].FileName);
            if (DocNiwasReport != null)
            {
                if (extensionPdf4.ToLower() == ".pdf" || extensionPdf4.ToLower() == ".png" || extensionPdf4.ToLower() == ".jpeg")
                {
                    if (DocNiwasReport.ContentLength > 0)
                    {
                        DocNiwasReport = Request.Files["DocNiwasReport"];
                        string Name = DateTime.Now.Ticks + "_DC" + extensionPdf4.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocNiwasReport = pathtosave;
                        DocNiwasReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocNiwasReport = Session["DocNiwasReport"].ToString();
            }

            string extensionPdf5 = System.IO.Path.GetExtension(Request.Files["DocQualificationReport"].FileName);
            if (DocQualificationReport != null)
            {
                if (extensionPdf5.ToLower() == ".pdf" || extensionPdf5.ToLower() == ".png" || extensionPdf5.ToLower() == ".jpeg")
                {
                    if (DocQualificationReport.ContentLength > 0)
                    {
                        DocQualificationReport = Request.Files["DocQualificationReport"];
                        string Name = DateTime.Now.Ticks + "_QF" + extensionPdf5.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocQualificationReport = pathtosave;
                        DocQualificationReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocQualificationReport = Session["DocQualificationReport"].ToString();
            }
            string extension6 = System.IO.Path.GetExtension(Request.Files["DocPhoto"].FileName);
            if (DocPhoto != null)
            {
                if (extension6.ToLower() == ".jpg" || extension6.ToLower() == ".jpeg" || extension6.ToLower() == ".png" || extension6.ToLower() == ".pdf")
                {
                    if (DocPhoto.ContentLength > 0)
                    {
                        DocPhoto = Request.Files["DocPhoto"];
                        string Name = DateTime.Now.Ticks + "_AP" + extension6.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocPhoto = pathtosave;
                        DocPhoto.SaveAs(path);
                    }
                }
            }
            else
            {
                Model.DocPhoto = Session["DocPhoto"].ToString();
            }
            string extension7 = System.IO.Path.GetExtension(Request.Files["DocSign"].FileName);
            if (DocSign != null)
            {
                if (extension7.ToLower() == ".jpg" || extension7.ToLower() == ".jpeg" || extension7.ToLower() == ".png" || extension7.ToLower() == ".pdf")
                {
                    if (DocSign.ContentLength > 0)
                    {
                        DocSign = Request.Files["DocSign"];
                        string Name = DateTime.Now.Ticks + "_AS" + extension7.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocSign = pathtosave;
                        DocSign.SaveAs(path);
                    }
                }
            }
            else
            {
                Model.DocSign = Session["DocSign"].ToString();
            }
            List<Registration> ExpSet = JsonConvert.DeserializeObject<List<Registration>>(Model.ListJson);
            //List<Registration> EduSet = JsonConvert.DeserializeObject<List<Registration>>(Model.IsLifePolicy);

            for (int i = 0; i < ExpSet.Count; i++)
            {
                Model.IsLifePolicy += (ExpSet[i].InsuranceId.ToString());
                if (i < ExpSet.Count - 1)
                {
                    Model.IsLifePolicy += ",";
                }
            }


            var list = DB.proc_Registration(Model).ToList();
            if (list.Count > 0)
            {
                if (list[0].msg == "Already Exists")
                {
                    TempData["Message"] = "7";
                    TempData["FinYear"] = list[0].FinYear;
                    TempData["AadharNo"] = list[0].AadharNo;
                    TempData["SchemeName"] = list[0].SchemeName;
                }
                else if (Model.ProcId == 1)
                {
                    //ViewBag.ListData = list;
                    TempData["Message"] = "1";
                    TempData["regn"] = list[0].ApplicationId;
                    TempData["Score"] = list[0].TotalScoreNumber;
                    TempData["Id"] = list[0].Id;
                    TempData["SchemeCode"] = list[0].SchemeCode;
                    //if (list[0].ApplicationId != "")
                    //{
                    //    DB.SendSMS(Model.MobileNo1, "Your Application No. is " + list[0].ApplicationId + " and Passwors is Your Mobile No. Submitted for UP-MKRY");
                    //}

                }
                /*For Confirm Page*/
                else if (Model.ProcId == 5 && Model.StatusId == 0 && Model.UserTypeId == 0)
                {
                    TempData["Id"] = list[0].Id;
                    TempData["Score"] = list[0].TotalScoreNumber;
                    TempData["Message"] = "2";
                    TempData["Id"] = list[0].Id;
                    TempData["SchemeCode"] = list[0].SchemeCode;
                }
                /*For Applicant Level*/
                else if (Model.ProcId == 5 && Model.StatusId == 2 && Model.UserTypeId == 5)
                {
                    TempData["Id"] = list[0].Id;
                    TempData["Message"] = "4";
                    TempData["SchemeCode"] = list[0].SchemeCode;
                }
                /*For DVIO Level*/
                else if (Model.ProcId == 5 && Model.UserTypeId == 3)
                {
                    TempData["Id"] = list[0].Id;
                    TempData["Message"] = "3";
                    TempData["SchemeCode"] = list[0].SchemeCode;
                }


            }

            else
            {
                ViewBag.ListData = null;
            }

            ViewBag.ButtonName = "Submit";
            ModelState.Clear();
            return View();
            //return RedirectToAction("Registration", "Home");

        }

        //public ActionResult RegistrationEdit(int Id)
        //{
        //    return RedirectToAction("Registration", "Home", new { sid = Id });
        //}

        public ActionResult ApplicationReport()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Registration Model = new Registration();

            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());

            if (Model.UserTypeId == 3)
            {
                Model.Flag = 1;
                Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
                Model.ProcId = 4;
            }
            if (Model.UserTypeId == 4)
            {
                Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
                Model.ProcId = 8;
            }
            if (Model.UserTypeId == 5)
            {
                Model.Id = Convert.ToInt32(Session["UserId"].ToString());
                Model.ProcId = 11;
            }

            if (Request.QueryString["Dash"] != null)
            {
                Model.StatusId = Convert.ToInt32(Request.QueryString["StatusId"].ToString());
                var list = DB.proc_Registration(Model).ToList();

                if (list.Count > 0)
                {
                    ViewBag.ListData = list;
                }
                else
                {
                    ViewBag.ListData = null;
                }
            }
            else
            {
                var list = DB.proc_Registration(Model).ToList();

                if (list.Count > 0)
                {
                    ViewBag.ListData = list;
                }
                else
                {
                    ViewBag.ListData = null;
                }
            }
            if (Convert.ToInt32(Session["UserType"].ToString()) == 3)
            {
                Model.StatusId = 1;
            }
            else
            {
                Model.StatusId = 0;
            }
            return View(Model);
        }
        [HttpPost]
        public ActionResult ApplicationReport(Registration Model)
        {
            Model.Flag = Model.StatusId;
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());

            if (Model.UserTypeId == 3)
            {
                Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
                Model.ProcId = 4;
            }
            if (Model.UserTypeId == 4)
            {
                Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
                Model.ProcId = 8;
            }
            if (Model.UserTypeId == 5)
            {
                Model.Id = Convert.ToInt32(Session["Id"].ToString());
                Model.ProcId = 11;
            }
            var list = DB.proc_Registration(Model).ToList();

            if (list.Count > 0)
            {
                ViewBag.ListData = list;
            }
            else
            {
                ViewBag.ListData = null;
            }
            return View(Model);
        }
        public ActionResult EditApplication(int Id, int UserTypeId)
        {
            return RedirectToAction("Registration", "Home", new { sid = Id, UserTypeId = UserTypeId });
        }

        public ActionResult FullAppDetails(int Id = 0)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Registration Model = new Registration();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            //Model.Flag = Model.StatusId;
            Model.Id = Id;
            Model.ProcId = 6;
            Model = DB.proc_Registration(Model).FirstOrDefault();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            Model.ProcId = 7;
            Model.Id = Id;
            var list = DB.proc_Registration(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
            }
            else
            {
                ViewBag.listdata = null;
            }
            TempData["msg"] = "00001";
            return View(Model);
        }
        [HttpPost]
        public ActionResult FullAppDetails(Registration Model, HttpPostedFileBase Documents)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.Id = Model.Id;
            Model.Flag = Model.Flag;
            Model.StatusId = Model.Flag;
            Model.ProcId = 1;

            //Model.Flag = Model.StatusId;
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            string prepath = "~/Content/Document";
            string path = "";
            var uploadUrl = Server.MapPath(prepath);

            string extensionPhoto = System.IO.Path.GetExtension(Request.Files["Documents"].FileName);
            if (Documents != null)
            {
                if (extensionPhoto.ToLower() == ".pdf" || extensionPhoto.ToLower() == ".png" || extensionPhoto.ToLower() == ".jpeg")
                {
                    if (Documents.ContentLength > 0)
                    {
                        Documents = Request.Files["Documents"];
                        string Name = DateTime.Now.Ticks + "_AF" + ".pdf";
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.Documents = pathtosave;
                        Documents.SaveAs(path);

                    }

                }
            }

            var List = DB.Proc_ActionApplication(Model).ToList();
            if (List.Count > 0)
            {
                if (List[0].msg.ToLower() == "success")
                {
                    if (Model.UserTypeId == 3 && Model.StatusId == 2)
                    {
                        TempData["Message"] = "2";
                    }
                    else if (Model.UserTypeId == 3 && Model.StatusId == 3)
                    {
                        TempData["Message"] = "3";
                    }

                    TempData["Id"] = Model.Id;
                    //return RedirectToAction("AllAppReport", "Home");
                }
                else if (List[0].msg.ToLower() == "fail")
                {
                    TempData["Message"] = 7;
                    TempData["Id"] = Model.Id;
                    //return RedirectToAction("AllAppReport", "Home");
                }
            }
            return View();
        }
        public JsonResult ApplicationTransfer(int id, int Fid, string AmendmentReason, string ForwardReason, string RejectReason, string ActionDate)
        {
            Registration Model = new Registration();
            Model.ProcId = 1;
            //if (Convert.ToInt32(Session["UserType"].ToString()) == 3)
            //{
            //    Model.Id = Convert.ToInt32(Session["Id"].ToString());
            //}
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.AmendmentReason = AmendmentReason;
            Model.ForwardReason = ForwardReason;
            Model.RejectReason = RejectReason;
            Model.ActionDate = ActionDate;
            Model.Id = id;
            Model.Flag = Fid;
            var data = DB.Proc_ActionApplication(Model).FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ScoreCardDetail(int Id = 0)
        {
            //if (Session["UserId"] == null)
            //{
            //    return RedirectToAction("UserLogin", "Login");
            //}
            Registration Model = new Registration();
            //Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            //Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.Id = Id;
            Model.ProcId = 9;
            Model = DB.proc_Registration(Model).FirstOrDefault();
            TempData["msg"] = "00001";
            return View(Model);
        }

        public ActionResult MatikalaToolKit()
        {
            return View();
        }
        public ActionResult MatikalaRojgaar()
        {
            return View();
        }
        public ActionResult MatikalaKaushal()
        {
            return View();
        }
        public ActionResult MicroMatikala()
        {
            return View();
        }
        public ActionResult MatikalaVipran()
        {
            return View();
        }
        public ActionResult MatikalaPuraskar()
        {
            return View();
        }

        public ActionResult Tender()
        {
            return View();
        }
        public ActionResult Example()
        {
            return View();
        }
        public ActionResult Introduction()
        {
            return View();
        }
        public ActionResult BoardObjective()
        {
            return View();
        }
        public ActionResult BoardWorkAreas()
        {
            return View();
        }


        public ActionResult ConfirmPage()
        {


            Registration Model = new Registration();
            TempData["regn"] = Model.ApplicationId;
            //Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            //Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
            Model.SchemeCode = Convert.ToString(Request.QueryString["SchemeCode"].ToString());
            Model.ProcId = 20;
            var list = DB.proc_Registration(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
            }
            else
            {
                ViewBag.listdata = null;
            }
            // Model = DB.proc_Registration(Model).FirstOrDefault();
            return View(Model);
            //return Json(DB.proc_Registration(Model).ToList(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult FinalSubmitForm(int Id)
        {
            Registration Model = new Registration();
            Model.ProcId = 10;
            Model.Id = Id;
            var data = DB.proc_Registration(Model).FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewApplicationDetail(int Id = 0)
        {
            Registration Model = new Registration();

            //Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            //Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.Id = Id;
            Model.ProcId = 6;
            Model = DB.proc_Registration(Model).FirstOrDefault();
            Model.ProcId = 9;
            var list = DB.proc_Registration(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
            }
            else
            {
                ViewBag.listdata = null;
            }
            TempData["msg"] = "00001";
            return View(Model);
        }
        public ActionResult ForwardingLetter(int Id = 0)
        {
            Registration Model = new Registration();
            //Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            //Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.Id = Id;
            Model.ProcId = 6;
            Model = DB.proc_Registration(Model).FirstOrDefault();
            TempData["msg"] = "00001";
            return View(Model);
        }

        public ActionResult ConfirmStatus()
        {

            Registration Model = new Registration();
            Model.ProcId = 12;
            var list = DB.proc_Registration(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
                ViewBag.Message = "No Record";
            }

            TempData["msg"] = "5";
            return View(Model);
        }

        public ActionResult FormCompletion()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Registration Model = new Registration();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.ProcId = 13;
            var list = DB.proc_Registration(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
                ViewBag.Message = "No Record";
            }

            TempData["msg"] = "5";
            return View(Model);
        }


        public ActionResult CUGList()
        {
            Registration Model = new Registration();
            Model.ProcId = 14;
            var list = DB.proc_Registration(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
                ViewBag.Message = "No Record";
            }

            TempData["msg"] = "5";
            return View(Model);
        }
        public ActionResult SUCHI()
        {

            Registration Model = new Registration();
            Model.ProcId = 15;
            var list = DB.proc_Registration(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
                ViewBag.Message = "No Record";
            }

            TempData["msg"] = "5";
            return View(Model);
        }
        public ActionResult Progress()
        {
            Registration Model = new Registration();
            Model.ProcId = 16;
            var list = DB.proc_Registration(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
                ViewBag.Message = "No Record";
            }

            TempData["msg"] = "5";
            return View(Model);
        }

        public ActionResult Nivida()
        {
            ProgressMaster Model = new ProgressMaster();
            Model.ProcId = 4;
            var list = DB.Proc_Nivida(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
                ViewBag.Message = "No Record";
            }

            TempData["msg"] = "5";
            return View(Model);
        }
        public ActionResult Budget()
        {
            Registration Model = new Registration();
            Model.ProcId = 17;
            var list = DB.proc_Registration(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
                ViewBag.Message = "No Record";
            }

            TempData["msg"] = "5";
            return View(Model);
        }
        public ActionResult Photo()
        {
            ProgressMaster Model = new ProgressMaster();
            Model.ProcId = 6;
            var list = DB.Proc_PhotoGallery(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
                ViewBag.Message = "No Record";
            }

            TempData["msg"] = "5";
            return View(Model);
        }

        public ActionResult Video()
        {
            ProgressMaster Model = new ProgressMaster();
            Model.ProcId = 6;
            var list = DB.Proc_VideoGallery(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
                ViewBag.Message = "No Record";
            }

            TempData["msg"] = "5";
            return View(Model);
        }

        public ActionResult PhotoGallery()
        {
            ProgressMaster Model = new ProgressMaster();
            if (Request.QueryString["PhotoCategoryId"] != null)
            {
                Model.PhotoCategoryId = Convert.ToInt32(Request.QueryString["PhotoCategoryId"].ToString());
            }
            Model.ProcId = 7;
            var list = DB.Proc_PhotoGallery(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
                ViewBag.Message = "No Record";
            }

            TempData["msg"] = "5";
            return View(Model);
        }

        public ActionResult VideoGallery()
        {
            ProgressMaster Model = new ProgressMaster();
            if (Request.QueryString["VideoCategoryId"] != null)
            {
                Model.VideoCategoryId = Convert.ToInt32(Request.QueryString["VideoCategoryId"].ToString());
            }
            Model.ProcId = 7;
            var list = DB.Proc_VideoGallery(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
                ViewBag.Message = "No Record";
            }

            TempData["msg"] = "5";
            return View(Model);
        }



        /*Start Registration*/

        public ActionResult FinRegistration()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            Registration Model = new Registration();

            if (Request.QueryString["sid"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                Model.ProcId = 2;
                Model = DB.proc_Registration(Model).FirstOrDefault();
                Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                ViewBag.buttonname = "Update";
                Session["DocAadharCardReport"] = Model.DocAadharCardReport;
                Session["DocJatiReport"] = Model.DocJatiReport;
                Session["DocProjectReport"] = Model.DocProjectReport;
                Session["DocNiwasReport"] = Model.DocNiwasReport;
                Session["DocQualificationReport"] = Model.DocQualificationReport;
                Session["DocPhoto"] = Model.DocPhoto;
                Session["DocSign"] = Model.DocSign;

                ViewBag.CADistrict = Model.CADistrict;
                ViewBag.CATehsil = Model.HCATehsil;
                ViewBag.CABlock = Model.HCABlock;
                ViewBag.PADistrictId = Model.PADistrictId;
                ViewBag.PATehsil = Model.HPATehsil;
                ViewBag.PABlock = Model.HPABlock;
                ViewBag.FBDistrict = Model.HFBDistrict;
                ViewBag.BankId = Model.BankId;
                ViewBag.BranchId = Model.HBranchId;
                ViewBag.IsLifePolicy = Model.IsLifePolicy;
            }
            else

            {
                ViewBag.buttonname = "Submit";
                Model.ProcId = 4;
                Model.UserTypeId = 0;
                var list = DB.proc_Registration(Model).ToList();
                if (list.Count > 0)
                {
                    ViewBag.listdata = list;
                }
                else
                {
                    ViewBag.listdata = null;
                }
                Session["DocAadharCardReport"] = "";
                Session["DocJatiReport"] = "";
                Session["DocProjectReport"] = "";
                Session["DocNiwasReport"] = "";
                Session["DocQualificationReport"] = "";
                Session["DocPhoto"] = "";
                Session["DocSign"] = "";

            }
            ModelState.Clear();
            return View(Model);
        }

        /*End Registration*/
        public ActionResult ApplicaitonView(int Id = 0)
        {
            Registration Model = new Registration();

            //Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            //Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.Id = Id;
            Model.ProcId = 6;
            Model = DB.proc_Registration(Model).FirstOrDefault();
            Model.ProcId = 9;
            var list = DB.proc_Registration(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
            }
            else
            {
                ViewBag.listdata = null;
            }
            TempData["msg"] = "00001";
            return View(Model);
        }

        #region Toolkit Application 
        public ActionResult ToolkitApplication()
        {

            Registration Model = new Registration();

            if (Request.QueryString["sid"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                // Model.SchemeCode = Convert.ToString(Request.QueryString["SchemeCode"].ToString());
                Model.ProcId = 2;
                Model = DB.proc_Registration(Model).FirstOrDefault();
                Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                //Model.SchemeCode = Convert.ToString(Request.QueryString["SchemeCode"].ToString());
                ViewBag.buttonname = "Update";
                Session["DocAadharCardReport"] = Model.DocAadharCardReport;
                Session["DocJatiReport"] = Model.DocJatiReport;
                Session["DocProjectReport"] = Model.DocProjectReport;
                Session["DocNiwasReport"] = Model.DocNiwasReport;
                Session["DocQualificationReport"] = Model.DocQualificationReport;
                Session["DocPhoto"] = Model.DocPhoto;
                Session["DocSign"] = Model.DocSign;

                ViewBag.CADistrict = Model.CADistrict;
                ViewBag.CATehsil = Model.HCATehsil;
                ViewBag.CABlock = Model.HCABlock;
                ViewBag.PADistrictId = Model.PADistrictId;
                ViewBag.PATehsil = Model.HPATehsil;
                ViewBag.PABlock = Model.HPABlock;
                ViewBag.FBDistrict = Model.HFBDistrict;
                ViewBag.BankId = Model.BankId;
                ViewBag.BranchId = Model.HBranchId;
                ViewBag.IsLifePolicy = Model.IsLifePolicy;
            }
            else

            {
                ViewBag.buttonname = "Submit";
                Model.ProcId = 4;
                Model.UserTypeId = 0;
                var list = DB.proc_Registration(Model).ToList();
                if (list.Count > 0)
                {
                    ViewBag.listdata = list;
                }
                else
                {
                    ViewBag.listdata = null;
                }
                Session["DocAadharCardReport"] = "";
                Session["DocJatiReport"] = "";
                Session["DocProjectReport"] = "";
                Session["DocNiwasReport"] = "";
                Session["DocQualificationReport"] = "";
                Session["DocPhoto"] = "";
                Session["DocSign"] = "";

            }
            ModelState.Clear();
            return View(Model);
        }
        [HttpPost]
        public ActionResult ToolkitApplication(Registration Model, string command, HttpPostedFileBase DocAadharCardReport,
        HttpPostedFileBase DocJatiReport, HttpPostedFileBase DocProjectReport,
        HttpPostedFileBase DocNiwasReport, HttpPostedFileBase DocQualificationReport, HttpPostedFileBase DocPhoto, HttpPostedFileBase DocSign)
        {



            if (command == "Submit")
            {
                Model.ProcId = 18;

                //TempData["Message"] = 1;
            }
            if (command == "Update")
            {

                Model.ProcId = 5;
                //Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                //TempData["Message"] = 2;
            }

            string prepath = "~/Content/Document";
            string path = "";
            var uploadUrl = Server.MapPath(prepath);
            //Random r = new Random(6);
            //string number = r.ToString();
            string extensionPdf1 = System.IO.Path.GetExtension(Request.Files["DocAadharCardReport"].FileName);
            if (DocAadharCardReport != null)
            {
                if (extensionPdf1.ToLower() == ".pdf" || extensionPdf1.ToLower() == ".png" || extensionPdf1.ToLower() == ".jpeg")
                {
                    if (DocAadharCardReport.ContentLength > 0)
                    {
                        DocAadharCardReport = Request.Files["DocAadharCardReport"];
                        string Name = DateTime.Now.Ticks + "_AD" + extensionPdf1.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocAadharCardReport = pathtosave;
                        DocAadharCardReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocAadharCardReport = Session["DocAadharCardReport"].ToString();
            }

            string extensionPdf2 = System.IO.Path.GetExtension(Request.Files["DocJatiReport"].FileName);
            if (DocJatiReport != null)
            {
                if (extensionPdf2.ToLower() == ".pdf" || extensionPdf2.ToLower() == ".png" || extensionPdf2.ToLower() == ".jpeg")
                {
                    if (DocJatiReport.ContentLength > 0)
                    {
                        DocJatiReport = Request.Files["DocJatiReport"];
                        string Name = DateTime.Now.Ticks + "_Cast" + extensionPdf2.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocJatiReport = pathtosave;
                        DocJatiReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocJatiReport = Session["DocJatiReport"].ToString();
            }

            string extensionPdf5 = System.IO.Path.GetExtension(Request.Files["DocQualificationReport"].FileName);
            if (DocQualificationReport != null)
            {
                if (extensionPdf5.ToLower() == ".pdf" || extensionPdf5.ToLower() == ".png" || extensionPdf5.ToLower() == ".jpeg")
                {
                    if (DocQualificationReport.ContentLength > 0)
                    {
                        DocQualificationReport = Request.Files["DocQualificationReport"];
                        string Name = DateTime.Now.Ticks + "_QF" + extensionPdf5.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocQualificationReport = pathtosave;
                        DocQualificationReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocQualificationReport = Session["DocQualificationReport"].ToString();
            }
            string extension6 = System.IO.Path.GetExtension(Request.Files["DocPhoto"].FileName);
            if (DocPhoto != null)
            {
                if (extension6.ToLower() == ".jpg" || extension6.ToLower() == ".jpeg" || extension6.ToLower() == ".png" || extension6.ToLower() == ".pdf")
                {
                    if (DocPhoto.ContentLength > 0)
                    {
                        DocPhoto = Request.Files["DocPhoto"];
                        string Name = DateTime.Now.Ticks + "_AP" + extension6.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocPhoto = pathtosave;
                        DocPhoto.SaveAs(path);
                    }
                }
            }
            else
            {
                Model.DocPhoto = Session["DocPhoto"].ToString();
            }


            var list = DB.proc_Registration(Model).ToList();
            if (list.Count > 0)
            {
                if (list[0].msg == "Already Exists")
                {
                    TempData["Message"] = "7";
                    TempData["FinYear"] = list[0].FinYear;
                    TempData["AadharNo"] = list[0].AadharNo;
                    TempData["SchemeName"] = list[0].SchemeName;
                }
                else if (Model.ProcId == 18)
                {
                    //ViewBag.ListData = list;
                    TempData["Message"] = "6";
                    TempData["regn"] = list[0].ApplicationId;
                    TempData["Id"] = list[0].Id;
                    TempData["SchemeCode"] = list[0].SchemeCode;
                    //if (list[0].ApplicationId != "")
                    //{
                    //    DB.SendSMS(Model.MobileNo1, "Your Application No. is " + list[0].ApplicationId + " and Passwors is Your Mobile No. Submitted for UP-MKRY");
                    //}
                }
                /*For Confirm Page*/
                else if (Model.ProcId == 5 && Model.StatusId == 0 && Model.UserTypeId == 0)
                {
                    TempData["Id"] = list[0].Id;
                    TempData["Score"] = list[0].TotalScoreNumber;
                    TempData["Message"] = "2";
                    TempData["Id"] = list[0].Id;
                    TempData["SchemeCode"] = list[0].SchemeCode;
                }
                /*For DVIO Level*/
                else if (Model.ProcId == 5 && Model.UserTypeId == 3)
                {
                    TempData["Id"] = list[0].Id;
                    TempData["Message"] = "3";
                    TempData["Id"] = list[0].Id;
                    TempData["SchemeCode"] = list[0].SchemeCode;
                }


            }

            else
            {
                ViewBag.ListData = null;
            }

            ViewBag.ButtonName = "Submit";
            ModelState.Clear();
            return View();
            //return RedirectToAction("Registration", "Home");

        }
        public ActionResult EditToolkitApplication(int Id, int UserTypeId)
        {
            return RedirectToAction("ToolkitApplication", "Home", new { sid = Id, UserTypeId = UserTypeId });
        }
        #endregion


        #region Award Vikash
        public ActionResult AwardScheme()
        {

            Registration Model = new Registration();

            if (Request.QueryString["sid"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                Model.ProcId = 2;
                Model = DB.proc_Registration(Model).FirstOrDefault();
                Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                ViewBag.buttonname = "Update";
                Session["DocAadharCardReport"] = Model.DocAadharCardReport;
                Session["DocPhoto"] = Model.DocPhoto;
                Session["DocSign"] = Model.DocSign;
                Session["DocJatiReport"] = Model.DocJatiReport;
                ViewBag.CADistrict = Model.CADistrict;
                ViewBag.CATehsil = Model.HCATehsil;
                ViewBag.CABlock = Model.HCABlock;
                ViewBag.FBDistrict = Model.HFBDistrict;
                ViewBag.BankId = Model.BankId;
                ViewBag.BranchId = Model.HBranchId;
                ViewBag.IsLifePolicy = Model.IsLifePolicy;
            }
            else

            {
                ViewBag.buttonname = "Submit";
                Model.ProcId = 4;
                Model.UserTypeId = 0;
                var list = DB.proc_Registration(Model).ToList();
                if (list.Count > 0)
                {
                    ViewBag.listdata = list;
                }
                else
                {
                    ViewBag.listdata = null;
                }
                Session["DocAadharCardReport"] = "";
                Session["DocPhoto"] = "";
                Session["DocSign"] = "";
                Session["DocJatiReport"] = "";
            }
            ModelState.Clear();
            return View(Model);
        }
        [HttpPost]
        public ActionResult AwardScheme(Registration Model, string command, HttpPostedFileBase DocAadharCardReport,
         HttpPostedFileBase DocPhoto, HttpPostedFileBase DocSign , HttpPostedFileBase DocJatiReport)
        {



            if (command == "Submit")
            {
                Model.ProcId = 18;
                //TempData["Message"] = 1;
            }
            if (command == "Update")
            {

                Model.ProcId = 5;
                //Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                //TempData["Message"] = 2;
            }

            string prepath = "~/Content/Document";
            string path = "";
            var uploadUrl = Server.MapPath(prepath);
            //Random r = new Random(6);
            //string number = r.ToString();
            string extensionPdf1 = System.IO.Path.GetExtension(Request.Files["DocAadharCardReport"].FileName);
            if (DocAadharCardReport != null)
            {
                if (extensionPdf1.ToLower() == ".pdf" || extensionPdf1.ToLower() == ".png" || extensionPdf1.ToLower() == ".jpeg")
                {
                    if (DocAadharCardReport.ContentLength > 0)
                    {
                        DocAadharCardReport = Request.Files["DocAadharCardReport"];
                        string Name = DateTime.Now.Ticks + "_AD" + extensionPdf1.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocAadharCardReport = pathtosave;
                        DocAadharCardReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocAadharCardReport = Session["DocAadharCardReport"].ToString();
            }
            string extensionPdf2 = System.IO.Path.GetExtension(Request.Files["DocJatiReport"].FileName);
            if (DocJatiReport != null)
            {
                if (extensionPdf2.ToLower() == ".pdf" || extensionPdf2.ToLower() == ".png" || extensionPdf2.ToLower() == ".jpeg")
                {
                    if (DocJatiReport.ContentLength > 0)
                    {
                        DocJatiReport = Request.Files["DocJatiReport"];
                        string Name = DateTime.Now.Ticks + "_Cast" + extensionPdf2.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocJatiReport = pathtosave;
                        DocJatiReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocJatiReport = Session["DocJatiReport"].ToString();
            }
            string extension6 = System.IO.Path.GetExtension(Request.Files["DocPhoto"].FileName);
            if (DocPhoto != null)
            {
                if (extension6.ToLower() == ".jpg" || extension6.ToLower() == ".jpeg" || extension6.ToLower() == ".png" || extension6.ToLower() == ".pdf")
                {
                    if (DocPhoto.ContentLength > 0)
                    {
                        DocPhoto = Request.Files["DocPhoto"];
                        string Name = DateTime.Now.Ticks + "_AP" + extension6.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocPhoto = pathtosave;
                        DocPhoto.SaveAs(path);
                    }
                }
            }
            else
            {
                Model.DocPhoto = Session["DocPhoto"].ToString();
            }
            string extension7 = System.IO.Path.GetExtension(Request.Files["DocSign"].FileName);
            if (DocSign != null)
            {
                if (extension7.ToLower() == ".jpg" || extension7.ToLower() == ".jpeg" || extension7.ToLower() == ".png" || extension7.ToLower() == ".pdf")
                {
                    if (DocSign.ContentLength > 0)
                    {
                        DocSign = Request.Files["DocSign"];
                        string Name = DateTime.Now.Ticks + "_AS" + extension7.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocSign = pathtosave;
                        DocSign.SaveAs(path);
                    }
                }
            }
            else
            {
                Model.DocSign = Session["DocSign"].ToString();
            }

          

            var list = DB.proc_Registration(Model).ToList();
            if (list[0].msg == "Already Exists")
            {
                TempData["Message"] = "7";
                TempData["FinYear"] = list[0].FinYear;
                TempData["AadharNo"] = list[0].AadharNo;
                TempData["SchemeName"] = list[0].SchemeName;
            }
            else if (list.Count > 0)
            {
                if (Model.ProcId == 18)
                {
                    //ViewBag.ListData = list;
                    TempData["Message"] = "6";
                    TempData["regn"] = list[0].ApplicationId;
                    TempData["Id"] = list[0].Id;
                    TempData["SchemeCode"] = list[0].SchemeCode;
                }
            }
            /*For Confirm Page*/
           else if (Model.ProcId == 5 && Model.StatusId == 0 && Model.UserTypeId == 0)
            {
                TempData["Id"] = list[0].Id;
                TempData["Score"] = list[0].TotalScoreNumber;
                TempData["Message"] = "2";
                TempData["Id"] = list[0].Id;
                TempData["SchemeCode"] = list[0].SchemeCode;
            }
            /*For DVIO Level*/
           else if (Model.ProcId == 5 && Model.UserTypeId == 3)
            {
                TempData["Id"] = list[0].Id;
                TempData["Message"] = "3";
                TempData["Id"] = list[0].Id;
                TempData["SchemeCode"] = list[0].SchemeCode;
            }
            else
            {
                ViewBag.ListData = null;
            }
            ViewBag.ButtonName = "Submit";
            ModelState.Clear();
            return View();
            //return RedirectToAction("Registration", "Home");

        }

        public ActionResult EditAwardScheme(int Id, int UserTypeId)
        {
            return RedirectToAction("AwardScheme", "Home", new { sid = Id, UserTypeId = UserTypeId });
        }



        #endregion
        #region Vipdan Scheme
        public ActionResult VipdanScheme()
        {

            Registration Model = new Registration();

            if (Request.QueryString["sid"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                Model.ProcId = 2;
                Model = DB.proc_Registration(Model).FirstOrDefault();
                Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                ViewBag.buttonname = "Update";
                Session["DocAadharCardReport"] = Model.DocAadharCardReport;
                Session["DocJatiReport"] = Model.DocJatiReport;
                Session["DocProjectReport"] = Model.DocProjectReport;
                Session["DocPhoto"] = Model.DocPhoto;


                ViewBag.CADistrict = Model.CADistrict;
                ViewBag.CATehsil = Model.HCATehsil;
                ViewBag.CABlock = Model.HCABlock;
                ViewBag.PADistrictId = Model.PADistrictId;
                ViewBag.PATehsil = Model.HPATehsil;
                ViewBag.PABlock = Model.HPABlock;
                ViewBag.FBDistrict = Model.HFBDistrict;
                ViewBag.BankId = Model.BankId;
                ViewBag.BranchId = Model.HBranchId;
                ViewBag.IsLifePolicy = Model.IsLifePolicy;
            }
            else

            {
                ViewBag.buttonname = "Submit";
                Model.ProcId = 4;
                Model.UserTypeId = 0;
                var list = DB.proc_Registration(Model).ToList();
                if (list.Count > 0)
                {
                    ViewBag.listdata = list;
                }
                else
                {
                    ViewBag.listdata = null;
                }
                Session["DocAadharCardReport"] = "";
                Session["DocJatiReport"] = "";
                Session["DocProjectReport"] = "";
                Session["DocPhoto"] = "";

            }
            ModelState.Clear();
            return View(Model);
        }
        [HttpPost]
        public ActionResult VipdanScheme(Registration Model, string command, HttpPostedFileBase DocAadharCardReport,
        HttpPostedFileBase DocJatiReport, HttpPostedFileBase DocProjectReport,
        HttpPostedFileBase DocPhoto)
        {



            if (command == "Submit")
            {
                Model.ProcId = 18;
                //TempData["Message"] = 1;
            }
            if (command == "Update")
            {

                Model.ProcId = 5;
                //Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                //TempData["Message"] = 2;
            }

            string prepath = "~/Content/Document";
            string path = "";
            var uploadUrl = Server.MapPath(prepath);
            //Random r = new Random(6);
            //string number = r.ToString();
            string extensionPdf1 = System.IO.Path.GetExtension(Request.Files["DocAadharCardReport"].FileName);
            if (DocAadharCardReport != null)
            {
                if (extensionPdf1.ToLower() == ".pdf" || extensionPdf1.ToLower() == ".png" || extensionPdf1.ToLower() == ".jpeg")
                {
                    if (DocAadharCardReport.ContentLength > 0)
                    {
                        DocAadharCardReport = Request.Files["DocAadharCardReport"];
                        string Name = DateTime.Now.Ticks + "_AD" + extensionPdf1.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocAadharCardReport = pathtosave;
                        DocAadharCardReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocAadharCardReport = Session["DocAadharCardReport"].ToString();
            }

            string extensionPdf2 = System.IO.Path.GetExtension(Request.Files["DocJatiReport"].FileName);
            if (DocJatiReport != null)
            {
                if (extensionPdf2.ToLower() == ".pdf" || extensionPdf2.ToLower() == ".png" || extensionPdf2.ToLower() == ".jpeg")
                {
                    if (DocJatiReport.ContentLength > 0)
                    {
                        DocJatiReport = Request.Files["DocJatiReport"];
                        string Name = DateTime.Now.Ticks + "_Cast" + extensionPdf2.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocJatiReport = pathtosave;
                        DocJatiReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocJatiReport = Session["DocJatiReport"].ToString();
            }
            string extensionPdf3 = System.IO.Path.GetExtension(Request.Files["DocProjectReport"].FileName);
            if (DocProjectReport != null)
            {
                if (extensionPdf3.ToLower() == ".pdf" || extensionPdf3.ToLower() == ".png" || extensionPdf3.ToLower() == ".jpeg")
                {
                    if (DocProjectReport.ContentLength > 0)
                    {
                        DocProjectReport = Request.Files["DocProjectReport"];
                        string Name = DateTime.Now.Ticks + "_PR" + extensionPdf3.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocProjectReport = pathtosave;
                        DocProjectReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocProjectReport = Session["DocProjectReport"].ToString();
            }

            string extension6 = System.IO.Path.GetExtension(Request.Files["DocPhoto"].FileName);
            if (DocPhoto != null)
            {
                if (extension6.ToLower() == ".jpg" || extension6.ToLower() == ".jpeg" || extension6.ToLower() == ".png" || extension6.ToLower() == ".pdf")
                {
                    if (DocPhoto.ContentLength > 0)
                    {
                        DocPhoto = Request.Files["DocPhoto"];
                        string Name = DateTime.Now.Ticks + "_AP" + extension6.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocPhoto = pathtosave;
                        DocPhoto.SaveAs(path);
                    }
                }
            }
            else
            {
                Model.DocPhoto = Session["DocPhoto"].ToString();
            }
            var list = DB.proc_Registration(Model).ToList();
            if (list[0].msg == "Already Exists")
            {
                TempData["Message"] = "7";
                TempData["FinYear"] = list[0].FinYear;
                TempData["AadharNo"] = list[0].AadharNo;
                TempData["SchemeName"] = list[0].SchemeName;
            }
            else if (list.Count > 0)
            {
                if (Model.ProcId == 18)
                {
                    TempData["Message"] = "6";
                    TempData["regn"] = list[0].ApplicationId;
                    TempData["Id"] = list[0].Id;
                    TempData["SchemeCode"] = list[0].SchemeCode;
                }
            }
            /*For Confirm Page*/
           else  if (Model.ProcId == 5 && Model.StatusId == 0 && Model.UserTypeId == 0)
            {
                TempData["Id"] = list[0].Id;
                TempData["Score"] = list[0].TotalScoreNumber;
                TempData["Message"] = "2";
                TempData["Id"] = list[0].Id;
                TempData["SchemeCode"] = list[0].SchemeCode;
            }
            /*For DVIO Level*/
           else  if (Model.ProcId == 5 && Model.UserTypeId == 3)
            {
                TempData["Id"] = list[0].Id;
                TempData["Message"] = "3";
                TempData["Id"] = list[0].Id;
                TempData["SchemeCode"] = list[0].SchemeCode;
            }

            else
            {
                ViewBag.ListData = null;
            }

            ViewBag.ButtonName = "Submit";
            ModelState.Clear();
            return View();
            //return RedirectToAction("Registration", "Home");

        }

        public ActionResult EditVipdanScheme(int Id, int UserTypeId)
        {
            return RedirectToAction("VipdanScheme", "Home", new { sid = Id, UserTypeId = UserTypeId });
        }
        #endregion
        #region Kaushal Vikash
        public ActionResult KaushalScheme()
        {
            Registration Model = new Registration();
            if (Request.QueryString["sid"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                Model.ProcId = 2;
                Model = DB.proc_Registration(Model).FirstOrDefault();
                Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                ViewBag.buttonname = "Update";
                Session["DocAadharCardReport"] = Model.DocAadharCardReport;
                Session["DocJatiReport"] = Model.DocJatiReport;
                Session["DocProjectReport"] = Model.DocProjectReport;
                Session["DocQualificationReport"] = Model.DocQualificationReport;
                Session["DocPhoto"] = Model.DocPhoto;


                ViewBag.CADistrict = Model.CADistrict;
                ViewBag.CATehsil = Model.HCATehsil;
                ViewBag.CABlock = Model.HCABlock;
                ViewBag.PADistrictId = Model.PADistrictId;
                ViewBag.PATehsil = Model.HPATehsil;
                ViewBag.PABlock = Model.HPABlock;
                ViewBag.FBDistrict = Model.HFBDistrict;
                ViewBag.BankId = Model.BankId;
                ViewBag.BranchId = Model.HBranchId;
                ViewBag.IsLifePolicy = Model.IsLifePolicy;
            }
            else

            {
                ViewBag.buttonname = "Submit";
                Model.ProcId = 4;
                Model.UserTypeId = 0;
                var list = DB.proc_Registration(Model).ToList();
                if (list.Count > 0)
                {
                    ViewBag.listdata = list;
                }
                else
                {
                    ViewBag.listdata = null;
                }
                Session["DocAadharCardReport"] = "";
                Session["DocJatiReport"] = "";
                Session["DocProjectReport"] = "";
                Session["DocPhoto"] = "";
                Session["DocQualificationReport"] = "";

            }
            ModelState.Clear();
            return View(Model);
        }
        [HttpPost]
        public ActionResult KaushalScheme(Registration Model, string command, HttpPostedFileBase DocAadharCardReport,
        HttpPostedFileBase DocJatiReport, HttpPostedFileBase DocProjectReport, HttpPostedFileBase DocQualificationReport,
        HttpPostedFileBase DocPhoto)
        {



            if (command == "Submit")
            {
                Model.ProcId = 18;
                //TempData["Message"] = 1;
            }
            if (command == "Update")
            {

                Model.ProcId = 5;
                //Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                //TempData["Message"] = 2;
            }

            string prepath = "~/Content/Document";
            string path = "";
            var uploadUrl = Server.MapPath(prepath);
            //Random r = new Random(6);
            //string number = r.ToString();
            string extensionPdf1 = System.IO.Path.GetExtension(Request.Files["DocAadharCardReport"].FileName);
            if (DocAadharCardReport != null)
            {
                if (extensionPdf1.ToLower() == ".pdf" || extensionPdf1.ToLower() == ".png" || extensionPdf1.ToLower() == ".jpeg")
                {
                    if (DocAadharCardReport.ContentLength > 0)
                    {
                        DocAadharCardReport = Request.Files["DocAadharCardReport"];
                        string Name = DateTime.Now.Ticks + "_AD" + extensionPdf1.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocAadharCardReport = pathtosave;
                        DocAadharCardReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocAadharCardReport = Session["DocAadharCardReport"].ToString();
            }

            string extensionPdf2 = System.IO.Path.GetExtension(Request.Files["DocJatiReport"].FileName);
            if (DocJatiReport != null)
            {
                if (extensionPdf2.ToLower() == ".pdf" || extensionPdf2.ToLower() == ".png" || extensionPdf2.ToLower() == ".jpeg")
                {
                    if (DocJatiReport.ContentLength > 0)
                    {
                        DocJatiReport = Request.Files["DocJatiReport"];
                        string Name = DateTime.Now.Ticks + "_Cast" + extensionPdf2.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocJatiReport = pathtosave;
                        DocJatiReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocJatiReport = Session["DocJatiReport"].ToString();
            }
            string extensionPdf3 = System.IO.Path.GetExtension(Request.Files["DocProjectReport"].FileName);
            if (DocProjectReport != null)
            {
                if (extensionPdf3.ToLower() == ".pdf" || extensionPdf3.ToLower() == ".png" || extensionPdf3.ToLower() == ".jpeg")
                {
                    if (DocProjectReport.ContentLength > 0)
                    {
                        DocProjectReport = Request.Files["DocProjectReport"];
                        string Name = DateTime.Now.Ticks + "_PR" + extensionPdf3.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocProjectReport = pathtosave;
                        DocProjectReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocProjectReport = Session["DocProjectReport"].ToString();
            }
            string extensionPdf5 = System.IO.Path.GetExtension(Request.Files["DocQualificationReport"].FileName);
            if (DocQualificationReport != null)
            {
                if (extensionPdf5.ToLower() == ".pdf" || extensionPdf5.ToLower() == ".png" || extensionPdf5.ToLower() == ".jpeg")
                {
                    if (DocQualificationReport.ContentLength > 0)
                    {
                        DocQualificationReport = Request.Files["DocQualificationReport"];
                        string Name = DateTime.Now.Ticks + "_QF" + extensionPdf5.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocQualificationReport = pathtosave;
                        DocQualificationReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocQualificationReport = Session["DocQualificationReport"].ToString();
            }
            string extension6 = System.IO.Path.GetExtension(Request.Files["DocPhoto"].FileName);
            if (DocPhoto != null)
            {
                if (extension6.ToLower() == ".jpg" || extension6.ToLower() == ".jpeg" || extension6.ToLower() == ".png" || extension6.ToLower() == ".pdf")
                {
                    if (DocPhoto.ContentLength > 0)
                    {
                        DocPhoto = Request.Files["DocPhoto"];
                        string Name = DateTime.Now.Ticks + "_AP" + extension6.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocPhoto = pathtosave;
                        DocPhoto.SaveAs(path);
                    }
                }
            }
            else
            {
                Model.DocPhoto = Session["DocPhoto"].ToString();
            }
            var list = DB.proc_Registration(Model).ToList();
            if (list[0].msg == "Already Exists")
            {
                TempData["Message"] = "7";
                TempData["FinYear"] = list[0].FinYear;
                TempData["AadharNo"] = list[0].AadharNo;
                TempData["SchemeName"] = list[0].SchemeName;
            }
           else if (list.Count > 0)
            {
                if (Model.ProcId == 18)
                {
                    TempData["Message"] = "6";
                    TempData["regn"] = list[0].ApplicationId;
                    TempData["Id"] = list[0].Id;
                    TempData["SchemeCode"] = list[0].SchemeCode;
                }
            }
            /*For Confirm Page*/
            else if (Model.ProcId == 5 && Model.StatusId == 0 && Model.UserTypeId == 0)
            {
                TempData["Id"] = list[0].Id;
                TempData["Score"] = list[0].TotalScoreNumber;
                TempData["Message"] = "2";
                TempData["Id"] = list[0].Id;
                TempData["SchemeCode"] = list[0].SchemeCode;
            }
            /*For DVIO Level*/
            else if (Model.ProcId == 5 && Model.UserTypeId == 3)
            {
                TempData["Id"] = list[0].Id;
                TempData["Message"] = "3";
                TempData["Id"] = list[0].Id;
                TempData["SchemeCode"] = list[0].SchemeCode;
            }

            else
            {
                ViewBag.ListData = null;
            }

            ViewBag.ButtonName = "Submit";
            ModelState.Clear();
            return View();
            //return RedirectToAction("Registration", "Home");

        }
        public ActionResult EditKaushalScheme(int Id, int UserTypeId)
        {
            return RedirectToAction("KaushalScheme", "Home", new { sid = Id, UserTypeId = UserTypeId });
        }

        #endregion
        #region Micro Matikala
        public ActionResult MicroMatikalaCommon()
        {

            Registration Model = new Registration();

            if (Request.QueryString["sid"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                Model.ProcId = 2;
                Model = DB.proc_Registration(Model).FirstOrDefault();
                Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                ViewBag.buttonname = "Update";
                Session["DocAadharCardReport"] = Model.DocAadharCardReport;
                Session["DocJatiReport"] = Model.DocJatiReport;
                Session["DocProjectReport"] = Model.DocProjectReport;
                Session["DocNiwasReport"] = Model.DocNiwasReport;
                Session["DocQualificationReport"] = Model.DocQualificationReport;
                Session["DocPhoto"] = Model.DocPhoto;
                Session["DocSign"] = Model.DocSign;
                Session["CAEmail"] = Model.CAEmail;

                ViewBag.CADistrict = Model.CADistrict;
                ViewBag.CATehsil = Model.HCATehsil;
                ViewBag.CABlock = Model.HCABlock;
                ViewBag.PADistrictId = Model.PADistrictId;
                ViewBag.PATehsil = Model.HPATehsil;
                ViewBag.PABlock = Model.HPABlock;
                ViewBag.FBDistrict = Model.HFBDistrict;
                ViewBag.BankId = Model.BankId;
                ViewBag.BranchId = Model.HBranchId;
                ViewBag.IsLifePolicy = Model.IsLifePolicy;
            }
            else

            {
                ViewBag.buttonname = "Submit";
                Model.ProcId = 4;
                Model.UserTypeId = 0;
                var list = DB.proc_Registration(Model).ToList();
                if (list.Count > 0)
                {
                    ViewBag.listdata = list;
                }
                else
                {
                    ViewBag.listdata = null;
                }
                Session["DocAadharCardReport"] = "";
                Session["DocJatiReport"] = "";
                Session["DocProjectReport"] = "";
                Session["DocNiwasReport"] = "";
                Session["DocQualificationReport"] = "";
                Session["DocPhoto"] = "";
                Session["DocSign"] = "";
                Session["CAEmail"] = "";

            }
            ModelState.Clear();
            return View(Model);
        }
        [HttpPost]
        public ActionResult MicroMatikalaCommon(Registration Model, string command, HttpPostedFileBase DocAadharCardReport,
        HttpPostedFileBase DocJatiReport, HttpPostedFileBase DocProjectReport,
        HttpPostedFileBase DocNiwasReport, HttpPostedFileBase DocQualificationReport, HttpPostedFileBase DocPhoto,
        HttpPostedFileBase DocSign,HttpPostedFileBase CAEmail)
        {
            if (command == "Submit")
            {
                Model.ProcId = 18;
                //TempData["Message"] = 1;
            }
            if (command == "Update")
            {

                Model.ProcId = 5;
                //Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                //TempData["Message"] = 2;
            }

            string prepath = "~/Content/Document";
            string path = "";
            var uploadUrl = Server.MapPath(prepath);
            //Random r = new Random(6);
            //string number = r.ToString();
            string extensionPdf1 = System.IO.Path.GetExtension(Request.Files["DocAadharCardReport"].FileName);
            if (DocAadharCardReport != null)
            {
                if (extensionPdf1.ToLower() == ".pdf" || extensionPdf1.ToLower() == ".png" || extensionPdf1.ToLower() == ".jpeg")
                {
                    if (DocAadharCardReport.ContentLength > 0)
                    {
                        DocAadharCardReport = Request.Files["DocAadharCardReport"];
                        string Name = DateTime.Now.Ticks + "_AD" + extensionPdf1.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocAadharCardReport = pathtosave;
                        DocAadharCardReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocAadharCardReport = Session["DocAadharCardReport"].ToString();
            }

            string extensionPdf2 = System.IO.Path.GetExtension(Request.Files["DocJatiReport"].FileName);
            if (DocJatiReport != null)
            {
                if (extensionPdf2.ToLower() == ".pdf" || extensionPdf2.ToLower() == ".png" || extensionPdf2.ToLower() == ".jpeg")
                {
                    if (DocJatiReport.ContentLength > 0)
                    {
                        DocJatiReport = Request.Files["DocJatiReport"];
                        string Name = DateTime.Now.Ticks + "_Cast" + extensionPdf2.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocJatiReport = pathtosave;
                        DocJatiReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocJatiReport = Session["DocJatiReport"].ToString();
            }
            string extensionPdf3 = System.IO.Path.GetExtension(Request.Files["DocProjectReport"].FileName);
            if (DocProjectReport != null)
            {
                if (extensionPdf3.ToLower() == ".pdf" || extensionPdf3.ToLower() == ".png" || extensionPdf3.ToLower() == ".jpeg")
                {
                    if (DocProjectReport.ContentLength > 0)
                    {
                        DocProjectReport = Request.Files["DocProjectReport"];
                        string Name = DateTime.Now.Ticks + "_PR" + extensionPdf3.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocProjectReport = pathtosave;
                        DocProjectReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocProjectReport = Session["DocProjectReport"].ToString();
            }
            string extensionPdf4 = System.IO.Path.GetExtension(Request.Files["DocNiwasReport"].FileName);
            if (DocNiwasReport != null)
            {
                if (extensionPdf4.ToLower() == ".pdf" || extensionPdf4.ToLower() == ".png" || extensionPdf4.ToLower() == ".jpeg")
                {
                    if (DocNiwasReport.ContentLength > 0)
                    {
                        DocNiwasReport = Request.Files["DocNiwasReport"];
                        string Name = DateTime.Now.Ticks + "_DC" + extensionPdf4.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocNiwasReport = pathtosave;
                        DocNiwasReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocNiwasReport = Session["DocNiwasReport"].ToString();
            }

            string extensionPdf5 = System.IO.Path.GetExtension(Request.Files["DocQualificationReport"].FileName);
            if (DocQualificationReport != null)
            {
                if (extensionPdf5.ToLower() == ".pdf" || extensionPdf5.ToLower() == ".png" || extensionPdf5.ToLower() == ".jpeg")
                {
                    if (DocQualificationReport.ContentLength > 0)
                    {
                        DocQualificationReport = Request.Files["DocQualificationReport"];
                        string Name = DateTime.Now.Ticks + "_QF" + extensionPdf5.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocQualificationReport = pathtosave;
                        DocQualificationReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocQualificationReport = Session["DocQualificationReport"].ToString();
            }
            string extension6 = System.IO.Path.GetExtension(Request.Files["DocPhoto"].FileName);
            if (DocPhoto != null)
            {
                if (extension6.ToLower() == ".jpg" || extension6.ToLower() == ".jpeg" || extension6.ToLower() == ".png" || extension6.ToLower() == ".pdf")
                {
                    if (DocPhoto.ContentLength > 0)
                    {
                        DocPhoto = Request.Files["DocPhoto"];
                        string Name = DateTime.Now.Ticks + "_AP" + extension6.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocPhoto = pathtosave;
                        DocPhoto.SaveAs(path);
                    }
                }
            }
            else
            {
                Model.DocPhoto = Session["DocPhoto"].ToString();
            }
            string extension7 = System.IO.Path.GetExtension(Request.Files["DocSign"].FileName);
            if (DocSign != null)
            {
                if (extension7.ToLower() == ".jpg" || extension7.ToLower() == ".jpeg" || extension7.ToLower() == ".png" || extension7.ToLower() == ".pdf")
                {
                    if (DocSign.ContentLength > 0)
                    {
                        DocSign = Request.Files["DocSign"];
                        string Name = DateTime.Now.Ticks + "_AS" + extension7.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocSign = pathtosave;
                        DocSign.SaveAs(path);
                    }
                }
            }
            else
            {
                Model.DocSign = Session["DocSign"].ToString();
            }
            string extension8 = System.IO.Path.GetExtension(Request.Files["CAEmail"].FileName);
            if (CAEmail != null)
            {
                if (extension8.ToLower() == ".jpg" || extension8.ToLower() == ".jpeg" || extension8.ToLower() == ".png" || extension8.ToLower() == ".pdf")
                {
                    if (CAEmail.ContentLength > 0)
                    {
                        CAEmail = Request.Files["CAEmail"];
                        string Name = DateTime.Now.Ticks + "_PER" + extension8.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.CAEmail = pathtosave;
                        CAEmail.SaveAs(path);
                    }
                }
            }
            else
            {
                Model.CAEmail = Session["CAEmail"].ToString();
            }


            var list = DB.proc_Registration(Model).ToList();
            if (list[0].msg == "Already Exists")
            {
                TempData["Message"] = "7";
                TempData["FinYear"] = list[0].FinYear;
                TempData["AadharNo"] = list[0].AadharNo;
                TempData["SchemeName"] = list[0].SchemeName;
            }
           else if (list.Count > 0)
            {
                if (Model.ProcId == 18)
                {
                    TempData["Message"] = "6";
                    TempData["regn"] = list[0].ApplicationId;
                    TempData["Id"] = list[0].Id;
                    TempData["SchemeCode"] = list[0].SchemeCode;
                }
                /*For Confirm Page*/
               else if (Model.ProcId == 5 && Model.StatusId == 0 && Model.UserTypeId == 0)
                {
                    TempData["Id"] = list[0].Id;
                    TempData["Score"] = list[0].TotalScoreNumber;
                    TempData["Message"] = "2";
                    TempData["Id"] = list[0].Id;
                    TempData["SchemeCode"] = list[0].SchemeCode;
                }
                /*For DVIO Level*/
                else if (Model.ProcId == 5 && Model.UserTypeId == 3)
                {
                    TempData["Id"] = list[0].Id;
                    TempData["Message"] = "3";
                    TempData["Id"] = list[0].Id;
                    TempData["SchemeCode"] = list[0].SchemeCode;
                }


            }

            else
            {
                ViewBag.ListData = null;
            }

            ViewBag.ButtonName = "Submit";
            ModelState.Clear();
            return View();
            //return RedirectToAction("Registration", "Home");

        }
        public ActionResult EditMicroMatikalaCommon(int Id, int UserTypeId)
        {
            return RedirectToAction("MicroMatikalaCommon", "Home", new { sid = Id, UserTypeId = UserTypeId });
        }
        #endregion

        #region Application View All Scheme
        public ActionResult ViewToolkitApp(int Id = 0)
        {
            Registration Model = new Registration();
            Model.ProcId = 19;
            Model.Id = Id;
            var list = DB.proc_Registration(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
                ViewBag.Message = "No Record";
            }

            TempData["msg"] = "5";
            return View(Model);
        }
        public ActionResult ViewVidpanApp(int Id = 0)
        {
            Registration Model = new Registration();
            Model.ProcId = 19;
            Model.Id = Id;
            var list = DB.proc_Registration(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
                ViewBag.Message = "No Record";
            }

            TempData["msg"] = "5";
            return View(Model);
        }
        public ActionResult ViewKaushalApp(int Id = 0)
        {
            Registration Model = new Registration();
            Model.ProcId = 19;
            Model.Id = Id;
            var list = DB.proc_Registration(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
                ViewBag.Message = "No Record";
            }

            TempData["msg"] = "5";
            return View(Model);
        }
        public ActionResult ViewMicroCommonApp(int Id = 0)
        {
            Registration Model = new Registration();
            Model.ProcId = 19;
            Model.Id = Id;
            var list = DB.proc_Registration(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
                ViewBag.Message = "No Record";
            }

            TempData["msg"] = "5";
            return View(Model);
        }
        public ActionResult ViewAwardApp(int Id = 0)
        {
            Registration Model = new Registration();
            Model.ProcId = 19;
            Model.Id = Id;
            var list = DB.proc_Registration(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
                ViewBag.Message = "No Record";
            }

            TempData["msg"] = "5";
            return View(Model);
        }
        #endregion

        public JsonResult getprojectMap(int Procid, string district, int FinYear)
        {
            ProgressMaster Model = new ProgressMaster();
            Model.ProcId = Procid;
            Model.City = district;
            Model.FinYear = FinYear;
            var list = DB.Proc_Progress(Model).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}



