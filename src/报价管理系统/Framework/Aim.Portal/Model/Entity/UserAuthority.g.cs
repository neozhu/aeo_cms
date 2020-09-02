// Business class UserAuthority generated from UserAuthority
// Creator: Rongwei
// Created Date: [2012-07-19]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Portal.Model
{
	[ActiveRecord("UserAuthority")]
    public partial class UserAuthority : EntityBase<UserAuthority>
	{
		#region Property_Names

		public static string Prop_Id = "Id";
		public static string Prop_AuthorityUserId = "AuthorityUserId";
		public static string Prop_AuthorityUserName = "AuthorityUserName";
		public static string Prop_ModuleId = "ModuleId";
		public static string Prop_ModuleName = "ModuleName";
		public static string Prop_ModuleAlias = "ModuleAlias";
		public static string Prop_ModuleUrl = "ModuleUrl";
		public static string Prop_CreateId = "CreateId";
		public static string Prop_CreateName = "CreateName";
		public static string Prop_CreateTime = "CreateTime";
		public static string Prop_Remark = "Remark";

		#endregion

		#region Private_Variables

		private string _id;
		private string _authorityUserId;
		private string _authorityUserName;
		private string _moduleId;
		private string _moduleName;
		private string _moduleAlias;
		private string _moduleUrl;
		private string _createId;
		private string _createName;
		private DateTime? _createTime;
		private string _remark;


		#endregion

		#region Constructors

		public UserAuthority()
		{
		}

		public UserAuthority(
			string p_id,
			string p_authorityUserId,
			string p_authorityUserName,
			string p_moduleId,
			string p_moduleName,
			string p_moduleAlias,
			string p_moduleUrl,
			string p_createId,
			string p_createName,
			DateTime? p_createTime,
			string p_remark)
		{
			_id = p_id;
			_authorityUserId = p_authorityUserId;
			_authorityUserName = p_authorityUserName;
			_moduleId = p_moduleId;
			_moduleName = p_moduleName;
			_moduleAlias = p_moduleAlias;
			_moduleUrl = p_moduleUrl;
			_createId = p_createId;
			_createName = p_createName;
			_createTime = p_createTime;
			_remark = p_remark;
		}

		#endregion

		#region Properties

		[PrimaryKey("Id", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string Id
		{
			get { return _id; }
			set { _id = value; } // 处理列表编辑时去掉注释

		}

		[Property("AuthorityUserId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string AuthorityUserId
		{
			get { return _authorityUserId; }
			set
			{
				if ((_authorityUserId == null) || (value == null) || (!value.Equals(_authorityUserId)))
				{
                    object oldValue = _authorityUserId;
					_authorityUserId = value;
					RaisePropertyChanged(UserAuthority.Prop_AuthorityUserId, oldValue, value);
				}
			}

		}

		[Property("AuthorityUserName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string AuthorityUserName
		{
			get { return _authorityUserName; }
			set
			{
				if ((_authorityUserName == null) || (value == null) || (!value.Equals(_authorityUserName)))
				{
                    object oldValue = _authorityUserName;
					_authorityUserName = value;
					RaisePropertyChanged(UserAuthority.Prop_AuthorityUserName, oldValue, value);
				}
			}

		}

		[Property("ModuleId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string ModuleId
		{
			get { return _moduleId; }
			set
			{
				if ((_moduleId == null) || (value == null) || (!value.Equals(_moduleId)))
				{
                    object oldValue = _moduleId;
					_moduleId = value;
					RaisePropertyChanged(UserAuthority.Prop_ModuleId, oldValue, value);
				}
			}

		}

		[Property("ModuleName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string ModuleName
		{
			get { return _moduleName; }
			set
			{
				if ((_moduleName == null) || (value == null) || (!value.Equals(_moduleName)))
				{
                    object oldValue = _moduleName;
					_moduleName = value;
					RaisePropertyChanged(UserAuthority.Prop_ModuleName, oldValue, value);
				}
			}

		}

		[Property("ModuleAlias", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string ModuleAlias
		{
			get { return _moduleAlias; }
			set
			{
				if ((_moduleAlias == null) || (value == null) || (!value.Equals(_moduleAlias)))
				{
                    object oldValue = _moduleAlias;
					_moduleAlias = value;
					RaisePropertyChanged(UserAuthority.Prop_ModuleAlias, oldValue, value);
				}
			}

		}

		[Property("ModuleUrl", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string ModuleUrl
		{
			get { return _moduleUrl; }
			set
			{
				if ((_moduleUrl == null) || (value == null) || (!value.Equals(_moduleUrl)))
				{
                    object oldValue = _moduleUrl;
					_moduleUrl = value;
					RaisePropertyChanged(UserAuthority.Prop_ModuleUrl, oldValue, value);
				}
			}

		}

		[Property("CreateId", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string CreateId
		{
			get { return _createId; }
			set
			{
				if ((_createId == null) || (value == null) || (!value.Equals(_createId)))
				{
                    object oldValue = _createId;
					_createId = value;
					RaisePropertyChanged(UserAuthority.Prop_CreateId, oldValue, value);
				}
			}

		}

		[Property("CreateName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string CreateName
		{
			get { return _createName; }
			set
			{
				if ((_createName == null) || (value == null) || (!value.Equals(_createName)))
				{
                    object oldValue = _createName;
					_createName = value;
					RaisePropertyChanged(UserAuthority.Prop_CreateName, oldValue, value);
				}
			}

		}

		[Property("CreateTime", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? CreateTime
		{
			get { return _createTime; }
			set
			{
				if (value != _createTime)
				{
                    object oldValue = _createTime;
					_createTime = value;
					RaisePropertyChanged(UserAuthority.Prop_CreateTime, oldValue, value);
				}
			}

		}

		[Property("Remark", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string Remark
		{
			get { return _remark; }
			set
			{
				if ((_remark == null) || (value == null) || (!value.Equals(_remark)))
				{
                    object oldValue = _remark;
					_remark = value;
					RaisePropertyChanged(UserAuthority.Prop_Remark, oldValue, value);
				}
			}

		}

		#endregion
	} // UserAuthority
}

