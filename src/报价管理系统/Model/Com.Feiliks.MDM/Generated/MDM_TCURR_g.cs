// Business class MDM_TCURR generated from MDM_TCURR
// Creator: rw
// Created Date: [2019-11-25]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.MDM
{
	[ActiveRecord("MDM_TCURR")]
	public partial class MDM_TCURR : EntityBase<MDM_TCURR>
	{
		#region Property_Names

		public static string Prop_KURST = "KURST";
		public static string Prop_FCURR = "FCURR";
		public static string Prop_TCURR = "TCURR";
		public static string Prop_GDATE = "GDATE";
		public static string Prop_UKURS = "UKURS";
		public static string Prop_RID = "RID";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";
		public static string Prop_FFACT = "FFACT";
		public static string Prop_TFACT = "TFACT";

		#endregion

		#region Private_Variables

		private string _kURST;
		private string _fCURR;
		private string _tCURR;
		private DateTime? _gDATE;
		private System.Decimal? _uKURS;
		private string _rid;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _sTATUS;
		private string _mEMO;
		private System.Decimal? _fFACT;
		private System.Decimal? _tFACT;


		#endregion

		#region Constructors

		public MDM_TCURR()
		{
		}

		public MDM_TCURR(
			string p_kURST,
			string p_fCURR,
			string p_tCURR,
			DateTime? p_gDATE,
			System.Decimal? p_uKURS,
			string p_rid,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_sTATUS,
			string p_mEMO,
			System.Decimal? p_fFACT,
			System.Decimal? p_tFACT)
		{
			_kURST = p_kURST;
			_fCURR = p_fCURR;
			_tCURR = p_tCURR;
			_gDATE = p_gDATE;
			_uKURS = p_uKURS;
			_rid = p_rid;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
			_fFACT = p_fFACT;
			_tFACT = p_tFACT;
		}

		#endregion

		#region Properties

		[Property("KURST", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string KURST
		{
			get { return _kURST; }
			set
			{
				if ((_kURST == null) || (value == null) || (!value.Equals(_kURST)))
				{
                    object oldValue = _kURST;
					_kURST = value;
					RaisePropertyChanged(MDM_TCURR.Prop_KURST, oldValue, value);
				}
			}
		}

		[Property("FCURR", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string FCURR
		{
			get { return _fCURR; }
			set
			{
				if ((_fCURR == null) || (value == null) || (!value.Equals(_fCURR)))
				{
                    object oldValue = _fCURR;
					_fCURR = value;
					RaisePropertyChanged(MDM_TCURR.Prop_FCURR, oldValue, value);
				}
			}
		}

		[Property("TCURR", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string TCURR
		{
			get { return _tCURR; }
			set
			{
				if ((_tCURR == null) || (value == null) || (!value.Equals(_tCURR)))
				{
                    object oldValue = _tCURR;
					_tCURR = value;
					RaisePropertyChanged(MDM_TCURR.Prop_TCURR, oldValue, value);
				}
			}
		}

		[Property("GDATE", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public DateTime? GDATE
		{
			get { return _gDATE; }
			set
			{
				if (value != _gDATE)
				{
                    object oldValue = _gDATE;
					_gDATE = value;
					RaisePropertyChanged(MDM_TCURR.Prop_GDATE, oldValue, value);
				}
			}
		}

		[Property("UKURS", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? UKURS
		{
			get { return _uKURS; }
			set
			{
				if (value != _uKURS)
				{
                    object oldValue = _uKURS;
					_uKURS = value;
					RaisePropertyChanged(MDM_TCURR.Prop_UKURS, oldValue, value);
				}
			}
		}

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
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
					RaisePropertyChanged(MDM_TCURR.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(MDM_TCURR.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(MDM_TCURR.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(MDM_TCURR.Prop_MODIFYUSER, oldValue, value);
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
					RaisePropertyChanged(MDM_TCURR.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(MDM_TCURR.Prop_MEMO, oldValue, value);
				}
			}
		}

		[Property("FFACT", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? FFACT
		{
			get { return _fFACT; }
			set
			{
				if (value != _fFACT)
				{
                    object oldValue = _fFACT;
					_fFACT = value;
					RaisePropertyChanged(MDM_TCURR.Prop_FFACT, oldValue, value);
				}
			}
		}

		[Property("TFACT", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? TFACT
		{
			get { return _tFACT; }
			set
			{
				if (value != _tFACT)
				{
                    object oldValue = _tFACT;
					_tFACT = value;
					RaisePropertyChanged(MDM_TCURR.Prop_TFACT, oldValue, value);
				}
			}
		}

		#endregion
	} // MDM_TCURR
}

