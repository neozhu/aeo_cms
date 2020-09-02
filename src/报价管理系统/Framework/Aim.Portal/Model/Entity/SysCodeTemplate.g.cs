// Business class SysCodeTemplate generated from SysCodeTemplate
// Creator: Ray
// Created Date: [2011-03-17]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Aim.Portal.Model
{
	[ActiveRecord("SysCodeTemplate")]
    public partial class SysCodeTemplate : EntityBase<SysCodeTemplate>
	{
		#region Property_Names

		public static string Prop_CodeTemplateID = "CodeTemplateID";
		public static string Prop_Code = "Code";
		public static string Prop_Name = "Name";
		public static string Prop_Type = "Type";
		public static string Prop_Tag = "Tag";
		public static string Prop_Description = "Description";
		public static string Prop_TemplateString = "TemplateString";
		public static string Prop_CreaterID = "CreaterID";
		public static string Prop_CreaterName = "CreaterName";
		public static string Prop_LastModifiedDate = "LastModifiedDate";
		public static string Prop_CreatedDate = "CreatedDate";

		#endregion

		#region Private_Variables

		private string _codetemplateid;
		private string _code;
		private string _name;
		private string _type;
		private string _tag;
		private string _description;
		private string _templateString;
		private string _createrID;
		private string _createrName;
		private DateTime? _lastModifiedDate;
		private DateTime? _createdDate;


		#endregion

		#region Constructors

		public SysCodeTemplate()
		{
		}

		public SysCodeTemplate(
			string p_codetemplateid,
			string p_code,
			string p_name,
			string p_type,
			string p_tag,
			string p_description,
			string p_templateString,
			string p_createrID,
			string p_createrName,
			DateTime? p_lastModifiedDate,
			DateTime? p_createdDate)
		{
			_codetemplateid = p_codetemplateid;
			_code = p_code;
			_name = p_name;
			_type = p_type;
			_tag = p_tag;
			_description = p_description;
			_templateString = p_templateString;
			_createrID = p_createrID;
			_createrName = p_createrName;
			_lastModifiedDate = p_lastModifiedDate;
			_createdDate = p_createdDate;
		}

		#endregion

		#region Properties

		[PrimaryKey("CodeTemplateID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string CodeTemplateID
		{
			get { return _codetemplateid; }
			// set { _codetemplateid = value; } // 处理列表编辑时去掉注释

		}

		[Property("Code", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string Code
		{
			get { return _code; }
			set
			{
				if ((_code == null) || (value == null) || (!value.Equals(_code)))
				{
                    object oldValue = _code;
					_code = value;
					RaisePropertyChanged(SysCodeTemplate.Prop_Code, oldValue, value);
				}
			}

		}

		[Property("Name", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string Name
		{
			get { return _name; }
			set
			{
				if ((_name == null) || (value == null) || (!value.Equals(_name)))
				{
                    object oldValue = _name;
					_name = value;
					RaisePropertyChanged(SysCodeTemplate.Prop_Name, oldValue, value);
				}
			}

		}

		[Property("Type", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string Type
		{
			get { return _type; }
			set
			{
				if ((_type == null) || (value == null) || (!value.Equals(_type)))
				{
                    object oldValue = _type;
					_type = value;
					RaisePropertyChanged(SysCodeTemplate.Prop_Type, oldValue, value);
				}
			}

		}

		[Property("Tag", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string Tag
		{
			get { return _tag; }
			set
			{
				if ((_tag == null) || (value == null) || (!value.Equals(_tag)))
				{
                    object oldValue = _tag;
					_tag = value;
					RaisePropertyChanged(SysCodeTemplate.Prop_Tag, oldValue, value);
				}
			}

		}

		[Property("Description", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
		public string Description
		{
			get { return _description; }
			set
			{
				if ((_description == null) || (value == null) || (!value.Equals(_description)))
				{
                    object oldValue = _description;
					_description = value;
					RaisePropertyChanged(SysCodeTemplate.Prop_Description, oldValue, value);
				}
			}

		}

		[Property("TemplateString", Access = PropertyAccess.NosetterCamelcaseUnderscore, ColumnType = "StringClob")]
		public string TemplateString
		{
			get { return _templateString; }
			set
			{
				if ((_templateString == null) || (value == null) || (!value.Equals(_templateString)))
				{
                    object oldValue = _templateString;
					_templateString = value;
					RaisePropertyChanged(SysCodeTemplate.Prop_TemplateString, oldValue, value);
				}
			}

		}

		[Property("CreaterID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 36)]
		public string CreaterID
		{
			get { return _createrID; }
			set
			{
				if ((_createrID == null) || (value == null) || (!value.Equals(_createrID)))
				{
                    object oldValue = _createrID;
					_createrID = value;
					RaisePropertyChanged(SysCodeTemplate.Prop_CreaterID, oldValue, value);
				}
			}

		}

		[Property("CreaterName", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string CreaterName
		{
			get { return _createrName; }
			set
			{
				if ((_createrName == null) || (value == null) || (!value.Equals(_createrName)))
				{
                    object oldValue = _createrName;
					_createrName = value;
					RaisePropertyChanged(SysCodeTemplate.Prop_CreaterName, oldValue, value);
				}
			}

		}

		[Property("LastModifiedDate", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? LastModifiedDate
		{
			get { return _lastModifiedDate; }
			set
			{
				if (value != _lastModifiedDate)
				{
                    object oldValue = _lastModifiedDate;
					_lastModifiedDate = value;
					RaisePropertyChanged(SysCodeTemplate.Prop_LastModifiedDate, oldValue, value);
				}
			}

		}

		[Property("CreatedDate", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? CreatedDate
		{
			get { return _createdDate; }
			set
			{
				if (value != _createdDate)
				{
                    object oldValue = _createdDate;
					_createdDate = value;
					RaisePropertyChanged(SysCodeTemplate.Prop_CreatedDate, oldValue, value);
				}
			}

		}

		#endregion
	} // SysCodeTemplate
}

