using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Aim.Portal.Model;
using Aim.CacheManager;
using System.Web.Caching;

namespace Aim.Portal.Services
{
    public class CacheHelpTooler
    {
        private static CacheHelper Chache = CacheHelper.getInstance();
        public static bool MonitorCacheChange = false;
        public static bool GroupCacheChange = false;

        /// <summary>
        /// 加载监视缓存配置
        /// </summary>
        public static void LoadMonitorCache()
        {
            LoadSysGroupSet();
            LoadMonitorSet();
        }
        private static void LoadSysGroupSet()
        {
            var GroupEnt = SysGroup.FindAll();
            Chache.AddObject(CacheTypeEnum.SysGroup.ToString(), GroupEnt,
            (String key, object value, CacheItemRemovedReason removedReason) =>
            {
                MonitorCacheChange = true;
                Chache.RemoveObject(key);
                LoadSysGroupSet();
                MonitorCacheChange = false;
            }, 600);//10 min
        }

        private static void LoadMonitorSet()
        {
            var Ent = SYSMONTORSET.FindAll();
            Chache.AddObject(CacheTypeEnum.Moinitor.ToString(), Ent,
            (String key, object value, CacheItemRemovedReason removedReason) =>
            {
                GroupCacheChange = true;
                Chache.RemoveObject(key);
                LoadMonitorSet();
                GroupCacheChange = false;
            }, 300);//5
        }

        public static SYSMONTORSET[] MonitorCacheSet
        {
            get
            {
                if (Chache.GetObject(CacheTypeEnum.Moinitor.ToString()) == null)
                {
                    return null;
                }
                else
                {
                    return Chache.GetObject<SYSMONTORSET[]>(CacheTypeEnum.Moinitor.ToString());
                }

            }
        }
        public static bool HasMonitorVal()
        {
            return Chache.GetObject(CacheTypeEnum.Moinitor.ToString()) == null ? false : true;
        }

        public static bool HasGrourpVal()
        {
            return Chache.GetObject(CacheTypeEnum.SysGroup.ToString()) == null ? false : true;
        }

        public static SysGroup[] GrourpCacheSet
        {
            get
            {
                if (Chache.GetObject(CacheTypeEnum.SysGroup.ToString()) == null)
                {
                    return null;
                }
                else
                {
                    return Chache.GetObject<SysGroup[]>(CacheTypeEnum.SysGroup.ToString());
                }
            }
        }
    }

    /// <summary>
    /// 缓存类型枚举
    /// </summary>
    public enum CacheTypeEnum
    {
        Moinitor, SysGroup
    }
}