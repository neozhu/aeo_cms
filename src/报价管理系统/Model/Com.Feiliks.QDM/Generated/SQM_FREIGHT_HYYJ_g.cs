// Business class SQM_FREIGHT_HYYJ generated from SQM_FREIGHT_HYYJ
// Creator: rw
// Created Date: [2020-02-17]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_FREIGHT_HYYJ")]
	public partial class SQM_FREIGHT_HYYJ : EntityBase<SQM_FREIGHT_HYYJ>
	{
		#region Property_Names

		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYID = "MODIFYID";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_AREA = "AREA";
		public static string Prop_QYG = "QYG";
		public static string Prop_MDG = "MDG";
		public static string Prop_BZ = "BZ";
		public static string Prop_GP20 = "GP20";
		public static string Prop_GP40 = "GP40";
		public static string Prop_HQ40 = "HQ40";
		public static string Prop_DL20 = "DL20";
		public static string Prop_DL40 = "DL40";
		public static string Prop_QYGMT = "QYGMT";
		public static string Prop_QYGMTCODE = "QYGMTCODE";
		public static string Prop_CGS = "CGS";
		public static string Prop_ZZG = "ZZG";
		public static string Prop_MDGMT = "MDGMT";
		public static string Prop_MDGMTCODE = "MDGMTCODE";
		public static string Prop_STARTDATE = "STARTDATE";
		public static string Prop_ENDDATE = "ENDDATE";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_KHR = "KHR";
		public static string Prop_QYGCODE = "QYGCODE";
		public static string Prop_AREACODE = "AREACODE";
		public static string Prop_MDGCODE = "MDGCODE";
		public static string Prop_ZZGCODE = "ZZGCODE";
		public static string Prop_BZCODE = "BZCODE";

		#endregion

		#region Private_Variables

		private DateTime? _cREATETIME;
		private string _cREATEID;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYID;
		private string _mODIFYUSER;
		private string _rid;
		private string _aREA;
		private string _qYG;
		private string _mDG;
		private string _bZ;
		private System.Decimal? _gP20;
		private System.Decimal? _gP40;
		private System.Decimal? _hQ40;
		private System.Decimal? _dL20;
		private System.Decimal? _dL40;
		private string _qYGMT;
		private string _qYGMTCODE;
		private string _cGS;
		private string _zZG;
		private string _mDGMT;
		private string _mDGMTCODE;
		private DateTime? _sTARTDATE;
		private DateTime? _eNDDATE;
		private string _sTATUS;
		private string _mEMO;
		private string _kHR;
		private string _qYGCODE;
		private string _aREACODE;
		private string _mDGCODE;
		private string _zZGCODE;
		private string _bZCODE;


		#endregion

		#region Constructors

		public SQM_FREIGHT_HYYJ()
		{
		}

		public SQM_FREIGHT_HYYJ(
			DateTime? p_cREATETIME,
			string p_cREATEID,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYID,
			string p_mODIFYUSER,
			string p_rid,
			string p_aREA,
			string p_qYG,
			string p_mDG,
			string p_bZ,
			System.Decimal? p_gP20,
			System.Decimal? p_gP40,
			System.Decimal? p_hQ40,
			System.Decimal? p_dL20,
			System.Decimal? p_dL40,
			string p_qYGMT,
			string p_qYGMTCODE,
			string p_cGS,
			string p_zZG,
			string p_mDGMT,
			string p_mDGMTCODE,
			DateTime? p_sTARTDATE,
			DateTime? p_eNDDATE,
			string p_sTATUS,
			string p_mEMO,
			string p_kHR,
			string p_qYGCODE,
			string p_aREACODE,
			string p_mDGCODE,
			string p_zZGCODE,
			string p_bZCODE)
		{
			_cREATETIME = p_cREATETIME;
			_cREATEID = p_cREATEID;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYID = p_mODIFYID;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_aREA = p_aREA;
			_qYG = p_qYG;
			_mDG = p_mDG;
			_bZ = p_bZ;
			_gP20 = p_gP20;
			_gP40 = p_gP40;
			_hQ40 = p_hQ40;
			_dL20 = p_dL20;
			_dL40 = p_dL40;
			_qYGMT = p_qYGMT;
			_qYGMTCODE = p_qYGMTCODE;
			_cGS = p_cGS;
			_zZG = p_zZG;
			_mDGMT = p_mDGMT;
			_mDGMTCODE = p_mDGMTCODE;
			_sTARTDATE = p_sTARTDATE;
			_eNDDATE = p_eNDDATE;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
			_kHR = p_kHR;
			_qYGCODE = p_qYGCODE;
			_aREACODE = p_aREACODE;
			_mDGCODE = p_mDGCODE;
			_zZGCODE = p_zZGCODE;
			_bZCODE = p_bZCODE;
		}

		#endregion

		#region Properties

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
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_MODIFYID, oldValue, value);
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
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
		}

		[Property("AREA", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string AREA
		{
			get { return _aREA; }
			set
			{
				if ((_aREA == null) || (value == null) || (!value.Equals(_aREA)))
				{
                    object oldValue = _aREA;
					_aREA = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_AREA, oldValue, value);
				}
			}
		}

		[Property("QYG", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string QYG
		{
			get { return _qYG; }
			set
			{
				if ((_qYG == null) || (value == null) || (!value.Equals(_qYG)))
				{
                    object oldValue = _qYG;
					_qYG = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_QYG, oldValue, value);
				}
			}
		}

		[Property("MDG", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MDG
		{
			get { return _mDG; }
			set
			{
				if ((_mDG == null) || (value == null) || (!value.Equals(_mDG)))
				{
                    object oldValue = _mDG;
					_mDG = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_MDG, oldValue, value);
				}
			}
		}

		[Property("BZ", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string BZ
		{
			get { return _bZ; }
			set
			{
				if ((_bZ == null) || (value == null) || (!value.Equals(_bZ)))
				{
                    object oldValue = _bZ;
					_bZ = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_BZ, oldValue, value);
				}
			}
		}

		[Property("GP20", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? GP20
		{
			get { return _gP20; }
			set
			{
				if (value != _gP20)
				{
                    object oldValue = _gP20;
					_gP20 = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_GP20, oldValue, value);
				}
			}
		}

		[Property("GP40", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? GP40
		{
			get { return _gP40; }
			set
			{
				if (value != _gP40)
				{
                    object oldValue = _gP40;
					_gP40 = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_GP40, oldValue, value);
				}
			}
		}

		[Property("HQ40", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? HQ40
		{
			get { return _hQ40; }
			set
			{
				if (value != _hQ40)
				{
                    object oldValue = _hQ40;
					_hQ40 = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_HQ40, oldValue, value);
				}
			}
		}

		[Property("DL20", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? DL20
		{
			get { return _dL20; }
			set
			{
				if (value != _dL20)
				{
                    object oldValue = _dL20;
					_dL20 = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_DL20, oldValue, value);
				}
			}
		}

		[Property("DL40", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? DL40
		{
			get { return _dL40; }
			set
			{
				if (value != _dL40)
				{
                    object oldValue = _dL40;
					_dL40 = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_DL40, oldValue, value);
				}
			}
		}

		[Property("QYGMT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string QYGMT
		{
			get { return _qYGMT; }
			set
			{
				if ((_qYGMT == null) || (value == null) || (!value.Equals(_qYGMT)))
				{
                    object oldValue = _qYGMT;
					_qYGMT = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_QYGMT, oldValue, value);
				}
			}
		}

		[Property("QYGMTCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string QYGMTCODE
		{
			get { return _qYGMTCODE; }
			set
			{
				if ((_qYGMTCODE == null) || (value == null) || (!value.Equals(_qYGMTCODE)))
				{
                    object oldValue = _qYGMTCODE;
					_qYGMTCODE = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_QYGMTCODE, oldValue, value);
				}
			}
		}

		[Property("CGS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CGS
		{
			get { return _cGS; }
			set
			{
				if ((_cGS == null) || (value == null) || (!value.Equals(_cGS)))
				{
                    object oldValue = _cGS;
					_cGS = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_CGS, oldValue, value);
				}
			}
		}

		[Property("ZZG", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string ZZG
		{
			get { return _zZG; }
			set
			{
				if ((_zZG == null) || (value == null) || (!value.Equals(_zZG)))
				{
                    object oldValue = _zZG;
					_zZG = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_ZZG, oldValue, value);
				}
			}
		}

		[Property("MDGMT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MDGMT
		{
			get { return _mDGMT; }
			set
			{
				if ((_mDGMT == null) || (value == null) || (!value.Equals(_mDGMT)))
				{
                    object oldValue = _mDGMT;
					_mDGMT = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_MDGMT, oldValue, value);
				}
			}
		}

		[Property("MDGMTCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string MDGMTCODE
		{
			get { return _mDGMTCODE; }
			set
			{
				if ((_mDGMTCODE == null) || (value == null) || (!value.Equals(_mDGMTCODE)))
				{
                    object oldValue = _mDGMTCODE;
					_mDGMTCODE = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_MDGMTCODE, oldValue, value);
				}
			}
		}

		[Property("STARTDATE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? STARTDATE
		{
			get { return _sTARTDATE; }
			set
			{
				if (value != _sTARTDATE)
				{
                    object oldValue = _sTARTDATE;
					_sTARTDATE = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_STARTDATE, oldValue, value);
				}
			}
		}

		[Property("ENDDATE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? ENDDATE
		{
			get { return _eNDDATE; }
			set
			{
				if (value != _eNDDATE)
				{
                    object oldValue = _eNDDATE;
					_eNDDATE = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_ENDDATE, oldValue, value);
				}
			}
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
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_MEMO, oldValue, value);
				}
			}
		}

		[Property("KHR", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string KHR
		{
			get { return _kHR; }
			set
			{
				if ((_kHR == null) || (value == null) || (!value.Equals(_kHR)))
				{
                    object oldValue = _kHR;
					_kHR = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_KHR, oldValue, value);
				}
			}
		}

		[Property("QYGCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string QYGCODE
		{
			get { return _qYGCODE; }
			set
			{
				if ((_qYGCODE == null) || (value == null) || (!value.Equals(_qYGCODE)))
				{
                    object oldValue = _qYGCODE;
					_qYGCODE = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_QYGCODE, oldValue, value);
				}
			}
		}

		[Property("AREACODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string AREACODE
		{
			get { return _aREACODE; }
			set
			{
				if ((_aREACODE == null) || (value == null) || (!value.Equals(_aREACODE)))
				{
                    object oldValue = _aREACODE;
					_aREACODE = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_AREACODE, oldValue, value);
				}
			}
		}

		[Property("MDGCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string MDGCODE
		{
			get { return _mDGCODE; }
			set
			{
				if ((_mDGCODE == null) || (value == null) || (!value.Equals(_mDGCODE)))
				{
                    object oldValue = _mDGCODE;
					_mDGCODE = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_MDGCODE, oldValue, value);
				}
			}
		}

		[Property("ZZGCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string ZZGCODE
		{
			get { return _zZGCODE; }
			set
			{
				if ((_zZGCODE == null) || (value == null) || (!value.Equals(_zZGCODE)))
				{
                    object oldValue = _zZGCODE;
					_zZGCODE = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_ZZGCODE, oldValue, value);
				}
			}
		}

		[Property("BZCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string BZCODE
		{
			get { return _bZCODE; }
			set
			{
				if ((_bZCODE == null) || (value == null) || (!value.Equals(_bZCODE)))
				{
                    object oldValue = _bZCODE;
					_bZCODE = value;
					RaisePropertyChanged(SQM_FREIGHT_HYYJ.Prop_BZCODE, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_FREIGHT_HYYJ
}

