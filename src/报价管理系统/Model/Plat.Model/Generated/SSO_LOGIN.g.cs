// Business class SSO_LOGIN generated from SSO_LOGIN
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
	[ActiveRecord("SSO_LOGIN")]
	public partial class SSO_LOGIN : PlatModelBase<SSO_LOGIN>
	{
		#region Property_Names

		public static string Prop_DEPTKEY = "DEPTKEY";
		public static string Prop_COMPKEY = "COMPKEY";
		public static string Prop_SYSTEMKEY = "SYSTEMKEY";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_HASH = "HASH";
		public static string Prop_STAFFID = "STAFFID";
		public static string Prop_DEPTID = "DEPTID";
		public static string Prop_COMPID = "COMPID";
		public static string Prop_STAFFKEY = "STAFFKEY";

		#endregion

		#region Private_Variables

		private string _dEPTKEY;
		private string _cOMPKEY;
		private string _sYSTEMKEY;
		private string _sTATUS;
		private string _mEMO;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rID;
		private string _hash;
		private System.Decimal? _sTAFFID;
		private System.Decimal? _dEPTID;
		private System.Decimal? _cOMPID;
		private string _sTAFFKEY;


		#endregion

		#region Constructors

		public SSO_LOGIN()
		{
		}

		public SSO_LOGIN(
			string p_dEPTKEY,
			string p_cOMPKEY,
			string p_sYSTEMKEY,
			string p_sTATUS,
			string p_mEMO,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rID,
			string p_hash,
			System.Decimal? p_sTAFFID,
			System.Decimal? p_dEPTID,
			System.Decimal? p_cOMPID,
			string p_sTAFFKEY)
		{
			_dEPTKEY = p_dEPTKEY;
			_cOMPKEY = p_cOMPKEY;
			_sYSTEMKEY = p_sYSTEMKEY;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_rID = p_rID;
			_hash = p_hash;
			_sTAFFID = p_sTAFFID;
			_dEPTID = p_dEPTID;
			_cOMPID = p_cOMPID;
			_sTAFFKEY = p_sTAFFKEY;
		}

		#endregion

		#region Properties

		[Property("DEPTKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string DEPTKEY
		{
			get { return _dEPTKEY; }
			set
			{
				if ((_dEPTKEY == null) || (value == null) || (!value.Equals(_dEPTKEY)))
				{
                    object oldValue = _dEPTKEY;
					_dEPTKEY = value;
					RaisePropertyChanged(SSO_LOGIN.Prop_DEPTKEY, oldValue, value);
				}
			}

		}

		[Property("COMPKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string COMPKEY
		{
			get { return _cOMPKEY; }
			set
			{
				if ((_cOMPKEY == null) || (value == null) || (!value.Equals(_cOMPKEY)))
				{
                    object oldValue = _cOMPKEY;
					_cOMPKEY = value;
					RaisePropertyChanged(SSO_LOGIN.Prop_COMPKEY, oldValue, value);
				}
			}

		}

		[Property("SYSTEMKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string SYSTEMKEY
		{
			get { return _sYSTEMKEY; }
			set
			{
				if ((_sYSTEMKEY == null) || (value == null) || (!value.Equals(_sYSTEMKEY)))
				{
                    object oldValue = _sYSTEMKEY;
					_sYSTEMKEY = value;
					RaisePropertyChanged(SSO_LOGIN.Prop_SYSTEMKEY, oldValue, value);
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
					RaisePropertyChanged(SSO_LOGIN.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SSO_LOGIN.Prop_MEMO, oldValue, value);
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
					RaisePropertyChanged(SSO_LOGIN.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SSO_LOGIN.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SSO_LOGIN.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SSO_LOGIN.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(SSO_LOGIN.Prop_RID, oldValue, value);
				}
			}

		}

		[PrimaryKey("HASH", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string HASH
		{
			get { return _hash; }
			set { _hash = value; } // 处理列表编辑时去掉注释

		}

		[Property("STAFFID", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? STAFFID
		{
			get { return _sTAFFID; }
			set
			{
				if (value != _sTAFFID)
				{
                    object oldValue = _sTAFFID;
					_sTAFFID = value;
					RaisePropertyChanged(SSO_LOGIN.Prop_STAFFID, oldValue, value);
				}
			}

		}

		[Property("DEPTID", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? DEPTID
		{
			get { return _dEPTID; }
			set
			{
				if (value != _dEPTID)
				{
                    object oldValue = _dEPTID;
					_dEPTID = value;
					RaisePropertyChanged(SSO_LOGIN.Prop_DEPTID, oldValue, value);
				}
			}

		}

		[Property("COMPID", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? COMPID
		{
			get { return _cOMPID; }
			set
			{
				if (value != _cOMPID)
				{
                    object oldValue = _cOMPID;
					_cOMPID = value;
					RaisePropertyChanged(SSO_LOGIN.Prop_COMPID, oldValue, value);
				}
			}

		}

		[Property("STAFFKEY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string STAFFKEY
		{
			get { return _sTAFFKEY; }
			set
			{
				if ((_sTAFFKEY == null) || (value == null) || (!value.Equals(_sTAFFKEY)))
				{
                    object oldValue = _sTAFFKEY;
					_sTAFFKEY = value;
					RaisePropertyChanged(SSO_LOGIN.Prop_STAFFKEY, oldValue, value);
				}
			}

		}

		#endregion
	} // SSO_LOGIN
}

