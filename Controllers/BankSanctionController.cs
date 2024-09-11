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
    public class BankSanctionController : Controller
    {
        DB_Layer DB = new DB_Layer();
        // GET: BankSanction
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult ApplicationSanctionDetail()
        //{
        //    if (Session["UserId"] == null)
        //    {
        //        return RedirectToAction("UserLogin", "Login");
        //    }

        //    SanctionDetail Model = new SanctionDetail();
        //    Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
        //    Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
        //        //ViewBag.ButtonName = "Submit";
        //        //Model.ProcId = 1;
        //        //var list = DB.Proc_SanctionDetails(Model).ToList();
        //        //if (list.Count > 0)
        //        //{
        //        //    ViewBag.ListData = list;
        //        //}
        //        //else
        //        //{
        //        //    ViewBag.ListData = null;
        //        //}

        //    ModelState.Clear();

        //    return View();

        //}

        //[HttpPost]

        //public ActionResult ApplicationSanctionDetail(SanctionDetail Model, string command)
        //{
        //    if (Session["UserId"] == null)
        //    {
        //        return RedirectToAction("UserLogin", "Login");
        //    }

        //    if (command == "Submit")
        //    {
        //        Model.ProcId = 1;
        //        TempData["Message"] = 1;
        //    }

        //    var list = DB.Proc_SanctionDetails(Model).ToList();
        //    if (list.Count > 0)
        //    {
        //        ViewBag.ListData = list;

        //        if (Model.ProcId == 1)
        //        {
        //            TempData["Message"] = "1";
        //        }

        //    }
        //    else
        //    {
        //        ViewBag.ListData = null;
        //    }
        //    ViewBag.ButtonName = "Submit";
        //    ModelState.Clear();
        //    return View();
        //}

        public ActionResult ApplicationSanctionDetail(int Id = 0)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            SanctionDetail Model = new SanctionDetail();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            //Model.ApplicationId = Convert.ToInt32(Session["ApplicationId"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.Id = Id;
            Model.ProcId = 2;
            Model = DB.Proc_SanctionDetails(Model).FirstOrDefault();
            //Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            //Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            //Model.ProcId = 7;
            //Model.Id = Id;
            var list = DB.Proc_SanctionDetails(Model).ToList();
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
        public ActionResult ApplicationSanctionDetail(SanctionDetail Model, string command)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.StatusId = Model.Flag;

            if (Model.StatusId == 4 || Model.StatusId == 6)
            {
                Model.Id = Model.Id;
                Model.Flag = Model.Flag;

                Model.ProcId = 4;

                Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
                Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
                var List = DB.Proc_SanctionDetails(Model).ToList();
                if (List.Count > 0)
                {
                    if (List[0].msg.ToLower() == "success")
                    {
                        if (Model.UserTypeId == 4 && Model.StatusId == 4)
                        {
                            TempData["Message"] = "4";
                        }
                        else if (Model.UserTypeId == 4 && Model.StatusId == 6)
                        {
                            TempData["Message"] = "6";
                        }
                        TempData["Id"] = Model.Id;
                    }

                    else if (List[0].msg.ToLower() == "fail")
                    {
                        TempData["Message"] = 7;
                        TempData["Id"] = Model.Id;
                        //return RedirectToAction("AllAppReport", "Home");
                    }
                }
            }
            return View(Model);
        }

        public ActionResult SanctionLetter(int Id = 0)
        {
            SanctionDetail Model = new SanctionDetail();
            //Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            //Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.Id = Id;
            Model.ProcId = 3;
            Model = DB.Proc_SanctionDetails(Model).FirstOrDefault();
            TempData["msg"] = "00001";
            return View(Model);
        }

        public ActionResult GenerateTDR(int Id = 0)
        {
            SanctionDetail Model = new SanctionDetail();
            //Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            //Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.Id = Id;
            Model.ProcId = 3;
            Model = DB.Proc_SanctionDetails(Model).FirstOrDefault();
            TempData["msg"] = "00001";
            return View(Model);
        }
        public ActionResult MarginLetter(int Id = 0)
        {
            SanctionDetail Model = new SanctionDetail();
            //Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            //Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.Id = Id;
            Model.ProcId = 3;
            Model = DB.Proc_SanctionDetails(Model).FirstOrDefault();
            TempData["msg"] = "00001";
            return View(Model);
        }

        public JsonResult ApplicationTransferBank(int id, int Fid, string UnderProcessRemark, string RejectRemark)
        {
            SanctionDetail Model = new SanctionDetail();
            Model.ProcId = 4;
            //if (Convert.ToInt32(Session["UserType"].ToString()) == 3)
            //{
            //    Model.Id = Convert.ToInt32(Session["Id"].ToString());
            //}
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.UnderProcessRemark = UnderProcessRemark;
            Model.RejectRemark = RejectRemark;

            Model.Id = id;
            Model.Flag = Fid;
            var data = DB.Proc_SanctionDetails(Model).FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MMPayment(int Id = 0, int SanctionId = 0)
        {
            SanctionDetail Model = new SanctionDetail();
            return View(Model);
        }
        [HttpPost]
        public ActionResult MMPayment(SanctionDetail Model, string command)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.Id = Model.Id;
            Model.SanctionId = Model.SanctionId;

            Model.ProcId = 1;
            Model.StepId = 5;

            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());

            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            var List = DB.Proc_SanctionDetails(Model).ToList();
            if (List.Count > 0)
            {
                if (List[0].msg.ToLower() == "success")
                {

                    TempData["Message"] = "1";


                    TempData["Id"] = Model.Id;
                    //return RedirectToAction("AllAppReport", "Home");
                }
                //else if (List[0].msg.ToLower() == "success")
                //{
                //    if (Model.UserTypeId == 4 && Model.StatusId == 4)
                //    {
                //        TempData["Message"] = "4";
                //    }
                //    else if (Model.UserTypeId == 4 && Model.StatusId == 6)
                //    {
                //        TempData["Message"] = "6";
                //    }
                //    TempData["Id"] = Model.Id;
                //}

                else if (List[0].msg.ToLower() == "fail")
                {
                    TempData["Message"] = 3;
                    TempData["Id"] = Model.Id;
                    //return RedirectToAction("AllAppReport", "Home");
                }
            }
            return View(Model);
        }
        //public JsonResult SanctionInStep(int ProcId,string JsonData,int RegId)
        //{
        //    SanctionDetail Model = new SanctionDetail();
        //    Model.ProcId = ProcId;
        //    Model.JSONApproval = JsonData;
        //    Model.Id = RegId;
        //    List<SanctionDetail> Fillamend = JsonConvert.DeserializeObject<List<SanctionDetail>>(Model.JSONApproval);
        //    XElement Amend = new XElement("Amend");
        //    XElement XMAmend = new XElement("XMLAmend",
        //        new XElement("ApprovedCapitalExpenditure", Fillamend[0].ApprovedCapitalExpenditure),
        //        new XElement("BankFinanceCapitalExpenditure", Fillamend[0].BankFinanceCapitalExpenditure)
        //        );
        //    Amend.Add(XMAmend);

        //    XElement XMLAmendset = Amend;
        //    Model.XMLApproval = XMLAmendset.ToString();
        //    //if (Convert.ToInt32(Session["UserType"].ToString()) == 3)
        //    //{
        //    //    Model.Id = Convert.ToInt32(Session["Id"].ToString());
        //    //}
        //    var data = DB.Proc_SanctionInSteps(Model, ProcId).ToList();
        //    //return Json(true, JsonRequestBehavior.AllowGet);
        //    return Json(DB.Proc_SanctionInSteps(Model,ProcId), JsonRequestBehavior.AllowGet);
        //}

        public JsonResult SanctionInStep(int ProcId, string ReceivedDate, decimal AppCapiExp, decimal BankFinanceCapiExp,
            decimal AppWorkCap, decimal BankFinWorkCap, decimal AppTotalProCost, decimal BankFinTotPro,
            string LoanSanDate, int WhetherCovered, decimal RICE, decimal RIWC, decimal LPCE, decimal LRWC,
            decimal MPCE, decimal MPWC, string Remark, string DepositDate, decimal Amount,
            string AccountNumber, decimal MarginMoneyAmount, string ClaimedDate, string MarginMoneyReceived,
            string LoanHeadCEAccount, string LoanHeadCEDate, decimal LoanHeadCEAmount, decimal LoanHeadCETotal,
            string LoanHeadWCAccount, string LoanHeadWCDate, decimal LoanHeadWCAmount, decimal LoanHeadWCTotal,
            int RegId, int SanctionId, int StepId)
        {
            SanctionDetail Model = new SanctionDetail();

            Model.ProcId = ProcId;
            Model.ReceivedDate = ReceivedDate;
            Model.ApprovedCapitalExpenditure = AppCapiExp;
            Model.BankFinanceCapitalExpenditure = BankFinanceCapiExp;
            Model.ApprovedWorkingCapital = AppWorkCap;
            Model.BankFinanceWorkingCapital = BankFinWorkCap;
            Model.ApprovedTotalProjectCost = AppTotalProCost;
            Model.BankFinanceTotalProjectCost = BankFinTotPro;
            Model.LoanSanctionDate = LoanSanDate;
            Model.WhetherCovered = WhetherCovered;
            Model.RateInterestCE = RICE;
            Model.RateInterestWC = RIWC;
            Model.LoanRepaymentCE = LPCE;
            Model.LoanRepaymentWC = LRWC;
            Model.MoratoriumPeriodCE = MPCE;
            Model.MoratoriumPeriodWC = MPWC;
            Model.Remark = Remark;
            Model.DepositDate = DepositDate;
            Model.Amount = Amount;
            Model.AccountNumber = AccountNumber;
            Model.MarginMoneyAmount = MarginMoneyAmount;
            Model.ClaimedDate = ClaimedDate;
            Model.MarginMoneyReceived = MarginMoneyReceived;
            Model.LoanHeadCEAccount = LoanHeadCEAccount;
            Model.LoanHeadCEDate = LoanHeadCEDate;
            Model.LoanHeadCEAmount = LoanHeadCEAmount;
            Model.LoanHeadCETotal = LoanHeadCETotal;
            Model.LoanHeadWCAccount = LoanHeadWCAccount;
            Model.LoanHeadWCDate = LoanHeadWCDate;
            Model.LoanHeadWCAmount = LoanHeadWCAmount;
            Model.LoanHeadWCTotal = LoanHeadWCTotal;
            Model.Id = RegId;
            Model.SanctionId = SanctionId;
            Model.StepId = StepId;

            var data = DB.Proc_SanctionDetails(Model).ToList();
            //return Json(true, JsonRequestBehavior.AllowGet);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UnderProController(int ProcId, int Flag, string ReceivedDate, string UnderProcessRemark, string RejectRemark,
          int Id)
        {
            SanctionDetail Model = new SanctionDetail();
            Model.Flag = Flag;
            Model.ProcId = ProcId;
            Model.ReceivedDate = ReceivedDate;
            Model.UnderProcessRemark = UnderProcessRemark;
            Model.RejectRemark = RejectRemark;
            Model.Id = Id;
            var data = DB.Proc_SanctionDetails(Model).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult SanctionInStep1(int ProcId, string DepositDate, decimal Amount,
        //   string AccountNumber, decimal MarginMoneyAmount, string ClaimedDate, 
        //   int RegId, int SanctionId)
        //{
        //    SanctionDetail Model = new SanctionDetail();
        //    Model.ProcId = ProcId;
        //    Model.DepositDate = DepositDate;
        //    Model.Amount = Amount;
        //    Model.AccountNumber = AccountNumber;
        //    Model.MarginMoneyAmount = MarginMoneyAmount;
        //    Model.ClaimedDate = ClaimedDate;
        //    Model.Id = RegId;
        //    Model.SanctionId = SanctionId;

        //    var data = DB.Proc_SanctionDetails(Model).ToList();
        //    //return Json(true, JsonRequestBehavior.AllowGet);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}


        public ActionResult BankBranch()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            BankBranch Model = new BankBranch();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            if (Request.QueryString["sid"] != null)
            {
                Model.BranchId = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.ProcId = 5;
                Model = DB.proc_BankBranch(Model).FirstOrDefault();
                ViewBag.ButtonName = "Update";
            }
            else
            {
                ViewBag.ButtonName = "Save";
                Model.ProcId = 4;
                var list = DB.proc_BankBranch(Model).ToList();
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

        public ActionResult BankBranch(BankBranch Model, string command)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            if (command == "Save")
            {
                Model.ProcId = 1;
            }
            if (command == "Update")
            {
                Model.ProcId = 2;
            }

            var list = DB.proc_BankBranch(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.ListData = list;
                if (list[0].msg == "success")
                {
                    if (Model.ProcId == 1)
                    {
                        TempData["RTGSCode"] = Model.RTGSCode;
                        TempData["BranchCode"] = Model.BranchCode;
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

        public JsonResult DeleteBankBranch(int BranchId)
        {
            BankBranch Model = new BankBranch();
            Model.ProcId = 3;
            Model.BranchId = BranchId;
            var data = DB.proc_BankBranch(Model).FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditBankBranch(int BranchId)
        {
            return RedirectToAction("BankBranch", "BankSanction", new { sid = BranchId });
        }

        public ActionResult ClosureForm()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Closure Model = new Closure();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            if (Request.QueryString["sid"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.ProcId = 5;
                Model = DB.Proc_Closure(Model).FirstOrDefault();
                ViewBag.ButtonName = "Update";
            }
            else
            {
                ViewBag.ButtonName = "Save";
                Model.ProcId = 4;
                var list = DB.Proc_Closure(Model).ToList();
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

        public ActionResult ClosureForm(Closure Model, string command)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            if (command == "Save")
            {
                Model.ProcId = 1;
            }
            if (command == "Update")
            {
                Model.ProcId = 2;
            }

            var list = DB.Proc_Closure(Model).ToList();
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

        public JsonResult DeleteClosureForm(int sid)
        {
            Closure Model = new Closure();
            Model.Id = sid;
            Model.ProcId = 3;
            var data = DB.Proc_Closure(Model).ToList().FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditClosureForm(int Id)
        {
            return RedirectToAction("ClosureForm", "BankSanction", new { sid = Id });
        }

       

        public ActionResult BankRejection()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            BankBranch Model = new BankBranch();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.ProcId = 6;
            var list = DB.proc_BankBranch(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "No Record";
            }

            TempData["msg"] = "5";
            return View(Model);
        }
        [HttpPost]
        public ActionResult BankRejection(BankBranch Model)
        {
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.ProcId = 6;

            var list = DB.proc_BankBranch(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "No Record";
            }
            return View(Model);
        }

        public ActionResult BankMisRpt()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            BankMisRpt Model = new BankMisRpt();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            //Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            //Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            //Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            //Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            return View();
        }
        [HttpPost]
        public ActionResult BankMisRpt(BankMisRpt Model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            Model.ProcId = 1;

            var list = DB.Proc_MisReport(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
            }
            return View(Model);
        }


        public ActionResult MisRptToolKitScheme()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            BankMisRpt Model = new BankMisRpt();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            return View();
        }
        [HttpPost]
        public ActionResult MisRptToolKitScheme(BankMisRpt Model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            Model.ProcId = 2;

            var list = DB.Proc_MisReport(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
            }
            return View(Model);
        }

        public ActionResult MisRptKaushalScheme()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            BankMisRpt Model = new BankMisRpt();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            Model.ProcId = 3;
            return View();
        }
        [HttpPost]
        public ActionResult MisRptKaushalScheme(BankMisRpt Model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            Model.ProcId = 3;

            var list = DB.Proc_MisReport(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
            }
            return View(Model);
        }
        public ActionResult MisRptSchemeDrill()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Registration Model = new Registration();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (Request.QueryString["DistrictId"] != null)
            {
                Model.DistrictId = Convert.ToInt32(Request.QueryString["DistrictId"].ToString());
                Model.ProcId = Convert.ToInt32(Request.QueryString["ProcId"].ToString());
                Model.FinYear = Convert.ToString(Request.QueryString["FinYear"].ToString());
                Model.SchemeCode = Convert.ToString(Request.QueryString["SchemeCode"].ToString());
                Model.ExpinTrand = Convert.ToInt32(Request.QueryString["ExpinTrand"].ToString());
            }

            var list = DB.Proc_MisRptSchemeDrill(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.Status = "Show";
                ViewBag.listdata = list;
                ViewBag.CADistrictName = list[0].CADistrictName;
                ViewBag.ToolkitName = list[0].ToolkitName;
                ViewBag.FinYear = Model.FinYear;
                ViewBag.SchemeCode = list[0].SchemeCode;
            }
            else
            {
             
                
                ViewBag.FinYear = Model.FinYear;
                ViewBag.SchemeCode = Model.SchemeCode;
                ViewBag.Status = "";
                ViewBag.listdata = null;
            }
            return View();
        }
        public ActionResult MisRptVipdamScheme()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            BankMisRpt Model = new BankMisRpt();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            Model.ProcId = 4;
            return View();
        }
        [HttpPost]
        public ActionResult MisRptVipdamScheme(BankMisRpt Model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            Model.ProcId = 4;

            var list = DB.Proc_MisReport(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
            }
            return View(Model);
        }

        public ActionResult MisRptAwardScheme()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            BankMisRpt Model = new BankMisRpt();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            Model.ProcId = 5;
            return View();
        }
        [HttpPost]
        public ActionResult MisRptAwardScheme(BankMisRpt Model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            Model.ProcId = 5;

            var list = DB.Proc_MisReport(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
                ViewBag.ZoneCityName = list[0].ZoneCityName;
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
            }
            return View(Model);
        }

        public ActionResult MisRptMicroScheme()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            BankMisRpt Model = new BankMisRpt();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            Model.ProcId = 6;
            return View();
        }
        [HttpPost]
        public ActionResult MisRptMicroScheme(BankMisRpt Model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            Model.ProcId = 6;

            var list = DB.Proc_MisReport(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
            }
            return View(Model);
        }
        public ActionResult MisRptDrill()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            SanctionDetail Model = new SanctionDetail();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            if (Request.QueryString["DistrictId"] != null)
            {
                Model.DistrictId = Convert.ToInt32(Request.QueryString["DistrictId"].ToString());
                Model.ProcId = Convert.ToInt32(Request.QueryString["ProcId"].ToString());
                Model.DivCode = Convert.ToInt32(Request.QueryString["DivisionCode"].ToString());
                Model.TraningId = Convert.ToInt32(Request.QueryString["TraningId"].ToString());
                Model.BranchId = Convert.ToInt32(Request.QueryString["UserId"].ToString());
                Model.FinYear = Convert.ToString(Request.QueryString["FinYear"].ToString());
                //Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserType"].ToString());
            }


            //Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            //Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            // Model.ProcId = 1;
            var list = DB.Proc_MisRptDrill(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                if (Request.QueryString["CADistrictName"]==null)
                {
                    ViewBag.CADistrictName = list[0].CADistrictName;
                }
                else
                {
                    ViewBag.CADistrictName = Request.QueryString["CADistrictName"].ToString();
                }
                ViewBag.FinYear = Model.FinYear;
            }
            else
            {
                ViewBag.listdata = null;
            }
            return View();
        }

        public ActionResult BankWiseRptDrill(string FinYear)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            BankMisRpt Model = new BankMisRpt();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            //Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            //Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            if (Request.QueryString["DistrictId"] != null)
            {
                Model.DistrictId = Convert.ToInt32(Request.QueryString["DistrictId"].ToString());
                Model.FinYear = Convert.ToString(Request.QueryString["FinYear"].ToString());
                Model.City = Convert.ToString(Request.QueryString["City"].ToString());
            }
            if(Request.QueryString["Flag"]!=null)
            {
                Model.Flag = Convert.ToInt32(Request.QueryString["Flag"].ToString());
            }
            else
            {
                Model.Flag = 0;
            }
            Model.ProcId = 8;
            Model.FinYear = FinYear;
            var list = DB.Proc_MisReport(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
            }
            else
            {
                ViewBag.listdata = null;
            }
            return View();
        }

        public ActionResult DistrictWiseRptDrill(string FinYear)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            BankMisRpt Model = new BankMisRpt();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            //Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            if (Request.QueryString["DistrictId"] != null)
            {
                Model.DistrictId = Convert.ToInt32(Request.QueryString["DistrictId"].ToString());
                Model.City = Convert.ToString(Request.QueryString["City"].ToString());
                Model.FinYear = Convert.ToString(Request.QueryString["FinYear"].ToString());
            }
            Model.ProcId = 7;
            Model.FinYear = FinYear;
            var list = DB.Proc_MisReport(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
            }
            else
            {
                ViewBag.listdata = null;
            }
            return View();
        }


        public ActionResult DistrictWiseRanking()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            BankMisRpt Model = new BankMisRpt();
            return View();
        }
        [HttpPost]
        public ActionResult DistrictWiseRanking(BankMisRpt Model, string command)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            //var list = DB.Proc_DistrictWiseRank(Model).ToList();
            //if (list.Count > 0)
            //{
            //    ViewBag.listdata = list;
            //}
            //else
            //{
            //    ViewBag.listdata = null;
            //}
            return View();
        }

        public ActionResult UploadSuchi()
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            ProgressMaster Model = new ProgressMaster();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.DivisionId = Convert.ToInt32(Session["DivisionCode"].ToString());

            if (Request.QueryString["sid"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.ProcId = 5;
                Model = DB.Proc_UploadSuchi(Model).FirstOrDefault();
                Session["Documents"] = Model.Documents;
                ViewBag.ButtonName = "Update";
            }
            else

            {
                ViewBag.ButtonName = "Save";
                Model.ProcId = 4;
                var list = DB.Proc_UploadSuchi(Model).ToList();
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
        public ActionResult UploadSuchi(ProgressMaster Model, string command, HttpPostedFileBase Documents)
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DivisionId = Convert.ToInt32(Session["DivisionCode"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            if (command == "Save")
            {
                List<ProgressMaster> Fillamend = JsonConvert.DeserializeObject<List<ProgressMaster>>(Model.JSONApproval);
                XElement Amend = new XElement("Amend");
                for (int i = 0; i < Fillamend.Count; i++)
                {
                    XElement XMAmend = new XElement("XMLAmend",
                        new XElement("ApplicantId", Fillamend[i].ApplicantId)
                        );
                    Amend.Add(XMAmend);
                }

                XElement XMLAmendset = Amend;
                Model.XMLApproval = XMLAmendset.ToString();
                Model.ProcId = 1;

            }
            if (command == "Update")
            {
                Model.ProcId = 2;
            }

            string prepath = "~/Content/UploadSuchi";
            string path = "";
            var uploadUrl = Server.MapPath(prepath);
            if (Documents != null)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["Documents"].FileName);
                if (extension.ToLower() == ".pdf" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    if (Documents.ContentLength > 0)
                    {
                        Documents = Request.Files["Documents"];
                        string Name = DateTime.Now.Ticks + "_US" + extension.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.Documents = pathtosave;
                        Documents.SaveAs(path);
                    }
                }
            }
            else if (Session["Documents"] != null)
            {
                Model.Documents = Session["Documents"].ToString();
            }
            else
            {
                TempData["Msg"] = "Please upload valid Documents";
            }
            var list = DB.Proc_UploadSuchi(Model).ToList();
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

        public JsonResult DeleteUploadSuchi(int Id)
        {
            ProgressMaster Model = new ProgressMaster();
            Model.ProcId = 3;
            Model.Id = Id;
            var data = DB.Proc_UploadSuchi(Model).FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditUploadSuchi(int Id)
        {
            return RedirectToAction("UploadSuchi", "BankSanction", new { sid = Id });
        }

        public ActionResult TrainingSheduled()
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
                Model = DB.Proc_TraningSheduled(Model).FirstOrDefault();
                Session["TraningFile"] = Model.TraningFile;
                ViewBag.ButtonName = "Update";
            }
            else

            {
                ViewBag.ButtonName = "Scheduled Training";
                Model.ProcId = 4;
                var list = DB.Proc_TraningSheduled(Model).ToList();
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
        public ActionResult TrainingSheduled(ProgressMaster Model, string command, HttpPostedFileBase TraningFile)
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (command == "Scheduled Training")
            {
                List<ProgressMaster> Fillamend = JsonConvert.DeserializeObject<List<ProgressMaster>>(Model.JSONApproval);
                XElement Amend = new XElement("Amend");
                for (int i = 0; i < Fillamend.Count; i++)
                {
                    XElement XMAmend = new XElement("XMLAmend",
                        new XElement("ApplicantId", Fillamend[i].ApplicantId)
                        );
                    Amend.Add(XMAmend);
                }

                XElement XMLAmendset = Amend;
                Model.XMLApproval = XMLAmendset.ToString();
                Model.TrainingOfficeId = Convert.ToInt32(Session["TraningId"].ToString());
                Model.ProcId = 1;

            }
            if (command == "Update")
            {
                Model.ProcId = 2;
            }

            string prepath = "~/Content/UploadSuchi";
            string path = "";
            var uploadUrl = Server.MapPath(prepath);
            if (TraningFile != null)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["TraningFile"].FileName);
                if (extension.ToLower() == ".pdf" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    if (TraningFile.ContentLength > 0)
                    {
                        TraningFile = Request.Files["TraningFile"];
                        string Name = DateTime.Now.Ticks + "_US" + extension.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.TraningFile = pathtosave;
                        TraningFile.SaveAs(path);
                    }
                }
            }
            else if (Session["TraningFile"] != null)
            {
                Model.TraningFile = Session["TraningFile"].ToString();
            }
            else
            {
                TempData["Msg"] = "Please upload valid Training File";
            }
            var list = DB.Proc_TraningSheduled(Model).ToList();
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

            ViewBag.ButtonName = "Scheduled Training";
            ModelState.Clear();
            return View();

        }

        public JsonResult DeleteTrainingSheduled(int Id)
        {
            ProgressMaster Model = new ProgressMaster();
            Model.ProcId = 3;
            Model.Id = Id;
            var data = DB.Proc_TraningSheduled(Model).FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditTrainingSheduled(int Id)
        {
            return RedirectToAction("TrainingSheduled", "BankSanction", new { sid = Id });
        }


        public ActionResult TrainingCompletion()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            ProgressMaster Model = new ProgressMaster();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            return View();
        }
        [HttpPost]
        public ActionResult TrainingCompletion(ProgressMaster Model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.TrainingOfficeId = Convert.ToInt32(Session["TraningId"].ToString());
            Model.ProcId = 7;

            var list = DB.Proc_TraningSheduled(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
            }
            return View(Model);
        }

        public ActionResult FinalCompletedTraining()
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            ProgressMaster Model = new ProgressMaster();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());

            if (Request.QueryString["Id"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                Model.ProcId = 5;
                Model = DB.Proc_TraningSheduled(Model).FirstOrDefault();
                Session["TrainingCompletionFile"] = Model.TrainingCompletionFile;
                ViewBag.ButtonName = "Final Complete Training";
            }
            else

            {
                ViewBag.ButtonName = "Final Complete Training";
                Model.ProcId = 4;
                var list = DB.Proc_TraningSheduled(Model).ToList();
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
        //public ActionResult FinalCompletedTraining(ProgressMaster Model, string command, HttpPostedFileBase TrainingCompletionFile)
        //{

        //    if (Session["UserId"] == null)
        //    {
        //        return RedirectToAction("UserLogin", "Login");
        //    }
        //    Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
        //    if (command == "Final Complete Training")
        //    {
        //        Model.TrainingOfficeId = Convert.ToInt32(Session["TraningId"].ToString());
        //        Model.ProcId = 8;

        //    }


        //    string prepath = "~/Content/UploadSuchi";
        //    string path = "";
        //    var uploadUrl = Server.MapPath(prepath);
        //    if (TrainingCompletionFile != null)
        //    {
        //        string extension = System.IO.Path.GetExtension(Request.Files["TrainingCompletionFile"].FileName);
        //        if (extension.ToLower() == ".pdf" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
        //        {
        //            if (TrainingCompletionFile.ContentLength > 0)
        //            {
        //                TrainingCompletionFile = Request.Files["TrainingCompletionFile"];
        //                string Name = DateTime.Now.Ticks + "_TC" + extension.ToLower();
        //                string pathtosave = prepath + "/" + Name;
        //                path = uploadUrl + "/" + Name;
        //                Model.TrainingCompletionFile = pathtosave;
        //                TrainingCompletionFile.SaveAs(path);
        //            }
        //        }
        //    }
        //    else if (Session["TrainingCompletionFile"] != null)
        //    {
        //        Model.TrainingCompletionFile = Session["TrainingCompletionFile"].ToString();
        //    }
        //    else
        //    {
        //        TempData["Msg"] = "Please upload valid Training File";
        //    }

        //    if (Model.ProcId == 8)
        //    {
        //        TempData["Message"] = "1";
        //    }

        //    else
        //    {
        //        TempData["Message"] = "3";
        //        ViewBag.ListData = null;
        //    }

        //    ViewBag.ButtonName = "Final Complete Training";
        //    ModelState.Clear();
        //    return View();

        //}


        public ActionResult FinalCompletedTraining(ProgressMaster Model, string command, HttpPostedFileBase TrainingCompletionFile)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());

            if (command == "Final Complete Training")
            {
                // Validate user input (optional, but recommended for security)
                if (!ModelState.IsValid)
                {
                    ViewBag.ButtonName = "Final Complete Training";
                    return View(Model); // Return with validation errors
                }

                Model.TrainingOfficeId = Convert.ToInt32(Session["TraningId"].ToString());


                // Handle file upload and validation
                string prepath = "~/Content/UploadSuchi";
                string path = "";
                var uploadUrl = Server.MapPath(prepath);
                if (TrainingCompletionFile != null)
                {
                    string extension = System.IO.Path.GetExtension(Request.Files["TrainingCompletionFile"].FileName).ToLower();
                    if (extension == ".pdf" || extension == ".png" || extension == ".jpeg")
                    {
                        if (TrainingCompletionFile.ContentLength > 0)
                        {
                            TrainingCompletionFile = Request.Files["TrainingCompletionFile"];
                            string name = DateTime.Now.Ticks + "_TC" + extension;
                            string pathToSave = prepath + "/" + name;
                            path = uploadUrl + "/" + name;
                            Model.TrainingCompletionFile = pathToSave;
                            TrainingCompletionFile.SaveAs(path);
                        }
                    }
                    else
                    {
                        TempData["Msg"] = "Invalid file format. Please upload a PDF, PNG, or JPEG file.";
                        ViewBag.ButtonName = "Final Complete Training";
                        return View(Model);
                    }
                }
                else if (Session["TrainingCompletionFile"] == null)
                {
                    TempData["Msg"] = "Please upload a valid Training File.";
                    ViewBag.ButtonName = "Final Complete Training";
                    return View(Model);
                }
                Model.ProcId = 8;
                var list = DB.Proc_TraningSheduled(Model).ToList();
                if (list.Count > 0)
                {
                    if (list[0].msg == "Success")
                    {
                        TempData["Message"] = "1";
                    }
                    else
                    {
                        TempData["Message"] = "2";
                    }
                }


                ModelState.Clear();
                return View();
            }
            else
            {
                // Handle other button clicks (if applicable)
                ViewBag.ButtonName = "Final Complete Training";
                // ... (code for other button actions)
                return View(Model);
            }
        }





        public ActionResult TrainingYozana()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            BankMisRpt Model = new BankMisRpt();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            return View();
        }
        [HttpPost]
        public ActionResult TrainingYozana(BankMisRpt Model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            Model.ProcId = 9;

            var list = DB.Proc_MisReport(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
            }
            Model.ProcId = 10;

            var listtotal = DB.Proc_MisReport(Model).ToList();
            if (listtotal.Count > 0)
            {
                ViewBag.RS_1 = listtotal[0].TotalCity;
                ViewBag.RS_2 = listtotal[1].TotalCity;
                ViewBag.RS_3 = listtotal[2].TotalCity;
                ViewBag.RS_4 = listtotal[3].TotalCity;
                ViewBag.RS_5 = listtotal[4].TotalCity;
                ViewBag.RS_6 = listtotal[5].TotalCity;
                ViewBag.RS_7 = listtotal[6].TotalCity;
                ViewBag.RS_8 = listtotal[7].TotalCity;
                ViewBag.RS_9 = listtotal[8].TotalCity;
                ViewBag.RS_10 = listtotal[9].TotalCity;
                ViewBag.RS_11 = listtotal[10].TotalCity;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.Status = "";
            }
            return View(Model);
        }

        public ActionResult SwapFinYear()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Registration Model = new Registration();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            ViewBag.ButtonName = "Change Financial Year";
            return View(Model);
        }
        [HttpPost]
        public ActionResult SwapFinYear(Registration Model, string command)
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (command == "Change Financial Year")
            {
                List<Registration> ExpSet = JsonConvert.DeserializeObject<List<Registration>>(Model.ListJson);
                //List<Registration> ExpSet = JsonConvert.DeserializeObject<List<Registration>>(Model.UpdatedId);

                for (int i = 0; i < ExpSet.Count; i++)
                {
                    Model.UpdatedId += (ExpSet[i].Id.ToString());
                    if (i < ExpSet.Count - 1)
                    {
                        Model.UpdatedId += ",";
                    }
                }

                //List<Registration> Fillamend = JsonConvert.DeserializeObject<List<Registration>>(Model.JSONApproval);
                //XElement Amend = new XElement("Amend");
                //for (int i = 0; i < Fillamend.Count; i++)
                //{
                //    XElement XMAmend = new XElement("XMLAmend",
                //        new XElement("Id", Fillamend[i].Id)
                //        );
                //    Amend.Add(XMAmend);
                //}

                //XElement XMLAmendset = Amend;
                //Model.XMLApproval = XMLAmendset.ToString();

                Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
                Model.ProcId = 22;

                var list = DB.proc_Registration(Model).ToList();
                if (list.Count > 0)
                {
                    if (list[0].msg == "success")
                    {
                        TempData["Message"] = "1";
                        TempData["FinYear"] = list[0].FinYear;
                        TempData["UpdatedFinancialYear"] = list[0].UpdatedFinancialYear;
                    }
                    else if(list[0].msg == "Fail")
                    {
                        TempData["Message"] = "2";
                    }

                }
                else
                {
                    TempData["Message"] = "3";
                }
            }
            ViewBag.ButtonName = "Change Financial Year";
            ModelState.Clear();
            return View();

        }

        public ActionResult BranchWiseRpt()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            SanctionDetail Model = new SanctionDetail();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            Model.BranchId = Convert.ToInt32(Session["UserId"].ToString());
            Model.TraningId = Convert.ToInt32(Session["TraningId"].ToString());
            if (Request.QueryString["DistrictId"] != null)
            {
                Model.DistrictId = Convert.ToInt32(Request.QueryString["DistrictId"].ToString());
                Model.ProcId = Convert.ToInt32(Request.QueryString["ProcId"].ToString());
                Model.BankId = Convert.ToInt32(Request.QueryString["BankId"].ToString());
                Model.FinYear = Convert.ToString(Request.QueryString["FinYear"].ToString());
            }
            var list = DB.Proc_MisRptDrill(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;
                ViewBag.CADistrictName = list[0].CADistrictName;
                ViewBag.BankName = list[0].BankName;
                ViewBag.FinYear = Model.FinYear;
            }
            else
            {
                ViewBag.listdata = null;
            }
            return View();
        }

        public ActionResult PuraskarYozana()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            BankMisRpt Model = new BankMisRpt();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            return View();
        }
        [HttpPost]
        public ActionResult PuraskarYozana(BankMisRpt Model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
            Model.DivCode = Convert.ToInt32(Session["DivisionCode"].ToString());
            Model.ProcId = 11;

            var list = DB.Proc_MisReport(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.listdata = list;

                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.listdata = null;
                ViewBag.Status = "";
            }
            Model.ProcId = 12;

            var listtotal = DB.Proc_MisReport(Model).ToList();
            if (listtotal.Count > 0)
            {
                ViewBag.RS_1 = listtotal[0].TotalCity;
                ViewBag.RS_2 = listtotal[1].TotalCity;
                ViewBag.RS_3 = listtotal[2].TotalCity;
                ViewBag.RS_4 = listtotal[3].TotalCity;
                ViewBag.RS_5 = listtotal[4].TotalCity;
                ViewBag.RS_6 = listtotal[5].TotalCity;
                ViewBag.RS_7 = listtotal[6].TotalCity;
                ViewBag.RS_8 = listtotal[7].TotalCity;
                ViewBag.RS_9 = listtotal[8].TotalCity;
                ViewBag.RS_10 = listtotal[9].TotalCity;
                ViewBag.RS_11 = listtotal[10].TotalCity;
                ViewBag.RS_12 = listtotal[11].TotalCity;
                ViewBag.RS_13 = listtotal[12].TotalCity;
                ViewBag.RS_14 = listtotal[13].TotalCity;
                ViewBag.RS_15 = listtotal[14].TotalCity;
                ViewBag.RS_16 = listtotal[15].TotalCity;
                ViewBag.RS_17 = listtotal[16].TotalCity;
                ViewBag.RS_18 = listtotal[17].TotalCity;
                ViewBag.Status = "Show";
            }
            else
            {
                ViewBag.Status = "";
            }
            return View(Model);
        }


        public ActionResult ReturnForm()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Closure Model = new Closure();
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            ViewBag.ButtonName = "Change Status";
            return View(Model);
        }
        [HttpPost]
        public ActionResult ReturnForm(Closure Model, string command)
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (command == "Change Status")
            {
                List<Registration> ExpSet = JsonConvert.DeserializeObject<List<Registration>>(Model.ListJson);
                //List<Registration> ExpSet = JsonConvert.DeserializeObject<List<Registration>>(Model.UpdatedId);

                for (int i = 0; i < ExpSet.Count; i++)
                {
                    Model.UpdatedId += (ExpSet[i].Id.ToString());
                    if (i < ExpSet.Count - 1)
                    {
                        Model.UpdatedId += ",";
                    }
                }

                //List<Registration> Fillamend = JsonConvert.DeserializeObject<List<Registration>>(Model.JSONApproval);
                //XElement Amend = new XElement("Amend");
                //for (int i = 0; i < Fillamend.Count; i++)
                //{
                //    XElement XMAmend = new XElement("XMLAmend",
                //        new XElement("Id", Fillamend[i].Id)
                //        );
                //    Amend.Add(XMAmend);
                //}

                //XElement XMLAmendset = Amend;
                //Model.XMLApproval = XMLAmendset.ToString();

                //Model.DistrictId = Convert.ToInt32(Session["DistrictCode"].ToString());
                Model.ProcId = 8;

                var list = DB.Proc_Closure(Model).ToList();
                if (list.Count > 0)
                {
                    if (list[0].msg == "success")
                    {
                        TempData["Message"] = "1";
                        TempData["FinYear"] = list[0].FinYear;
                        TempData["SchemeCode"] = list[0].SchemeCode;
                    }
                    else if (list[0].msg == "Fail")
                    {
                        TempData["Message"] = "2";
                    }

                }
                else
                {
                    TempData["Message"] = "3";
                }
            }
            ViewBag.ButtonName = "Change Status";
            ModelState.Clear();
            return View();

        }
    }
}

