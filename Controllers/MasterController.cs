using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Matikala.DBLayer;
using Matikala.Models;

namespace Matikala.Controllers
{
    public class MasterController : Controller
    {
        DB_Layer DB = new DB_Layer();
        public JsonResult getToMasterSingle(int Procid)
        {
            return Json(DB.Proc_BindMaster(Procid), JsonRequestBehavior.AllowGet);
        }

        // GET: Master
        public JsonResult getToMaster(int Procid = 0, int id = 0)
        {
            var list = DB.Proc_BindMasterById(Procid, id).ToList();
            return Json(DB.Proc_BindMasterById(Procid, id), JsonRequestBehavior.AllowGet);
        }

        // GET: Master
        public JsonResult getToMasterDocs(int Procid = 0, int id = 0)
        {
            var list = DB.Proc_BindMasterDocs(Procid, id).ToList();
            return Json(DB.Proc_BindMasterDocs(Procid, id), JsonRequestBehavior.AllowGet);
        }

        // GET: Master
        public JsonResult getToMastertoVal(int Procid, int id, int id1)
        {
            return Json(DB.Proc_BindMasterByTwoVar(Procid, id, id1), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getDistrictwise(int RadioValue, string FinYear )
        {
            BankMisRpt Model = new BankMisRpt();
            Model.FinYear = FinYear;
            var list = DB.Proc_DistrictWiseRanking(RadioValue, FinYear).ToList();
            return Json(DB.Proc_DistrictWiseRanking(RadioValue, FinYear), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ToStopForm(int DistrictId, string SchemeCode)
        {
            Closure Model = new Closure();
            Model.ProcId = 6;
            Model.DistrictId = DistrictId;
            Model.SchemeCode = SchemeCode;
            //var data = DB.Proc_Closure(Model).FirstOrDefault();
            return Json(DB.Proc_Closure(Model).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}