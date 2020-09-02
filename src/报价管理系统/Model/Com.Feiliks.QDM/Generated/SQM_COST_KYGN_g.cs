// Business class SQM_COST_KYGN generated from SQM_COST_KYGN
// Creator: rw
// Created Date: [2018-04-09]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace OnControl.Model
{
	[ActiveRecord("SQM_COST_KYGN")]
	public partial class SQM_COST_KYGN : EntityBase<SQM_COST_KYGN>
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
		public static string Prop_HX = "HX";
		public static string Prop_QYG = "QYG";
		public static string Prop_HWLB = "HWLB";
		public static string Prop_MINPRICE = "MINPRICE";
		public static string Prop_DEFGPRICE = "DEFGPRICE";
		public static string Prop_WEIGHTXY45 = "WEIGHTXY45";
		public static string Prop_WEIGHTDY45 = "WEIGHTDY45";
		public static string Prop_WEIGHTDY100 = "WEIGHTDY100";
		public static string Prop_WEIGHTDY500 = "WEIGHTDY500";
		public static string Prop_WEIGHTDY1000 = "WEIGHTDY1000";
		public static string Prop_HKGS = "HKGS";
		public static string Prop_HBH = "HBH";
		public static string Prop_SKB = "SKB";
		public static string Prop_STARTDATE = "STARTDATE";
		public static string Prop_ENDDATE = "ENDDATE";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";
        public static string Prop_M2 = "M2";
        public static string Prop_BZ = "BZ";
        public static string Prop_DESCR = "DESCR";
        public static string Prop_EXT1 = "EXT1";
        public static string Prop_EXT2 = "EXT2";
        public static string Prop_EXT3 = "EXT3";
        public static string Prop_AREACODE = "AREACODE";
        public static string Prop_QYGCODE = "QYGCODE";
        public static string Prop_HXCODE = "HXCODE";
        public static string Prop_HKGSCODE = "HKGSCODE";
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
		private string _hX;
		private string _qYG;
		private string _hWLB;
		private System.Decimal? _mINPRICE;
		private System.Decimal? _dEFGPRICE;
		private System.Decimal? _wEIGHTXY45;
		private System.Decimal? _wEIGHTDY45;
		private System.Decimal? _wEIGHTDY100;
		private System.Decimal? _wEIGHTDY500;
		private System.Decimal? _wEIGHTDY1000;
		private string _hKGS;
		private string _hBH;
		private string _sKB;
		private DateTime? _sTARTDATE;
		private DateTime? _eNDDATE;
		private string _sTATUS;
		private string _mEMO;
        private decimal? _m2;
        private string _bZ;
        private string _dESCR;
        private string _eXT1;
        private string _eXT2;
        private string _eXT3;
        private string _aREACODE;
        private string _qYGCODE;
        private string _hXCODE;
        private string _hKGSCODE;
        private string _bZCODE;
        #endregion

        #region Constructors

        public SQM_COST_KYGN()
		{
		}

		public SQM_COST_KYGN(
			DateTime? p_cREATETIME,
			string p_cREATEID,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYID,
			string p_mODIFYUSER,
			string p_rid,
			string p_aREA,
			string p_hX,
			string p_qYG,
			string p_hWLB,
			System.Decimal? p_mINPRICE,
			System.Decimal? p_dEFGPRICE,
			System.Decimal? p_wEIGHTXY45,
			System.Decimal? p_wEIGHTDY45,
			System.Decimal? p_wEIGHTDY100,
			System.Decimal? p_wEIGHTDY500,
			System.Decimal? p_wEIGHTDY1000,
			string p_hKGS,
			string p_hBH,
            string p_sKB,
			DateTime? p_sTARTDATE,
			DateTime? p_eNDDATE,
			string p_sTATUS,
			string p_mEMO,
            System.Decimal? p_m2,
            string p_bZ,
            string p_dESCR,
            string p_eXT1,
            string p_eXT2,
            string p_eXT3,
            string p_aREACODE,
            string p_qYGCODE,
            string p_hXCODE,
            string p_hKGSCODE,
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
			_hX = p_hX;
			_qYG = p_qYG;
			_hWLB = p_hWLB;
			_mINPRICE = p_mINPRICE;
			_dEFGPRICE = p_dEFGPRICE;
			_wEIGHTXY45 = p_wEIGHTXY45;
			_wEIGHTDY45 = p_wEIGHTDY45;
			_wEIGHTDY100 = p_wEIGHTDY100;
			_wEIGHTDY500 = p_wEIGHTDY500;
			_wEIGHTDY1000 = p_wEIGHTDY1000;
			_hKGS = p_hKGS;
			_hBH = p_hBH;
			_sKB = p_sKB;
			_sTARTDATE = p_sTARTDATE;
			_eNDDATE = p_eNDDATE;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
            _m2 = p_m2;
            _bZ = p_bZ;
            _dESCR = p_dESCR;
            _eXT1 = p_eXT1;
            _eXT2 = p_eXT2;
            _eXT3 = p_eXT3;
            _aREACODE = p_aREACODE;
            _qYGCODE = p_qYGCODE;
            _hXCODE = p_hXCODE;
            _hKGSCODE = p_hKGSCODE;
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_MODIFYID, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
            set { _rid = value; }
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_AREA, oldValue, value);
				}
			}
		}

		[Property("HX", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string HX
		{
			get { return _hX; }
			set
			{
				if ((_hX == null) || (value == null) || (!value.Equals(_hX)))
				{
                    object oldValue = _hX;
					_hX = value;
					RaisePropertyChanged(SQM_COST_KYGN.Prop_HX, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_QYG, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_HWLB, oldValue, value);
				}
			}
		}

		[Property("MINPRICE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? MINPRICE
		{
			get { return _mINPRICE; }
			set
			{
				if (value != _mINPRICE)
				{
                    object oldValue = _mINPRICE;
					_mINPRICE = value;
					RaisePropertyChanged(SQM_COST_KYGN.Prop_MINPRICE, oldValue, value);
				}
			}
		}

        [Property("M2", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public System.Decimal? M2
        {
            get { return _m2; }
            set
            {
                if (value != _m2)
                {
                    object oldValue = _m2;
                    _m2 = value;
                    RaisePropertyChanged(SQM_COST_KYGN.Prop_M2, oldValue, value);
                }
            }
        }

        [Property("DEFGPRICE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? DEFGPRICE
		{
			get { return _dEFGPRICE; }
			set
			{
				if (value != _dEFGPRICE)
				{
                    object oldValue = _dEFGPRICE;
					_dEFGPRICE = value;
					RaisePropertyChanged(SQM_COST_KYGN.Prop_DEFGPRICE, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_WEIGHTXY45, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_WEIGHTDY45, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_WEIGHTDY100, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_WEIGHTDY500, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_WEIGHTDY1000, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_HKGS, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_HBH, oldValue, value);
				}
			}
		}

		[Property("SKB", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public string SKB
		{
			get { return _sKB; }  
			set
			{
				if (value != _sKB)
				{
                    object oldValue = _sKB;
					_sKB = value;
					RaisePropertyChanged(SQM_COST_KYGN.Prop_SKB, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_STARTDATE, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_ENDDATE, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_COST_KYGN.Prop_MEMO, oldValue, value);
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
                    RaisePropertyChanged(SQM_COST_KYGN.Prop_BZ, oldValue, value);
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
                    RaisePropertyChanged(SQM_COST_KYGN.Prop_DESCR, oldValue, value);
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
                    RaisePropertyChanged(SQM_COST_KYGN.Prop_EXT1, oldValue, value);
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
                    RaisePropertyChanged(SQM_COST_KYGN.Prop_EXT2, oldValue, value);
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
                    RaisePropertyChanged(SQM_COST_KYGN.Prop_EXT3, oldValue, value);
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
                    RaisePropertyChanged(SQM_COST_KYGN.Prop_AREACODE, oldValue, value);
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
                    RaisePropertyChanged(SQM_COST_KYGN.Prop_QYGCODE, oldValue, value);
                }
            }
        }
        [Property("HXCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
        public string HXCODE
        {
            get { return _hXCODE; }
            set
            {
                if ((_hXCODE == null) || (value == null) || (!value.Equals(_hXCODE)))
                {
                    object oldValue = _hXCODE;
                    _hXCODE = value;
                    RaisePropertyChanged(SQM_COST_KYGN.Prop_HXCODE, oldValue, value);
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
                    RaisePropertyChanged(SQM_COST_KYGN.Prop_HKGSCODE, oldValue, value);
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
                    RaisePropertyChanged(SQM_COST_KYGN.Prop_BZCODE, oldValue, value);
                }
            }
        }
        #endregion
    } // SQM_COST_KYGN
}

