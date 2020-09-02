// Business class MDM_SRVRQCD generated from MDM_SRVRQCD
// Creator: rw
// Created Date: [2019-12-11]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.MDM
{
	[ActiveRecord("MDM_SRVRQCD")]
	public partial class MDM_SRVRQCD : EntityBase<MDM_SRVRQCD>
	{
		#region Property_Names

		public static string Prop_CLIENT = "CLIENT";
		public static string Prop_SRVRQCD = "SRVRQCD";
		public static string Prop_INS_SET_ID = "INS_SET_ID";
		public static string Prop_SRV_CATEGORY = "SRV_CATEGORY";
		public static string Prop_CLIENTTEXT = "CLIENTTEXT";
		public static string Prop_SPRAS = "SPRAS";
		public static string Prop_SRVRQCDTEXT = "SRVRQCDTEXT";
		public static string Prop_DESCRIPTION = "DESCRIPTION";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_RID = "RID";

		#endregion

		#region Private_Variables

		private string _cLIENT;
		private string _sRVRQCD;
		private string _iNS_SET_ID;
		private string _sRV_CATEGORY;
		private string _cLIENTTEXT;
		private string _sPRAS;
		private string _sRVRQCDTEXT;
		private string _dESCRIPTION;
		private string _sTATUS;
		private string _cREATEUSER;
		private DateTime? _cREATETIME;
		private string _mODIFYUSER;
		private DateTime? _mODIFYTIME;
		private string _rid;


		#endregion

		#region Constructors

		public MDM_SRVRQCD()
		{
		}

		public MDM_SRVRQCD(
			string p_cLIENT,
			string p_sRVRQCD,
			string p_iNS_SET_ID,
			string p_sRV_CATEGORY,
			string p_cLIENTTEXT,
			string p_sPRAS,
			string p_sRVRQCDTEXT,
			string p_dESCRIPTION,
			string p_sTATUS,
			string p_cREATEUSER,
			DateTime? p_cREATETIME,
			string p_mODIFYUSER,
			DateTime? p_mODIFYTIME,
			string p_rid)
		{
			_cLIENT = p_cLIENT;
			_sRVRQCD = p_sRVRQCD;
			_iNS_SET_ID = p_iNS_SET_ID;
			_sRV_CATEGORY = p_sRV_CATEGORY;
			_cLIENTTEXT = p_cLIENTTEXT;
			_sPRAS = p_sPRAS;
			_sRVRQCDTEXT = p_sRVRQCDTEXT;
			_dESCRIPTION = p_dESCRIPTION;
			_sTATUS = p_sTATUS;
			_cREATEUSER = p_cREATEUSER;
			_cREATETIME = p_cREATETIME;
			_mODIFYUSER = p_mODIFYUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_rid = p_rid;
		}

		#endregion

		#region Properties

		[Property("CLIENT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2000)]
		public string CLIENT
		{
			get { return _cLIENT; }
			set
			{
				if ((_cLIENT == null) || (value == null) || (!value.Equals(_cLIENT)))
				{
                    object oldValue = _cLIENT;
					_cLIENT = value;
					RaisePropertyChanged(MDM_SRVRQCD.Prop_CLIENT, oldValue, value);
				}
			}
		}

		[Property("SRVRQCD", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string SRVRQCD
		{
			get { return _sRVRQCD; }
			set
			{
				if ((_sRVRQCD == null) || (value == null) || (!value.Equals(_sRVRQCD)))
				{
                    object oldValue = _sRVRQCD;
					_sRVRQCD = value;
					RaisePropertyChanged(MDM_SRVRQCD.Prop_SRVRQCD, oldValue, value);
				}
			}
		}

		[Property("INS_SET_ID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string INS_SET_ID
		{
			get { return _iNS_SET_ID; }
			set
			{
				if ((_iNS_SET_ID == null) || (value == null) || (!value.Equals(_iNS_SET_ID)))
				{
                    object oldValue = _iNS_SET_ID;
					_iNS_SET_ID = value;
					RaisePropertyChanged(MDM_SRVRQCD.Prop_INS_SET_ID, oldValue, value);
				}
			}
		}

		[Property("SRV_CATEGORY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string SRV_CATEGORY
		{
			get { return _sRV_CATEGORY; }
			set
			{
				if ((_sRV_CATEGORY == null) || (value == null) || (!value.Equals(_sRV_CATEGORY)))
				{
                    object oldValue = _sRV_CATEGORY;
					_sRV_CATEGORY = value;
					RaisePropertyChanged(MDM_SRVRQCD.Prop_SRV_CATEGORY, oldValue, value);
				}
			}
		}

		[Property("CLIENTTEXT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2000)]
		public string CLIENTTEXT
		{
			get { return _cLIENTTEXT; }
			set
			{
				if ((_cLIENTTEXT == null) || (value == null) || (!value.Equals(_cLIENTTEXT)))
				{
                    object oldValue = _cLIENTTEXT;
					_cLIENTTEXT = value;
					RaisePropertyChanged(MDM_SRVRQCD.Prop_CLIENTTEXT, oldValue, value);
				}
			}
		}

		[Property("SPRAS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string SPRAS
		{
			get { return _sPRAS; }
			set
			{
				if ((_sPRAS == null) || (value == null) || (!value.Equals(_sPRAS)))
				{
                    object oldValue = _sPRAS;
					_sPRAS = value;
					RaisePropertyChanged(MDM_SRVRQCD.Prop_SPRAS, oldValue, value);
				}
			}
		}

		[Property("SRVRQCDTEXT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string SRVRQCDTEXT
		{
			get { return _sRVRQCDTEXT; }
			set
			{
				if ((_sRVRQCDTEXT == null) || (value == null) || (!value.Equals(_sRVRQCDTEXT)))
				{
                    object oldValue = _sRVRQCDTEXT;
					_sRVRQCDTEXT = value;
					RaisePropertyChanged(MDM_SRVRQCD.Prop_SRVRQCDTEXT, oldValue, value);
				}
			}
		}

		[Property("DESCRIPTION", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 2000)]
		public string DESCRIPTION
		{
			get { return _dESCRIPTION; }
			set
			{
				if ((_dESCRIPTION == null) || (value == null) || (!value.Equals(_dESCRIPTION)))
				{
                    object oldValue = _dESCRIPTION;
					_dESCRIPTION = value;
					RaisePropertyChanged(MDM_SRVRQCD.Prop_DESCRIPTION, oldValue, value);
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
					RaisePropertyChanged(MDM_SRVRQCD.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(MDM_SRVRQCD.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(MDM_SRVRQCD.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(MDM_SRVRQCD.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(MDM_SRVRQCD.Prop_MODIFYTIME, oldValue, value);
				}
			}
		}

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
		}

		#endregion
	} // MDM_SRVRQCD
}

