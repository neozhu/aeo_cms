// Business class SysCodeSN generated from SysCodeSN
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
	[ActiveRecord("SysCodeSN")]
    public partial class SysCodeSN : EntityBase<SysCodeSN>
	{
		#region Property_Names

		public static string Prop_CodeSNID = "CodeSNID";
		public static string Prop_TemplateID = "TemplateID";
		public static string Prop_Code = "Code";
		public static string Prop_Name = "Name";
		public static string Prop_Type = "Type";
		public static string Prop_IncreaseType = "IncreaseType";
        public static string Prop_SN = "SN";
        public static string Prop_Length = "Length";
        public static string Prop_Tag = "Tag";
        public static string Prop_Description = "Description";
		public static string Prop_LastModifiedDate = "LastModifiedDate";

		#endregion

		#region Private_Variables

		private string _codesnid;
		private string _templateID;
		private string _code;
		private string _name;
		private string _type;
		private string _increaseType;
        private string _sN;
        private int? _length;
        private string _tag;
        private string _description;
		private DateTime? _lastModifiedDate;


		#endregion

		#region Constructors

		public SysCodeSN()
		{
		}

		public SysCodeSN(
			string p_codesnid,
			string p_templateID,
			string p_code,
			string p_name,
			string p_type,
			string p_increaseType,
            string p_sN,
            int? p_length,
            string p_tag,
            string p_description,
			DateTime? p_lastModifiedDate)
		{
			_codesnid = p_codesnid;
			_templateID = p_templateID;
			_code = p_code;
			_name = p_name;
			_type = p_type;
			_increaseType = p_increaseType;
			_sN = p_sN;
            _length = p_length;
            _tag = p_tag;
            _description = p_description;
			_lastModifiedDate = p_lastModifiedDate;
		}

		#endregion

		#region Properties

		[PrimaryKey("CodeSNID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string CodeSNID
		{
			get { return _codesnid; }
			protected set { _codesnid = value; } // 处理列表编辑时去掉注释

		}

		[Property("TemplateID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string TemplateID
		{
			get { return _templateID; }
			set
			{
				if ((_templateID == null) || (value == null) || (!value.Equals(_templateID)))
				{
                    object oldValue = _templateID;
					_templateID = value;
					RaisePropertyChanged(SysCodeSN.Prop_TemplateID, oldValue, value);
				}
			}

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
					RaisePropertyChanged(SysCodeSN.Prop_Code, oldValue, value);
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
					RaisePropertyChanged(SysCodeSN.Prop_Name, oldValue, value);
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
					RaisePropertyChanged(SysCodeSN.Prop_Type, oldValue, value);
				}
			}

		}

		[Property("IncreaseType", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string IncreaseType
		{
			get { return _increaseType; }
			set
			{
				if ((_increaseType == null) || (value == null) || (!value.Equals(_increaseType)))
				{
                    object oldValue = _increaseType;
					_increaseType = value;
					RaisePropertyChanged(SysCodeSN.Prop_IncreaseType, oldValue, value);
				}
			}

		}

		[Property("SN", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string SN
		{
			get { return _sN; }
			set
			{
				if ((_sN == null) || (value == null) || (!value.Equals(_sN)))
				{
                    object oldValue = _sN;
					_sN = value;
					RaisePropertyChanged(SysCodeSN.Prop_SN, oldValue, value);
				}
			}

        }

        [Property("Length", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public int? Length
        {
            get { return _length; }
            set
            {
                if ((_length == null) || (value == null) || (!value.Equals(_length)))
                {
                    object oldValue = _length;
                    _length = value;
                    RaisePropertyChanged(SysCodeSN.Prop_Length, oldValue, value);
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
					RaisePropertyChanged(SysCodeSN.Prop_Tag, oldValue, value);
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
					RaisePropertyChanged(SysCodeSN.Prop_LastModifiedDate, oldValue, value);
				}
			}

		}

		#endregion
	} // SysCodeSN
}

