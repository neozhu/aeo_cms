using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using NHibernate;
using NHibernate.SqlTypes;
using Aim;

namespace Aim.WorkFlow
{
    [Serializable]
    public abstract class WFBase<T> : ModelBase<T> where T : WFBase<T>, new()
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
        }
    }


}
