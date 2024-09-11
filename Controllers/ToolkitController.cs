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
using Newtonsoft.Json;
using System.Xml.Linq;

namespace Matikala.Controllers
{
    public class ToolkitController : Controller
    {
        DB_Layer DB = new DB_Layer();
        // GET: Toolkit
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ToolkitApplication()
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            Toolkit Model = new Toolkit();

            if (Request.QueryString["sid"] != null)
            {
                Model.Id = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model.ProcId = 5;
                Model = DB.Proc_ToolkitRegistration(Model).FirstOrDefault();
                Model.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"].ToString());
                ViewBag.buttonname = "Update";
                Session["DocQualificationReport"] = Model.DocQualificationReport;
                Session["DocJatiReport"] = Model.DocJatiReport;
                Session["DocRationReport"] = Model.DocRationReport;
                Session["DocPhoto"] = Model.DocPhoto;
            }
            else

            {
                ViewBag.ButtonName = "Submit";
                Model.ProcId = 4;
                var list = DB.Proc_ToolkitRegistration(Model).ToList();
                if (list.Count > 0)
                {
                    ViewBag.ListData = list;
                }
                else
                {
                    ViewBag.ListData = null;
                }
                Session["DocQualificationReport"] = "";
                Session["DocJatiReport"] = "";
                Session["DocRationReport"] = "";
                Session["DocPhoto"] = "";
            }
            return View(Model);
        }
        [HttpPost]
        public ActionResult ToolkitApplication(Toolkit Model, string command, HttpPostedFileBase DocQualificationReport,HttpPostedFileBase DocJatiReport,HttpPostedFileBase DocRationReport,HttpPostedFileBase DocPhoto)
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            Model.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (command == "Submit")
            {
                List<Toolkit> Fillamend = JsonConvert.DeserializeObject<List<Toolkit>>(Model.JSONApproval);
                XElement Amend = new XElement("Amend");
                for (int i = 0; i < Fillamend.Count; i++)
                {
                    XElement XMAmend = new XElement("XMLAmend",
                        new XElement("MemberName", Fillamend[i].MemberName),
                        new XElement("RelationId", Fillamend[i].RelationId),
                        new XElement("MemberAgeId", Fillamend[i].MemberAgeId)
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

            string prepath = "~/Content/ToolkitDocument";
            string path = "";
            var uploadUrl = Server.MapPath(prepath);
            //Random r = new Random(6);
            //string number = r.ToString();
            string extensionPdf1 = System.IO.Path.GetExtension(Request.Files["DocQualificationReport"].FileName);
            if (DocQualificationReport != null)
            {
                if (extensionPdf1.ToLower() == ".pdf" || extensionPdf1.ToLower() == ".png" || extensionPdf1.ToLower() == ".jpeg")
                {
                    if (DocQualificationReport.ContentLength > 0)
                    {
                        DocQualificationReport = Request.Files["DocQualificationReport"];
                        string Name = DateTime.Now.Ticks + "_QR" + extensionPdf1.ToLower();
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
            string extensionPdf3 = System.IO.Path.GetExtension(Request.Files["DocRationReport"].FileName);
            if (DocRationReport != null)
            {
                if (extensionPdf3.ToLower() == ".pdf" || extensionPdf3.ToLower() == ".png" || extensionPdf3.ToLower() == ".jpeg")
                {
                    if (DocRationReport.ContentLength > 0)
                    {
                        DocRationReport = Request.Files["DocRationReport"];
                        string Name = DateTime.Now.Ticks + "_RR" + extensionPdf3.ToLower();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.DocRationReport = pathtosave;
                        DocRationReport.SaveAs(path);

                    }
                }
            }
            else
            {
                Model.DocRationReport = Session["DocRationReport"].ToString();
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
            var list = DB.Proc_ToolkitRegistration(Model).ToList();
            if (list.Count > 0)
            {
                ViewBag.ListData = list;
                if (list[0].msg == "success")
                {
                    if (Model.ProcId == 1)
                    {
                        TempData["Message"] = "1";
                        TempData["regn"] = list[0].ApplicationId;
                        TempData["Id"] = list[0].Id;
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

            ViewBag.ButtonName = "Submit";
            ModelState.Clear();
            return View();

        }

        public JsonResult DeleteToolkitApplication(int Id)
        {
            Toolkit Model = new Toolkit();
            Model.ProcId = 3;
            Model.Id = Id;
            var data = DB.Proc_ToolkitRegistration(Model).FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditToolkitApplication(int Id)
        {
            return RedirectToAction("ToolkitApplication", "Toolkit", new { sid = Id });
        }
    }
}