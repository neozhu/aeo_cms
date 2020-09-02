using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Aim.Data;
using Aim.Common;
using Aim.Portal;
using Aim.Portal.Data;
using Aim.Portal.Model;

namespace Plat.Model
{
    [Serializable]
    public abstract class ModelBase<T> : EntityBase<T> where T : ModelBase<T>
    {
    }
}
