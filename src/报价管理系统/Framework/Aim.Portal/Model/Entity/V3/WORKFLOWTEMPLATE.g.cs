// Business class WORKFLOWTEMPLATE generated from WORKFLOWTEMPLATE
// Creator: Ray
// Created Date: [2014-04-09]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Aim.Portal.Model
{
	[ActiveRecord("WORKFLOWTEMPLATE")]
    public partial class WORKFLOWTEMPLATE : ModelBase<WORKFLOWTEMPLATE>
	{
		#region Property_Names

		public static string Prop_ID = "ID";
		public static string Prop_CODE = "CODE";
		public static string Prop_TEMPLATENAME = "TEMPLATENAME";
		public static string Prop_CATEGORY = "CATEGORY";
		public static string Prop_DESCRIPTION = "DESCRIPTION";
		public static string Prop_VERSION = "VERSION";
		public static string Prop_XAML = "XAML";
		public static string Prop_CONFIG = "CONFIG";
		public static string Prop_CREATOR = "CREATOR";
		public static string Prop_LASTREVISER = "LASTREVISER";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_UPDATETIME = "UPDATETIME";
		public static string Prop_STATUS = "STATUS";

		#endregion

		#region Private_Variables

		private string _id;
		private string _cODE;
		private string _tEMPLATENAME;
		private string _cATEGORY;
		private string _dESCRIPTION;
		private string _vERSION;
		private string _xAML;
		private string _cONFIG;
		private string _cREATOR;
		private string _lASTREVISER;
		private DateTime? _cREATETIME;
		private DateTime? _uPDATETIME;
		private System.Decimal _sTATUS;


		#endregion

		#region Constructors

		public WORKFLOWTEMPLATE()
		{
		}

		public WORKFLOWTEMPLATE(
			string p_id,
			string p_cODE,
			string p_tEMPLATENAME,
			string p_cATEGORY,
			string p_dESCRIPTION,
			string p_vERSION,
			string p_xAML,
			string p_cONFIG,
			string p_cREATOR,
			string p_lASTREVISER,
			DateTime? p_cREATETIME,
			DateTime? p_uPDATETIME,
			System.Decimal p_sTATUS)
		{
			_id = p_id;
			_cODE = p_cODE;
			_tEMPLATENAME = p_tEMPLATENAME;
			_cATEGORY = p_cATEGORY;
			_dESCRIPTION = p_dESCRIPTION;
			_vERSION = p_vERSION;
			_xAML = p_xAML;
			_cONFIG = p_cONFIG;
			_cREATOR = p_cREATOR;
			_lASTREVISER = p_lASTREVISER;
			_cREATETIME = p_cREATETIME;
			_uPDATETIME = p_uPDATETIME;
			_sTATUS = p_sTATUS;
		}

		#endregion

		#region Properties

		[PrimaryKey("ID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string ID
		{
			get { return _id; }
			set { _id = value; } // 处理列表编辑时去掉注释

		}

		[Property("CODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 150)]
		public string CODE
		{
			get { return _cODE; }
			set
			{
				if ((_cODE == null) || (value == null) || (!value.Equals(_cODE)))
				{
                    object oldValue = _cODE;
					_cODE = value;
					RaisePropertyChanged(WORKFLOWTEMPLATE.Prop_CODE, oldValue, value);
				}
			}

		}

		[Property("TEMPLATENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 300)]
		public string TEMPLATENAME
		{
			get { return _tEMPLATENAME; }
			set
			{
				if ((_tEMPLATENAME == null) || (value == null) || (!value.Equals(_tEMPLATENAME)))
				{
                    object oldValue = _tEMPLATENAME;
					_tEMPLATENAME = value;
					RaisePropertyChanged(WORKFLOWTEMPLATE.Prop_TEMPLATENAME, oldValue, value);
				}
			}

		}

		[Property("CATEGORY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 300)]
		public string CATEGORY
		{
			get { return _cATEGORY; }
			set
			{
				if ((_cATEGORY == null) || (value == null) || (!value.Equals(_cATEGORY)))
				{
                    object oldValue = _cATEGORY;
					_cATEGORY = value;
					RaisePropertyChanged(WORKFLOWTEMPLATE.Prop_CATEGORY, oldValue, value);
				}
			}

		}

		[Property("DESCRIPTION", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1500)]
		public string DESCRIPTION
		{
			get { return _dESCRIPTION; }
			set
			{
				if ((_dESCRIPTION == null) || (value == null) || (!value.Equals(_dESCRIPTION)))
				{
                    object oldValue = _dESCRIPTION;
					_dESCRIPTION = value;
					RaisePropertyChanged(WORKFLOWTEMPLATE.Prop_DESCRIPTION, oldValue, value);
				}
			}

		}

		[Property("VERSION", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 150)]
		public string VERSION
		{
			get { return _vERSION; }
			set
			{
				if ((_vERSION == null) || (value == null) || (!value.Equals(_vERSION)))
				{
                    object oldValue = _vERSION;
					_vERSION = value;
					RaisePropertyChanged(WORKFLOWTEMPLATE.Prop_VERSION, oldValue, value);
				}
			}

		}

		[Property("XAML", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 4000)]
		public string XAML
		{
			get { return _xAML; }
			set
			{
				if ((_xAML == null) || (value == null) || (!value.Equals(_xAML)))
				{
                    object oldValue = _xAML;
					_xAML = value;
					RaisePropertyChanged(WORKFLOWTEMPLATE.Prop_XAML, oldValue, value);
				}
			}

		}

		[Property("CONFIG", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string CONFIG
		{
			get { return _cONFIG; }
			set
			{
				if ((_cONFIG == null) || (value == null) || (!value.Equals(_cONFIG)))
				{
                    object oldValue = _cONFIG;
					_cONFIG = value;
					RaisePropertyChanged(WORKFLOWTEMPLATE.Prop_CONFIG, oldValue, value);
				}
			}

		}

		[Property("CREATOR", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 300)]
		public string CREATOR
		{
			get { return _cREATOR; }
			set
			{
				if ((_cREATOR == null) || (value == null) || (!value.Equals(_cREATOR)))
				{
                    object oldValue = _cREATOR;
					_cREATOR = value;
					RaisePropertyChanged(WORKFLOWTEMPLATE.Prop_CREATOR, oldValue, value);
				}
			}

		}

		[Property("LASTREVISER", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 300)]
		public string LASTREVISER
		{
			get { return _lASTREVISER; }
			set
			{
				if ((_lASTREVISER == null) || (value == null) || (!value.Equals(_lASTREVISER)))
				{
                    object oldValue = _lASTREVISER;
					_lASTREVISER = value;
					RaisePropertyChanged(WORKFLOWTEMPLATE.Prop_LASTREVISER, oldValue, value);
				}
			}

		}

		[Property("CREATETIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? CREATETIME
		{
			get { return _cREATETIME; }
			set
			{
				if (value != _cREATETIME)
				{
                    object oldValue = _cREATETIME;
					_cREATETIME = value;
					RaisePropertyChanged(WORKFLOWTEMPLATE.Prop_CREATETIME, oldValue, value);
				}
			}

		}

		[Property("UPDATETIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? UPDATETIME
		{
			get { return _uPDATETIME; }
			set
			{
				if (value != _uPDATETIME)
				{
                    object oldValue = _uPDATETIME;
					_uPDATETIME = value;
					RaisePropertyChanged(WORKFLOWTEMPLATE.Prop_UPDATETIME, oldValue, value);
				}
			}

		}

		[Property("STATUS", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true)]
		public System.Decimal STATUS
		{
			get { return _sTATUS; }
			set
			{
				if (value != _sTATUS)
				{
                    object oldValue = _sTATUS;
					_sTATUS = value;
					RaisePropertyChanged(WORKFLOWTEMPLATE.Prop_STATUS, oldValue, value);
				}
			}

		}

		#endregion
	} // WORKFLOWTEMPLATE
}

