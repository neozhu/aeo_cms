// Business class MDM_MSR_UNIT generated from MDM_MSR_UNIT
// Creator: rw
// Created Date: [2018-04-17]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.MDM
{
	[ActiveRecord("MDM_MSR_UNIT")]
	public partial class MDM_MSR_UNIT : EntityBase<MDM_MSR_UNIT>
	{
		#region Property_Names

		public static string Prop_KZEX3 = "KZEX3";
		public static string Prop_KZEX6 = "KZEX6";
		public static string Prop_ANDEC = "ANDEC";
		public static string Prop_KZKEH = "KZKEH";
		public static string Prop_KZWOB = "KZWOB";
		public static string Prop_KZ1EH = "KZ1EH";
		public static string Prop_KZ2EH = "KZ2EH";
		public static string Prop_DIMID = "DIMID";
		public static string Prop_ZAEHL = "ZAEHL";
		public static string Prop_NENNR = "NENNR";
		public static string Prop_EXP10 = "EXP10";
		public static string Prop_ADDKO = "ADDKO";
		public static string Prop_EXPON = "EXPON";
		public static string Prop_DECAN = "DECAN";
		public static string Prop_ISOCODE = "ISOCODE";
		public static string Prop_PRIMARY = "PRIMARY";
		public static string Prop_TEMP_VALUE = "TEMP_VALUE";
		public static string Prop_TEMP_UNIT = "TEMP_UNIT";
		public static string Prop_FAMUNIT = "FAMUNIT";
		public static string Prop_PRESS_VAL = "PRESS_VAL";
		public static string Prop_PRESS_UNIT = "PRESS_UNIT";
		public static string Prop_DDTEXT = "DDTEXT";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_DB_KEY = "DB_KEY";
		public static string Prop_MSEHI = "MSEHI";

		#endregion

		#region Private_Variables

		private string _kZEX3;
		private string _kZEX6;
		private string _aNDEC;
		private string _kZKEH;
		private string _kZWOB;
		private string _kZ1EH;
		private string _kZ2EH;
		private string _dIMID;
		private string _zAEHL;
		private string _nENNR;
		private string _eXP10;
		private string _aDDKO;
		private string _eXPON;
		private string _dECAN;
		private string _iSOCODE;
		private string _pRIMARY;
		private string _tEMP_VALUE;
		private string _tEMP_UNIT;
		private string _fAMUNIT;
		private string _pRESS_VAL;
		private string _pRESS_UNIT;
		private string _dDTEXT;
		private string _sTATUS;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rid;
		private string _mEMO;
		private string _dB_KEY;
		private string _mSEHI;


		#endregion

		#region Constructors

		public MDM_MSR_UNIT()
		{
		}

		public MDM_MSR_UNIT(
			string p_kZEX3,
			string p_kZEX6,
			string p_aNDEC,
			string p_kZKEH,
			string p_kZWOB,
			string p_kZ1EH,
			string p_kZ2EH,
			string p_dIMID,
			string p_zAEHL,
			string p_nENNR,
			string p_eXP10,
			string p_aDDKO,
			string p_eXPON,
			string p_dECAN,
			string p_iSOCODE,
			string p_pRIMARY,
			string p_tEMP_VALUE,
			string p_tEMP_UNIT,
			string p_fAMUNIT,
			string p_pRESS_VAL,
			string p_pRESS_UNIT,
			string p_dDTEXT,
			string p_sTATUS,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rid,
			string p_mEMO,
			string p_dB_KEY,
			string p_mSEHI)
		{
			_kZEX3 = p_kZEX3;
			_kZEX6 = p_kZEX6;
			_aNDEC = p_aNDEC;
			_kZKEH = p_kZKEH;
			_kZWOB = p_kZWOB;
			_kZ1EH = p_kZ1EH;
			_kZ2EH = p_kZ2EH;
			_dIMID = p_dIMID;
			_zAEHL = p_zAEHL;
			_nENNR = p_nENNR;
			_eXP10 = p_eXP10;
			_aDDKO = p_aDDKO;
			_eXPON = p_eXPON;
			_dECAN = p_dECAN;
			_iSOCODE = p_iSOCODE;
			_pRIMARY = p_pRIMARY;
			_tEMP_VALUE = p_tEMP_VALUE;
			_tEMP_UNIT = p_tEMP_UNIT;
			_fAMUNIT = p_fAMUNIT;
			_pRESS_VAL = p_pRESS_VAL;
			_pRESS_UNIT = p_pRESS_UNIT;
			_dDTEXT = p_dDTEXT;
			_sTATUS = p_sTATUS;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_mEMO = p_mEMO;
			_dB_KEY = p_dB_KEY;
			_mSEHI = p_mSEHI;
		}

		#endregion

		#region Properties

		[Property("KZEX3", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string KZEX3
		{
			get { return _kZEX3; }
			set
			{
				if ((_kZEX3 == null) || (value == null) || (!value.Equals(_kZEX3)))
				{
                    object oldValue = _kZEX3;
					_kZEX3 = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_KZEX3, oldValue, value);
				}
			}
		}

		[Property("KZEX6", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string KZEX6
		{
			get { return _kZEX6; }
			set
			{
				if ((_kZEX6 == null) || (value == null) || (!value.Equals(_kZEX6)))
				{
                    object oldValue = _kZEX6;
					_kZEX6 = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_KZEX6, oldValue, value);
				}
			}
		}

		[Property("ANDEC", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string ANDEC
		{
			get { return _aNDEC; }
			set
			{
				if ((_aNDEC == null) || (value == null) || (!value.Equals(_aNDEC)))
				{
                    object oldValue = _aNDEC;
					_aNDEC = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_ANDEC, oldValue, value);
				}
			}
		}

		[Property("KZKEH", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string KZKEH
		{
			get { return _kZKEH; }
			set
			{
				if ((_kZKEH == null) || (value == null) || (!value.Equals(_kZKEH)))
				{
                    object oldValue = _kZKEH;
					_kZKEH = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_KZKEH, oldValue, value);
				}
			}
		}

		[Property("KZWOB", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string KZWOB
		{
			get { return _kZWOB; }
			set
			{
				if ((_kZWOB == null) || (value == null) || (!value.Equals(_kZWOB)))
				{
                    object oldValue = _kZWOB;
					_kZWOB = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_KZWOB, oldValue, value);
				}
			}
		}

		[Property("KZ1EH", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string KZ1EH
		{
			get { return _kZ1EH; }
			set
			{
				if ((_kZ1EH == null) || (value == null) || (!value.Equals(_kZ1EH)))
				{
                    object oldValue = _kZ1EH;
					_kZ1EH = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_KZ1EH, oldValue, value);
				}
			}
		}

		[Property("KZ2EH", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string KZ2EH
		{
			get { return _kZ2EH; }
			set
			{
				if ((_kZ2EH == null) || (value == null) || (!value.Equals(_kZ2EH)))
				{
                    object oldValue = _kZ2EH;
					_kZ2EH = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_KZ2EH, oldValue, value);
				}
			}
		}

		[Property("DIMID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string DIMID
		{
			get { return _dIMID; }
			set
			{
				if ((_dIMID == null) || (value == null) || (!value.Equals(_dIMID)))
				{
                    object oldValue = _dIMID;
					_dIMID = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_DIMID, oldValue, value);
				}
			}
		}

		[Property("ZAEHL", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string ZAEHL
		{
			get { return _zAEHL; }
			set
			{
				if ((_zAEHL == null) || (value == null) || (!value.Equals(_zAEHL)))
				{
                    object oldValue = _zAEHL;
					_zAEHL = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_ZAEHL, oldValue, value);
				}
			}
		}

		[Property("NENNR", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string NENNR
		{
			get { return _nENNR; }
			set
			{
				if ((_nENNR == null) || (value == null) || (!value.Equals(_nENNR)))
				{
                    object oldValue = _nENNR;
					_nENNR = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_NENNR, oldValue, value);
				}
			}
		}

		[Property("EXP10", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string EXP10
		{
			get { return _eXP10; }
			set
			{
				if ((_eXP10 == null) || (value == null) || (!value.Equals(_eXP10)))
				{
                    object oldValue = _eXP10;
					_eXP10 = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_EXP10, oldValue, value);
				}
			}
		}

		[Property("ADDKO", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string ADDKO
		{
			get { return _aDDKO; }
			set
			{
				if ((_aDDKO == null) || (value == null) || (!value.Equals(_aDDKO)))
				{
                    object oldValue = _aDDKO;
					_aDDKO = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_ADDKO, oldValue, value);
				}
			}
		}

		[Property("EXPON", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string EXPON
		{
			get { return _eXPON; }
			set
			{
				if ((_eXPON == null) || (value == null) || (!value.Equals(_eXPON)))
				{
                    object oldValue = _eXPON;
					_eXPON = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_EXPON, oldValue, value);
				}
			}
		}

		[Property("DECAN", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string DECAN
		{
			get { return _dECAN; }
			set
			{
				if ((_dECAN == null) || (value == null) || (!value.Equals(_dECAN)))
				{
                    object oldValue = _dECAN;
					_dECAN = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_DECAN, oldValue, value);
				}
			}
		}

		[Property("ISOCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string ISOCODE
		{
			get { return _iSOCODE; }
			set
			{
				if ((_iSOCODE == null) || (value == null) || (!value.Equals(_iSOCODE)))
				{
                    object oldValue = _iSOCODE;
					_iSOCODE = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_ISOCODE, oldValue, value);
				}
			}
		}

		[Property("PRIMARY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string PRIMARY
		{
			get { return _pRIMARY; }
			set
			{
				if ((_pRIMARY == null) || (value == null) || (!value.Equals(_pRIMARY)))
				{
                    object oldValue = _pRIMARY;
					_pRIMARY = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_PRIMARY, oldValue, value);
				}
			}
		}

		[Property("TEMP_VALUE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string TEMP_VALUE
		{
			get { return _tEMP_VALUE; }
			set
			{
				if ((_tEMP_VALUE == null) || (value == null) || (!value.Equals(_tEMP_VALUE)))
				{
                    object oldValue = _tEMP_VALUE;
					_tEMP_VALUE = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_TEMP_VALUE, oldValue, value);
				}
			}
		}

		[Property("TEMP_UNIT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string TEMP_UNIT
		{
			get { return _tEMP_UNIT; }
			set
			{
				if ((_tEMP_UNIT == null) || (value == null) || (!value.Equals(_tEMP_UNIT)))
				{
                    object oldValue = _tEMP_UNIT;
					_tEMP_UNIT = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_TEMP_UNIT, oldValue, value);
				}
			}
		}

		[Property("FAMUNIT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string FAMUNIT
		{
			get { return _fAMUNIT; }
			set
			{
				if ((_fAMUNIT == null) || (value == null) || (!value.Equals(_fAMUNIT)))
				{
                    object oldValue = _fAMUNIT;
					_fAMUNIT = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_FAMUNIT, oldValue, value);
				}
			}
		}

		[Property("PRESS_VAL", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string PRESS_VAL
		{
			get { return _pRESS_VAL; }
			set
			{
				if ((_pRESS_VAL == null) || (value == null) || (!value.Equals(_pRESS_VAL)))
				{
                    object oldValue = _pRESS_VAL;
					_pRESS_VAL = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_PRESS_VAL, oldValue, value);
				}
			}
		}

		[Property("PRESS_UNIT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string PRESS_UNIT
		{
			get { return _pRESS_UNIT; }
			set
			{
				if ((_pRESS_UNIT == null) || (value == null) || (!value.Equals(_pRESS_UNIT)))
				{
                    object oldValue = _pRESS_UNIT;
					_pRESS_UNIT = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_PRESS_UNIT, oldValue, value);
				}
			}
		}

		[Property("DDTEXT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string DDTEXT
		{
			get { return _dDTEXT; }
			set
			{
				if ((_dDTEXT == null) || (value == null) || (!value.Equals(_dDTEXT)))
				{
                    object oldValue = _dDTEXT;
					_dDTEXT = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_DDTEXT, oldValue, value);
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
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_MEMO, oldValue, value);
				}
			}
		}

		[Property("DB_KEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string DB_KEY
		{
			get { return _dB_KEY; }
			set
			{
				if ((_dB_KEY == null) || (value == null) || (!value.Equals(_dB_KEY)))
				{
                    object oldValue = _dB_KEY;
					_dB_KEY = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_DB_KEY, oldValue, value);
				}
			}
		}

		[Property("MSEHI", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string MSEHI
		{
			get { return _mSEHI; }
			set
			{
				if ((_mSEHI == null) || (value == null) || (!value.Equals(_mSEHI)))
				{
                    object oldValue = _mSEHI;
					_mSEHI = value;
					RaisePropertyChanged(MDM_MSR_UNIT.Prop_MSEHI, oldValue, value);
				}
			}
		}

		#endregion
	} // MDM_MSR_UNIT
}

