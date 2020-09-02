// Business class MDM_SERVICE generated from MDM_SERVICE
// Creator: rw
// Created Date: [2019-12-10]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.MDM
{
	[ActiveRecord("MDM_SERVICE")]
	public partial class MDM_SERVICE : EntityBase<MDM_SERVICE>
	{
		#region Property_Names

		public static string Prop_FOOTYPE = "FOOTYPE";
		public static string Prop_LANGTYPE = "LANGTYPE";
		public static string Prop_SERVICETYPE = "SERVICETYPE";
		public static string Prop_SERVICENAME = "SERVICENAME";
		public static string Prop_PRODUCTCODE = "PRODUCTCODE";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";

		#endregion

		#region Private_Variables

		private string _fOOTYPE;
		private string _lANGTYPE;
		private string _sERVICETYPE;
		private string _sERVICENAME;
		private string _pRODUCTCODE;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rid;
		private string _sTATUS;
		private string _mEMO;


		#endregion

		#region Constructors

		public MDM_SERVICE()
		{
		}

		public MDM_SERVICE(
			string p_fOOTYPE,
			string p_lANGTYPE,
			string p_sERVICETYPE,
			string p_sERVICENAME,
			string p_pRODUCTCODE,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rid,
			string p_sTATUS,
			string p_mEMO)
		{
			_fOOTYPE = p_fOOTYPE;
			_lANGTYPE = p_lANGTYPE;
			_sERVICETYPE = p_sERVICETYPE;
			_sERVICENAME = p_sERVICENAME;
			_pRODUCTCODE = p_pRODUCTCODE;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
		}

		#endregion

		#region Properties

		[Property("FOOTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string FOOTYPE
		{
			get { return _fOOTYPE; }
			set
			{
				if ((_fOOTYPE == null) || (value == null) || (!value.Equals(_fOOTYPE)))
				{
                    object oldValue = _fOOTYPE;
					_fOOTYPE = value;
					RaisePropertyChanged(MDM_SERVICE.Prop_FOOTYPE, oldValue, value);
				}
			}
		}

		[Property("LANGTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string LANGTYPE
		{
			get { return _lANGTYPE; }
			set
			{
				if ((_lANGTYPE == null) || (value == null) || (!value.Equals(_lANGTYPE)))
				{
                    object oldValue = _lANGTYPE;
					_lANGTYPE = value;
					RaisePropertyChanged(MDM_SERVICE.Prop_LANGTYPE, oldValue, value);
				}
			}
		}

		[Property("SERVICETYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string SERVICETYPE
		{
			get { return _sERVICETYPE; }
			set
			{
				if ((_sERVICETYPE == null) || (value == null) || (!value.Equals(_sERVICETYPE)))
				{
                    object oldValue = _sERVICETYPE;
					_sERVICETYPE = value;
					RaisePropertyChanged(MDM_SERVICE.Prop_SERVICETYPE, oldValue, value);
				}
			}
		}

		[Property("SERVICENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string SERVICENAME
		{
			get { return _sERVICENAME; }
			set
			{
				if ((_sERVICENAME == null) || (value == null) || (!value.Equals(_sERVICENAME)))
				{
                    object oldValue = _sERVICENAME;
					_sERVICENAME = value;
					RaisePropertyChanged(MDM_SERVICE.Prop_SERVICENAME, oldValue, value);
				}
			}
		}

		[Property("PRODUCTCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string PRODUCTCODE
		{
			get { return _pRODUCTCODE; }
			set
			{
				if ((_pRODUCTCODE == null) || (value == null) || (!value.Equals(_pRODUCTCODE)))
				{
                    object oldValue = _pRODUCTCODE;
					_pRODUCTCODE = value;
					RaisePropertyChanged(MDM_SERVICE.Prop_PRODUCTCODE, oldValue, value);
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
					RaisePropertyChanged(MDM_SERVICE.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		[Property("CREATEUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string CREATEUSER
		{
			get { return _cREATEUSER; }
			set
			{
				if ((_cREATEUSER == null) || (value == null) || (!value.Equals(_cREATEUSER)))
				{
                    object oldValue = _cREATEUSER;
					_cREATEUSER = value;
					RaisePropertyChanged(MDM_SERVICE.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(MDM_SERVICE.Prop_MODIFYTIME, oldValue, value);
				}
			}
		}

		[Property("MODIFYUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string MODIFYUSER
		{
			get { return _mODIFYUSER; }
			set
			{
				if ((_mODIFYUSER == null) || (value == null) || (!value.Equals(_mODIFYUSER)))
				{
                    object oldValue = _mODIFYUSER;
					_mODIFYUSER = value;
					RaisePropertyChanged(MDM_SERVICE.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(MDM_SERVICE.Prop_STATUS, oldValue, value);
				}
			}
		}

		[Property("MEMO", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string MEMO
		{
			get { return _mEMO; }
			set
			{
				if ((_mEMO == null) || (value == null) || (!value.Equals(_mEMO)))
				{
                    object oldValue = _mEMO;
					_mEMO = value;
					RaisePropertyChanged(MDM_SERVICE.Prop_MEMO, oldValue, value);
				}
			}
		}

		#endregion
	} // MDM_SERVICE
}

