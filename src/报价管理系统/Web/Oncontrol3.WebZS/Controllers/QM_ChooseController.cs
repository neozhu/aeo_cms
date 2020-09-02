using Aim;
using Aim.Data;
using Aim.Portal.Model;
using Aim.Portal.Web;
using Castle.ActiveRecord;
using Com.Feiliks.QDM;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Oncontrol3.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oncontrol3.Web.Controllers
{
    /// <summary>
    /// 返回消息
    /// </summary>

    public class QM_ChooseController : BaseController
    {
        [AllowAnonymous]
        public ActionResult QM_ChooseIndex()
        {
            //ViewBag.StatusSel = JsonHelper.GetJsonString(SysEnumeration.GetEnumComboDatas("Status"));//追加状态枚举
            //ViewBag.Status = JsonHelper.GetJsonString(SysEnumeration.GetEnumDict("Status"));
            return View();
        }

        [AllowAnonymous]
        public ActionResult QM_ChooseEdit()
        {
            return View();
        }

    }
}
