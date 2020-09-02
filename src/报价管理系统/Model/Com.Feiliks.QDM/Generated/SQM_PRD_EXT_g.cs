// Business class SQM_PRD_EXT generated from SQM_PRD_EXT
// Creator: rw
// Created Date: [2018-04-17]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_PRD_EXT")]
	public partial class SQM_PRD_EXT : EntityBase<SQM_PRD_EXT>
	{
		#region Property_Names

		public static string Prop_MEMO = "MEMO";
		public static string Prop_BUSINESSTYPE = "BUSINESSTYPE";
		public static string Prop_BUSINESSORG = "BUSINESSORG";
		public static string Prop_PRODUCTMANAGERID = "PRODUCTMANAGERID";
		public static string Prop_PRODUCTMANAGERNAME = "PRODUCTMANAGERNAME";
		public static string Prop_DEAGIRATE = "DEAGIRATE";
		public static string Prop_SORD = "SORD";
		public static string Prop_PRODUCTKEY = "PRODUCTKEY";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYID = "MODIFYID";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
        public static string Prop_SQPRODUCTNAME = "SQPRODUCTNAME";

        #endregion

        #region Private_Variables

        private string _mEMO;
		private string _bUSINESSTYPE;
		private string _bUSINESSORG;
		private string _pRODUCTMANAGERID;
		private string _pRODUCTMANAGERNAME;
		private System.Decimal? _dEAGIRATE;
		private System.Decimal? _sORD;
		private string _productkey;
		private string _sTATUS;
		private DateTime? _cREATETIME;
		private string _cREATEID;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYID;
		private string _mODIFYUSER;
		private string _rID;
        public string _sQPRODUCTNAME;


        #endregion

        #region Constructors

        public SQM_PRD_EXT()
		{
		}

		public SQM_PRD_EXT(
			string p_mEMO,
			string p_bUSINESSTYPE,
			string p_bUSINESSORG,
			string p_pRODUCTMANAGERID,
			string p_pRODUCTMANAGERNAME,
			System.Decimal? p_dEAGIRATE,
			System.Decimal? p_sORD,
			string p_productkey,
			string p_sTATUS,
			DateTime? p_cREATETIME,
			string p_cREATEID,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYID,
			string p_mODIFYUSER,
			string p_rID,
            string p_sQPRODUCTNAME)
		{
			_mEMO = p_mEMO;
			_bUSINESSTYPE = p_bUSINESSTYPE;
			_bUSINESSORG = p_bUSINESSORG;
			_pRODUCTMANAGERID = p_pRODUCTMANAGERID;
			_pRODUCTMANAGERNAME = p_pRODUCTMANAGERNAME;
			_dEAGIRATE = p_dEAGIRATE;
			_sORD = p_sORD;
			_productkey = p_productkey;
			_sTATUS = p_sTATUS;
			_cREATETIME = p_cREATETIME;
			_cREATEID = p_cREATEID;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYID = p_mODIFYID;
			_mODIFYUSER = p_mODIFYUSER;
			_rID = p_rID;
            _sQPRODUCTNAME = p_sQPRODUCTNAME;
		}

		#endregion

		#region Properties

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
					RaisePropertyChanged(SQM_PRD_EXT.Prop_MEMO, oldValue, value);
				}
			}
		}

		[Property("BUSINESSTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string BUSINESSTYPE
		{
			get { return _bUSINESSTYPE; }
			set
			{
				if ((_bUSINESSTYPE == null) || (value == null) || (!value.Equals(_bUSINESSTYPE)))
				{
                    object oldValue = _bUSINESSTYPE;
					_bUSINESSTYPE = value;
					RaisePropertyChanged(SQM_PRD_EXT.Prop_BUSINESSTYPE, oldValue, value);
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
					RaisePropertyChanged(SQM_PRD_EXT.Prop_BUSINESSORG, oldValue, value);
				}
			}
		}

		[Property("PRODUCTMANAGERID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string PRODUCTMANAGERID
		{
			get { return _pRODUCTMANAGERID; }
			set
			{
				if ((_pRODUCTMANAGERID == null) || (value == null) || (!value.Equals(_pRODUCTMANAGERID)))
				{
                    object oldValue = _pRODUCTMANAGERID;
					_pRODUCTMANAGERID = value;
					RaisePropertyChanged(SQM_PRD_EXT.Prop_PRODUCTMANAGERID, oldValue, value);
				}
			}
		}

		[Property("PRODUCTMANAGERNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string PRODUCTMANAGERNAME
		{
			get { return _pRODUCTMANAGERNAME; }
			set
			{
				if ((_pRODUCTMANAGERNAME == null) || (value == null) || (!value.Equals(_pRODUCTMANAGERNAME)))
				{
                    object oldValue = _pRODUCTMANAGERNAME;
					_pRODUCTMANAGERNAME = value;
					RaisePropertyChanged(SQM_PRD_EXT.Prop_PRODUCTMANAGERNAME, oldValue, value);
				}
			}
		}

		[Property("DEAGIRATE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? DEAGIRATE
		{
			get { return _dEAGIRATE; }
			set
			{
				if (value != _dEAGIRATE)
				{
                    object oldValue = _dEAGIRATE;
					_dEAGIRATE = value;
					RaisePropertyChanged(SQM_PRD_EXT.Prop_DEAGIRATE, oldValue, value);
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
					RaisePropertyChanged(SQM_PRD_EXT.Prop_SORD, oldValue, value);
				}
			}
		}

        [PrimaryKey(PrimaryKeyType.Assigned, "PRODUCTKEY", Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string PRODUCTKEY
        {
            set { _productkey = value; }
            get { return _productkey; }
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
					RaisePropertyChanged(SQM_PRD_EXT.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_PRD_EXT.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SQM_PRD_EXT.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SQM_PRD_EXT.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_PRD_EXT.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SQM_PRD_EXT.Prop_MODIFYID, oldValue, value);
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
					RaisePropertyChanged(SQM_PRD_EXT.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

        [Property("SQPRODUCTNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
        public string SQPRODUCTNAME
        {
            get { return _sQPRODUCTNAME; }
            set
            {
                if ((_sQPRODUCTNAME == null) || (value == null) || (!value.Equals(_sQPRODUCTNAME)))
                {
                    object oldValue = _sQPRODUCTNAME;
                    _sQPRODUCTNAME = value;
                    RaisePropertyChanged(SQM_PRD_EXT.Prop_SQPRODUCTNAME, oldValue, value);
                }
            }
        }


        [Property("RID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string RID
		{
			get { return _rID; }
			set
			{
				if ((_rID == null) || (value == null) || (!value.Equals(_rID)))
				{
                    object oldValue = _rID;
					_rID = value;
					RaisePropertyChanged(SQM_PRD_EXT.Prop_RID, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_PRD_EXT
}

