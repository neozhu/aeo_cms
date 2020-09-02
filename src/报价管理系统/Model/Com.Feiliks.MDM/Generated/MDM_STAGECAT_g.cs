// Business class MDM_STAGECAT generated from MDM_STAGECAT
// Creator: rw
// Created Date: [2018-04-17]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.MDM
{
	[ActiveRecord("MDM_STAGECAT")]
	public partial class MDM_STAGECAT : EntityBase<MDM_STAGECAT>
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
		public static string Prop_DOMVALUE_L = "DOMVALUE_L";
		public static string Prop_DOMVALUE_H = "DOMVALUE_H";
		public static string Prop_DDTEXT = "DDTEXT";

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
		private string _dOMVALUE_L;
		private string _dOMVALUE_H;
		private string _dDTEXT;


		#endregion

		#region Constructors

		public MDM_STAGECAT()
		{
		}

		public MDM_STAGECAT(
			string p_sTATUS,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rid,
			string p_mEMO,
			string p_dB_KEY,
			string p_dOMVALUE_L,
			string p_dOMVALUE_H,
			string p_dDTEXT)
		{
			_sTATUS = p_sTATUS;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_mEMO = p_mEMO;
			_dB_KEY = p_dB_KEY;
			_dOMVALUE_L = p_dOMVALUE_L;
			_dOMVALUE_H = p_dOMVALUE_H;
			_dDTEXT = p_dDTEXT;
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
					RaisePropertyChanged(MDM_STAGECAT.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(MDM_STAGECAT.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(MDM_STAGECAT.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(MDM_STAGECAT.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(MDM_STAGECAT.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(MDM_STAGECAT.Prop_MEMO, oldValue, value);
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
					RaisePropertyChanged(MDM_STAGECAT.Prop_DB_KEY, oldValue, value);
				}
			}
		}

		[Property("DOMVALUE_L", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string DOMVALUE_L
		{
			get { return _dOMVALUE_L; }
			set
			{
				if ((_dOMVALUE_L == null) || (value == null) || (!value.Equals(_dOMVALUE_L)))
				{
                    object oldValue = _dOMVALUE_L;
					_dOMVALUE_L = value;
					RaisePropertyChanged(MDM_STAGECAT.Prop_DOMVALUE_L, oldValue, value);
				}
			}
		}

		[Property("DOMVALUE_H", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string DOMVALUE_H
		{
			get { return _dOMVALUE_H; }
			set
			{
				if ((_dOMVALUE_H == null) || (value == null) || (!value.Equals(_dOMVALUE_H)))
				{
                    object oldValue = _dOMVALUE_H;
					_dOMVALUE_H = value;
					RaisePropertyChanged(MDM_STAGECAT.Prop_DOMVALUE_H, oldValue, value);
				}
			}
		}

		[Property("DDTEXT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string DDTEXT
		{
			get { return _dDTEXT; }
			set
			{
				if ((_dDTEXT == null) || (value == null) || (!value.Equals(_dDTEXT)))
				{
                    object oldValue = _dDTEXT;
					_dDTEXT = value;
					RaisePropertyChanged(MDM_STAGECAT.Prop_DDTEXT, oldValue, value);
				}
			}
		}

		#endregion
	} // MDM_STAGECAT
}

