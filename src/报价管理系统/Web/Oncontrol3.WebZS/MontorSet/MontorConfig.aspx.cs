using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aim.Portal.Web.UI;
using Aim.Portal.Model;
using Aim.Portal.Model;
using Newtonsoft.Json.Linq;

namespace Aim.OnControl.Web.MontorSet
{
    public partial class MontorConfig : BaseListPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (RequestActionString)
            {
                case "reflash":
                    DoFlashData();
                    break;
                case "update":
                    DoUpdate();
                    break;
                case "UpdataState":
                    DoUpdataState();
                    break;
                default:
                    DoSelect();
                    break;
            }
        }
        private void DoUpdataState()
        {
            string ID = RequestData.Get("ID") + "";
            string ISMONTOR = RequestData.Get("ISMONTOR") + "";
            if (!string.IsNullOrEmpty(ID))
            {
                var Ent = SYSMONTORSET.Find(ID);
                Ent.ISMONTOR = ISMONTOR;
                Ent.DoUpdate();
                this.PageState.Add("state", "1");
            }
        }

        private void DoUpdate()
        {
            string jsonObj = RequestData.Get("objJson") + "";
            //string oldObjJon = RequestData.Get("RequestData") + "";
            if (!string.IsNullOrEmpty(jsonObj))
            {
                var Ent = JsonHelper.GetObject<SYSMONTORSET>(jsonObj) as SYSMONTORSET;
                Ent.DoUpdate();
                this.PageState.Add("state", "1");
            }
        }
        private void DoSelect()
        {
            var Ents = SYSMONTORSET.FindAll(SearchCriterion);
            this.PageState.Add("DataList", Ents);
        }

        private void DoFlashData()
        {
            var Ents = SYSTBLMM.FindAll();
            foreach (var item in Ents)
            {
                var clnEnts = SYSTBLCLNSMM.FindAllByProperties(SYSTBLCLNSMM.Prop_REFTBLKEY, item.ID);
                var MontEnt = SYSMONTORSET.FindFirstByProperties(SYSMONTORSET.Prop_TBLENCODE, item.TBLCODE);
                if (MontEnt != null)
                {
                    List<SYSMONTBLCLNSSET> clnsetEnts = JArray.Parse(MontEnt.TBLCLNS).Select(ten => JsonHelper.GetObject<SYSMONTBLCLNSSET>(JsonHelper.GetJsonString(ten)) as SYSMONTBLCLNSSET).ToList();
                    foreach (var it in clnEnts)
                    {
                        if (clnsetEnts.Where(ten => ten.CLNCODE == it.CLNCODE).Count() > 0)
                        {
                            continue;
                        }
                        else
                        {
                            SYSMONTBLCLNSSET tempEnt = new SYSMONTBLCLNSSET();
                            tempEnt.ID = it.ID;
                            tempEnt.CLNCODE = it.CLNCODE;
                            tempEnt.CLNNAME = it.CLNNAME;
                            tempEnt.CLNDATATYPE = it.CLNDATATYPE;
                            tempEnt.ISCHECKED = "N";
                            clnsetEnts.Add(tempEnt);
                        }
                    }
                    MontEnt.TBLCLNS = JsonHelper.GetJsonString(clnsetEnts);
                    MontEnt.DoUpdate();
                }
                else
                {

                    List<SYSMONTBLCLNSSET> fieldEntList = null;
                    foreach (var it in clnEnts)
                    {
                        if (fieldEntList == null)
                            fieldEntList = new List<SYSMONTBLCLNSSET>();
                        SYSMONTBLCLNSSET tempEnt = new SYSMONTBLCLNSSET();
                        tempEnt.ID = it.ID;
                        tempEnt.CLNCODE = it.CLNCODE;
                        tempEnt.CLNNAME = it.CLNNAME;
                        tempEnt.CLNDATATYPE = it.CLNDATATYPE;
                        tempEnt.ISCHECKED = "N";
                        fieldEntList.Add(tempEnt);
                    }

                    SYSMONTORSET mon = new SYSMONTORSET();
                    mon.TBLENCODE = item.TBLCODE;
                    mon.TBLNAME = item.TBLNAME;
                    mon.ISMONTOR = "N";
                    mon.TBLCLNS = JsonHelper.GetJsonString(fieldEntList);
                    mon.DoCreate();
                }
            }
            this.PageState.Add("statue", "1");
        }
    }

}