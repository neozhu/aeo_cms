// Business class SQM_COST_HY generated from SQM_COST_HY
// Creator: rw
// Created Date: [2020-03-11]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace OnControl.Web
{
	[ActiveRecord("SQM_COST_HY")]
	public partial class SQM_COST_HY : EntityBase<SQM_COST_HY>
	{
		#region Property_Names

		public static string Prop_MDGMT = "MDGMT";
		public static string Prop_MDGMTCODE = "MDGMTCODE";
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
		public static string Prop_CGS = "CGS";
		public static string Prop_BZ = "BZ";
		public static string Prop_GP20 = "GP20";
		public static string Prop_GP40 = "GP40";
		public static string Prop_HQ40 = "HQ40";
		public static string Prop_DL20 = "DL20";
		public static string Prop_DL40 = "DL40";
		public static string Prop_HC = "HC";
		public static string Prop_ZZG = "ZZG";
		public static string Prop_MT = "MT";
		public static string Prop_KHR = "KHR";
		public static string Prop_STARTDATE = "STARTDATE";
		public static string Prop_ENDDATE = "ENDDATE";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_DESCR = "DESCR";
		public static string Prop_EXT1 = "EXT1";
		public static string Prop_EXT2 = "EXT2";
		public static string Prop_EXT3 = "EXT3";
		public static string Prop_AREACODE = "AREACODE";
		public static string Prop_QYGCODE = "QYGCODE";
		public static string Prop_MDGCODE = "MDGCODE";
		public static string Prop_CGSCODE = "CGSCODE";
		public static string Prop_ZZGCODE = "ZZGCODE";
		public static string Prop_MTCODE = "MTCODE";
		public static string Prop_BZCODE = "BZCODE";

		#endregion

		#region Private_Variables

		private string _mDGMT;
		private string _mDGMTCODE;
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
		private string _cGS;
		private string _bZ;
		private System.Decimal? _gP20;
		private System.Decimal? _gP40;
		private System.Decimal? _hQ40;
		private System.Decimal? _dL20;
		private System.Decimal? _dL40;
		private System.Decimal? _hC;
		private string _zZG;
		private string _mT;
		private string _kHR;
		private DateTime? _sTARTDATE;
		private DateTime? _eNDDATE;
		private string _sTATUS;
		private string _mEMO;
		private string _dESCR;
		private string _eXT1;
		private string _eXT2;
		private string _eXT3;
		private string _aREACODE;
		private string _qYGCODE;
		private string _mDGCODE;
		private string _cGSCODE;
		private string _zZGCODE;
		private string _mTCODE;
		private string _bZCODE;


		#endregion

		#region Constructors

		public SQM_COST_HY()
		{
		}

		public SQM_COST_HY(
			string p_mDGMT,
			string p_mDGMTCODE,
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
			string p_cGS,
			string p_bZ,
			System.Decimal? p_gP20,
			System.Decimal? p_gP40,
			System.Decimal? p_hQ40,
			System.Decimal? p_dL20,
			System.Decimal? p_dL40,
			System.Decimal? p_hC,
			string p_zZG,
			string p_mT,
			string p_kHR,
			DateTime? p_sTARTDATE,
			DateTime? p_eNDDATE,
			string p_sTATUS,
			string p_mEMO,
			string p_dESCR,
			string p_eXT1,
			string p_eXT2,
			string p_eXT3,
			string p_aREACODE,
			string p_qYGCODE,
			string p_mDGCODE,
			string p_cGSCODE,
			string p_zZGCODE,
			string p_mTCODE,
			string p_bZCODE)
		{
			_mDGMT = p_mDGMT;
			_mDGMTCODE = p_mDGMTCODE;
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
			_cGS = p_cGS;
			_bZ = p_bZ;
			_gP20 = p_gP20;
			_gP40 = p_gP40;
			_hQ40 = p_hQ40;
			_dL20 = p_dL20;
			_dL40 = p_dL40;
			_hC = p_hC;
			_zZG = p_zZG;
			_mT = p_mT;
			_kHR = p_kHR;
			_sTARTDATE = p_sTARTDATE;
			_eNDDATE = p_eNDDATE;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
			_dESCR = p_dESCR;
			_eXT1 = p_eXT1;
			_eXT2 = p_eXT2;
			_eXT3 = p_eXT3;
			_aREACODE = p_aREACODE;
			_qYGCODE = p_qYGCODE;
			_mDGCODE = p_mDGCODE;
			_cGSCODE = p_cGSCODE;
			_zZGCODE = p_zZGCODE;
			_mTCODE = p_mTCODE;
			_bZCODE = p_bZCODE;
		}

		#endregion

		#region Properties

		[Property("MDGMT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string MDGMT
		{
			get { return _mDGMT; }
			set
			{
				if ((_mDGMT == null) || (value == null) || (!value.Equals(_mDGMT)))
				{
                    object oldValue = _mDGMT;
					_mDGMT = value;
					RaisePropertyChanged(SQM_COST_HY.Prop_MDGMT, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_MDGMTCODE, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_MODIFYID, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_AREA, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_QYG, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_MDG, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_CGS, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_BZ, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_GP20, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_GP40, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_HQ40, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_DL20, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_DL40, oldValue, value);
				}
			}
		}

		[Property("HC", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? HC
		{
			get { return _hC; }
			set
			{
				if (value != _hC)
				{
                    object oldValue = _hC;
					_hC = value;
					RaisePropertyChanged(SQM_COST_HY.Prop_HC, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_ZZG, oldValue, value);
				}
			}
		}

		[Property("MT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MT
		{
			get { return _mT; }
			set
			{
				if ((_mT == null) || (value == null) || (!value.Equals(_mT)))
				{
                    object oldValue = _mT;
					_mT = value;
					RaisePropertyChanged(SQM_COST_HY.Prop_MT, oldValue, value);
				}
			}
		}

		[Property("KHR", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string KHR
		{
			get { return _kHR; }
			set
			{
				if ((_kHR == null) || (value == null) || (!value.Equals(_kHR)))
				{
                    object oldValue = _kHR;
					_kHR = value;
					RaisePropertyChanged(SQM_COST_HY.Prop_KHR, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_STARTDATE, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_ENDDATE, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_MEMO, oldValue, value);
				}
			}
		}

		[Property("DESCR", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string DESCR
		{
			get { return _dESCR; }
			set
			{
				if ((_dESCR == null) || (value == null) || (!value.Equals(_dESCR)))
				{
                    object oldValue = _dESCR;
					_dESCR = value;
					RaisePropertyChanged(SQM_COST_HY.Prop_DESCR, oldValue, value);
				}
			}
		}

		[Property("EXT1", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string EXT1
		{
			get { return _eXT1; }
			set
			{
				if ((_eXT1 == null) || (value == null) || (!value.Equals(_eXT1)))
				{
                    object oldValue = _eXT1;
					_eXT1 = value;
					RaisePropertyChanged(SQM_COST_HY.Prop_EXT1, oldValue, value);
				}
			}
		}

		[Property("EXT2", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string EXT2
		{
			get { return _eXT2; }
			set
			{
				if ((_eXT2 == null) || (value == null) || (!value.Equals(_eXT2)))
				{
                    object oldValue = _eXT2;
					_eXT2 = value;
					RaisePropertyChanged(SQM_COST_HY.Prop_EXT2, oldValue, value);
				}
			}
		}

		[Property("EXT3", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string EXT3
		{
			get { return _eXT3; }
			set
			{
				if ((_eXT3 == null) || (value == null) || (!value.Equals(_eXT3)))
				{
                    object oldValue = _eXT3;
					_eXT3 = value;
					RaisePropertyChanged(SQM_COST_HY.Prop_EXT3, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_AREACODE, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_QYGCODE, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_MDGCODE, oldValue, value);
				}
			}
		}

		[Property("CGSCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string CGSCODE
		{
			get { return _cGSCODE; }
			set
			{
				if ((_cGSCODE == null) || (value == null) || (!value.Equals(_cGSCODE)))
				{
                    object oldValue = _cGSCODE;
					_cGSCODE = value;
					RaisePropertyChanged(SQM_COST_HY.Prop_CGSCODE, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_ZZGCODE, oldValue, value);
				}
			}
		}

		[Property("MTCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string MTCODE
		{
			get { return _mTCODE; }
			set
			{
				if ((_mTCODE == null) || (value == null) || (!value.Equals(_mTCODE)))
				{
                    object oldValue = _mTCODE;
					_mTCODE = value;
					RaisePropertyChanged(SQM_COST_HY.Prop_MTCODE, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_HY.Prop_BZCODE, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_COST_HY
}

