// Business class SQM_DJ_PSF generated from SQM_DJ_PSF
// Creator: rw
// Created Date: [2018-08-31]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_DJ_PSF")]
	public partial class SQM_DJ_PSF : EntityBase<SQM_DJ_PSF>
	{
		#region Property_Names

        public static string Prop_IFCOST = "IFCOST";
		public static string Prop_IFDPDX = "IFDPDX";
		public static string Prop_ALONEFEE = "ALONEFEE";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYID = "MODIFYID";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_DB_KEY = "DB_KEY";
		public static string Prop_PRDNAME = "PRDNAME";
		public static string Prop_PRDCODE = "PRDCODE";
		public static string Prop_SRVNAME = "SRVNAME";
		public static string Prop_SRVCODE = "SRVCODE";
        public static string Prop_FEENAME = "FEENAME";
        public static string Prop_ORGRID = "ORGRID";
		public static string Prop_FEECODE = "FEECODE";
        public static string Prop_DJFS = "DJFS";
        public static string Prop_CREATESOURCE = "CREATESOURCE";
		public static string Prop_ORGNAME = "ORGNAME";
		public static string Prop_ORGCODE = "ORGCODE";
		public static string Prop_BUSINESSORG = "BUSINESSORG";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATEUSER = "CREATEUSER";

		#endregion

		#region Private_Variables
       
        private string _iFCOST;
		private string _iFDPDX;
		private string _aLONEFEE;
		private DateTime? _mODIFYTIME;
		private string _mODIFYID;
		private string _mODIFYUSER;
		private string _rid;
		private string _mEMO;
		private string _dB_KEY;
		private string _pRDNAME;
		private string _pRDCODE;
		private string _sRVNAME;
		private string _sRVCODE;
        private string _fEENAME;
        private string _oRGRID;
		private string _fEECODE;
        private string _dJFS;
        private string _cREATESOURCE;
		private string _oRGNAME;
		private string _oRGCODE;
		private string _bUSINESSORG;
		private string _sTATUS;
		private DateTime? _cREATETIME;
		private string _cREATEID;
		private string _cREATEUSER;


		#endregion

		#region Constructors

		public SQM_DJ_PSF()
		{
		}

		public SQM_DJ_PSF(
            string p_iFCOST,
			string p_iFDPDX,
			string p_aLONEFEE,
			DateTime? p_mODIFYTIME,
			string p_mODIFYID,
			string p_mODIFYUSER,
			string p_rid,
			string p_mEMO,
			string p_dB_KEY,
			string p_pRDNAME,
			string p_pRDCODE,
			string p_sRVNAME,
			string p_sRVCODE,
            string p_fEENAME,
            string p_oRGRID,
			string p_fEECODE,
            string p_dJFS,
            string p_cREATESOURCE,
			string p_oRGNAME,
			string p_oRGCODE,
			string p_bUSINESSORG,
			string p_sTATUS,
			DateTime? p_cREATETIME,
			string p_cREATEID,
			string p_cREATEUSER)
		{
            _iFCOST = p_iFCOST;
			_iFDPDX = p_iFDPDX;
			_aLONEFEE = p_aLONEFEE;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYID = p_mODIFYID;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_mEMO = p_mEMO;
			_dB_KEY = p_dB_KEY;
			_pRDNAME = p_pRDNAME;
			_pRDCODE = p_pRDCODE;
			_sRVNAME = p_sRVNAME;
			_sRVCODE = p_sRVCODE;
            _fEENAME = p_fEENAME;
            _oRGRID = p_oRGRID;
			_fEECODE = p_fEECODE;
            _dJFS = p_dJFS;
            _cREATESOURCE = p_cREATESOURCE;
			_oRGNAME = p_oRGNAME;
			_oRGCODE = p_oRGCODE;
			_bUSINESSORG = p_bUSINESSORG;
			_sTATUS = p_sTATUS;
			_cREATETIME = p_cREATETIME;
			_cREATEID = p_cREATEID;
			_cREATEUSER = p_cREATEUSER;

		}

		#endregion

		#region Properties

        [Property("IFCOST", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 10)]
        public string IFCOST
        {
            get { return _iFCOST; }
            set
            {
                if ((_iFCOST == null) || (value == null) || (!value.Equals(_iFCOST)))
                {
                    object oldValue = _iFCOST;
                    _iFCOST = value;
                    RaisePropertyChanged(SQM_DJ_PSF.Prop_IFCOST, oldValue, value);
                }
            }
        }

		[Property("IFDPDX", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string IFDPDX
		{
			get { return _iFDPDX; }
			set
			{
				if ((_iFDPDX == null) || (value == null) || (!value.Equals(_iFDPDX)))
				{
                    object oldValue = _iFDPDX;
					_iFDPDX = value;
					RaisePropertyChanged(SQM_DJ_PSF.Prop_IFDPDX, oldValue, value);
				}
			}
		}

		[Property("ALONEFEE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string ALONEFEE
		{
			get { return _aLONEFEE; }
			set
			{
				if ((_aLONEFEE == null) || (value == null) || (!value.Equals(_aLONEFEE)))
				{
                    object oldValue = _aLONEFEE;
					_aLONEFEE = value;
					RaisePropertyChanged(SQM_DJ_PSF.Prop_ALONEFEE, oldValue, value);
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
					RaisePropertyChanged(SQM_DJ_PSF.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SQM_DJ_PSF.Prop_MODIFYID, oldValue, value);
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
					RaisePropertyChanged(SQM_DJ_PSF.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

        [PrimaryKey(PrimaryKeyType.Assigned, "RID", Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string RID
        {
            set { _rid = value; }
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
					RaisePropertyChanged(SQM_DJ_PSF.Prop_MEMO, oldValue, value);
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
					RaisePropertyChanged(SQM_DJ_PSF.Prop_DB_KEY, oldValue, value);
				}
			}
		}

		[Property("PRDNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string PRDNAME
		{
			get { return _pRDNAME; }
			set
			{
				if ((_pRDNAME == null) || (value == null) || (!value.Equals(_pRDNAME)))
				{
                    object oldValue = _pRDNAME;
					_pRDNAME = value;
					RaisePropertyChanged(SQM_DJ_PSF.Prop_PRDNAME, oldValue, value);
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
					RaisePropertyChanged(SQM_DJ_PSF.Prop_PRDCODE, oldValue, value);
				}
			}
		}

		[Property("SRVNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string SRVNAME
		{
			get { return _sRVNAME; }
			set
			{
				if ((_sRVNAME == null) || (value == null) || (!value.Equals(_sRVNAME)))
				{
                    object oldValue = _sRVNAME;
					_sRVNAME = value;
					RaisePropertyChanged(SQM_DJ_PSF.Prop_SRVNAME, oldValue, value);
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
					RaisePropertyChanged(SQM_DJ_PSF.Prop_SRVCODE, oldValue, value);
				}
			}
		}

		[Property("FEENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string FEENAME
		{
			get { return _fEENAME; }
			set
			{
				if ((_fEENAME == null) || (value == null) || (!value.Equals(_fEENAME)))
				{
                    object oldValue = _fEENAME;
					_fEENAME = value;
					RaisePropertyChanged(SQM_DJ_PSF.Prop_FEENAME, oldValue, value);
				}
			}
		}

        [Property("ORGRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
        public string ORGRID
        {
            get { return _oRGRID; }
            set
            {
                if ((_oRGRID == null) || (value == null) || (!value.Equals(_oRGRID)))
                {
                    object oldValue = _oRGRID;
                    _oRGRID = value;
                    RaisePropertyChanged(SQM_DJ_PSF.Prop_ORGRID, oldValue, value);
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
					RaisePropertyChanged(SQM_DJ_PSF.Prop_FEECODE, oldValue, value);
				}
			}
		}

		[Property("DJFS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string DJFS
		{
			get { return _dJFS; }
			set
			{
				if ((_dJFS == null) || (value == null) || (!value.Equals(_dJFS)))
				{
                    object oldValue = _dJFS;
					_dJFS = value;
					RaisePropertyChanged(SQM_DJ_PSF.Prop_DJFS, oldValue, value);
				}
			}
		}

        [Property("CREATESOURCE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
        public string CREATESOURCE
        {
            get { return _cREATESOURCE; }
            set
            {
                if ((_cREATESOURCE == null) || (value == null) || (!value.Equals(_cREATESOURCE)))
                {
                    object oldValue = _cREATESOURCE;
                    _cREATESOURCE = value;
                    RaisePropertyChanged(SQM_DJ_PSF.Prop_CREATESOURCE, oldValue, value);
                }
            }
        }

		[Property("ORGNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string ORGNAME
		{
			get { return _oRGNAME; }
			set
			{
				if ((_oRGNAME == null) || (value == null) || (!value.Equals(_oRGNAME)))
				{
                    object oldValue = _oRGNAME;
					_oRGNAME = value;
					RaisePropertyChanged(SQM_DJ_PSF.Prop_ORGNAME, oldValue, value);
				}
			}
		}

		[Property("ORGCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string ORGCODE
		{
			get { return _oRGCODE; }
			set
			{
				if ((_oRGCODE == null) || (value == null) || (!value.Equals(_oRGCODE)))
				{
                    object oldValue = _oRGCODE;
					_oRGCODE = value;
					RaisePropertyChanged(SQM_DJ_PSF.Prop_ORGCODE, oldValue, value);
				}
			}
		}

		[Property("BUSINESSORG", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string BUSINESSORG
		{
			get { return _bUSINESSORG; }
			set
			{
				if ((_bUSINESSORG == null) || (value == null) || (!value.Equals(_bUSINESSORG)))
				{
                    object oldValue = _bUSINESSORG;
					_bUSINESSORG = value;
					RaisePropertyChanged(SQM_DJ_PSF.Prop_BUSINESSORG, oldValue, value);
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
					RaisePropertyChanged(SQM_DJ_PSF.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_DJ_PSF.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SQM_DJ_PSF.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SQM_DJ_PSF.Prop_CREATEUSER, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_DJ_PSF
}

