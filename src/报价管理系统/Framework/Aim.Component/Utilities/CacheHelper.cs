using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using System.Collections;
using System.Web;

namespace Aim.Component
{
    public class CacheHelper
    {
        private static  CacheHelper instance;
        protected static volatile System.Web.Caching.Cache webCache;

        /// <summary>
        /// 默认缓存存活期为3600秒(1小时)
        /// </summary>
        private  int _timeOut = 3600;

        /// <summary>
        /// 同步双检索锁
        /// </summary>
        private static object syncObj = new object();

        static  CacheHelper()
        {
            lock (syncObj)
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;
                if (context != null)
                {
                    webCache = context.Cache;
                }
                else
                {
                    webCache = System.Web.HttpRuntime.Cache;
                }
            }  
        }

        /// <summary>
        /// 设置到期相对时间[单位: 秒] 
        /// </summary>
        public  int TimeOut
        {
            set {_timeOut=value>0?value:3600;}
            get{return _timeOut>0?_timeOut:3600;}
        }

        public static  System.Web.Caching.Cache  GetWebLanguageCacheObj
        {
            get { return webCache; }
        }

        public static CacheHelper getInstance()
        {
            if (instance == null)
            {
                return instance = new CacheHelper();
            }
            return instance;
        }


        public  void AddObject(string objId, object o)
        {
            if (objId == null || objId.Length == 0 || o == null)
            {
                return;
            }
         
            CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);
            if (TimeOut > 7200)
            {
                webCache.Insert(objId, o, null, DateTime.MaxValue, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, callBack);
            }
            else
            {
                webCache.Insert(objId, o, null, DateTime.Now.AddSeconds(TimeOut), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, callBack);
            }
        }

        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        ///  <param name="timeOut">单位秒</param>
        public  void AddObjectWith(string objId, object o)
        {
            if (objId == null || objId.Length == 0 || o == null)
            {
                return;
            }
            CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);
            webCache.Insert(objId, o, null, System.DateTime.Now.AddSeconds(TimeOut), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, callBack);
        }

        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="objId">对象的键值</param>
        ///  <param name="callback">缓存机制通知应用程序</param>
        /// <param name="o">缓存的对象</param>
        public void AddObjectWith(string objId,CacheItemRemovedCallback callback, object o)
        {
            if (objId == null || objId.Length == 0 || o == null)
            {
                return;
            }
            webCache.Insert(objId, o, null, System.DateTime.Now.AddSeconds(TimeOut), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, callback);
        }

        /// <summary>
        /// 加入当前对象到缓存中,并对相关文件建立依赖
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="files">监视的路径文件</param>
        public  void AddObjectWithFileChange(string objId, object o, params string[] files)
        {
            if (objId == null || objId.Length == 0 || o == null)
            {
                return;
            }
            CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);
            CacheDependency dep = new CacheDependency(files, DateTime.Now);
            webCache.Insert(objId, o, dep, System.DateTime.Now.AddSeconds(TimeOut), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, callBack);
        }

        /// <summary>
        /// 加入当前对象到缓存中,并对相关文件建立依赖
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        ///  <param name="callback">缓存机制通知应用程序</param>
        /// <param name="files">监视的路径文件</param>
        public  void AddObjectWithFileChange(string objId, object o,  CacheItemRemovedCallback callback, params string[] files)
        {
            if (objId == null || objId.Length == 0 || o == null)
            {
                return;
            }
            CacheDependency dep = new CacheDependency(files, DateTime.Now);

            webCache.Insert(objId, o,dep, System.DateTime.Now.AddSeconds(TimeOut), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, callback);
        }

        //文件依賴緩存
        //List<Student> list = DefaultCacheManager.getInstance().RetrieveObject("Items1") as List<Student>;
        //if (list != null && list.Count > 0)
        //{
        //    list.ForEach(item => { Response.Write(item.Name + "  " + item.Age + "  " + item.Sex + "<br/>"); });
        //}
        //else
        //{
        //    string[] parm = new string[1];
        //    string fielPath = Server.MapPath("XMLFile1.xml");
        //    parm[0]=fielPath;
        //    list = this.GetStudentList(fielPath);
        //    DefaultCacheManager.getInstance().AddObjectWithFileChange("Items1", list,parm);
        //} 

        /// <summary>
        /// 加入当前对象到缓存中,并使用依赖键
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="dependKey">依赖关联的键值</param>
        public  void AddObjectWithDepend(string objId, object o, params string[] dependKey)
        {
            if (objId == null || objId.Length == 0 || o == null)
            {
                return;
            }

            CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);

            CacheDependency dep = new CacheDependency(null, dependKey, DateTime.Now);

            webCache.Insert(objId, o, dep, System.DateTime.Now.AddSeconds(TimeOut), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, callBack);
        }

        /// <summary>
        /// 加入当前对象到缓存中,并使用依赖键
        /// </summary>
        /// <param name="objId">对象的键值</param>
        ///  <param name="callback">缓存机制通知应用程序</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="dependKey">依赖关联的键值</param>
        public void AddObjectWithDepend(string objId, object o,CacheItemRemovedCallback callback, params string[] dependKey)
        {
            if (objId == null || objId.Length == 0 || o == null)
            {
                return;
            }
            CacheDependency dep = new CacheDependency(null, dependKey, DateTime.Now);

            webCache.Insert(objId, o, dep, System.DateTime.Now.AddSeconds(TimeOut), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, callback);
        }


        public void onRemove(string key, object value, CacheItemRemovedReason reason)
        {
            switch (reason)
            {
                case CacheItemRemovedReason.DependencyChanged:
                     
                    break;
                case CacheItemRemovedReason.Expired:
                    {
                       CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(this.onRemove);
                        webCache.Insert(key,value, null, System.DateTime.Now.AddMinutes(TimeOut),
                          System.Web.Caching.Cache.NoSlidingExpiration,
                           System.Web.Caching.CacheItemPriority.High,
                            callBack);
                        break;
                    }
                case CacheItemRemovedReason.Removed:
                    {
                        break;
                    }
                case CacheItemRemovedReason.Underused:
                    {
                        break;
                    }
                default: break;
            }
        }
        /// <summary>
        /// 删除缓存对象
        /// </summary>
        /// <param name="objId">对象的关键字</param>
        public  void RemoveObject(string objId)
        {
            if (objId == null || objId.Length == 0)
            {
                return;
            }
            webCache.Remove(objId);
        }

        /// <summary>
        /// 返回一个指定的对象
        /// </summary>
        /// <param name="objId">对象的关键字</param>
        /// <returns>对象</returns>
        public  object GetObject(string objId)
        {
            if (objId == null || objId.Length == 0)
            {
                return null;
            }
            return webCache.Get(objId);
        }
        /// <summary>
        /// 返回指定ID的对象
        /// </summary>
        /// <param name="objId"></param>
        /// <typeparam name="T">返回数据的类型</typeparam>
        /// <returns></returns>
        public T GetObject<T>(string objId)
        {
            object o = GetObject(objId);
            return o != null ? (T)o : default(T);
        }


        public void AddObject(string objId, object o, int timeOut)
        {
            if (String.IsNullOrEmpty(objId) || String.IsNullOrEmpty(objId.Trim()))
                return;

            CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);
            if (timeOut > 0)
            {
                webCache.Insert(objId, o, null, DateTime.Now.AddMilliseconds(timeOut), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, callBack);
            }
            else
            {
                webCache.Insert(objId, o, null, DateTime.MaxValue, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, callBack);
            }
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="o"></param>
        /// <param name="callBack"></param>
        /// <param name="timeOut"></param>
        public   void AddObject(string objId, object o,CacheItemRemovedCallback callBack, int timeOut)
        {
            if (String.IsNullOrEmpty(objId) || String.IsNullOrEmpty(objId.Trim()))
                return;

            if (timeOut > 0)
            {
                webCache.Insert(objId, o, null, DateTime.Now.AddMilliseconds(timeOut), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, callBack);
            }
            else
            {
                webCache.Insert(objId, o, null, DateTime.MaxValue, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, callBack);
            }
        }

        /// <summary>
        /// 清空的有缓存数据
        /// </summary>
        public   void ClearAll()
        {
            IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                webCache.Remove(CacheEnum.Key.ToString());
            }
        }

    }
}
