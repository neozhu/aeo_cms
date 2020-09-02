//=====================================================================================
// All Rights Reserved MR.BiG
//=====================================================================================
using System;
using System.Collections;
using System.Reflection;
using System.Data;
using System.Text.RegularExpressions;

namespace Oncontrol3.Web.Helpers
{
    /// <summary>
    /// Hashtable帮助类
    /// </summary>
    public class HashtableHelper
    {
        /// <summary>
        /// 实体类Model转Hashtable(反射)
        /// </summary>
        public static Hashtable GetModelToHashtable<T>(T model)
        {
            Hashtable ht = new Hashtable();
            PropertyInfo[] properties = model.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo item in properties)
            {
                string key = item.Name;
                ht[key] = item.GetValue(model, null);
            }
            return ht;
        }
        /// <summary>
        /// Hashtable转换实体类
        /// </summary>
        public static T HashtableToModel<T>(Hashtable ht)
        {
            T model = Activator.CreateInstance<T>();
            Type type = model.GetType();
            //遍历每一个属性
            foreach (PropertyInfo prop in type.GetProperties())
            {
                object value = ht[prop.Name];
                if (value != null)
                {
                    if (prop.PropertyType.ToString() == "System.Nullable`1[System.DateTime]")
                    {
                        value = ToDateTime(value);
                    }
                    prop.SetValue(model, HackType(value, prop.PropertyType), null);
                }
            }
            return model;
        }
        //这个类对可空类型进行判断转换，要不然会报错
        public static object HackType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return null;
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }
        public static DateTime? ToDateTime(object obj)
        {
            if (obj != null && obj.ToString() != "")
                return DateTime.Parse(obj.ToString());
            else
                return null;
        }
    }
}