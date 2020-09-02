// Business class SQM_CALC_BASE_EXT generated from SQM_CALC_BASE_EXT
// Creator: rw
// Created Date: [2018-07-26]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_CALC_BASE_EXT")]
	public partial class SQM_CALC_BASE_EXT : EntityBase<SQM_CALC_BASE_EXT>
	{
		#region Property_Names

		public static string Prop_CALCNAME = "CALCNAME";
		public static string Prop_CALCCODE = "CALCCODE";
		public static string Prop_MDMTYPE = "MDMTYPE";
		public static string Prop_MDMKEY = "MDMKEY";
		public static string Prop_MDMFIELDNAME = "MDMFIELDNAME";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYID = "MODIFYID";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_SORD = "SORD";
		public static string Prop_MDMLOCTYPE = "MDMLOCTYPE";

		#endregion

		#region Private_Variables

		private string _cALCNAME;
		private string _cALCCODE;
		private string _mDMTYPE;
		private string _mDMKEY;
		private string _mDMFIELDNAME;
		private DateTime? _cREATETIME;
		private string _cREATEID;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYID;
		private string _mODIFYUSER;
		private string _rid;
		private string _sTATUS;
		private string _mEMO;
		private System.Decimal? _sORD;
		private string _mDMLOCTYPE;


		#endregion

		#region Constructors

		public SQM_CALC_BASE_EXT()
		{
		}

		public SQM_CALC_BASE_EXT(
			string p_cALCNAME,
			string p_cALCCODE,
			string p_mDMTYPE,
			string p_mDMKEY,
			string p_mDMFIELDNAME,
			DateTime? p_cREATETIME,
			string p_cREATEID,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYID,
			string p_mODIFYUSER,
			string p_rid,
			string p_sTATUS,
			string p_mEMO,
			System.Decimal? p_sORD,
			string p_mDMLOCTYPE)
		{
			_cALCNAME = p_cALCNAME;
			_cALCCODE = p_cALCCODE;
			_mDMTYPE = p_mDMTYPE;
			_mDMKEY = p_mDMKEY;
			_mDMFIELDNAME = p_mDMFIELDNAME;
			_cREATETIME = p_cREATETIME;
			_cREATEID = p_cREATEID;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYID = p_mODIFYID;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
			_sORD = p_sORD;
			_mDMLOCTYPE = p_mDMLOCTYPE;
		}

		#endregion

		#region Properties

		[Property("CALCNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CALCNAME
		{
			get { return _cALCNAME; }
			set
			{
				if ((_cALCNAME == null) || (value == null) || (!value.Equals(_cALCNAME)))
				{
                    object oldValue = _cALCNAME;
					_cALCNAME = value;
					RaisePropertyChanged(SQM_CALC_BASE_EXT.Prop_CALCNAME, oldValue, value);
				}
			}
		}

		[Property("CALCCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CALCCODE
		{
			get { return _cALCCODE; }
			set
			{
				if ((_cALCCODE == null) || (value == null) || (!value.Equals(_cALCCODE)))
				{
                    object oldValue = _cALCCODE;
					_cALCCODE = value;
					RaisePropertyChanged(SQM_CALC_BASE_EXT.Prop_CALCCODE, oldValue, value);
				}
			}
		}

		[Property("MDMTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MDMTYPE
		{
			get { return _mDMTYPE; }
			set
			{
				if ((_mDMTYPE == null) || (value == null) || (!value.Equals(_mDMTYPE)))
				{
                    object oldValue = _mDMTYPE;
					_mDMTYPE = value;
					RaisePropertyChanged(SQM_CALC_BASE_EXT.Prop_MDMTYPE, oldValue, value);
				}
			}
		}

		[Property("MDMKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MDMKEY
		{
			get { return _mDMKEY; }
			set
			{
				if ((_mDMKEY == null) || (value == null) || (!value.Equals(_mDMKEY)))
				{
                    object oldValue = _mDMKEY;
					_mDMKEY = value;
					RaisePropertyChanged(SQM_CALC_BASE_EXT.Prop_MDMKEY, oldValue, value);
				}
			}
		}

		[Property("MDMFIELDNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MDMFIELDNAME
		{
			get { return _mDMFIELDNAME; }
			set
			{
				if ((_mDMFIELDNAME == null) || (value == null) || (!value.Equals(_mDMFIELDNAME)))
				{
                    object oldValue = _mDMFIELDNAME;
					_mDMFIELDNAME = value;
					RaisePropertyChanged(SQM_CALC_BASE_EXT.Prop_MDMFIELDNAME, oldValue, value);
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
					RaisePropertyChanged(SQM_CALC_BASE_EXT.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		[Property("CREATEID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CREATEID
		{
			get { return _cREATEID; }
			set
			{
				if ((_cREATEID == null) || (value == null) || (!value.Equals(_cREATEID)))
				{
                    object oldValue = _cREATEID;
					_cREATEID = value;
					RaisePropertyChanged(SQM_CALC_BASE_EXT.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SQM_CALC_BASE_EXT.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_CALC_BASE_EXT.Prop_MODIFYTIME, oldValue, value);
				}
			}
		}

		[Property("MODIFYID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MODIFYID
		{
			get { return _mODIFYID; }
			set
			{
				if ((_mODIFYID == null) || (value == null) || (!value.Equals(_mODIFYID)))
				{
                    object oldValue = _mODIFYID;
					_mODIFYID = value;
					RaisePropertyChanged(SQM_CALC_BASE_EXT.Prop_MODIFYID, oldValue, value);
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
					RaisePropertyChanged(SQM_CALC_BASE_EXT.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_CALC_BASE_EXT.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_CALC_BASE_EXT.Prop_MEMO, oldValue, value);
				}
			}
		}

		[Property("SORD", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? SORD
		{
			get { return _sORD; }
			set
			{
				if (value != _sORD)
				{
                    object oldValue = _sORD;
					_sORD = value;
					RaisePropertyChanged(SQM_CALC_BASE_EXT.Prop_SORD, oldValue, value);
				}
			}
		}

		[Property("MDMLOCTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string MDMLOCTYPE
		{
			get { return _mDMLOCTYPE; }
			set
			{
				if ((_mDMLOCTYPE == null) || (value == null) || (!value.Equals(_mDMLOCTYPE)))
				{
                    object oldValue = _mDMLOCTYPE;
					_mDMLOCTYPE = value;
					RaisePropertyChanged(SQM_CALC_BASE_EXT.Prop_MDMLOCTYPE, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_CALC_BASE_EXT
}

