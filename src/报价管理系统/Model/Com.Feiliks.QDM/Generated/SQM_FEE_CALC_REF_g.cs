// Business class SQM_FEE_CALC_REF generated from SQM_FEE_CALC_REF
// Creator: rw
// Created Date: [2018-07-17]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_FEE_CALC_REF")]
	public partial class SQM_FEE_CALC_REF : EntityBase<SQM_FEE_CALC_REF>
	{
		#region Property_Names

		public static string Prop_DJFSRID = "DJFSRID";
		public static string Prop_ISSEARCH = "ISSEARCH";
		public static string Prop_SCALE = "SCALE";
		public static string Prop_MSRCODE = "MSRCODE";
		public static string Prop_MSRUNIT = "MSRUNIT";
		public static string Prop_GDZRID = "GDZRID";
		public static string Prop_GDZKEY = "GDZKEY";
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
		public static string Prop_CALCCODE = "CALCCODE";
		public static string Prop_CALCNAME = "CALCNAME";
		public static string Prop_ISCNT = "ISCNT";
		public static string Prop_ALIAS = "ALIAS";
		public static string Prop_VALCOL = "VALCOL";
		public static string Prop_CACLUNIT = "CACLUNIT";

		#endregion

		#region Private_Variables

		private string _dJFSRID;
		private string _iSSEARCH;
		private string _sCALE;
		private string _mSRCODE;
		private string _mSRUNIT;
		private string _gDZRID;
		private string _gDZKEY;
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
		private string _cALCCODE;
		private string _cALCNAME;
		private string _iSCNT;
		private string _aLIAS;
		private string _vALCOL;
		private string _cACLUNIT;


		#endregion

		#region Constructors

		public SQM_FEE_CALC_REF()
		{
		}

		public SQM_FEE_CALC_REF(
			string p_dJFSRID,
			string p_iSSEARCH,
			string p_sCALE,
			string p_mSRCODE,
			string p_mSRUNIT,
			string p_gDZRID,
			string p_gDZKEY,
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
			string p_cALCCODE,
			string p_cALCNAME,
			string p_iSCNT,
			string p_aLIAS,
			string p_vALCOL,
			string p_cACLUNIT)
		{
			_dJFSRID = p_dJFSRID;
			_iSSEARCH = p_iSSEARCH;
			_sCALE = p_sCALE;
			_mSRCODE = p_mSRCODE;
			_mSRUNIT = p_mSRUNIT;
			_gDZRID = p_gDZRID;
			_gDZKEY = p_gDZKEY;
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
			_cALCCODE = p_cALCCODE;
			_cALCNAME = p_cALCNAME;
			_iSCNT = p_iSCNT;
			_aLIAS = p_aLIAS;
			_vALCOL = p_vALCOL;
			_cACLUNIT = p_cACLUNIT;
		}

		#endregion

		#region Properties

		[Property("DJFSRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string DJFSRID
		{
			get { return _dJFSRID; }
			set
			{
				if ((_dJFSRID == null) || (value == null) || (!value.Equals(_dJFSRID)))
				{
                    object oldValue = _dJFSRID;
					_dJFSRID = value;
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_DJFSRID, oldValue, value);
				}
			}
		}

		[Property("ISSEARCH", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string ISSEARCH
		{
			get { return _iSSEARCH; }
			set
			{
				if ((_iSSEARCH == null) || (value == null) || (!value.Equals(_iSSEARCH)))
				{
                    object oldValue = _iSSEARCH;
					_iSSEARCH = value;
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_ISSEARCH, oldValue, value);
				}
			}
		}

		[Property("SCALE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string SCALE
		{
			get { return _sCALE; }
			set
			{
				if ((_sCALE == null) || (value == null) || (!value.Equals(_sCALE)))
				{
                    object oldValue = _sCALE;
					_sCALE = value;
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_SCALE, oldValue, value);
				}
			}
		}

		[Property("MSRCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string MSRCODE
		{
			get { return _mSRCODE; }
			set
			{
				if ((_mSRCODE == null) || (value == null) || (!value.Equals(_mSRCODE)))
				{
                    object oldValue = _mSRCODE;
					_mSRCODE = value;
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_MSRCODE, oldValue, value);
				}
			}
		}

		[Property("MSRUNIT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string MSRUNIT
		{
			get { return _mSRUNIT; }
			set
			{
				if ((_mSRUNIT == null) || (value == null) || (!value.Equals(_mSRUNIT)))
				{
                    object oldValue = _mSRUNIT;
					_mSRUNIT = value;
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_MSRUNIT, oldValue, value);
				}
			}
		}

		[Property("GDZRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string GDZRID
		{
			get { return _gDZRID; }
			set
			{
				if ((_gDZRID == null) || (value == null) || (!value.Equals(_gDZRID)))
				{
                    object oldValue = _gDZRID;
					_gDZRID = value;
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_GDZRID, oldValue, value);
				}
			}
		}

		[Property("GDZKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string GDZKEY
		{
			get { return _gDZKEY; }
			set
			{
				if ((_gDZKEY == null) || (value == null) || (!value.Equals(_gDZKEY)))
				{
                    object oldValue = _gDZKEY;
					_gDZKEY = value;
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_GDZKEY, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_MODIFYID, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_MEMO, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_SORD, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_FEECODE, oldValue, value);
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
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_CALCCODE, oldValue, value);
				}
			}
		}

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
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_CALCNAME, oldValue, value);
				}
			}
		}

		[Property("ISCNT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string ISCNT
		{
			get { return _iSCNT; }
			set
			{
				if ((_iSCNT == null) || (value == null) || (!value.Equals(_iSCNT)))
				{
                    object oldValue = _iSCNT;
					_iSCNT = value;
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_ISCNT, oldValue, value);
				}
			}
		}

		[Property("ALIAS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string ALIAS
		{
			get { return _aLIAS; }
			set
			{
				if ((_aLIAS == null) || (value == null) || (!value.Equals(_aLIAS)))
				{
                    object oldValue = _aLIAS;
					_aLIAS = value;
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_ALIAS, oldValue, value);
				}
			}
		}

		[Property("VALCOL", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string VALCOL
		{
			get { return _vALCOL; }
			set
			{
				if ((_vALCOL == null) || (value == null) || (!value.Equals(_vALCOL)))
				{
                    object oldValue = _vALCOL;
					_vALCOL = value;
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_VALCOL, oldValue, value);
				}
			}
		}

		[Property("CACLUNIT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string CACLUNIT
		{
			get { return _cACLUNIT; }
			set
			{
				if ((_cACLUNIT == null) || (value == null) || (!value.Equals(_cACLUNIT)))
				{
                    object oldValue = _cACLUNIT;
					_cACLUNIT = value;
					RaisePropertyChanged(SQM_FEE_CALC_REF.Prop_CACLUNIT, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_FEE_CALC_REF
}

