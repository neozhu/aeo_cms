// Business class SQM_BJ_VER generated from SQM_BJ_VER
// Creator: rw
// Created Date: [2018-08-25]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_BJ_VER")]
	public partial class SQM_BJ_VER : EntityBase<SQM_BJ_VER>
	{
		#region Property_Names

		public static string Prop_BPCODE9 = "BPCODE9";
		public static string Prop_CONTRSCTNUM = "CONTRSCTNUM";
		public static string Prop_UPLOADNAME = "UPLOADNAME";
		public static string Prop_UPLOADURL = "UPLOADURL";
		public static string Prop_REQUESTID = "REQUESTID";
		public static string Prop_DF = "DF";
		public static string Prop_WORKFLOW = "WORKFLOW";
		public static string Prop_WFFINISHTIME = "WFFINISHTIME";
        public static string Prop_SHOWMODE = "SHOWMODE";
        public static string Prop_ORGRID = "ORGRID";
		public static string Prop_UPLOADTIME = "UPLOADTIME";
		public static string Prop_DTTO = "DTTO";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYID = "MODIFYID";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_MRID = "MRID";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_DB_KEY = "DB_KEY";
		public static string Prop_ZVER = "ZVER";
		public static string Prop_DTFROM = "DTFROM";
        public static string Prop_FBREASONCODE = "FBRESONCODE";
        public static string Prop_FBREASONNAME = "FBRESONNAME";
        public static string Prop_FBREASONOTHER = "FBREASONOTHER";
        public static string Prop_FBMEMO = "FBMEMO";

        #endregion

        #region Private_Variables

        private string _bPCODE9;
		private string _cONTRSCTNUM;
		private string _uPLOADNAME;
		private string _uPLOADURL;
		private string _rEQUESTID;
		private string _dF;
		private string _wORKFLOW;
		private DateTime? _wFFINISHTIME;
        private string _sHOWMODE;
        private string _oRGRID;
		private DateTime? _uPLOADTIME;
		private DateTime? _dTTO;
		private string _sTATUS;
		private DateTime? _cREATETIME;
		private string _cREATEID;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYID;
		private string _mODIFYUSER;
		private string _rid;
		private string _mRID;
		private string _mEMO;
		private string _dB_KEY;
		private string _zVER;
		private DateTime? _dTFROM;
        private string _fBREASONOTHER;
        private string _fBREASONCODE;
        private string _fBREASONNAME;
        private string _fBMEMO;

        #endregion

        #region Constructors

        public SQM_BJ_VER()
		{
		}

        public SQM_BJ_VER(
            string p_bPCODE9,
            string p_cONTRSCTNUM,
            string p_uPLOADNAME,
            string p_uPLOADURL,
            string p_rEQUESTID,
            string p_dF,
            string p_wORKFLOW,
            DateTime? p_wFFINISHTIME,
            string p_sHOWMODE,
            string p__oRGRID,
            DateTime? p_uPLOADTIME,
            DateTime? p_dTTO,
            string p_sTATUS,
            DateTime? p_cREATETIME,
            string p_cREATEID,
            string p_cREATEUSER,
            DateTime? p_mODIFYTIME,
            string p_mODIFYID,
            string p_mODIFYUSER,
            string p_rid,
            string p_mRID,
            string p_mEMO,
            string p_dB_KEY,
            string p_zVER,
            string p_fBREASONCODE,
            string p_fBREASONNAME,
            string p_fBREASONOTHER,
            string p_fBMEMO,
			DateTime? p_dTFROM)
		{
			_bPCODE9 = p_bPCODE9;
			_cONTRSCTNUM = p_cONTRSCTNUM;
			_uPLOADNAME = p_uPLOADNAME;
			_uPLOADURL = p_uPLOADURL;
			_rEQUESTID = p_rEQUESTID;
			_dF = p_dF;
			_wORKFLOW = p_wORKFLOW;
			_wFFINISHTIME = p_wFFINISHTIME;
            _sHOWMODE = p_sHOWMODE;
            _oRGRID = p_sHOWMODE;
			_uPLOADTIME = p_uPLOADTIME;
			_dTTO = p_dTTO;
			_sTATUS = p_sTATUS;
			_cREATETIME = p_cREATETIME;
			_cREATEID = p_cREATEID;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYID = p_mODIFYID;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_mRID = p_mRID;
			_mEMO = p_mEMO;
			_dB_KEY = p_dB_KEY;
			_zVER = p_zVER;
			_dTFROM = p_dTFROM;
            _fBREASONCODE = p_fBREASONCODE;
            _fBREASONOTHER = p_fBREASONOTHER;
            _fBMEMO = p_fBMEMO;
            _fBREASONNAME = p_fBREASONNAME;
		}

		#endregion

		#region Properties

		[Property("BPCODE9", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string BPCODE9
		{
			get { return _bPCODE9; }
			set
			{
				if ((_bPCODE9 == null) || (value == null) || (!value.Equals(_bPCODE9)))
				{
                    object oldValue = _bPCODE9;
					_bPCODE9 = value;
					RaisePropertyChanged(SQM_BJ_VER.Prop_BPCODE9, oldValue, value);
				}
			}
		}

		[Property("CONTRSCTNUM", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CONTRSCTNUM
		{
			get { return _cONTRSCTNUM; }
			set
			{
				if ((_cONTRSCTNUM == null) || (value == null) || (!value.Equals(_cONTRSCTNUM)))
				{
                    object oldValue = _cONTRSCTNUM;
					_cONTRSCTNUM = value;
					RaisePropertyChanged(SQM_BJ_VER.Prop_CONTRSCTNUM, oldValue, value);
				}
			}
		}

		[Property("UPLOADNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string UPLOADNAME
		{
			get { return _uPLOADNAME; }
			set
			{
				if ((_uPLOADNAME == null) || (value == null) || (!value.Equals(_uPLOADNAME)))
				{
                    object oldValue = _uPLOADNAME;
					_uPLOADNAME = value;
					RaisePropertyChanged(SQM_BJ_VER.Prop_UPLOADNAME, oldValue, value);
				}
			}
		}

		[Property("UPLOADURL", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string UPLOADURL
		{
			get { return _uPLOADURL; }
			set
			{
				if ((_uPLOADURL == null) || (value == null) || (!value.Equals(_uPLOADURL)))
				{
                    object oldValue = _uPLOADURL;
					_uPLOADURL = value;
					RaisePropertyChanged(SQM_BJ_VER.Prop_UPLOADURL, oldValue, value);
				}
			}
		}

		[Property("REQUESTID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string REQUESTID
		{
			get { return _rEQUESTID; }
			set
			{
				if ((_rEQUESTID == null) || (value == null) || (!value.Equals(_rEQUESTID)))
				{
                    object oldValue = _rEQUESTID;
					_rEQUESTID = value;
					RaisePropertyChanged(SQM_BJ_VER.Prop_REQUESTID, oldValue, value);
				}
			}
		}

		[Property("DF", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string DF
		{
			get { return _dF; }
			set
			{
				if ((_dF == null) || (value == null) || (!value.Equals(_dF)))
				{
                    object oldValue = _dF;
					_dF = value;
					RaisePropertyChanged(SQM_BJ_VER.Prop_DF, oldValue, value);
				}
			}
		}

		[Property("WORKFLOW", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string WORKFLOW
		{
			get { return _wORKFLOW; }
			set
			{
				if ((_wORKFLOW == null) || (value == null) || (!value.Equals(_wORKFLOW)))
				{
                    object oldValue = _wORKFLOW;
					_wORKFLOW = value;
					RaisePropertyChanged(SQM_BJ_VER.Prop_WORKFLOW, oldValue, value);
				}
			}
		}

		[Property("WFFINISHTIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? WFFINISHTIME
		{
			get { return _wFFINISHTIME; }
			set
			{
				if (value != _wFFINISHTIME)
				{
                    object oldValue = _wFFINISHTIME;
					_wFFINISHTIME = value;
					RaisePropertyChanged(SQM_BJ_VER.Prop_WFFINISHTIME, oldValue, value);
				}
			}
		}

		[Property("SHOWMODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string SHOWMODE
		{
			get { return _sHOWMODE; }
			set
			{
				if ((_sHOWMODE == null) || (value == null) || (!value.Equals(_sHOWMODE)))
				{
                    object oldValue = _sHOWMODE;
					_sHOWMODE = value;
					RaisePropertyChanged(SQM_BJ_VER.Prop_SHOWMODE, oldValue, value);
				}
			}
		}

        [Property("ORGRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
        public string ORGRID
        {
            get { return _oRGRID; }
            set
            {
                if ((_oRGRID == null) || (value == null) || (!value.Equals(_oRGRID)))
                {
                    object oldValue = _oRGRID;
                    _oRGRID = value;
                    RaisePropertyChanged(SQM_BJ_VER.Prop_ORGRID, oldValue, value);
                }
            }
        }

		[Property("UPLOADTIME", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? UPLOADTIME
		{
			get { return _uPLOADTIME; }
			set
			{
				if (value != _uPLOADTIME)
				{
                    object oldValue = _uPLOADTIME;
					_uPLOADTIME = value;
					RaisePropertyChanged(SQM_BJ_VER.Prop_UPLOADTIME, oldValue, value);
				}
			}
		}

		[Property("DTTO", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? DTTO
		{
			get { return _dTTO; }
			set
			{
				if (value != _dTTO)
				{
                    object oldValue = _dTTO;
					_dTTO = value;
					RaisePropertyChanged(SQM_BJ_VER.Prop_DTTO, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_VER.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_VER.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_VER.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_VER.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_VER.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_VER.Prop_MODIFYID, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_VER.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

        [PrimaryKey(PrimaryKeyType.Assigned, "RID", Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string RID
        {
            set { _rid = value; }
            get { return _rid; }
        }

		[Property("MRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string MRID
		{
			get { return _mRID; }
			set
			{
				if ((_mRID == null) || (value == null) || (!value.Equals(_mRID)))
				{
                    object oldValue = _mRID;
					_mRID = value;
					RaisePropertyChanged(SQM_BJ_VER.Prop_MRID, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_VER.Prop_MEMO, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_VER.Prop_DB_KEY, oldValue, value);
				}
			}
		}

		[Property("ZVER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string ZVER
		{
			get { return _zVER; }
			set
			{
				if ((_zVER == null) || (value == null) || (!value.Equals(_zVER)))
				{
                    object oldValue = _zVER;
					_zVER = value;
					RaisePropertyChanged(SQM_BJ_VER.Prop_ZVER, oldValue, value);
				}
			}
		}

		[Property("DTFROM", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? DTFROM
		{
			get { return _dTFROM; }
			set
			{
				if (value != _dTFROM)
				{
                    object oldValue = _dTFROM;
					_dTFROM = value;
					RaisePropertyChanged(SQM_BJ_VER.Prop_DTFROM, oldValue, value);
				}
			}
		}

        [Property("FBREASONCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
        public string FBREASONCODE
        {
            get { return _fBREASONCODE; }
            set
            {
                if ((_fBREASONCODE == null) || (value == null) || (!value.Equals(_fBREASONCODE)))
                {
                    object oldValue = _fBREASONCODE;
                    _fBREASONCODE = value;
                    RaisePropertyChanged(SQM_BJ_VER.Prop_FBREASONCODE, oldValue, value);
                }
            }
        }

        [Property("FBREASONNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
        public string FBREASONNAME
        {
            get { return _fBREASONNAME; }
            set
            {
                if ((_fBREASONNAME == null) || (value == null) || (!value.Equals(_fBREASONNAME)))
                {
                    object oldValue = _fBREASONNAME;
                    _fBREASONNAME = value;
                    RaisePropertyChanged(SQM_BJ_VER.Prop_FBREASONNAME, oldValue, value);
                }
            }
        }

        [Property("FBREASONOTHER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
        public string FBREASONOTHER
        {
            get { return _fBREASONOTHER; }
            set
            {
                if ((_fBREASONOTHER == null) || (value == null) || (!value.Equals(_fBREASONOTHER)))
                {
                    object oldValue = _fBREASONOTHER;
                    _fBREASONOTHER = value;
                    RaisePropertyChanged(SQM_BJ_VER.Prop_FBREASONOTHER, oldValue, value);
                }
            }
        }

        [Property("FBMEMO", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 500)]
        public string FBMEMO
        {
            get { return _fBMEMO; }
            set
            {
                if ((_fBMEMO == null) || (value == null) || (!value.Equals(_fBMEMO)))
                {
                    object oldValue = _fBMEMO;
                    _fBMEMO = value;
                    RaisePropertyChanged(SQM_BJ_VER.Prop_FBMEMO, oldValue, value);
                }
            }
        }

        #endregion
    } // SQM_BJ_VER
}

