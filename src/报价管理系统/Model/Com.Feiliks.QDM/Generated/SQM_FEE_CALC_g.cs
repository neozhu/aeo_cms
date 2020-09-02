// Business class SQM_FEE_CALC generated from SQM_FEE_CALC
// Creator: rw
// Created Date: [2018-08-29]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_FEE_CALC")]
	public partial class SQM_FEE_CALC : EntityBase<SQM_FEE_CALC>
	{
		#region Property_Names

		public static string Prop_CACLCODE = "CACLCODE";
		public static string Prop_JSFFZS = "JSFFZS";
		public static string Prop_ZLZS = "ZLZS";
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
		public static string Prop_FEECODE = "FEECODE";
		public static string Prop_FEENAME = "FEENAME";
		public static string Prop_CACLUNIT = "CACLUNIT";
		public static string Prop_MINPRICE = "MINPRICE";
		public static string Prop_PRECOND = "PRECOND";
		public static string Prop_RSLBASE = "RSLBASE";
		public static string Prop_ALLOWCACLOFFER = "ALLOWCACLOFFER";
		public static string Prop_MULBJFS = "MULBJFS";

		#endregion

		#region Private_Variables

		private string _cACLCODE;
		private string _jSFFZS;
		private string _zLZS;
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
		private string _fEECODE;
		private string _fEENAME;
		private string _cACLUNIT;
		private string _mINPRICE;
		private string _pRECOND;
		private string _rSLBASE;
		private string _aLLOWCACLOFFER;
		private string _mULBJFS;


		#endregion

		#region Constructors

		public SQM_FEE_CALC()
		{
		}

		public SQM_FEE_CALC(
			string p_cACLCODE,
			string p_jSFFZS,
			string p_zLZS,
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
			string p_fEECODE,
			string p_fEENAME,
			string p_cACLUNIT,
			string p_mINPRICE,
			string p_pRECOND,
			string p_rSLBASE,
			string p_aLLOWCACLOFFER,
			string p_mULBJFS)
		{
			_cACLCODE = p_cACLCODE;
			_jSFFZS = p_jSFFZS;
			_zLZS = p_zLZS;
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
			_fEECODE = p_fEECODE;
			_fEENAME = p_fEENAME;
			_cACLUNIT = p_cACLUNIT;
			_mINPRICE = p_mINPRICE;
			_pRECOND = p_pRECOND;
			_rSLBASE = p_rSLBASE;
			_aLLOWCACLOFFER = p_aLLOWCACLOFFER;
			_mULBJFS = p_mULBJFS;
		}

		#endregion

		#region Properties

		[Property("CACLCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string CACLCODE
		{
			get { return _cACLCODE; }
			set
			{
				if ((_cACLCODE == null) || (value == null) || (!value.Equals(_cACLCODE)))
				{
                    object oldValue = _cACLCODE;
					_cACLCODE = value;
					RaisePropertyChanged(SQM_FEE_CALC.Prop_CACLCODE, oldValue, value);
				}
			}
		}

		[Property("JSFFZS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string JSFFZS
		{
			get { return _jSFFZS; }
			set
			{
				if ((_jSFFZS == null) || (value == null) || (!value.Equals(_jSFFZS)))
				{
                    object oldValue = _jSFFZS;
					_jSFFZS = value;
					RaisePropertyChanged(SQM_FEE_CALC.Prop_JSFFZS, oldValue, value);
				}
			}
		}

		[Property("ZLZS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string ZLZS
		{
			get { return _zLZS; }
			set
			{
				if ((_zLZS == null) || (value == null) || (!value.Equals(_zLZS)))
				{
                    object oldValue = _zLZS;
					_zLZS = value;
					RaisePropertyChanged(SQM_FEE_CALC.Prop_ZLZS, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_CALC.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		[Property("CREATEID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string CREATEID
		{
			get { return _cREATEID; }
			set
			{
				if ((_cREATEID == null) || (value == null) || (!value.Equals(_cREATEID)))
				{
                    object oldValue = _cREATEID;
					_cREATEID = value;
					RaisePropertyChanged(SQM_FEE_CALC.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_CALC.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_CALC.Prop_MODIFYTIME, oldValue, value);
				}
			}
		}

		[Property("MODIFYID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string MODIFYID
		{
			get { return _mODIFYID; }
			set
			{
				if ((_mODIFYID == null) || (value == null) || (!value.Equals(_mODIFYID)))
				{
                    object oldValue = _mODIFYID;
					_mODIFYID = value;
					RaisePropertyChanged(SQM_FEE_CALC.Prop_MODIFYID, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_CALC.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
		}

		[Property("STATUS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string STATUS
		{
			get { return _sTATUS; }
			set
			{
				if ((_sTATUS == null) || (value == null) || (!value.Equals(_sTATUS)))
				{
                    object oldValue = _sTATUS;
					_sTATUS = value;
					RaisePropertyChanged(SQM_FEE_CALC.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_CALC.Prop_MEMO, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_CALC.Prop_SORD, oldValue, value);
				}
			}
		}

		[Property("FEECODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string FEECODE
		{
			get { return _fEECODE; }
			set
			{
				if ((_fEECODE == null) || (value == null) || (!value.Equals(_fEECODE)))
				{
                    object oldValue = _fEECODE;
					_fEECODE = value;
					RaisePropertyChanged(SQM_FEE_CALC.Prop_FEECODE, oldValue, value);
				}
			}
		}

		[Property("FEENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string FEENAME
		{
			get { return _fEENAME; }
			set
			{
				if ((_fEENAME == null) || (value == null) || (!value.Equals(_fEENAME)))
				{
                    object oldValue = _fEENAME;
					_fEENAME = value;
					RaisePropertyChanged(SQM_FEE_CALC.Prop_FEENAME, oldValue, value);
				}
			}
		}

		[Property("CACLUNIT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string CACLUNIT
		{
			get { return _cACLUNIT; }
			set
			{
				if ((_cACLUNIT == null) || (value == null) || (!value.Equals(_cACLUNIT)))
				{
                    object oldValue = _cACLUNIT;
					_cACLUNIT = value;
					RaisePropertyChanged(SQM_FEE_CALC.Prop_CACLUNIT, oldValue, value);
				}
			}
		}

		[Property("MINPRICE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string MINPRICE
		{
			get { return _mINPRICE; }
			set
			{
				if ((_mINPRICE == null) || (value == null) || (!value.Equals(_mINPRICE)))
				{
                    object oldValue = _mINPRICE;
					_mINPRICE = value;
					RaisePropertyChanged(SQM_FEE_CALC.Prop_MINPRICE, oldValue, value);
				}
			}
		}

		[Property("PRECOND", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string PRECOND
		{
			get { return _pRECOND; }
			set
			{
				if ((_pRECOND == null) || (value == null) || (!value.Equals(_pRECOND)))
				{
                    object oldValue = _pRECOND;
					_pRECOND = value;
					RaisePropertyChanged(SQM_FEE_CALC.Prop_PRECOND, oldValue, value);
				}
			}
		}

		[Property("RSLBASE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string RSLBASE
		{
			get { return _rSLBASE; }
			set
			{
				if ((_rSLBASE == null) || (value == null) || (!value.Equals(_rSLBASE)))
				{
                    object oldValue = _rSLBASE;
					_rSLBASE = value;
					RaisePropertyChanged(SQM_FEE_CALC.Prop_RSLBASE, oldValue, value);
				}
			}
		}

		[Property("ALLOWCACLOFFER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string ALLOWCACLOFFER
		{
			get { return _aLLOWCACLOFFER; }
			set
			{
				if ((_aLLOWCACLOFFER == null) || (value == null) || (!value.Equals(_aLLOWCACLOFFER)))
				{
                    object oldValue = _aLLOWCACLOFFER;
					_aLLOWCACLOFFER = value;
					RaisePropertyChanged(SQM_FEE_CALC.Prop_ALLOWCACLOFFER, oldValue, value);
				}
			}
		}

		[Property("MULBJFS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string MULBJFS
		{
			get { return _mULBJFS; }
			set
			{
				if ((_mULBJFS == null) || (value == null) || (!value.Equals(_mULBJFS)))
				{
                    object oldValue = _mULBJFS;
					_mULBJFS = value;
					RaisePropertyChanged(SQM_FEE_CALC.Prop_MULBJFS, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_FEE_CALC
}

