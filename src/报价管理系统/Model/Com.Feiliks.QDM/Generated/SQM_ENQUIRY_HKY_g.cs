// Business class SQM_ENQUIRY_HKY generated from SQM_ENQUIRY_HKY
// Creator: rw
// Created Date: [2018_07_13]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.QDM.Model
{
	[ActiveRecord("SQM_ENQUIRY_HKY")]
	public partial class SQM_ENQUIRY_HKY : EntityBase<SQM_ENQUIRY_HKY>
	{
		#region Property_Names

		public static string Prop_SHIPPER = "SHIPPER";
		public static string Prop_CONSIGNEE = "CONSIGNEE";
		public static string Prop_TUOCHE_YSLX = "TUOCHE_YSLX";
		public static string Prop_TUOCHE_QSD = "TUOCHE_QSD";
		public static string Prop_TUOCHE_MDD = "TUOCHE_MDD";
		public static string Prop_TUOCHE_TGFS = "TUOCHE_TGFS";
		public static string Prop_TUOCHE_HL = "TUOCHE_HL";
		public static string Prop_TUOCHE_XXXL = "TUOCHE_XXXL";
		public static string Prop_TUOCHE_QT = "TUOCHE_QT";
		public static string Prop_HWYQ = "HWYQ";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYID = "MODIFYID";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_QYG = "QYG";
		public static string Prop_MDG = "MDG";
		public static string Prop_PRODUCT = "PRODUCT";
		public static string Prop_SPECIFICATION = "SPECIFICATION";
		public static string Prop_GROSSWEIGHT = "GROSSWEIGHT";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_ENQUIRYUSER = "ENQUIRYUSER";
		public static string Prop_PRDNAME = "PRDNAME";
		public static string Prop_SRVNAME = "SRVNAME";
		public static string Prop_HWLB = "HWLB";
		public static string Prop_DZYJ = "DZYJ";
		public static string Prop_SPONSEDATE = "SPONSEDATE";
		public static string Prop_CQDATE = "CQDATE";
		public static string Prop_JGDATE = "JGDATE";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_MYFS = "MYFS";
		public static string Prop_XQLX = "XQLX";
		public static string Prop_YWLX = "YWLX";
		public static string Prop_XJLX = "XJLX";
		public static string Prop_HL = "HL";
		public static string Prop_YSFS = "YSFS";
        public static string Prop_YFFS = "YFFS";
        public static string Prop_TYPE = "TYPE";
        public static string Prop_DJZBRID = "DJZBRID";
        public static string Prop_FEECODE = "FEECODE";
        public static string Prop_FEENAME = "FEENAME";
        public static string Prop_PRDCODE = "PRDCODE";
        public static string Prop_SRVCODE = "SRVCODE";
        public static string Prop_DJFSRID = "DJFSRID";
        public static string Prop_GDZRID = "GDZRID";
        public static string Prop_DJRID = "DJRID";
        public static string Prop_SBLX = "SBLX";
        public static string Prop_SBLXCODE = "SBLXCODE";
        public static string Prop_ORGCODE = "ORGCODE";
        public static string Prop_ORGNAME = "ORGNAME";
        public static string Prop_SERIALNUMBER = "SERIALNUMBER";

        #endregion

        #region Private_Variables

        private string _sHIPPER;
		private string _cONSIGNEE;
		private string _tUOCHE_YSLX;
		private string _tUOCHE_QSD;
		private string _tUOCHE_MDD;
		private string _tUOCHE_TGFS;
		private System.Decimal? _tUOCHE_HL;
		private System.Decimal? _tUOCHE_XXXL;
		private string _tUOCHE_QT;
		private string _hWYQ;
		private DateTime? _cREATETIME;
		private string _cREATEID;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYID;
		private string _mODIFYUSER;
		private string _rid;
		private string _qYG;
		private string _mDG;
		private string _pRODUCT;
		private string _sPECIFICATION;
		private System.Decimal? _gROSSWEIGHT;
		private string _sTATUS;
		private string _eNQUIRYUSER;
		private string _pRDNAME;
		private string _sRVNAME;
		private string _hWLB;
		private string _dZYJ;
		private DateTime? _sPONSEDATE;
		private string _cQDATE;
		private DateTime? _jGDATE;
		private string _mEMO;
		private string _mYFS;
		private string _xQLX;
		private string _yWLX;
		private string _xJLX;
		private System.Decimal? _hL;
		private string _ySFS;
        private string _yFFS;
        private string _tYPE;
        private string _dJZBRID;
        private string _fEECODE;
        private string _fEENAME;
        private string _pRDCODE;
        private string _sRVCODE;
        private string _dJFSRID;
        private string _gDZRID;
        private string _dJRID;
        private string _sBLX;
        private string _sBLXCODE;
        private string _oRGCODE;
        private string _oRGNAME;
        private string _sERIALNUMBER;
        #endregion

        #region Constructors

        public SQM_ENQUIRY_HKY()
		{
		}

		public SQM_ENQUIRY_HKY(
			string p_sHIPPER,
			string p_cONSIGNEE,
			string p_tUOCHE_YSLX,
			string p_tUOCHE_QSD,
			string p_tUOCHE_MDD,
			string p_tUOCHE_TGFS,
			System.Decimal? p_tUOCHE_HL,
			System.Decimal? p_tUOCHE_XXXL,
			string p_tUOCHE_QT,
			string p_hWYQ,
			DateTime? p_cREATETIME,
			string p_cREATEID,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYID,
			string p_mODIFYUSER,
			string p_rID,
			string p_qYG,
			string p_mDG,
			string p_pRODUCT,
			string p_sPECIFICATION,
			System.Decimal? p_gROSSWEIGHT,
			string p_sTATUS,
			string p_eNQUIRYUSER,
			string p_pRDNAME,
			string p_sRVNAME,
			string p_hWLB,
			string p_dZYJ,
			DateTime? p_sPONSEDATE,
            string p_cQDATE,
			DateTime? p_jGDATE,
			string p_mEMO,
			string p_mYFS,
			string p_xQLX,
			string p_yWLX,
			string p_xJLX,
			System.Decimal? p_hL,
			string p_ySFS,
            string p_yFFS,
            string p_tYPE,
            string p_dJZBRID,
            string p_fEECODE,
            string p_fEENAME,
            string p_pRDCODE,
            string p_sRVCODE,
            string p_dJFSRID,
            string p_gDZRID,
            string p_dJRID,
            string p_sBLX,
            string p_sBLXCODE,
            string p_oRGCODE,
            string p_oRGNAME,
            string p_sERIALNUMBER)
		{
			_sHIPPER = p_sHIPPER;
			_cONSIGNEE = p_cONSIGNEE;
			_tUOCHE_YSLX = p_tUOCHE_YSLX;
			_tUOCHE_QSD = p_tUOCHE_QSD;
			_tUOCHE_MDD = p_tUOCHE_MDD;
			_tUOCHE_TGFS = p_tUOCHE_TGFS;
			_tUOCHE_HL = p_tUOCHE_HL;
			_tUOCHE_XXXL = p_tUOCHE_XXXL;
			_tUOCHE_QT = p_tUOCHE_QT;
			_hWYQ = p_hWYQ;
			_cREATETIME = p_cREATETIME;
			_cREATEID = p_cREATEID;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYID = p_mODIFYID;
			_mODIFYUSER = p_mODIFYUSER;
            _rid = p_rID;
			_qYG = p_qYG;
			_mDG = p_mDG;
			_pRODUCT = p_pRODUCT;
			_sPECIFICATION = p_sPECIFICATION;
			_gROSSWEIGHT = p_gROSSWEIGHT;
			_sTATUS = p_sTATUS;
			_eNQUIRYUSER = p_eNQUIRYUSER;
			_pRDNAME = p_pRDNAME;
			_sRVNAME = p_sRVNAME;
			_hWLB = p_hWLB;
			_dZYJ = p_dZYJ;
			_sPONSEDATE = p_sPONSEDATE;
			_cQDATE = p_cQDATE;
			_jGDATE = p_jGDATE;
			_mEMO = p_mEMO;
			_mYFS = p_mYFS;
            _xQLX = p_xQLX;
			_yWLX = p_yWLX;
			_xJLX = p_xJLX;
			_hL = p_hL;
			_ySFS = p_ySFS;
            _yFFS = p_yFFS;
            _tYPE = p_tYPE;
            _dJZBRID = p_dJZBRID;
            _fEECODE = p_fEECODE;
            _fEENAME = p_fEENAME;
            _pRDCODE = p_pRDCODE;
            _sRVCODE = p_sRVCODE;
            _dJFSRID = p_dJFSRID;
            _gDZRID = p_gDZRID;
            _dJRID = p_dJRID;
            _sBLX = p_sBLX;
            _sBLXCODE = p_sBLXCODE;
            _oRGCODE = p_oRGCODE;
            _oRGNAME = p_oRGNAME;
            _sERIALNUMBER = p_sERIALNUMBER;
        }

		#endregion

		#region Properties

		[Property("SHIPPER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string SHIPPER
		{
			get { return _sHIPPER; }
			set
			{
				if ((_sHIPPER == null) || (value == null) || (!value.Equals(_sHIPPER)))
				{
                    object oldValue = _sHIPPER;
					_sHIPPER = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_SHIPPER, oldValue, value);
				}
			}
		}

		[Property("CONSIGNEE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CONSIGNEE
		{
			get { return _cONSIGNEE; }
			set
			{
				if ((_cONSIGNEE == null) || (value == null) || (!value.Equals(_cONSIGNEE)))
				{
                    object oldValue = _cONSIGNEE;
					_cONSIGNEE = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_CONSIGNEE, oldValue, value);
				}
			}
		}

		[Property("TUOCHE_YSLX", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string TUOCHE_YSLX
		{
			get { return _tUOCHE_YSLX; }
			set
			{
				if ((_tUOCHE_YSLX == null) || (value == null) || (!value.Equals(_tUOCHE_YSLX)))
				{
                    object oldValue = _tUOCHE_YSLX;
					_tUOCHE_YSLX = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_TUOCHE_YSLX, oldValue, value);
				}
			}
		}

		[Property("TUOCHE_QSD", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string TUOCHE_QSD
		{
			get { return _tUOCHE_QSD; }
			set
			{
				if ((_tUOCHE_QSD == null) || (value == null) || (!value.Equals(_tUOCHE_QSD)))
				{
                    object oldValue = _tUOCHE_QSD;
					_tUOCHE_QSD = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_TUOCHE_QSD, oldValue, value);
				}
			}
		}

		[Property("TUOCHE_MDD", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string TUOCHE_MDD
		{
			get { return _tUOCHE_MDD; }
			set
			{
				if ((_tUOCHE_MDD == null) || (value == null) || (!value.Equals(_tUOCHE_MDD)))
				{
                    object oldValue = _tUOCHE_MDD;
					_tUOCHE_MDD = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_TUOCHE_MDD, oldValue, value);
				}
			}
		}

		[Property("TUOCHE_TGFS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string TUOCHE_TGFS
		{
			get { return _tUOCHE_TGFS; }
			set
			{
				if ((_tUOCHE_TGFS == null) || (value == null) || (!value.Equals(_tUOCHE_TGFS)))
				{
                    object oldValue = _tUOCHE_TGFS;
					_tUOCHE_TGFS = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_TUOCHE_TGFS, oldValue, value);
				}
			}
		}

		[Property("TUOCHE_HL", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? TUOCHE_HL
		{
			get { return _tUOCHE_HL; }
			set
			{
				if (value != _tUOCHE_HL)
				{
                    object oldValue = _tUOCHE_HL;
					_tUOCHE_HL = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_TUOCHE_HL, oldValue, value);
				}
			}
		}

		[Property("TUOCHE_XXXL", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? TUOCHE_XXXL
		{
			get { return _tUOCHE_XXXL; }
			set
			{
				if (value != _tUOCHE_XXXL)
				{
                    object oldValue = _tUOCHE_XXXL;
					_tUOCHE_XXXL = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_TUOCHE_XXXL, oldValue, value);
				}
			}
		}

		[Property("TUOCHE_QT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string TUOCHE_QT
		{
			get { return _tUOCHE_QT; }
			set
			{
				if ((_tUOCHE_QT == null) || (value == null) || (!value.Equals(_tUOCHE_QT)))
				{
                    object oldValue = _tUOCHE_QT;
					_tUOCHE_QT = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_TUOCHE_QT, oldValue, value);
				}
			}
		}

		[Property("HWYQ", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string HWYQ
		{
			get { return _hWYQ; }
			set
			{
				if ((_hWYQ == null) || (value == null) || (!value.Equals(_hWYQ)))
				{
                    object oldValue = _hWYQ;
					_hWYQ = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_HWYQ, oldValue, value);
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
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_MODIFYID, oldValue, value);
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
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

        [PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string RID
		{
			get { return _rid; }
			set
			{
				if ((_rid == null) || (value == null) || (!value.Equals(_rid)))
				{
                    object oldValue = _rid;
                    _rid = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_RID, oldValue, value);
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
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_QYG, oldValue, value);
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
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_MDG, oldValue, value);
				}
			}
		}

		[Property("PRODUCT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string PRODUCT
		{
			get { return _pRODUCT; }
			set
			{
				if ((_pRODUCT == null) || (value == null) || (!value.Equals(_pRODUCT)))
				{
                    object oldValue = _pRODUCT;
					_pRODUCT = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_PRODUCT, oldValue, value);
				}
			}
		}

		[Property("SPECIFICATION", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string SPECIFICATION
		{
			get { return _sPECIFICATION; }
			set
			{
				if ((_sPECIFICATION == null) || (value == null) || (!value.Equals(_sPECIFICATION)))
				{
                    object oldValue = _sPECIFICATION;
					_sPECIFICATION = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_SPECIFICATION, oldValue, value);
				}
			}
		}

		[Property("GROSSWEIGHT", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? GROSSWEIGHT
		{
			get { return _gROSSWEIGHT; }
			set
			{
				if (value != _gROSSWEIGHT)
				{
                    object oldValue = _gROSSWEIGHT;
					_gROSSWEIGHT = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_GROSSWEIGHT, oldValue, value);
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
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_STATUS, oldValue, value);
				}
			}
		}

		[Property("ENQUIRYUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string ENQUIRYUSER
		{
			get { return _eNQUIRYUSER; }
			set
			{
				if ((_eNQUIRYUSER == null) || (value == null) || (!value.Equals(_eNQUIRYUSER)))
				{
                    object oldValue = _eNQUIRYUSER;
					_eNQUIRYUSER = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_ENQUIRYUSER, oldValue, value);
				}
			}
		}

		[Property("PRDNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string PRDNAME
		{
			get { return _pRDNAME; }
			set
			{
				if ((_pRDNAME == null) || (value == null) || (!value.Equals(_pRDNAME)))
				{
                    object oldValue = _pRDNAME;
					_pRDNAME = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_PRDNAME, oldValue, value);
				}
			}
		}

		[Property("SRVNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string SRVNAME
		{
			get { return _sRVNAME; }
			set
			{
				if ((_sRVNAME == null) || (value == null) || (!value.Equals(_sRVNAME)))
				{
                    object oldValue = _sRVNAME;
					_sRVNAME = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_SRVNAME, oldValue, value);
				}
			}
		}

		[Property("HWLB", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string HWLB
		{
			get { return _hWLB; }
			set
			{
				if ((_hWLB == null) || (value == null) || (!value.Equals(_hWLB)))
				{
                    object oldValue = _hWLB;
					_hWLB = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_HWLB, oldValue, value);
				}
			}
		}

		[Property("DZYJ", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string DZYJ
		{
			get { return _dZYJ; }
			set
			{
				if ((_dZYJ == null) || (value == null) || (!value.Equals(_dZYJ)))
				{
                    object oldValue = _dZYJ;
					_dZYJ = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_DZYJ, oldValue, value);
				}
			}
		}

		[Property("SPONSEDATE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? SPONSEDATE
		{
			get { return _sPONSEDATE; }
			set
			{
				if (value != _sPONSEDATE)
				{
                    object oldValue = _sPONSEDATE;
					_sPONSEDATE = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_SPONSEDATE, oldValue, value);
				}
			}
		}

		[Property("CQDATE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CQDATE
		{
			get { return _cQDATE; }
			set
			{
				if (value != _cQDATE)
				{
                    object oldValue = _cQDATE;
					_cQDATE = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_CQDATE, oldValue, value);
				}
			}
		}

		[Property("JGDATE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? JGDATE
		{
			get { return _jGDATE; }
			set
			{
				if (value != _jGDATE)
				{
                    object oldValue = _jGDATE;
					_jGDATE = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_JGDATE, oldValue, value);
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
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_MEMO, oldValue, value);
				}
			}
		}

		[Property("MYFS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MYFS
		{
			get { return _mYFS; }
			set
			{
				if ((_mYFS == null) || (value == null) || (!value.Equals(_mYFS)))
				{
                    object oldValue = _mYFS;
					_mYFS = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_MYFS, oldValue, value);
				}
			}
		}

		[Property("XQLX", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string XQLX
        {
			get { return _xQLX; }
			set
			{
				if ((_xQLX == null) || (value == null) || (!value.Equals(_xQLX)))
				{
                    object oldValue = _xQLX;
                    _xQLX = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_XQLX, oldValue, value);
				}
			}
		}

		[Property("YWLX", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string YWLX
		{
			get { return _yWLX; }
			set
			{
				if ((_yWLX == null) || (value == null) || (!value.Equals(_yWLX)))
				{
                    object oldValue = _yWLX;
					_yWLX = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_YWLX, oldValue, value);
				}
			}
		}

		[Property("XJLX", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string XJLX
		{
			get { return _xJLX; }
			set
			{
				if ((_xJLX == null) || (value == null) || (!value.Equals(_xJLX)))
				{
                    object oldValue = _xJLX;
					_xJLX = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_XJLX, oldValue, value);
				}
			}
		}

		[Property("HL", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? HL
		{
			get { return _hL; }
			set
			{
				if (value != _hL)
				{
                    object oldValue = _hL;
					_hL = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_HL, oldValue, value);
				}
			}
		}

		[Property("YSFS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string YSFS
		{
			get { return _ySFS; }
			set
			{
				if ((_ySFS == null) || (value == null) || (!value.Equals(_ySFS)))
				{
                    object oldValue = _ySFS;
					_ySFS = value;
					RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_YSFS, oldValue, value);
				}
			}
		}
        [Property("YFFS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string YFFS
        {
            get { return _yFFS; }
            set
            {
                if ((_yFFS == null) || (value == null) || (!value.Equals(_yFFS)))
                {
                    object oldValue = _yFFS;
                    _yFFS = value;
                    RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_YFFS, oldValue, value);
                }
            }
        }
        [Property("TYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
        public string TYPE
        {
            get { return _tYPE; }
            set
            {
                if ((_tYPE == null) || (value == null) || (!value.Equals(_tYPE)))
                {
                    object oldValue = _tYPE;
                    _tYPE = value;
                    RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_TYPE, oldValue, value);
                }
            }
        }
        [Property("DJZBRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string DJZBRID
        {
            get { return _dJZBRID; }
            set
            {
                if ((_dJZBRID == null) || (value == null) || (!value.Equals(_dJZBRID)))
                {
                    object oldValue = _dJZBRID;
                    _dJZBRID = value;
                    RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_DJZBRID, oldValue, value);
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
                    RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_FEECODE, oldValue, value);
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
                    RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_FEENAME, oldValue, value);
                }
            }
        }
        [Property("PRDCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string PRDCODE
        {
            get { return _pRDCODE; }
            set
            {
                if ((_pRDCODE == null) || (value == null) || (!value.Equals(_pRDCODE)))
                {
                    object oldValue = _pRDCODE;
                    _pRDCODE = value;
                    RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_PRDCODE, oldValue, value);
                }
            }
        }
        [Property("SRVCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string SRVCODE
        {
            get { return _sRVCODE; }
            set
            {
                if ((_sRVCODE == null) || (value == null) || (!value.Equals(_sRVCODE)))
                {
                    object oldValue = _sRVCODE;
                    _sRVCODE = value;
                    RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_SRVCODE, oldValue, value);
                }
            }
        }
        [Property("DJFSRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string DJFSRID
        {
            get { return _dJFSRID; }
            set
            {
                if ((_dJFSRID == null) || (value == null) || (!value.Equals(_dJFSRID)))
                {
                    object oldValue = _dJFSRID;
                    _dJFSRID = value;
                    RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_DJFSRID, oldValue, value);
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
                    RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_GDZRID, oldValue, value);
                }
            }
        }
        [Property("DJRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string DJRID
        {
            get { return _dJRID; }
            set
            {
                if ((_dJRID == null) || (value == null) || (!value.Equals(_dJRID)))
                {
                    object oldValue = _dJRID;
                    _dJRID = value;
                    RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_DJRID, oldValue, value);
                }
            }
        }
        [Property("SBLX", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string SBLX
        {
            get { return _sBLX; }
            set
            {
                if ((_sBLX == null) || (value == null) || (!value.Equals(_sBLX)))
                {
                    object oldValue = _sBLX;
                    _sBLX = value;
                    RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_SBLX, oldValue, value);
                }
            }
        }
        [Property("SBLXCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string SBLXCODE
        {
            get { return _sBLXCODE; }
            set
            {
                if ((_sBLXCODE == null) || (value == null) || (!value.Equals(_sBLXCODE)))
                {
                    object oldValue = _sBLXCODE;
                    _sBLXCODE = value;
                    RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_SBLXCODE, oldValue, value);
                }
            }
        }
        [Property("ORGCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string ORGCODE
        {
            get { return _oRGCODE; }
            set
            {
                if ((_oRGCODE == null) || (value == null) || (!value.Equals(_oRGCODE)))
                {
                    object oldValue = _oRGCODE;
                    _oRGCODE = value;
                    RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_ORGCODE, oldValue, value);
                }
            }
        }
        [Property("ORGNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string ORGNAME
        {
            get { return _oRGNAME; }
            set
            {
                if ((_oRGNAME == null) || (value == null) || (!value.Equals(_oRGNAME)))
                {
                    object oldValue = _oRGNAME;
                    _oRGNAME = value;
                    RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_ORGNAME, oldValue, value);
                }
            }
        }
        [Property("SERIALNUMBER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string SERIALNUMBER
        {
            get { return _sERIALNUMBER; }
            set
            {
                if ((_sERIALNUMBER == null) || (value == null) || (!value.Equals(_sERIALNUMBER)))
                {
                    object oldValue = _sERIALNUMBER;
                    _sERIALNUMBER = value;
                    RaisePropertyChanged(SQM_ENQUIRY_HKY.Prop_SERIALNUMBER, oldValue, value);
                }
            }
        }
        #endregion
    } // SQM_ENQUIRY_HKY
}

