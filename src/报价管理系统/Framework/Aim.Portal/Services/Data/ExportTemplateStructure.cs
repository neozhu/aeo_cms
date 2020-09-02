using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using NHibernate.Id;
using Aim.Data;
using Aim.Portal.Model;

namespace Aim.Portal.Data
{
    /// <summary>
    /// 导出命令码
    /// </summary>
    public enum ExportTemplateCommandCode
    {
        Begin,
        End,
        Other
    }

    /// <summary>
    /// 导入模版结构
    /// </summary>
    public class ExportTemplateStructure : TemplateStructure
    {
        #region 成员属性

        private ExportTemplateGroupList _groupList;

        /// <summary>
        /// 组列表
        /// </summary>
        public ExportTemplateGroupList GroupList
        {
            get
            {
                return _groupList;
            }
        }

        public ExportTemplateGroup DefaultGroup
        {
            get
            {
                return GroupList.DefaultGroup;
            }
        }

        #endregion

        #region 构造函数

        public ExportTemplateStructure()
            : base()
        {
            _groupList = new ExportTemplateGroupList();
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <returns></returns>
        public override string GetConfig()
        {
            string _config = JsonHelper.GetJsonString(this);

            return _config;
        }

        #endregion
    }

    public class ExportTemplateGroupList : List<ExportTemplateGroup>
    {
        #region 成员属性

        /// <summary>
        /// 默认组
        /// </summary>
        [JsonIgnore]
        public ExportTemplateGroup DefaultGroup
        {
            get
            {
                ExportTemplateGroup _defaultGroup = base.Find(tent => tent.Name == ExportTemplateGroup.DEFULAT_GROUP_NAME);

                if (_defaultGroup == null)
                {
                    _defaultGroup = new DefaultExportTemplateGroup();

                    base.Add(_defaultGroup);
                }

                return _defaultGroup;
            }
        }

        #endregion

        #region 构造函数

        public ExportTemplateGroupList()
        {
            Init();
        }

        private void Init()
        {
            base.Clear();
        }

        #endregion

        #region List成员

        /// <summary>
        /// 添加节点
        /// </summary>
        new public void Remove(ExportTemplateGroup group)
        {
            if (group.IsDefault)
            {
                throw new Exception("不可移除默认组");
            }
            else
            {
                base.Remove(group);
            }
        }

        /// <summary>
        /// 移除指定index
        /// </summary>
        /// <param name="index"></param>
        new public void RemoveAt(int index)
        {
            ExportTemplateGroup group = this[index];

            this.Remove(group);
        }

        /// <summary>
        /// 移除所有，并且重新初始化
        /// </summary>
        new public void RemoveAll(Predicate<ExportTemplateGroup> match)
        {
            List<ExportTemplateGroup> groups = base.FindAll(match);

            for (int i = 0; i < groups.Count; i++)
            {
                this.Remove(groups[i]);
            }
        }

        /// <summary>
        /// 移除指定范围内对象
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        new public void RemoveRange(int index, int count)
        {
            List<ExportTemplateGroup> groups = base.GetRange(index, count);

            for (int i = 0; i < groups.Count; i++)
            {
                this.Remove(groups[i]);
            }
        }

        /// <summary>
        /// 清空操作
        /// </summary>
        new public void Clear()
        {
            this.Init();
        }

        /// <summary>
        /// 添加组
        /// </summary>
        /// <param name="group"></param>
        new public void Add(ExportTemplateGroup group)
        {
            if (group.IsDefault)
            {
                throw new Exception("不可添加默认组");
            }
        }

        /// <summary>
        /// 添加组
        /// </summary>
        /// <param name="groups"></param>
        new public void AddRange(IEnumerable<ExportTemplateGroup> groups)
        {
            foreach (ExportTemplateGroup tgroup in groups)
            {
                this.Add(tgroup);
            }
        }

        #endregion
    }

    /// <summary>
    /// 模版组
    /// </summary>
    public class ExportTemplateGroup
    {
        public const string DEFULAT_GROUP_NAME = "Default";

        #region 成员属性

        /// <summary>
        /// 是否默认组
        /// </summary>
        [JsonIgnore]
        public bool IsDefault
        {
            get
            {
                return this._name == DEFULAT_GROUP_NAME;
            }
        }

        protected string _name = String.Empty;

        /// <summary>
        /// 组名
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// 默认属性节点
        /// </summary>
        [JsonIgnore]
        public ExportTemplatePropertyNode PropertyNode
        {
            get
            {
                if (PropertyNodeList.Count > 0)
                {
                    return PropertyNodeList[0];
                }

                return null;
            }
        }

        private List<ExportTemplatePropertyNode> _propertyNodeList;

        /// <summary>
        /// 属性节点列表
        /// </summary>
        public IList<ExportTemplatePropertyNode> PropertyNodeList
        {
            get
            {
                if (_propertyNodeList == null)
                {
                    _propertyNodeList = new List<ExportTemplatePropertyNode>();
                }

                return _propertyNodeList;
            }
        }

        private List<ExportTemplateColumnNode> _columnNodeList;

        /// <summary>
        /// 列模版节点列表
        /// </summary>
        public IList<ExportTemplateColumnNode> ColumnNodeList
        {
            get
            {
                if (_columnNodeList == null)
                {
                    _columnNodeList = new List<ExportTemplateColumnNode>();
                }

                return _columnNodeList;
            }
        }

        private List<ExportTemplateCommandNode> _commandNodeList;

        /// <summary>
        /// 命令节点列表
        /// </summary>
        public IList<ExportTemplateCommandNode> CommandNodeList
        {
            get
            {
                if (_commandNodeList == null)
                {
                    _commandNodeList = new List<ExportTemplateCommandNode>();
                }

                return _commandNodeList;
            }
        }

        #endregion

        #region 构造函数

        public ExportTemplateGroup(string name)
        {
            _name = name;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 列节点字典
        /// </summary>
        public Dictionary<string, ExportTemplateColumnNode> GetColumnNodeDict()
        {
            Dictionary<string, ExportTemplateColumnNode> dict = new Dictionary<string, ExportTemplateColumnNode>();

            foreach (ExportTemplateColumnNode tnode in ColumnNodeList)
            {
                dict.Add(tnode.ColumnName, tnode);
            }

            return dict;
        }

        /// <summary>
        /// 获取DataTable架构
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTableSchema()
        {
            DataTable tdt = new DataTable();

            foreach (ExportTemplateColumnNode tcnode in ColumnNodeList)
            {
                tdt.Columns.Add(tcnode.ColumnName);
            }

            return tdt;
        }

        #endregion
    }

    /// <summary>
    /// 默认模版组
    /// </summary>
    public class DefaultExportTemplateGroup : ExportTemplateGroup
    {
        #region 构造函数

        public DefaultExportTemplateGroup()
            : base(ExportTemplateGroup.DEFULAT_GROUP_NAME)
        {
        }

        #endregion
    }

    /// <summary>
    /// 导出模版节点
    /// </summary>
    public class ExportTemplateNode : TemplateNode
    {
        #region 构造函数

        public ExportTemplateNode()
        {
        }

        #endregion
    }

    /// <summary>
    /// 导出模版数据节点
    /// </summary>
    public class ExportTemplateColumnNode : ExportTemplateNode
    {
        #region 成员属性

        /// <summary>
        /// 数据库列名
        /// </summary>
        public string ColumnName
        {
            get;
            set;
        }

        /// <summary>
        /// 是否单值（不是列表型）
        /// </summary>
        public bool IsSingle
        {
            get;
            set;
        }

        /// <summary>
        /// 行值位置
        /// </summary>
        public int? ValueRowIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 列值位置
        /// </summary>
        public int? ValueColumnIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 单元格默认值
        /// </summary>
        public object DefaultValue
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public ExportTemplateColumnNode()
        {
            IsSingle = false;
        }

        #endregion
    }

    /// <summary>
    /// 导出模版命令节点
    /// </summary>
    public class ExportTemplateCommandNode : ExportTemplateNode
    {
        #region 成员属性

        /// <summary>
        /// 命令编码
        /// </summary>
        public ExportTemplateCommandCode CommandCode
        {
            get;
            set;
        }

        /// <summary>
        /// 命令行位置
        /// </summary>
        public int RowIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 命令列位置
        /// </summary>
        public int ColumnIndex
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public ExportTemplateCommandNode()
        {
            Name = String.Empty;
        }

        #endregion
    }

    /// <summary>
    /// 导入模版属性节点
    /// </summary>
    public class ExportTemplatePropertyNode : ExportTemplateNode
    {
        #region 成员属性

        /// <summary>
        /// 是否作事务处理
        /// </summary>
        public bool IsTransaction
        {
            get;
            set;
        }

        /// <summary>
        /// 一次导入操作大小(默认100)
        /// </summary>
        public int? BlockSize
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public ExportTemplatePropertyNode()
        {
            Name = String.Empty;
            IsTransaction = true;
            BlockSize = 100;
        }

        #endregion
    }
}

