// Business class MDM_FEE generated from MDM_FEE
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
	[ActiveRecord("MDM_FEE")]
	public partial class MDM_FEE : EntityBase<MDM_FEE>
	{
		#region Property_Names

		public static string Prop_LANGTYPE = "LANGTYPE";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_DB_KEY = "DB_KEY";
		public static string Prop_TCET084 = "TCET084";
		public static string Prop_CHRGCATCD021_I = "CHRGCATCD021_I";
		public static string Prop_TCCLASS037 = "TCCLASS037";
		public static string Prop_INACT_TCET_IND = "INACT_TCET_IND";
		public static string Prop_TEXTDESC = "TEXTDESC";

		#endregion

		#region Private_Variables

		private string _lANGTYPE;
		private string _sTATUS;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rid;
		private string _mEMO;
		private string _dB_KEY;
		private string _tCET084;
		private string _cHRGCATCD021_I;
		private string _tCCLASS037;
		private string _iNACT_TCET_IND;
		private string _tEXTDESC;


		#endregion

		#region Constructors

		public MDM_FEE()
		{
		}

		public MDM_FEE(
			string p_lANGTYPE,
			string p_sTATUS,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rid,
			string p_mEMO,
			string p_dB_KEY,
			string p_tCET084,
			string p_cHRGCATCD021_I,
			string p_tCCLASS037,
			string p_iNACT_TCET_IND,
			string p_tEXTDESC)
		{
			_lANGTYPE = p_lANGTYPE;
			_sTATUS = p_sTATUS;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_mEMO = p_mEMO;
			_dB_KEY = p_dB_KEY;
			_tCET084 = p_tCET084;
			_cHRGCATCD021_I = p_cHRGCATCD021_I;
			_tCCLASS037 = p_tCCLASS037;
			_iNACT_TCET_IND = p_iNACT_TCET_IND;
			_tEXTDESC = p_tEXTDESC;
		}

        #endregion

        #region Properties

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
                    RaisePropertyChanged(MDM_FEE.Prop_LANGTYPE, oldValue, value);
                }
            }
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
					RaisePropertyChanged(MDM_FEE.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(MDM_FEE.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		[Property("CREATEUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CREATEUSER
		{
			get { return _cREATEUSER; }
			set
			{
				if ((_cREATEUSER == null) || (value == null) || (!value.Equals(_cREATEUSER)))
				{
                    object oldValue = _cREATEUSER;
					_cREATEUSER = value;
					RaisePropertyChanged(MDM_FEE.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(MDM_FEE.Prop_MODIFYTIME, oldValue, value);
				}
			}
		}

		[Property("MODIFYUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MODIFYUSER
		{
			get { return _mODIFYUSER; }
			set
			{
				if ((_mODIFYUSER == null) || (value == null) || (!value.Equals(_mODIFYUSER)))
				{
                    object oldValue = _mODIFYUSER;
					_mODIFYUSER = value;
					RaisePropertyChanged(MDM_FEE.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
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
					RaisePropertyChanged(MDM_FEE.Prop_MEMO, oldValue, value);
				}
			}
		}

		[Property("DB_KEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string DB_KEY
		{
			get { return _dB_KEY; }
			set
			{
				if ((_dB_KEY == null) || (value == null) || (!value.Equals(_dB_KEY)))
				{
                    object oldValue = _dB_KEY;
					_dB_KEY = value;
					RaisePropertyChanged(MDM_FEE.Prop_DB_KEY, oldValue, value);
				}
			}
		}

		[Property("TCET084", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string TCET084
		{
			get { return _tCET084; }
			set
			{
				if ((_tCET084 == null) || (value == null) || (!value.Equals(_tCET084)))
				{
                    object oldValue = _tCET084;
					_tCET084 = value;
					RaisePropertyChanged(MDM_FEE.Prop_TCET084, oldValue, value);
				}
			}
		}

		[Property("CHRGCATCD021_I", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string CHRGCATCD021_I
		{
			get { return _cHRGCATCD021_I; }
			set
			{
				if ((_cHRGCATCD021_I == null) || (value == null) || (!value.Equals(_cHRGCATCD021_I)))
				{
                    object oldValue = _cHRGCATCD021_I;
					_cHRGCATCD021_I = value;
					RaisePropertyChanged(MDM_FEE.Prop_CHRGCATCD021_I, oldValue, value);
				}
			}
		}

		[Property("TCCLASS037", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string TCCLASS037
		{
			get { return _tCCLASS037; }
			set
			{
				if ((_tCCLASS037 == null) || (value == null) || (!value.Equals(_tCCLASS037)))
				{
                    object oldValue = _tCCLASS037;
					_tCCLASS037 = value;
					RaisePropertyChanged(MDM_FEE.Prop_TCCLASS037, oldValue, value);
				}
			}
		}

		[Property("INACT_TCET_IND", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string INACT_TCET_IND
		{
			get { return _iNACT_TCET_IND; }
			set
			{
				if ((_iNACT_TCET_IND == null) || (value == null) || (!value.Equals(_iNACT_TCET_IND)))
				{
                    object oldValue = _iNACT_TCET_IND;
					_iNACT_TCET_IND = value;
					RaisePropertyChanged(MDM_FEE.Prop_INACT_TCET_IND, oldValue, value);
				}
			}
		}

		[Property("TEXTDESC", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string TEXTDESC
		{
			get { return _tEXTDESC; }
			set
			{
				if ((_tEXTDESC == null) || (value == null) || (!value.Equals(_tEXTDESC)))
				{
                    object oldValue = _tEXTDESC;
					_tEXTDESC = value;
					RaisePropertyChanged(MDM_FEE.Prop_TEXTDESC, oldValue, value);
				}
			}
		}

		#endregion
	} // MDM_FEE
}

