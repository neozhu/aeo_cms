// Business class SQM_COST_KYGJ generated from SQM_COST_KYGJ
// Creator: rw
// Created Date: [2020-03-12]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace OnControl.Web
{
	[ActiveRecord("SQM_COST_KYGJ")]
	public partial class SQM_COST_KYGJ : EntityBase<SQM_COST_KYGJ>
	{
		#region Property_Names

		public static string Prop_WEIGHTXY45MC = "WEIGHTXY45MC";
		public static string Prop_MINMC = "MINMC";
		public static string Prop_WEIGHTDY45MC = "WEIGHTDY45MC";
		public static string Prop_WEIGHTDY100MC = "WEIGHTDY100MC";
		public static string Prop_WEIGHTDY500MC = "WEIGHTDY500MC";
		public static string Prop_WEIGHTDY1000MC = "WEIGHTDY1000MC";
		public static string Prop_HBZQ = "HBZQ";
		public static string Prop_DDTS = "DDTS";
		public static string Prop_BZGF = "BZGF";
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
		public static string Prop_HWLB = "HWLB";
		public static string Prop_MIN = "MIN";
		public static string Prop_WEIGHTXY45 = "WEIGHTXY45";
		public static string Prop_WEIGHTDY45 = "WEIGHTDY45";
		public static string Prop_WEIGHTDY100 = "WEIGHTDY100";
		public static string Prop_WEIGHTDY500 = "WEIGHTDY500";
		public static string Prop_WEIGHTDY1000 = "WEIGHTDY1000";
		public static string Prop_HKGS = "HKGS";
		public static string Prop_ZZG = "ZZG";
		public static string Prop_HBH = "HBH";
		public static string Prop_SKB = "SKB";
		public static string Prop_STARTDATE = "STARTDATE";
		public static string Prop_ENDDATE = "ENDDATE";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_DESCR = "DESCR";
		public static string Prop_EXT1 = "EXT1";
		public static string Prop_EXT2 = "EXT2";
		public static string Prop_EXT3 = "EXT3";
		public static string Prop_QYGCODE = "QYGCODE";
		public static string Prop_AREACODE = "AREACODE";
		public static string Prop_MDGCODE = "MDGCODE";
		public static string Prop_HKGSCODE = "HKGSCODE";
		public static string Prop_ZZGCODE = "ZZGCODE";
		public static string Prop_BZCODE = "BZCODE";

		#endregion

		#region Private_Variables

		private string _wEIGHTXY45MC;
		private string _mINMC;
		private string _wEIGHTDY45MC;
		private string _wEIGHTDY100MC;
		private string _wEIGHTDY500MC;
		private string _wEIGHTDY1000MC;
		private string _hBZQ;
		private string _dDTS;
		private string _bZGF;
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
		private string _hWLB;
		private System.Decimal? _mIN;
		private System.Decimal? _wEIGHTXY45;
		private System.Decimal? _wEIGHTDY45;
		private System.Decimal? _wEIGHTDY100;
		private System.Decimal? _wEIGHTDY500;
		private System.Decimal? _wEIGHTDY1000;
		private string _hKGS;
		private string _zZG;
		private string _hBH;
		private string _sKB;
		private DateTime? _sTARTDATE;
		private DateTime? _eNDDATE;
		private string _sTATUS;
		private string _mEMO;
		private string _dESCR;
		private string _eXT1;
		private string _eXT2;
		private string _eXT3;
		private string _qYGCODE;
		private string _aREACODE;
		private string _mDGCODE;
		private string _hKGSCODE;
		private string _zZGCODE;
		private string _bZCODE;


		#endregion

		#region Constructors

		public SQM_COST_KYGJ()
		{
		}

		public SQM_COST_KYGJ(
			string p_wEIGHTXY45MC,
			string p_mINMC,
			string p_wEIGHTDY45MC,
			string p_wEIGHTDY100MC,
			string p_wEIGHTDY500MC,
			string p_wEIGHTDY1000MC,
			string p_hBZQ,
			string p_dDTS,
			string p_bZGF,
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
			string p_hWLB,
			System.Decimal? p_mIN,
			System.Decimal? p_wEIGHTXY45,
			System.Decimal? p_wEIGHTDY45,
			System.Decimal? p_wEIGHTDY100,
			System.Decimal? p_wEIGHTDY500,
			System.Decimal? p_wEIGHTDY1000,
			string p_hKGS,
			string p_zZG,
			string p_hBH,
			string p_sKB,
			DateTime? p_sTARTDATE,
			DateTime? p_eNDDATE,
			string p_sTATUS,
			string p_mEMO,
			string p_dESCR,
			string p_eXT1,
			string p_eXT2,
			string p_eXT3,
			string p_qYGCODE,
			string p_aREACODE,
			string p_mDGCODE,
			string p_hKGSCODE,
			string p_zZGCODE,
			string p_bZCODE)
		{
			_wEIGHTXY45MC = p_wEIGHTXY45MC;
			_mINMC = p_mINMC;
			_wEIGHTDY45MC = p_wEIGHTDY45MC;
			_wEIGHTDY100MC = p_wEIGHTDY100MC;
			_wEIGHTDY500MC = p_wEIGHTDY500MC;
			_wEIGHTDY1000MC = p_wEIGHTDY1000MC;
			_hBZQ = p_hBZQ;
			_dDTS = p_dDTS;
			_bZGF = p_bZGF;
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
			_hWLB = p_hWLB;
			_mIN = p_mIN;
			_wEIGHTXY45 = p_wEIGHTXY45;
			_wEIGHTDY45 = p_wEIGHTDY45;
			_wEIGHTDY100 = p_wEIGHTDY100;
			_wEIGHTDY500 = p_wEIGHTDY500;
			_wEIGHTDY1000 = p_wEIGHTDY1000;
			_hKGS = p_hKGS;
			_zZG = p_zZG;
			_hBH = p_hBH;
			_sKB = p_sKB;
			_sTARTDATE = p_sTARTDATE;
			_eNDDATE = p_eNDDATE;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
			_dESCR = p_dESCR;
			_eXT1 = p_eXT1;
			_eXT2 = p_eXT2;
			_eXT3 = p_eXT3;
			_qYGCODE = p_qYGCODE;
			_aREACODE = p_aREACODE;
			_mDGCODE = p_mDGCODE;
			_hKGSCODE = p_hKGSCODE;
			_zZGCODE = p_zZGCODE;
			_bZCODE = p_bZCODE;
		}

		#endregion

		#region Properties

		[Property("WEIGHTXY45MC", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string WEIGHTXY45MC
		{
			get { return _wEIGHTXY45MC; }
			set
			{
				if ((_wEIGHTXY45MC == null) || (value == null) || (!value.Equals(_wEIGHTXY45MC)))
				{
                    object oldValue = _wEIGHTXY45MC;
					_wEIGHTXY45MC = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_WEIGHTXY45MC, oldValue, value);
				}
			}
		}

		[Property("MINMC", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string MINMC
		{
			get { return _mINMC; }
			set
			{
				if ((_mINMC == null) || (value == null) || (!value.Equals(_mINMC)))
				{
                    object oldValue = _mINMC;
					_mINMC = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_MINMC, oldValue, value);
				}
			}
		}

		[Property("WEIGHTDY45MC", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string WEIGHTDY45MC
		{
			get { return _wEIGHTDY45MC; }
			set
			{
				if ((_wEIGHTDY45MC == null) || (value == null) || (!value.Equals(_wEIGHTDY45MC)))
				{
                    object oldValue = _wEIGHTDY45MC;
					_wEIGHTDY45MC = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_WEIGHTDY45MC, oldValue, value);
				}
			}
		}

		[Property("WEIGHTDY100MC", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string WEIGHTDY100MC
		{
			get { return _wEIGHTDY100MC; }
			set
			{
				if ((_wEIGHTDY100MC == null) || (value == null) || (!value.Equals(_wEIGHTDY100MC)))
				{
                    object oldValue = _wEIGHTDY100MC;
					_wEIGHTDY100MC = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_WEIGHTDY100MC, oldValue, value);
				}
			}
		}

		[Property("WEIGHTDY500MC", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string WEIGHTDY500MC
		{
			get { return _wEIGHTDY500MC; }
			set
			{
				if ((_wEIGHTDY500MC == null) || (value == null) || (!value.Equals(_wEIGHTDY500MC)))
				{
                    object oldValue = _wEIGHTDY500MC;
					_wEIGHTDY500MC = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_WEIGHTDY500MC, oldValue, value);
				}
			}
		}

		[Property("WEIGHTDY1000MC", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string WEIGHTDY1000MC
		{
			get { return _wEIGHTDY1000MC; }
			set
			{
				if ((_wEIGHTDY1000MC == null) || (value == null) || (!value.Equals(_wEIGHTDY1000MC)))
				{
                    object oldValue = _wEIGHTDY1000MC;
					_wEIGHTDY1000MC = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_WEIGHTDY1000MC, oldValue, value);
				}
			}
		}

		[Property("HBZQ", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string HBZQ
		{
			get { return _hBZQ; }
			set
			{
				if ((_hBZQ == null) || (value == null) || (!value.Equals(_hBZQ)))
				{
                    object oldValue = _hBZQ;
					_hBZQ = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_HBZQ, oldValue, value);
				}
			}
		}

		[Property("DDTS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string DDTS
		{
			get { return _dDTS; }
			set
			{
				if ((_dDTS == null) || (value == null) || (!value.Equals(_dDTS)))
				{
                    object oldValue = _dDTS;
					_dDTS = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_DDTS, oldValue, value);
				}
			}
		}

		[Property("BZGF", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string BZGF
		{
			get { return _bZGF; }
			set
			{
				if ((_bZGF == null) || (value == null) || (!value.Equals(_bZGF)))
				{
                    object oldValue = _bZGF;
					_bZGF = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_BZGF, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_MODIFYID, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_AREA, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_QYG, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_MDG, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_BZ, oldValue, value);
				}
			}
		}

		[Property("HWLB", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string HWLB
		{
			get { return _hWLB; }
			set
			{
				if ((_hWLB == null) || (value == null) || (!value.Equals(_hWLB)))
				{
                    object oldValue = _hWLB;
					_hWLB = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_HWLB, oldValue, value);
				}
			}
		}

		[Property("MIN", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? MIN
		{
			get { return _mIN; }
			set
			{
				if (value != _mIN)
				{
                    object oldValue = _mIN;
					_mIN = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_MIN, oldValue, value);
				}
			}
		}

		[Property("WEIGHTXY45", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? WEIGHTXY45
		{
			get { return _wEIGHTXY45; }
			set
			{
				if (value != _wEIGHTXY45)
				{
                    object oldValue = _wEIGHTXY45;
					_wEIGHTXY45 = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_WEIGHTXY45, oldValue, value);
				}
			}
		}

		[Property("WEIGHTDY45", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? WEIGHTDY45
		{
			get { return _wEIGHTDY45; }
			set
			{
				if (value != _wEIGHTDY45)
				{
                    object oldValue = _wEIGHTDY45;
					_wEIGHTDY45 = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_WEIGHTDY45, oldValue, value);
				}
			}
		}

		[Property("WEIGHTDY100", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? WEIGHTDY100
		{
			get { return _wEIGHTDY100; }
			set
			{
				if (value != _wEIGHTDY100)
				{
                    object oldValue = _wEIGHTDY100;
					_wEIGHTDY100 = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_WEIGHTDY100, oldValue, value);
				}
			}
		}

		[Property("WEIGHTDY500", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? WEIGHTDY500
		{
			get { return _wEIGHTDY500; }
			set
			{
				if (value != _wEIGHTDY500)
				{
                    object oldValue = _wEIGHTDY500;
					_wEIGHTDY500 = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_WEIGHTDY500, oldValue, value);
				}
			}
		}

		[Property("WEIGHTDY1000", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? WEIGHTDY1000
		{
			get { return _wEIGHTDY1000; }
			set
			{
				if (value != _wEIGHTDY1000)
				{
                    object oldValue = _wEIGHTDY1000;
					_wEIGHTDY1000 = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_WEIGHTDY1000, oldValue, value);
				}
			}
		}

		[Property("HKGS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string HKGS
		{
			get { return _hKGS; }
			set
			{
				if ((_hKGS == null) || (value == null) || (!value.Equals(_hKGS)))
				{
                    object oldValue = _hKGS;
					_hKGS = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_HKGS, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_ZZG, oldValue, value);
				}
			}
		}

		[Property("HBH", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string HBH
		{
			get { return _hBH; }
			set
			{
				if ((_hBH == null) || (value == null) || (!value.Equals(_hBH)))
				{
                    object oldValue = _hBH;
					_hBH = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_HBH, oldValue, value);
				}
			}
		}

		[Property("SKB", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string SKB
		{
			get { return _sKB; }
			set
			{
				if ((_sKB == null) || (value == null) || (!value.Equals(_sKB)))
				{
                    object oldValue = _sKB;
					_sKB = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_SKB, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_STARTDATE, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_ENDDATE, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_MEMO, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_DESCR, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_EXT1, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_EXT2, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_EXT3, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_QYGCODE, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_AREACODE, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_MDGCODE, oldValue, value);
				}
			}
		}

		[Property("HKGSCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string HKGSCODE
		{
			get { return _hKGSCODE; }
			set
			{
				if ((_hKGSCODE == null) || (value == null) || (!value.Equals(_hKGSCODE)))
				{
                    object oldValue = _hKGSCODE;
					_hKGSCODE = value;
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_HKGSCODE, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_ZZGCODE, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGJ.Prop_BZCODE, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_COST_KYGJ
}

