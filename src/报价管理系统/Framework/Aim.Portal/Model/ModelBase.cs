using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aim.Data;
using Aim.Common;
using Aim.Portal;
using Aim.Portal.Data;
using System.Data.OracleClient;
using NHibernate;
using NHibernate.SqlTypes;
using System.Data;
using NHibernate.UserTypes;

namespace Aim.Portal.Model
{
    [Serializable]
    public class ModelBase<T> : EntityBase<T> where T : ModelBase<T>
    {
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserInfo UserInfo
        {
            get
            {
                return PortalService.CurrentUserInfo;
            }
        }
    }

    //Oraclenclob字段扩展
    public class OracleClobField : PatchForOracleLobField
    {
        public override void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            if (cmd is OracleCommand)
            {
                //CLob、NClob类型的字段，存入中文时参数的OracleDbType必须设置为OracleDbType.Clob
                //否则会变成乱码（Oracle 10g client环境）
                OracleParameter param = cmd.Parameters[index] as OracleParameter;
                if (param != null)
                {
                    param.OracleType = OracleType.Clob;// 关键就这里啦
                    param.IsNullable = true;
                }
            }
            NHibernate.NHibernateUtil.StringClob.NullSafeSet(cmd, value, index);
        }
    }
    public abstract class PatchForOracleLobField : IUserType
    {
        public PatchForOracleLobField()
        {
        }
        public bool IsMutable
        {
            get { return true; }
        }
        public System.Type ReturnedType
        {
            get { return typeof(String); }
        }
        public SqlType[] SqlTypes
        {
            get
            {
                return new SqlType[] { NHibernateUtil.String.SqlType };
            }
        }
        public object DeepCopy(object value)
        {
            return value;
        }
        public new bool Equals(object x, object y)
        {
            return x == y;
        }
        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }
        public object Assemble(object cached, object owner)
        {
            return DeepCopy(cached);
        }
        public object Disassemble(object value)
        {
            return DeepCopy(value);
        }
        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            return NHibernate.NHibernateUtil.StringClob.NullSafeGet(rs, names[0]);
        }
        public abstract void NullSafeSet(IDbCommand cmd, object value, int index);
        public object Replace(object original, object target, object owner)
        {
            return original;
        }
    }
}
