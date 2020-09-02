// Business class EDW_RPT_PROFILE generated from EDW_RPT_PROFILE
// Creator: rw
// Created Date: [2017-09-07]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace OnControl.Model
{
	[ActiveRecord("EDW_RPT_PROFILE")]
	public partial class EDW_RPT_PROFILE : EntityBase<EDW_RPT_PROFILE>
	{
		#region Property_Names

		public static string Prop_REPORTKEY = "REPORTKEY";
		public static string Prop_REPORTNAME = "REPORTNAME";
		public static string Prop_REPORTTYPE = "REPORTTYPE";
		public static string Prop_URL = "URL";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";

		#endregion

		#region Private_Variables

		private string _rEPORTKEY;
		private string _rEPORTNAME;
		private string _rEPORTTYPE;
		private string _uRL;
		private string _rid;
		private string _sTATUS;
		private string _mEMO;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;


		#endregion

		#region Constructors

		public EDW_RPT_PROFILE()
		{
		}

		public EDW_RPT_PROFILE(
			string p_rEPORTKEY,
			string p_rEPORTNAME,
			string p_rEPORTTYPE,
			string p_uRL,
			string p_rid,
			string p_sTATUS,
			string p_mEMO,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER)
		{
			_rEPORTKEY = p_rEPORTKEY;
			_rEPORTNAME = p_rEPORTNAME;
			_rEPORTTYPE = p_rEPORTTYPE;
			_uRL = p_uRL;
			_rid = p_rid;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
		}

		#endregion

		#region Properties

		[Property("REPORTKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 40)]
		public string REPORTKEY
		{
			get { return _rEPORTKEY; }
			set
			{
				if ((_rEPORTKEY == null) || (value == null) || (!value.Equals(_rEPORTKEY)))
				{
                    object oldValue = _rEPORTKEY;
					_rEPORTKEY = value;
					RaisePropertyChanged(EDW_RPT_PROFILE.Prop_REPORTKEY, oldValue, value);
				}
			}
		}

		[Property("REPORTNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 400)]
		public string REPORTNAME
		{
			get { return _rEPORTNAME; }
			set
			{
				if ((_rEPORTNAME == null) || (value == null) || (!value.Equals(_rEPORTNAME)))
				{
                    object oldValue = _rEPORTNAME;
					_rEPORTNAME = value;
					RaisePropertyChanged(EDW_RPT_PROFILE.Prop_REPORTNAME, oldValue, value);
				}
			}
		}

		[Property("REPORTTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 40)]
		public string REPORTTYPE
		{
			get { return _rEPORTTYPE; }
			set
			{
				if ((_rEPORTTYPE == null) || (value == null) || (!value.Equals(_rEPORTTYPE)))
				{
                    object oldValue = _rEPORTTYPE;
					_rEPORTTYPE = value;
					RaisePropertyChanged(EDW_RPT_PROFILE.Prop_REPORTTYPE, oldValue, value);
				}
			}
		}

		[Property("URL", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 2000)]
		public string URL
		{
			get { return _uRL; }
			set
			{
				if ((_uRL == null) || (value == null) || (!value.Equals(_uRL)))
				{
                    object oldValue = _uRL;
					_uRL = value;
					RaisePropertyChanged(EDW_RPT_PROFILE.Prop_URL, oldValue, value);
				}
			}
		}

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
		}

		[Property("STATUS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string STATUS
		{
			get { return _sTATUS; }
			set
			{
				if ((_sTATUS == null) || (value == null) || (!value.Equals(_sTATUS)))
				{
                    object oldValue = _sTATUS;
					_sTATUS = value;
					RaisePropertyChanged(EDW_RPT_PROFILE.Prop_STATUS, oldValue, value);
				}
			}
		}

		[Property("MEMO", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string MEMO
		{
			get { return _mEMO; }
			set
			{
				if ((_mEMO == null) || (value == null) || (!value.Equals(_mEMO)))
				{
                    object oldValue = _mEMO;
					_mEMO = value;
					RaisePropertyChanged(EDW_RPT_PROFILE.Prop_MEMO, oldValue, value);
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
					RaisePropertyChanged(EDW_RPT_PROFILE.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		[Property("CREATEUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string CREATEUSER
		{
			get { return _cREATEUSER; }
			set
			{
				if ((_cREATEUSER == null) || (value == null) || (!value.Equals(_cREATEUSER)))
				{
                    object oldValue = _cREATEUSER;
					_cREATEUSER = value;
					RaisePropertyChanged(EDW_RPT_PROFILE.Prop_CREATEUSER, oldValue, value);
				}
			}
		}

		[Property("MODIFYTIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? MODIFYTIME
		{
			get { return _mODIFYTIME; }
			set
			{
				if (value != _mODIFYTIME)
				{
                    object oldValue = _mODIFYTIME;
					_mODIFYTIME = value;
					RaisePropertyChanged(EDW_RPT_PROFILE.Prop_MODIFYTIME, oldValue, value);
				}
			}
		}

		[Property("MODIFYUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string MODIFYUSER
		{
			get { return _mODIFYUSER; }
			set
			{
				if ((_mODIFYUSER == null) || (value == null) || (!value.Equals(_mODIFYUSER)))
				{
                    object oldValue = _mODIFYUSER;
					_mODIFYUSER = value;
					RaisePropertyChanged(EDW_RPT_PROFILE.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

		#endregion
	} // EDW_RPT_PROFILE
}

