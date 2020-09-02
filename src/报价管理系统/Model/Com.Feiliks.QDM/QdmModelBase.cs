using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Aim.Portal.Model;
using NHibernate;
using NHibernate.SqlTypes;
using Aim;

namespace Com.Feiliks.QDM
{
    [Serializable]
    public abstract class QdmModelBase<T> : ModelBase<T> where T : QdmModelBase<T>, new()
    {
        public IList<T> GetOtherMap(string tableName, string withwhereString)
        {

            string query = string.Format("select * from {0} {1}", tableName, withwhereString);

            return (IList<T>)ActiveRecordMediator<T>.Execute(
                delegate(ISession session, object instance)
                {
                    //return session.CreateSQLQuery(query, "synonym", typeof(SmartDeal)).List<SmartDeal>();   
                    return session.CreateSQLQuery(query).AddEntity("synonym", typeof(T)).List<T>();
                }, new T());

        }
        public static IList<T> GetFromView(string tableName, string withwhereString)
        {

            string query = string.Format("select * from {0} {1}", tableName, withwhereString);

            return (IList<T>)ActiveRecordMediator<T>.Execute(
                delegate(ISession session, object instance)
                {
                    //return session.CreateSQLQuery(query, "synonym", typeof(SmartDeal)).List<SmartDeal>();   
                    return session.CreateSQLQuery(query).AddEntity("synonym", typeof(T)).List<T>();
                }, new T());

        }
        protected override void OnPropertyChanged(object sender, AimPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);
            if (e.OldValue != null && !e.OldValue.Equals(e.NewValue))
            {
                string userId = "";
                string usrName = "";
                if (Aim.Portal.PortalService.CurrentUserInfo != null && Aim.Portal.PortalService.CurrentUserInfo.UserID != null)
                {
                    userId = Aim.Portal.PortalService.CurrentUserInfo.UserID;
                    usrName = Aim.Portal.PortalService.CurrentUserInfo.Name;
                }
                Aim.Portal.ServicesProvider.LogServiceSingleton.Instance.BeginLogProperty(this.GetType().ToString(), e.PropertyName, e.OldValue.ToString(), e.NewValue == null ? "" : e.NewValue.ToString(), userId, usrName, DateTime.Now, null, null);
            }
        }
    }


}
