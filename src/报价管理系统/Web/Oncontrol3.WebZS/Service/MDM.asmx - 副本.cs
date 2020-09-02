using Aim.Data;
using Com.Feiliks.MDM;
using Foqus;
using Oncontrol3.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Oncontrol3.Web.Service
{
    /// <summary>
    /// MDMMAIN 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class MDM : System.Web.Services.WebService
    {

        [WebMethod]
        public bool MDMMAIN(MDM_MAIN_BASIC[] basics, MDM_MAIN_STRC[] strcs, MDM_MIAN_VALUE[] vals)
        {
            bool flag = true;
            if (basics != null)
            {
                MDM_MAIN_BASIC.DeleteAll();

                foreach (MDM_MAIN_BASIC b in basics)
                {
                    try
                    {
                        b.CREATETIME = DateTime.Now;
                        b.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            if (strcs != null)
            {
                MDM_MAIN_STRC.DeleteAll();
                foreach (MDM_MAIN_STRC s in strcs)
                {
                    try
                    {
                        s.CREATETIME = DateTime.Now;
                        s.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            if (vals != null)
            {
                MDM_MIAN_VALUE.DeleteAll();
                foreach (MDM_MIAN_VALUE v in vals)
                {
                    try
                    {
                        v.CREATETIME = DateTime.Now;
                        v.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }

        [WebMethod]
        public bool MDMCALCBSV(MDM_CALC_BASIC[] basics, MDM_CALC_STRC[] strcs, MDM_CALC_VALUE[] vals)
        {
            Log log = new Log(AppDomain.CurrentDomain.BaseDirectory + @"/servicelog/" + DateTime.Now.ToString("yyyyMMdd") + "mdmcalcbsv.txt");

            bool flag = true;
            if (basics != null)
            {
                MDM_CALC_BASIC.DeleteAll();

                foreach (MDM_CALC_BASIC b in basics)
                {
                    try
                    {
                        b.CREATETIME = DateTime.Now;
                        b.DoCreate();
                    }
                    catch (Exception ex)
                    {
                        log.log(ex.Message);
                        flag = false;
                        continue;
                    }
                }
            }

            if (strcs != null)
            {
                MDM_CALC_STRC.DeleteAll();
                foreach (MDM_CALC_STRC s in strcs)
                {
                    try
                    {
                        s.CREATETIME = DateTime.Now;
                        s.DoCreate();
                    }
                    catch (Exception ex)
                    {
                        log.log(ex.Message);
                        flag = false;
                        continue;
                    }
                }
            }

            if (vals != null)
            {
                MDM_CALC_VALUE.DeleteAll();
                foreach (MDM_CALC_VALUE v in vals)
                {
                    try
                    {
                        v.CREATETIME = DateTime.Now;
                        v.DoCreate();
                    }
                    catch (Exception ex)
                    {
                        log.log(ex.Message);
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }

        [WebMethod]
        public bool MDMMVMTYBIZSTGREF(MDM_MVMTY_BIZSTG_REF[] refs)
        {
            bool flag = true;
            if (refs != null)
            {
                MDM_MVMTY_BIZSTG_REF.DeleteAll();
                foreach (MDM_MVMTY_BIZSTG_REF mbref in refs)
                {
                    try
                    {
                        mbref.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }

        [WebMethod]
        public bool MDMPRODUCT(MDM_PRODUCT[] prds)
        {
            bool flag = true;
            if (prds != null)
            {
                MDM_PRODUCT.DeleteAll();
                foreach (MDM_PRODUCT prd in prds)
                {
                    try
                    {
                        prd.CREATETIME = DateTime.Now;
                        prd.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }
        [WebMethod]
        public bool MDMFEE(MDM_FEE[] fees)
        {
            bool flag = true;
            if (fees != null)
            {
                MDM_FEE.DeleteAll();
                foreach (MDM_FEE fee in fees)
                {
                    try
                    {
                        fee.CREATETIME = DateTime.Now;
                        fee.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }
        [WebMethod]
        public bool MDMSRVFEEREF(MDM_SRV_FEE_REF[] sfrs)
        {
            bool flag = true;
            if (sfrs != null)
            {
                MDM_SRV_FEE_REF.DeleteAll();
                foreach (MDM_SRV_FEE_REF sfr in sfrs)
                {
                    try
                    {
                        sfr.CREATETIME = DateTime.Now;
                        sfr.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }
        [WebMethod]
        public bool MDMCALCBASE(MDM_CALC_BASE[] cbs)
        {
            bool flag = true;
            if (cbs != null)
            {
                MDM_CALC_BASE.DeleteAll();
                foreach (MDM_CALC_BASE cb in cbs)
                {
                    try
                    {
                        cb.CREATETIME = DateTime.Now;
                        cb.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }
        [WebMethod]
        public bool MDMCOND(MDM_COND[] cs)
        {
            bool flag = true;
            if (cs != null)
            {
                MDM_COND.DeleteAll();
                foreach (MDM_COND c in cs)
                {
                    try
                    {
                        c.CREATETIME = DateTime.Now;
                        c.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }
        [WebMethod]
        public bool MDMSTAGECAT(MDM_STAGECAT[] ss)
        {
            bool flag = true;
            if (ss != null)
            {
                MDM_STAGECAT.DeleteAll();
                foreach (MDM_STAGECAT s in ss)
                {
                    try
                    {
                        s.CREATETIME = DateTime.Now;
                        s.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }
        [WebMethod]
        public bool MDMPERATOR(MDM_PERATOR[] ps)
        {
            bool flag = true;
            if (ps != null)
            {
                MDM_PERATOR.DeleteAll();
                foreach (MDM_PERATOR p in ps)
                {
                    try
                    {
                        p.CREATETIME = DateTime.Now;
                        p.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }
        [WebMethod]
        public bool MDMDIMWP(MDM_DIMWP[] ds)
        {
            bool flag = true;
            if (ds != null)
            {
                MDM_DIMWP.DeleteAll();
                foreach (MDM_DIMWP d in ds)
                {
                    try
                    {
                        d.CREATETIME = DateTime.Now;
                        d.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }
        [WebMethod]
        public bool MDMRNDPR(MDM_RNDPR[] rs)
        {
            bool flag = true;
            if (rs != null)
            {
                MDM_RNDPR.DeleteAll();
                foreach (MDM_RNDPR r in rs)
                {
                    try
                    {
                        r.CREATETIME = DateTime.Now;
                        r.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }
        [WebMethod]
        public bool MDMMSRUNIT(MDM_MSR_UNIT[] mus)
        {
            bool flag = true;
            if (mus != null)
            {
                MDM_MSR_UNIT.DeleteAll();
                foreach (MDM_MSR_UNIT mu in mus)
                {
                    try
                    {
                        mu.CREATETIME = DateTime.Now;
                        mu.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }
        [WebMethod]
        public bool MDMTSR(MDM_TSR[] ts)
        {
            Log log = new Log(AppDomain.CurrentDomain.BaseDirectory + @"/servicelog/" + DateTime.Now.ToString("yyyyMMdd") + "mdmtsr.txt");

            bool flag = true;
            if (ts != null)
            {
                MDM_TSR.DeleteAll();
                foreach (MDM_TSR t in ts)
                {
                    try
                    {
                        t.CREATETIME = DateTime.Now;
                        t.DoCreate();
                    }
                    catch (Exception ex)
                    {
                        log.log(ex.Message);
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }
        [WebMethod]
        public bool MDMINSSET(MDM_INSSET[] mis)
        {
            bool flag = true;
            if (mis != null)
            {
                MDM_INSSET.DeleteAll();
                foreach (MDM_INSSET mi in mis)
                {
                    try
                    {
                        mi.CREATETIME = DateTime.Now;
                        mi.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }
        [WebMethod]
        public bool MDMINS(MDM_INS[] ins)
        {
            bool flag = true;
            if (ins != null)
            {
                MDM_INS.DeleteAll();
                foreach (MDM_INS mi in ins)
                {
                    try
                    {
                        mi.CREATETIME = DateTime.Now;
                        mi.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }

        [WebMethod]
        public bool MDMSERVICE(MDM_SERVICE[] svcs)
        {
            bool flag = true;
            if (svcs != null)
            {
                MDM_SERVICE.DeleteAll();
                foreach (MDM_SERVICE svc in svcs)
                {
                    try
                    {
                        svc.CREATETIME = DateTime.Now;
                        svc.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }

        [WebMethod]
        public bool MDMPRDSRVREF(MDM_PRD_SRV_REF[] refs)
        {
            bool flag = true;
            if (refs != null)
            {
                MDM_PRD_SRV_REF.DeleteAll();
                foreach (MDM_PRD_SRV_REF psref in refs)
                {
                    try
                    {
                        psref.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }

        [WebMethod]
        public bool MDMBP(MDM_BP[] bps)
        {
            bool flag = true;
            if (bps != null)
            {
                MDM_BP.DeleteAll();
                foreach (MDM_BP bp in bps)
                {
                    try
                    {
                        bp.RID = DBKeyHelper.ToHex(Convert.FromBase64String(bp.RID));
                        bp.CREATETIME = DateTime.Now;
                        bp.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }

        [WebMethod]
        public bool MDMORG(MDM_ORG[] orgs)
        {
            bool flag = true;
            if (orgs != null)
            {
                MDM_ORG.DeleteAll();
                foreach (MDM_ORG org in orgs)
                {
                    try
                    {
                        org.RID = org.ORGKEY.TrimStart('0');
                        org.ORGKEY = org.ORGKEY.PadLeft(10, '0');
                        org.CREATETIME = DateTime.Now;
                        org.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }

        [WebMethod]
        public bool MDMLOC(MDM_LOC[] locs)
        {
            bool flag = true;
            if (locs != null)
            {
                MDM_LOC.DeleteAll();
                foreach (MDM_LOC loc in locs)
                {
                    try
                    {
                        loc.CREATETIME = DateTime.Now;
                        loc.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }

        [WebMethod]
        public bool MDMLOCK(QO_ET_SDPZ_S[] sdpzsstrc, QO_ET_SDPZ[] sdpzdata)
        {
            bool flag = true;
            if (sdpzsstrc != null)
            {
                QO_ET_SDPZ_S.DeleteAll();

                foreach (QO_ET_SDPZ_S s in sdpzsstrc)
                {
                    try
                    {
                        s.CREATETIME = DateTime.Now;
                        s.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            if (sdpzdata != null)
            {
                QO_ET_SDPZ.DeleteAll();
                foreach (QO_ET_SDPZ d in sdpzdata)
                {
                    try
                    {
                        d.CREATETIME = DateTime.Now;
                        d.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }

        [WebMethod]
        public bool MDMSERVICEPRODUCTID(MDM_SERVICE_PRODUCT_ID[] serviceproductids)
        {
            bool flag = true;
            if (serviceproductids != null)
            {
                MDM_SERVICE_PRODUCT_ID.DeleteAll();
                foreach (MDM_SERVICE_PRODUCT_ID serviceproductid in serviceproductids)
                {
                    try
                    {
                        serviceproductid.CREATETIME = DateTime.Now;
                        serviceproductid.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }

        [WebMethod]
        public bool MDMWAERS(MDM_WAERS[] wares)
        {
            bool flag = true;
            if (wares != null)
            {
                MDM_WAERS.DeleteAll();
                foreach (MDM_WAERS ware in wares)
                {
                    try
                    {
                        ware.CREATETIME = DateTime.Now;
                        ware.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }

        [WebMethod]
        public bool MDMPTRO(MDM_PTRO[] ptros)
        {
            bool flag = true;
            if (ptros != null)
            {
                MDM_PTRO.DeleteAll();
                foreach (MDM_PTRO ptro in ptros)
                {
                    try
                    {
                        ptro.CREATETIME = DateTime.Now;
                        ptro.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }

        [WebMethod]
        public bool MDMINSASN(MDM_INSASN[] insasns)
        {
            bool flag = true;
            if (insasns != null)
            {
                MDM_INSASN.DeleteAll();
                foreach (MDM_INSASN insasn in insasns)
                {
                    try
                    {
                        insasn.CREATETIME = DateTime.Now;
                        insasn.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }

        [WebMethod]
        public bool MDMFATY(MDM_FATY[] fatys)
        {
            bool flag = true;
            if (fatys != null)
            {
                MDM_FATY.DeleteAll();
                foreach (MDM_FATY faty in fatys)
                {
                    try
                    {
                        faty.CREATETIME = DateTime.Now;
                        faty.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }

        [WebMethod]
        public bool MDMATIASG(MDM_ATIASG[] atiasgs)
        {
            bool flag = true;
            if (atiasgs != null)
            {
                MDM_ATIASG.DeleteAll();
                foreach (MDM_ATIASG atiasg in atiasgs)
                {
                    try
                    {
                        atiasg.CREATETIME = DateTime.Now;
                        atiasg.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }
<<<<<<< .mine

        [WebMethod]
        public bool MDMTCURR(GETMDMTCURR[] tcurrs)
        {
            bool flag = true;
            if (tcurrs != null)
            {
                MDM_TCURR.DeleteAll();
                foreach (GETMDMTCURR tcurr in tcurrs)
                {
                    MDM_TCURR tccr = new MDM_TCURR();
                    try
                    {
                        string dateString = tcurr.GDATE.ToString();
                        DateTime dt = DateTime.ParseExact(dateString, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                        tccr.KURST = tcurr.KURST;
                        tccr.FCURR = tcurr.FCURR;
                        tccr.TCURR = tcurr.TCURR;
                        tccr.GDATE = dt;
                        tccr.UKURS = decimal.Parse(tcurr.UKURS.ToString());
                        tccr.FFACT = decimal.Parse(tcurr.FFACT.ToString());
                        tccr.TFACT = decimal.Parse(tcurr.TFACT.ToString());
                        tccr.CREATETIME = DateTime.Now;
                        tccr.DoCreate();
                    }
                    catch
                    {
                        flag = true;//不影响操作结果
                        continue;
                    }
                }
            }

            return flag;
        }
||||||| .r6204
=======

        [WebMethod]
        public bool MDMSRVRQCD(MDM_SRVRQCD[] bps)
        {
            bool flag = true;
            if (bps != null)
            {
                MDM_SRVRQCD.DeleteAll();
                foreach (MDM_SRVRQCD bp in bps)
                {
                    try
                    {
                        //bp.RID = DBKeyHelper.ToHex(Convert.FromBase64String(bp.RID));
                        bp.CREATETIME = DateTime.Now;
                        bp.DoCreate();
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }

            return flag;
        }
>>>>>>> .r6206
    }

    public class GETMDMTCURR
    {
        public string KURST { get; set; }
        public string FCURR { get; set; }
        public string TCURR { get; set; }
        public string GDATE { get; set; }
        public string UKURS { get; set; }
        public string FFACT { get; set; }
        public string TFACT { get; set; }
    }
}
