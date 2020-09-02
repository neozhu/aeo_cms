// Business class SQM_BJ_ORG generated from SQM_BJ_ORG
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
	[ActiveRecord("SQM_BJ_ORG")]
	public partial class SQM_BJ_ORG : EntityBase<SQM_BJ_ORG>
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
		public static string Prop_ORGNAME = "ORGNAME";
		public static string Prop_ORGCODE = "ORGCODE";
		public static string Prop_CREATEID = "CREATEID";
		public static string Prop_MODIFYID = "MODIFYID";
		public static string Prop_MRID = "MRID";

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
		private string _oRGNAME;
		private string _oRGCODE;
		private string _cREATEID;
		private string _mODIFYID;
		private string _mRID;


		#endregion

		#region Constructors

		public SQM_BJ_ORG()
		{
		}

		public SQM_BJ_ORG(
			string p_sTATUS,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rid,
			string p_mEMO,
			string p_dB_KEY,
			string p_bJMAINID,
			string p_oRGNAME,
			string p_oRGCODE,
			string p_cREATEID,
			string p_mODIFYID,
			string p_mRID)
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
			_oRGNAME = p_oRGNAME;
			_oRGCODE = p_oRGCODE;
			_cREATEID = p_cREATEID;
			_mODIFYID = p_mODIFYID;
			_mRID = p_mRID;
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
					RaisePropertyChanged(SQM_BJ_ORG.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_ORG.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_ORG.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_ORG.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_ORG.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_ORG.Prop_MEMO, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_ORG.Prop_DB_KEY, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_ORG.Prop_BJMAINID, oldValue, value);
				}
			}
		}

		[Property("ORGNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string ORGNAME
		{
			get { return _oRGNAME; }
			set
			{
				if ((_oRGNAME == null) || (value == null) || (!value.Equals(_oRGNAME)))
				{
                    object oldValue = _oRGNAME;
					_oRGNAME = value;
					RaisePropertyChanged(SQM_BJ_ORG.Prop_ORGNAME, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_ORG.Prop_ORGCODE, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_ORG.Prop_CREATEID, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_ORG.Prop_MODIFYID, oldValue, value);
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
					RaisePropertyChanged(SQM_BJ_ORG.Prop_MRID, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_BJ_ORG
}

