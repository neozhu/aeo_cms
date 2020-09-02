// Business class SSO_SYSTEM generated from SSO_SYSTEM
// Creator: Ray
// Created Date: [2016-12-21]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Plat.Model
{
	[ActiveRecord("SSO_SYSTEM")]
	public partial class SSO_SYSTEM : PlatModelBase<SSO_SYSTEM>
	{
		#region Property_Names

		public static string Prop_SYSTEMKEY = "SYSTEMKEY";
		public static string Prop_SYSTEMNAME = "SYSTEMNAME";
		public static string Prop_URL = "URL";
		public static string Prop_TOKEN = "TOKEN";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";

		#endregion

		#region Private_Variables

		private string _systemkey;
		private string _sYSTEMNAME;
		private string _uRL;
		private string _tOKEN;
		private string _sTATUS;
		private string _mEMO;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rID;


		#endregion

		#region Constructors

		public SSO_SYSTEM()
		{
		}

		public SSO_SYSTEM(
			string p_systemkey,
			string p_sYSTEMNAME,
			string p_uRL,
			string p_tOKEN,
			string p_sTATUS,
			string p_mEMO,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rID)
		{
			_systemkey = p_systemkey;
			_sYSTEMNAME = p_sYSTEMNAME;
			_uRL = p_uRL;
			_tOKEN = p_tOKEN;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_rID = p_rID;
		}

		#endregion

		#region Properties

		[PrimaryKey("SYSTEMKEY", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string SYSTEMKEY
		{
			get { return _systemkey; }
			set { _systemkey = value; } // 处理列表编辑时去掉注释

		}

		[Property("SYSTEMNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string SYSTEMNAME
		{
			get { return _sYSTEMNAME; }
			set
			{
				if ((_sYSTEMNAME == null) || (value == null) || (!value.Equals(_sYSTEMNAME)))
				{
                    object oldValue = _sYSTEMNAME;
					_sYSTEMNAME = value;
					RaisePropertyChanged(SSO_SYSTEM.Prop_SYSTEMNAME, oldValue, value);
				}
			}

		}

		[Property("URL", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4000)]
		public string URL
		{
			get { return _uRL; }
			set
			{
				if ((_uRL == null) || (value == null) || (!value.Equals(_uRL)))
				{
                    object oldValue = _uRL;
					_uRL = value;
					RaisePropertyChanged(SSO_SYSTEM.Prop_URL, oldValue, value);
				}
			}

		}

		[Property("TOKEN", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 512)]
		public string TOKEN
		{
			get { return _tOKEN; }
			set
			{
				if ((_tOKEN == null) || (value == null) || (!value.Equals(_tOKEN)))
				{
                    object oldValue = _tOKEN;
					_tOKEN = value;
					RaisePropertyChanged(SSO_SYSTEM.Prop_TOKEN, oldValue, value);
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
					RaisePropertyChanged(SSO_SYSTEM.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SSO_SYSTEM.Prop_MEMO, oldValue, value);
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
					RaisePropertyChanged(SSO_SYSTEM.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SSO_SYSTEM.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SSO_SYSTEM.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SSO_SYSTEM.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(SSO_SYSTEM.Prop_RID, oldValue, value);
				}
			}

		}

		#endregion
	} // SSO_SYSTEM
}

