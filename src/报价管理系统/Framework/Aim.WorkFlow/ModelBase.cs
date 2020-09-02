using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Aim.Data;

namespace Aim.WorkFlow
{
    [Serializable]
    public abstract class ModelBase<T> : EntityBase<T> where T : ModelBase<T>
    {
    }
}
