// Business class SQM_TRACKER generated from SQM_TRACKER
// Creator: rw
// Created Date: [2019-01-03]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_TRACKER")]
	public partial class SQM_TRACKER : EntityBase<SQM_TRACKER>
	{
		#region Property_Names

		public static string Prop_ACCOUNT = "ACCOUNT";
		public static string Prop_CONTROLLERNAME = "CONTROLLERNAME";
		public static string Prop_ACTIONNAME = "ACTIONNAME";
		public static string Prop_ACCESSDT = "ACCESSDT";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";

		#endregion

		#region Private_Variables

		private string _aCCOUNT;
		private string _cONTROLLERNAME;
		private string _aCTIONNAME;
		private DateTime? _aCCESSDT;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rid;
		private string _sTATUS;
		private string _mEMO;


		#endregion

		#region Constructors

		public SQM_TRACKER()
		{
		}

		public SQM_TRACKER(
			string p_aCCOUNT,
			string p_cONTROLLERNAME,
			string p_aCTIONNAME,
			DateTime? p_aCCESSDT,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rid,
			string p_sTATUS,
			string p_mEMO)
		{
			_aCCOUNT = p_aCCOUNT;
			_cONTROLLERNAME = p_cONTROLLERNAME;
			_aCTIONNAME = p_aCTIONNAME;
			_aCCESSDT = p_aCCESSDT;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
		}

		#endregion

		#region Properties

		[Property("ACCOUNT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string ACCOUNT
		{
			get { return _aCCOUNT; }
			set
			{
				if ((_aCCOUNT == null) || (value == null) || (!value.Equals(_aCCOUNT)))
				{
                    object oldValue = _aCCOUNT;
					_aCCOUNT = value;
					RaisePropertyChanged(SQM_TRACKER.Prop_ACCOUNT, oldValue, value);
				}
			}
		}

		[Property("CONTROLLERNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string CONTROLLERNAME
		{
			get { return _cONTROLLERNAME; }
			set
			{
				if ((_cONTROLLERNAME == null) || (value == null) || (!value.Equals(_cONTROLLERNAME)))
				{
                    object oldValue = _cONTROLLERNAME;
					_cONTROLLERNAME = value;
					RaisePropertyChanged(SQM_TRACKER.Prop_CONTROLLERNAME, oldValue, value);
				}
			}
		}

		[Property("ACTIONNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string ACTIONNAME
		{
			get { return _aCTIONNAME; }
			set
			{
				if ((_aCTIONNAME == null) || (value == null) || (!value.Equals(_aCTIONNAME)))
				{
                    object oldValue = _aCTIONNAME;
					_aCTIONNAME = value;
					RaisePropertyChanged(SQM_TRACKER.Prop_ACTIONNAME, oldValue, value);
				}
			}
		}

		[Property("ACCESSDT", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? ACCESSDT
		{
			get { return _aCCESSDT; }
			set
			{
				if (value != _aCCESSDT)
				{
                    object oldValue = _aCCESSDT;
					_aCCESSDT = value;
					RaisePropertyChanged(SQM_TRACKER.Prop_ACCESSDT, oldValue, value);
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
					RaisePropertyChanged(SQM_TRACKER.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		[Property("CREATEUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string CREATEUSER
		{
			get { return _cREATEUSER; }
			set
			{
				if ((_cREATEUSER == null) || (value == null) || (!value.Equals(_cREATEUSER)))
				{
                    object oldValue = _cREATEUSER;
					_cREATEUSER = value;
					RaisePropertyChanged(SQM_TRACKER.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_TRACKER.Prop_MODIFYTIME, oldValue, value);
				}
			}
		}

		[Property("MODIFYUSER", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string MODIFYUSER
		{
			get { return _mODIFYUSER; }
			set
			{
				if ((_mODIFYUSER == null) || (value == null) || (!value.Equals(_mODIFYUSER)))
				{
                    object oldValue = _mODIFYUSER;
					_mODIFYUSER = value;
					RaisePropertyChanged(SQM_TRACKER.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
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
					RaisePropertyChanged(SQM_TRACKER.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_TRACKER.Prop_MEMO, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_TRACKER
}

