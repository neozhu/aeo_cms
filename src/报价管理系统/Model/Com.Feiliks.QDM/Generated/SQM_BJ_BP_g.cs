// Business class SQM_BJ_BP generated from SQM_BJ_BP
// Creator: rw
// Created Date: [2018-05-10]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.QDM.Model
{
	[ActiveRecord("SQM_BJ_BP")]
	public partial class SQM_BJ_BP : EntityBase<SQM_BJ_BP>
	{
		#region Property_Names

		public static string Prop_STATUS = "STATUS";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_DB_KEY = "DB_KEY";
		public static string Prop_BJMAINID = "BJMAINID";
		public static string Prop_BPNAME = "BPNAME";
		public static string Prop_BPCODE = "BPCODE";
		public static string Prop_BPDBKEY = "BPDBKEY";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_MODIFYID = "MODIFYID";
		public static string Prop_MRID = "MRID";
        public static string Prop_INNER = "INNER";

		#endregion

		#region Private_Variables

		private string _sTATUS;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rid;
		private string _mEMO;
		private string _dB_KEY;
		private string _bJMAINID;
		private string _bPNAME;
		private string _bPCODE;
		private string _bPDBKEY;
		private string _cREATEID;
		private string _mODIFYID;
		private string _mRID;
        private string _iNNER;

		#endregion

		#region Constructors

		public SQM_BJ_BP()
		{
		}

		public SQM_BJ_BP(
			string p_sTATUS,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rid,
			string p_mEMO,
			string p_dB_KEY,
			string p_bJMAINID,
			string p_bPNAME,
			string p_bPCODE,
			string p_bPDBKEY,
			string p_cREATEID,
			string p_mODIFYID,
			string p_mRID,
            string p_iNNER)
		{
			_sTATUS = p_sTATUS;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_mEMO = p_mEMO;
			_dB_KEY = p_dB_KEY;
			_bJMAINID = p_bJMAINID;
			_bPNAME = p_bPNAME;
			_bPCODE = p_bPCODE;
			_bPDBKEY = p_bPDBKEY;
			_cREATEID = p_cREATEID;
			_mODIFYID = p_mODIFYID;
			_mRID = p_mRID;
            _iNNER = p_iNNER;
		}

		#endregion

		#region Properties

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
					RaisePropertyChanged(SQM_BJ_BP.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_BP.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_BP.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_BP.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_BP.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
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
					RaisePropertyChanged(SQM_BJ_BP.Prop_MEMO, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_BP.Prop_DB_KEY, oldValue, value);
				}
			}
		}

		[Property("BJMAINID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string BJMAINID
		{
			get { return _bJMAINID; }
			set
			{
				if ((_bJMAINID == null) || (value == null) || (!value.Equals(_bJMAINID)))
				{
                    object oldValue = _bJMAINID;
					_bJMAINID = value;
					RaisePropertyChanged(SQM_BJ_BP.Prop_BJMAINID, oldValue, value);
				}
			}
		}

		[Property("BPNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string BPNAME
		{
			get { return _bPNAME; }
			set
			{
				if ((_bPNAME == null) || (value == null) || (!value.Equals(_bPNAME)))
				{
                    object oldValue = _bPNAME;
					_bPNAME = value;
					RaisePropertyChanged(SQM_BJ_BP.Prop_BPNAME, oldValue, value);
				}
			}
		}

		[Property("BPCODE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string BPCODE
		{
			get { return _bPCODE; }
			set
			{
				if ((_bPCODE == null) || (value == null) || (!value.Equals(_bPCODE)))
				{
                    object oldValue = _bPCODE;
					_bPCODE = value;
					RaisePropertyChanged(SQM_BJ_BP.Prop_BPCODE, oldValue, value);
				}
			}
		}

		[Property("BPDBKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string BPDBKEY
		{
			get { return _bPDBKEY; }
			set
			{
				if ((_bPDBKEY == null) || (value == null) || (!value.Equals(_bPDBKEY)))
				{
                    object oldValue = _bPDBKEY;
					_bPDBKEY = value;
					RaisePropertyChanged(SQM_BJ_BP.Prop_BPDBKEY, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_BP.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_BP.Prop_MODIFYID, oldValue, value);
				}
			}
		}

		[Property("MRID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string MRID
		{
			get { return _mRID; }
			set
			{
				if ((_mRID == null) || (value == null) || (!value.Equals(_mRID)))
				{
                    object oldValue = _mRID;
					_mRID = value;
					RaisePropertyChanged(SQM_BJ_BP.Prop_MRID, oldValue, value);
				}
			}
		}

        [Property("INNER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
        public string INNER
        {
            get { return _iNNER; }
            set
            {
                if ((_iNNER == null) || (value == null) || (!value.Equals(_iNNER)))
                {
                    object oldValue = _iNNER;
                    _iNNER = value;
                    RaisePropertyChanged(SQM_BJ_BP.Prop_INNER, oldValue, value);
                }
            }
        }
        #endregion
    } // SQM_BJ_BP
}

