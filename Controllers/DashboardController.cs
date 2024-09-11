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
    public class DashboardController : Controller
    {
        DB_Layer DB = new DB_Layer();
        // GET: BankSanction
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApplicationRptDrill(int id = 0, string StepStatus1 = "",string StepStatus2 = "", string StepStatus4 = "",string FinYear = "",string SchemeCode="")
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Registration Model = new Registration();
            
            Model.StatusId = id;
            Model.StepStatus1 = StepStatus1;
            Model.StepStatus2 = StepStatus2;
            Model.StepStatus4 = StepStatus4;
            Model.FinYear = FinYear;
            Model.SchemeCode = SchemeCode;
            Model.Flag = id;
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (Model.UserTypeId == 3)
            {
                Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
                if (Model.Flag == 1)
                {
                    Model.ProcId = 1;
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    //Convert.ToString(Request.QueryString["FinYear"].ToString());
                    //Model.FinYear = ;
                    ViewBag.ReportName = "Application Submitted";
                }
                if (Model.Flag == 2)
                {
                    Model.ProcId = 2;
                    ViewBag.ReportName = "Forwarded To Bank";
                }
                if (Model.Flag == 3)
                {
                    Model.ProcId = 3;
                    ViewBag.ReportName = "Pending For Forwarding Bank";
                }
                if (Model.Flag == 4)
                {
                    Model.ProcId = 4;
                    ViewBag.ReportName = "Pending At Bank Level";
                }
                if (Model.Flag == 5)
                {
                    Model.ProcId = 5;
                    ViewBag.ReportName = "Sanction By Bank";
                }
                if (Model.Flag == 6)
                {
                    Model.ProcId = 6;
                    ViewBag.ReportName = "Disbursed Loan";
                }
                if (Model.Flag == 7)
                {
                    Model.ProcId = 7;
                    ViewBag.ReportName = "Rejected By Bank";
                }
                if (Model.Flag == 10)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 15;
                    ViewBag.ReportName = "Training Completed";
                }
                if (Model.Flag == 8)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 16;
                    ViewBag.ReportName = "Training Under Process";
                }
                if (Model.Flag == 9)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 17;
                    ViewBag.ReportName = "Training InCompleted";
                }

            }
            if (Model.UserTypeId == 4)
            {
                Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
                if (Model.Flag == 4)
                {
                    Model.ProcId = 1;
                    ViewBag.ReportName = "No.Of Application Received";
                }
                if (Model.Flag == 5)
                {
                    Model.ProcId = 2;
                    ViewBag.ReportName = "Sanction By Bank";
                }
                if (Model.Flag == 6)
                {
                    Model.ProcId = 3;
                    ViewBag.ReportName = "Disbursed By  Bank";
                }
                if (Model.Flag == 7)
                {
                    Model.ProcId = 4;
                    ViewBag.ReportName = "Rejected By Bank";
                }
            }
            if (Model.UserTypeId == 6)
            {
                Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
                if (Model.Flag == 4)
                {
                    Model.ProcId = 1;
                    ViewBag.ReportName = "No.Of Application Received";
                }
                if (Model.Flag == 5)
                {
                    Model.ProcId = 2;
                    ViewBag.ReportName = "Sanction By Bank";
                }
                if (Model.Flag == 6)
                {
                    Model.ProcId = 3;
                    ViewBag.ReportName = "Disbursed By  Bank";
                }
                if (Model.Flag == 10)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 7;
                    ViewBag.ReportName = "Training Completed";
                }
                if (Model.Flag == 8)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 8;
                    ViewBag.ReportName = "Training Under Process";
                }
                if (Model.Flag == 9)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 9;
                    ViewBag.ReportName = "Training InCompleted";
                }

            }
            if (Model.UserTypeId == 5)
            {
                Model.Id = Convert.ToInt32(Session["Id"].ToString());
                Model.ProcId = 11;
            }
            var list = DB.proc_RegistrationDrill(Model).ToList();

            if (list.Count > 0)
            {
                ViewBag.ListData = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.ListData = null;
                ViewBag.Status = "No Record";
            }
            //ViewBag.ReportName = "Application Submitted";
            return View(Model);
        }

        public ActionResult ApplicationRptSchemeDrill(int id = 0,string SchemeName="", string FinYear = "", string SchemeCode = "")
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Registration Model = new Registration();

            Model.StatusId = id;
            Model.FinYear = FinYear;
            Model.SchemeCode = SchemeCode;
            Model.SchemeName = SchemeName;
            Model.Flag = id;
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (Model.UserTypeId == 3)
            {
                Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
                if (Model.Flag == 1)
                {
                    Model.ProcId = 8;
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.SchemeName = SchemeName;
                    if(Model.SchemeCode == "MKBCFC" || Model.SchemeCode == "MKBEXHB") {
                    ViewBag.PrastavName = "प्राप्त प्रस्ताव";
                    }
                    if (Model.SchemeCode == "MKBPRIZE" || Model.SchemeCode == "MKBTK" || Model.SchemeCode == "MKBTRI" || Model.SchemeCode == "MMKRY") { 
                        ViewBag.ReportName = "प्राप्त आवेदन";
                    }
                }
                if (Model.Flag == 2)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 9;
                    ViewBag.ReportName = "चयनित आवेदन";
                }
                if (Model.Flag == 3)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 10;
                    ViewBag.ReportName = "निरस्त आवेदन";
                }
                if (Model.Flag == 4)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 11;
                    ViewBag.ReportName = "लंबित आवेदन";
                }
                if (Model.Flag == 14)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 14;
                    ViewBag.ReportName = "ZO को अग्रेषित आवेदन ";
                }
                if (Model.Flag == 15)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 15;
                    ViewBag.ReportName = "Training Completed";
                }
                if (Model.Flag == 16)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 16;
                    ViewBag.ReportName = "Training Under Process";
                }
                if (Model.Flag == 17)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 17;
                    ViewBag.ReportName = "Training InCompleted";
                }
                //if (Model.Flag == 12)
                //{
                //    Model.FinYear = FinYear;
                //    Model.SchemeCode = SchemeCode;
                //    Model.ProcId = 12;
                //    ViewBag.ReportName = "Applicaiton Submitted";
                //}
                //if (Model.Flag == 13)
                //{
                //    Model.FinYear = FinYear;
                //    Model.SchemeCode = SchemeCode;
                //    Model.ProcId = 13;
                //    ViewBag.ReportName = "Forward To ZO";
                //}

            }
            if (Model.UserTypeId == 2)
            {
                Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());

                if (Model.Flag == 14)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 14;
                    ViewBag.ReportName = "प्राप्त आवेदन  ";
                }
                if (Model.Flag == 2)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 9;
                    ViewBag.ReportName = "चयनित आवेदन";
                }
                if (Model.Flag == 3)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 10;
                    ViewBag.ReportName = "निरस्त आवेदन";
                }
                if (Model.Flag == 4)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 11;
                    ViewBag.ReportName = "लंबित आवेदन";
                }
            }
            if (Model.UserTypeId == 6)
            {
                Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
                if (Model.Flag == 2)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 6;
                    ViewBag.ReportName = "Selected List Received";
                }
                if (Model.Flag == 3)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 7;
                    ViewBag.ReportName = "Training Completed";
                }
                if (Model.Flag == 4)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 8;
                    ViewBag.ReportName = "Training Under Process";
                }
                if (Model.Flag == 5)
                {
                    Model.FinYear = FinYear;
                    Model.SchemeCode = SchemeCode;
                    Model.ProcId = 9;
                    ViewBag.ReportName = "Training InCompleted";
                }
            }
            var list = DB.proc_RegistrationDrill(Model).ToList();

            if (list.Count > 0)
            {
                ViewBag.ListData = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.ListData = null;
                ViewBag.Status = "No Record";
            }
            //ViewBag.ReportName = "Application Submitted";
            return View(Model);
        }
        public ActionResult LoanSanctionRpt()
        {
            return View();
        }

        public JsonResult MMPayment(int ProcId, string MarginMoneyRecDate,
         string TransactionNumber, int SanctionId, int StepId, int Id)
        {

            Registration Model = new Registration();

            Model.ProcId = ProcId;
            Model.MarginMoneyRecDate = MarginMoneyRecDate;
            Model.TransactionNumber = TransactionNumber;
            Model.Id = Id;
            Model.SanctionId = SanctionId;
            Model.StepId = StepId;

            var data = DB.Proc_MMPayment(Model).ToList();
            //return Json(true, JsonRequestBehavior.AllowGet);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Suchi()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            ProgressMaster Model = new ProgressMaster();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (Request.QueryString["sid"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.ProcId = 5;
                Model = DB.Proc_Suchi(Model).FirstOrDefault();
                ViewBag.ButtonName = "Update";
            }
            else
            {
                ViewBag.ButtonName = "Save";
                Model.ProcId = 4;
                var list = DB.Proc_Suchi(Model).ToList();
                if (list.Count > 0)
                {
                    ViewBag.ListData = list;
                }
                else
                {
                    ViewBag.ListData = null;
                }
            }
            ModelState.Clear();
            return View(Model);

        }

        [HttpPost]
        public ActionResult Suchi(ProgressMaster Model, string command)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (command == "Save")
            {
                Model.ProcId = 1;
            }
            if (command == "Update")
            {
                Model.ProcId = 2;
            }

            var list = DB.Proc_Suchi(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.ListData = list;
                if (list[0].msg == "success")
                {
                    if (Model.ProcId == 1)
                    {
                        TempData["Message"] = "1";
                    }

                }
                if (list[0].msg == "update")
                {
                    if (Model.ProcId == 2)
                    {

                        TempData["Message"] = "2";
                    }

                }

            }
            else
            {
                TempData["Message"] = "3";
                ViewBag.ListData = null;
            }
            ViewBag.ButtonName = "Save";
            ModelState.Clear();
            return View();
        }

        public JsonResult DeleteSuchi(int Id)
        {
            ProgressMaster Model = new ProgressMaster();
            Model.ProcId = 3;
            Model.Id = Id;
            var data = DB.Proc_Suchi(Model).FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditSuchi(int Id)
        {
            return RedirectToAction("Suchi", "Dashboard", new { sid = Id });
        }
        public ActionResult CUGList()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            ProgressMaster Model = new ProgressMaster();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (Request.QueryString["sid"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.ProcId = 5;
                Model = DB.Proc_CUGLIST(Model).FirstOrDefault();
                ViewBag.ButtonName = "Update";
            }
            else
            {
                ViewBag.ButtonName = "Save";
                Model.ProcId = 4;
                var list = DB.Proc_CUGLIST(Model).ToList();
                if (list.Count > 0)
                {
                    ViewBag.ListData = list;
                }
                else
                {
                    ViewBag.ListData = null;
                }
            }
            ModelState.Clear();
            return View(Model);

        }

        [HttpPost]

        public ActionResult CUGList(ProgressMaster Model, string command)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (command == "Save")
            {
                Model.ProcId = 1;
            }
            if (command == "Update")
            {
                Model.ProcId = 2;
            }

            var list = DB.Proc_CUGLIST(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.ListData = list;
                if (list[0].msg == "success")
                {
                    if (Model.ProcId == 1)
                    {
                        TempData["Message"] = "1";
                    }

                }
                if (list[0].msg == "update")
                {
                    if (Model.ProcId == 2)
                    {

                        TempData["Message"] = "2";
                    }

                }

            }
            else
            {
                TempData["Message"] = "3";
                ViewBag.ListData = null;
            }
            ViewBag.ButtonName = "Save";
            ModelState.Clear();
            return View();
        }

        public JsonResult DeleteCUGList(int Id)
        {
            ProgressMaster Model = new ProgressMaster();
            Model.ProcId = 3;
            Model.Id = Id;
            var data = DB.Proc_CUGLIST(Model).FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditCUGList(int Id)
        {
            return RedirectToAction("CUGList", "Dashboard", new { sid = Id });
        }



        public ActionResult Progress()
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            ProgressMaster Model = new ProgressMaster();

            if (Request.QueryString["sid"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.ProcId = 5;
                Model = DB.Proc_Progress(Model).FirstOrDefault();
                Session["File"] = Model.File;
                ViewBag.ButtonName = "Update";
            }
            else

            {
                ViewBag.ButtonName = "Save";
                Model.ProcId = 4;
                var list = DB.Proc_Progress(Model).ToList();
                if (list.Count > 0)
                {
                    ViewBag.ListData = list;
                }
                else
                {
                    ViewBag.ListData = null;
                }

            }
            return View(Model);
        }
        [HttpPost]
        public ActionResult Progress(ProgressMaster Model, string command, HttpPostedFileBase File)
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (command == "Save")
            {
                Model.ProcId = 1;
            }
            if (command == "Update")
            {
                Model.ProcId = 2;
            }

            string prepath = "~/Content/ProgressDocument";
            string path = "";
            var uploadUrl = Server.MapPath(prepath);
            if (File != null)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["File"].FileName);
                if (extension.ToLower() == ".pdf" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    if (File.ContentLength > 0)
                    {
                        File = Request.Files["File"];
                        string Name = DateTime.Now.Ticks + "_P" + extension.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.File = pathtosave;
                        File.SaveAs(path);
                    }
                }
            }
            else if(Session["File"] !=null)
            {
                Model.File = Session["File"].ToString();
            }
            else
            {
                TempData["Msg"] = "Please upload valid file";
            }
            var list = DB.Proc_Progress(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.ListData = list;
                if (list[0].msg == "success")
                {
                    if (Model.ProcId == 1)
                    {
                        TempData["Message"] = "1";
                    }

                }
                if (list[0].msg == "update")
                {
                    if (Model.ProcId == 2)
                    {

                        TempData["Message"] = "2";
                    }

                }

            }
            else
            {
                TempData["Message"] = "3";
                ViewBag.ListData = null;
            }

            ViewBag.ButtonName = "Save";
            ModelState.Clear();
            return View();

        }

        public JsonResult DeleteProgress(int Id)
        {
            ProgressMaster Model = new ProgressMaster();
            Model.ProcId = 3;
            Model.Id = Id;
            var data = DB.Proc_CUGLIST(Model).FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditProgress(int Id)
        {
            return RedirectToAction("Progress", "Dashboard", new { sid = Id });
        }

        public ActionResult Budget()
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            ProgressMaster Model = new ProgressMaster();

            if (Request.QueryString["sid"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.ProcId = 5;
                Model = DB.Proc_Budget(Model).FirstOrDefault();
                Session["File"] = Model.File;
                ViewBag.ButtonName = "Update";
            }
            else

            {
                ViewBag.ButtonName = "Save";
                Model.ProcId = 4;
                var list = DB.Proc_Budget(Model).ToList();
                if (list.Count > 0)
                {
                    ViewBag.ListData = list;
                }
                else
                {
                    ViewBag.ListData = null;
                }

            }
            return View(Model);
        }
        [HttpPost]
        public ActionResult Budget(ProgressMaster Model, string command, HttpPostedFileBase File)
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (command == "Save")
            {
                Model.ProcId = 1;
            }
            if (command == "Update")
            {
                Model.ProcId = 2;
            }

            string prepath = "~/Content/ProgressDocument";
            string path = "";
            var uploadUrl = Server.MapPath(prepath);
            if (File != null)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["File"].FileName);
                if (extension.ToLower() == ".pdf" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    if (File.ContentLength > 0)
                    {
                        File = Request.Files["File"];
                        string Name = DateTime.Now.Ticks + "_B" + extension.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.File = pathtosave;
                        File.SaveAs(path);
                    }
                }
            }
            else if (Session["File"] != null)
            {
                Model.File = Session["File"].ToString();
            }
            else
            {
                TempData["Msg"] = "Please upload valid file";
            }
            var list = DB.Proc_Budget(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.ListData = list;
                if (list[0].msg == "success")
                {
                    if (Model.ProcId == 1)
                    {
                        TempData["Message"] = "1";
                    }

                }
                if (list[0].msg == "update")
                {
                    if (Model.ProcId == 2)
                    {

                        TempData["Message"] = "2";
                    }

                }

            }
            else
            {
                TempData["Message"] = "3";
                ViewBag.ListData = null;
            }

            ViewBag.ButtonName = "Save";
            ModelState.Clear();
            return View();

        }

        public JsonResult DeleteBudget(int Id)
        {
            ProgressMaster Model = new ProgressMaster();
            Model.ProcId = 3;
            Model.Id = Id;
            var data = DB.Proc_Budget(Model).FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditBudget(int Id)
        {
            return RedirectToAction("Budget", "Dashboard", new { sid = Id });
        }

        public ActionResult PhotoGallery()
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            ProgressMaster Model = new ProgressMaster();

            if (Request.QueryString["sid"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.ProcId = 5;
                Model = DB.Proc_PhotoGallery(Model).FirstOrDefault();
                Session["File"] = Model.File;
                ViewBag.ButtonName = "Update";
            }
            else

            {
                ViewBag.ButtonName = "Save";
                Model.ProcId = 4;
                var list = DB.Proc_PhotoGallery(Model).ToList();
                if (list.Count > 0)
                {
                    ViewBag.ListData = list;
                }
                else
                {
                    ViewBag.ListData = null;
                }

            }
            return View(Model);
        }
        [HttpPost]
        public ActionResult PhotoGallery(ProgressMaster Model, string command, HttpPostedFileBase PhotoPath)
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (command == "Save")
            {
                Model.ProcId = 1;
            }
            if (command == "Update")
            {
                Model.ProcId = 2;
            }

            string prepath = "~/Content/Gallery/PhotoGallery";
            string path = "";
            var uploadUrl = Server.MapPath(prepath);
            if (PhotoPath != null)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["PhotoPath"].FileName);
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    if (PhotoPath.ContentLength > 0)
                    {
                        PhotoPath = Request.Files["PhotoPath"];
                        string Name = DateTime.Now.Ticks + "_pic" + extension.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.PhotoPath = pathtosave;
                        PhotoPath.SaveAs(path);
                    }
                }
            }
            else if (Session["PhotoPath"] != null)
            {
                Model.PhotoPath = Session["PhotoPath"].ToString();
            }
            else
            {
                TempData["Msg"] = "Please upload valid Photo";
            }
            var list = DB.Proc_PhotoGallery(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.ListData = list;
                if (list[0].msg == "success")
                {
                    if (Model.ProcId == 1)
                    {
                        TempData["Message"] = "1";
                    }

                }
                if (list[0].msg == "update")
                {
                    if (Model.ProcId == 2)
                    {

                        TempData["Message"] = "2";
                    }

                }

            }
            else
            {
                TempData["Message"] = "3";
                ViewBag.ListData = null;
            }

            ViewBag.ButtonName = "Save";
            ModelState.Clear();
            return View();

        }

        public JsonResult DeletePhotoGallery(int Id)
        {
            ProgressMaster Model = new ProgressMaster();
            Model.ProcId = 3;
            Model.Id = Id;
            var data = DB.Proc_PhotoGallery(Model).FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditPhotoGallery(int Id)
        {
            return RedirectToAction("PhotoGallery", "Dashboard", new { sid = Id });
        }

        public ActionResult VideoGallery()
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            ProgressMaster Model = new ProgressMaster();

            if (Request.QueryString["sid"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.ProcId = 5;
                Model = DB.Proc_VideoGallery(Model).FirstOrDefault();
                Session["File"] = Model.File;
                ViewBag.ButtonName = "Update";
            }
            else

            {
                ViewBag.ButtonName = "Save";
                Model.ProcId = 4;
                var list = DB.Proc_VideoGallery(Model).ToList();
                if (list.Count > 0)
                {
                    ViewBag.ListData = list;
                }
                else
                {
                    ViewBag.ListData = null;
                }

            }
            return View(Model);
        }
        [HttpPost]
        public ActionResult VideoGallery(ProgressMaster Model, string command, HttpPostedFileBase VideoPath)
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (command == "Save")
            {
                Model.ProcId = 1;
            }
            if (command == "Update")
            {
                Model.ProcId = 2;
            }

            string prepath = "~/Content/Gallery/VideoGallery";
            string path = "";
            var uploadUrl = Server.MapPath(prepath);
            if (VideoPath != null)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["VideoPath"].FileName);
                if (extension.ToLower() == ".mp4" || extension.ToLower() == ".mkv" )
                {
                    if (VideoPath.ContentLength > 0)
                    {
                        VideoPath = Request.Files["VideoPath"];
                        string Name = DateTime.Now.Ticks + "_Vid" + extension.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.VideoPath = pathtosave;
                        VideoPath.SaveAs(path);
                    }
                }
            }
            else if (Session["VideoPath"] != null)
            {
                Model.VideoPath = Session["VideoPath"].ToString();
            }
            else
            {
                TempData["Msg"] = "Please upload valid Video";
            }
            var list = DB.Proc_VideoGallery(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.ListData = list;
                if (list[0].msg == "success")
                {
                    if (Model.ProcId == 1)
                    {
                        TempData["Message"] = "1";
                    }

                }
                if (list[0].msg == "update")
                {
                    if (Model.ProcId == 2)
                    {

                        TempData["Message"] = "2";
                    }

                }

            }
            else
            {
                TempData["Message"] = "3";
                ViewBag.ListData = null;
            }

            ViewBag.ButtonName = "Save";
            ModelState.Clear();
            return View();

        }

        public JsonResult DeleteVideoGallery(int Id)
        {
            ProgressMaster Model = new ProgressMaster();
            Model.ProcId = 3;
            Model.Id = Id;
            var data = DB.Proc_VideoGallery(Model).FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditVideoGallery(int Id)
        {
            return RedirectToAction("VideoGallery", "Dashboard", new { sid = Id });
        }

        #region Suchna
        public ActionResult Suchna()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            ProgressMaster Model = new ProgressMaster();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (Request.QueryString["sid"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.ProcId = 5;
                Model = DB.Proc_Suchna(Model).FirstOrDefault();
                ViewBag.ButtonName = "Update";
            }
            else
            {
                ViewBag.ButtonName = "Save";
                Model.ProcId = 4;
                var list = DB.Proc_Suchna(Model).ToList();
                if (list.Count > 0)
                {
                    ViewBag.ListData = list;
                }
                else
                {
                    ViewBag.ListData = null;
                }
            }
            ModelState.Clear();
            return View(Model);

        }

        [HttpPost]

        public ActionResult Suchna(ProgressMaster Model, string command)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (command == "Save")
            {
                Model.ProcId = 1;
            }
            if (command == "Update")
            {
                Model.ProcId = 2;
            }

            var list = DB.Proc_Suchna(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.ListData = list;
                if (list[0].msg == "success")
                {
                    if (Model.ProcId == 1)
                    {
                        TempData["Message"] = "1";
                    }

                }
                if (list[0].msg == "update")
                {
                    if (Model.ProcId == 2)
                    {

                        TempData["Message"] = "2";
                    }

                }

            }
            else
            {
                TempData["Message"] = "3";
                ViewBag.ListData = null;
            }
            ViewBag.ButtonName = "Save";
            ModelState.Clear();
            return View();
        }

        public JsonResult DeleteSuchna(int Id)
        {
            ProgressMaster Model = new ProgressMaster();
            Model.ProcId = 3;
            Model.Id = Id;
            var data = DB.Proc_Suchna(Model).FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditSuchna(int Id)
        {
            return RedirectToAction("Suchna", "Dashboard", new { sid = Id });
        }

        public ActionResult Nivida()
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            ProgressMaster Model = new ProgressMaster();

            if (Request.QueryString["sid"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.ProcId = 5;
                Model = DB.Proc_Nivida(Model).FirstOrDefault();
                Session["SubFile"] = Model.SubFile;
                ViewBag.ButtonName = "Update";
            }
            else

            {
                ViewBag.ButtonName = "Save";
                Model.ProcId = 4;
                var list = DB.Proc_Nivida(Model).ToList();
                if (list.Count > 0)
                {
                    ViewBag.ListData = list;
                }
                else
                {
                    ViewBag.ListData = null;
                }

            }
            return View(Model);
        }
        [HttpPost]
        public ActionResult Nivida(ProgressMaster Model, string command, HttpPostedFileBase SubFile)
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (command == "Save")
            {
                Model.ProcId = 1;
            }
            if (command == "Update")
            {
                Model.ProcId = 2;
            }

            string prepath = "~/Content/ProgressDocument";
            string path = "";
            var uploadUrl = Server.MapPath(prepath);
            if (SubFile != null)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["SubFile"].FileName);
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".pdf" || extension.ToLower() == ".jpeg")
                {
                    if (SubFile.ContentLength > 0)
                    {
                        SubFile = Request.Files["SubFile"];
                        string Name = DateTime.Now.Ticks + "_ND" + extension.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.SubFile = pathtosave;
                        SubFile.SaveAs(path);
                    }
                }
            }
            else if (Session["SubFile"] != null)
            {
                Model.SubFile = Session["SubFile"].ToString();
            }
            else
            {
                TempData["Msg"] = "Please upload valid File";
            }
            var list = DB.Proc_Nivida(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.ListData = list;
                if (list[0].msg == "success")
                {
                    if (Model.ProcId == 1)
                    {
                        TempData["Message"] = "1";
                    }

                }
                if (list[0].msg == "update")
                {
                    if (Model.ProcId == 2)
                    {

                        TempData["Message"] = "2";
                    }

                }

            }
            else
            {
                TempData["Message"] = "3";
                ViewBag.ListData = null;
            }

            ViewBag.ButtonName = "Save";
            ModelState.Clear();
            return View();

        }

        public JsonResult DeleteNivida(int Id)
        {
            ProgressMaster Model = new ProgressMaster();
            Model.ProcId = 3;
            Model.Id = Id;
            var data = DB.Proc_Nivida(Model).FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditNivida(int Id)
        {
            return RedirectToAction("Nivida", "Dashboard", new { sid = Id });
        }
        #endregion
    }
}