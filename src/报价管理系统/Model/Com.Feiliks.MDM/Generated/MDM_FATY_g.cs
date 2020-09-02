// Business class MDM_FATY generated from MDM_FATY
// Creator: rw
// Created Date: [2018-06-22]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Com.Feiliks.MDM
{
	[ActiveRecord("MDM_FATY")]
	public partial class MDM_FATY : EntityBase<MDM_FATY>
	{
		#region Property_Names

		public static string Prop_RID = "RID";
		public static string Prop_AGITMTYPE = "AGITMTYPE";
		public static string Prop_FAGUSAGEID105 = "FAGUSAGEID105";
		public static string Prop_TCCS = "TCCS";
		public static string Prop_ITEM_HIERARCHY = "ITEM_HIERARCHY";
		public static string Prop_DEFAULT_ORDER_TYPE = "DEFAULT_ORDER_TYPE";
		public static string Prop_DEFAULT_BOOKING_TYPE = "DEFAULT_BOOKING_TYPE";
		public static string Prop_DEFAULT_SERVICE_ORDER_TYPE = "DEFAULT_SERVICE_ORDER_TYPE";
		public static string Prop_DEFAULT_QUOTATION_TYPE = "DEFAULT_QUOTATION_TYPE";
		public static string Prop_DEFAULT_FRE_ORDER_TYPE = "DEFAULT_FRE_ORDER_TYPE";
		public static string Prop_DESCRIPTION = "DESCRIPTION";
		public static string Prop_CREATETIME = "CREATETIME";

		#endregion

		#region Private_Variables

		private string _rid;
		private string _aGITMTYPE;
		private string _fAGUSAGEID105;
		private string _tCCS;
		private string _iTEM_HIERARCHY;
		private string _dEFAULT_ORDER_TYPE;
		private string _dEFAULT_BOOKING_TYPE;
		private string _dEFAULT_SERVICE_ORDER_TYPE;
		private string _dEFAULT_QUOTATION_TYPE;
		private string _dEFAULT_FRE_ORDER_TYPE;
		private string _dESCRIPTION;
		private DateTime? _cREATETIME;


		#endregion

		#region Constructors

		public MDM_FATY()
		{
		}

		public MDM_FATY(
			string p_rid,
			string p_aGITMTYPE,
			string p_fAGUSAGEID105,
			string p_tCCS,
			string p_iTEM_HIERARCHY,
			string p_dEFAULT_ORDER_TYPE,
			string p_dEFAULT_BOOKING_TYPE,
			string p_dEFAULT_SERVICE_ORDER_TYPE,
			string p_dEFAULT_QUOTATION_TYPE,
			string p_dEFAULT_FRE_ORDER_TYPE,
			string p_dESCRIPTION,
			DateTime? p_cREATETIME)
		{
			_rid = p_rid;
			_aGITMTYPE = p_aGITMTYPE;
			_fAGUSAGEID105 = p_fAGUSAGEID105;
			_tCCS = p_tCCS;
			_iTEM_HIERARCHY = p_iTEM_HIERARCHY;
			_dEFAULT_ORDER_TYPE = p_dEFAULT_ORDER_TYPE;
			_dEFAULT_BOOKING_TYPE = p_dEFAULT_BOOKING_TYPE;
			_dEFAULT_SERVICE_ORDER_TYPE = p_dEFAULT_SERVICE_ORDER_TYPE;
			_dEFAULT_QUOTATION_TYPE = p_dEFAULT_QUOTATION_TYPE;
			_dEFAULT_FRE_ORDER_TYPE = p_dEFAULT_FRE_ORDER_TYPE;
			_dESCRIPTION = p_dESCRIPTION;
			_cREATETIME = p_cREATETIME;
		}

		#endregion

		#region Properties

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
		}

		[Property("AGITMTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string AGITMTYPE
		{
			get { return _aGITMTYPE; }
			set
			{
				if ((_aGITMTYPE == null) || (value == null) || (!value.Equals(_aGITMTYPE)))
				{
                    object oldValue = _aGITMTYPE;
					_aGITMTYPE = value;
					RaisePropertyChanged(MDM_FATY.Prop_AGITMTYPE, oldValue, value);
				}
			}
		}

		[Property("FAGUSAGEID105", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string FAGUSAGEID105
		{
			get { return _fAGUSAGEID105; }
			set
			{
				if ((_fAGUSAGEID105 == null) || (value == null) || (!value.Equals(_fAGUSAGEID105)))
				{
                    object oldValue = _fAGUSAGEID105;
					_fAGUSAGEID105 = value;
					RaisePropertyChanged(MDM_FATY.Prop_FAGUSAGEID105, oldValue, value);
				}
			}
		}

		[Property("TCCS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string TCCS
		{
			get { return _tCCS; }
			set
			{
				if ((_tCCS == null) || (value == null) || (!value.Equals(_tCCS)))
				{
                    object oldValue = _tCCS;
					_tCCS = value;
					RaisePropertyChanged(MDM_FATY.Prop_TCCS, oldValue, value);
				}
			}
		}

		[Property("ITEM_HIERARCHY", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string ITEM_HIERARCHY
		{
			get { return _iTEM_HIERARCHY; }
			set
			{
				if ((_iTEM_HIERARCHY == null) || (value == null) || (!value.Equals(_iTEM_HIERARCHY)))
				{
                    object oldValue = _iTEM_HIERARCHY;
					_iTEM_HIERARCHY = value;
					RaisePropertyChanged(MDM_FATY.Prop_ITEM_HIERARCHY, oldValue, value);
				}
			}
		}

		[Property("DEFAULT_ORDER_TYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string DEFAULT_ORDER_TYPE
		{
			get { return _dEFAULT_ORDER_TYPE; }
			set
			{
				if ((_dEFAULT_ORDER_TYPE == null) || (value == null) || (!value.Equals(_dEFAULT_ORDER_TYPE)))
				{
                    object oldValue = _dEFAULT_ORDER_TYPE;
					_dEFAULT_ORDER_TYPE = value;
					RaisePropertyChanged(MDM_FATY.Prop_DEFAULT_ORDER_TYPE, oldValue, value);
				}
			}
		}

		[Property("DEFAULT_BOOKING_TYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string DEFAULT_BOOKING_TYPE
		{
			get { return _dEFAULT_BOOKING_TYPE; }
			set
			{
				if ((_dEFAULT_BOOKING_TYPE == null) || (value == null) || (!value.Equals(_dEFAULT_BOOKING_TYPE)))
				{
                    object oldValue = _dEFAULT_BOOKING_TYPE;
					_dEFAULT_BOOKING_TYPE = value;
					RaisePropertyChanged(MDM_FATY.Prop_DEFAULT_BOOKING_TYPE, oldValue, value);
				}
			}
		}

		[Property("DEFAULT_SERVICE_ORDER_TYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string DEFAULT_SERVICE_ORDER_TYPE
		{
			get { return _dEFAULT_SERVICE_ORDER_TYPE; }
			set
			{
				if ((_dEFAULT_SERVICE_ORDER_TYPE == null) || (value == null) || (!value.Equals(_dEFAULT_SERVICE_ORDER_TYPE)))
				{
                    object oldValue = _dEFAULT_SERVICE_ORDER_TYPE;
					_dEFAULT_SERVICE_ORDER_TYPE = value;
					RaisePropertyChanged(MDM_FATY.Prop_DEFAULT_SERVICE_ORDER_TYPE, oldValue, value);
				}
			}
		}

		[Property("DEFAULT_QUOTATION_TYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string DEFAULT_QUOTATION_TYPE
		{
			get { return _dEFAULT_QUOTATION_TYPE; }
			set
			{
				if ((_dEFAULT_QUOTATION_TYPE == null) || (value == null) || (!value.Equals(_dEFAULT_QUOTATION_TYPE)))
				{
                    object oldValue = _dEFAULT_QUOTATION_TYPE;
					_dEFAULT_QUOTATION_TYPE = value;
					RaisePropertyChanged(MDM_FATY.Prop_DEFAULT_QUOTATION_TYPE, oldValue, value);
				}
			}
		}

		[Property("DEFAULT_FRE_ORDER_TYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string DEFAULT_FRE_ORDER_TYPE
		{
			get { return _dEFAULT_FRE_ORDER_TYPE; }
			set
			{
				if ((_dEFAULT_FRE_ORDER_TYPE == null) || (value == null) || (!value.Equals(_dEFAULT_FRE_ORDER_TYPE)))
				{
                    object oldValue = _dEFAULT_FRE_ORDER_TYPE;
					_dEFAULT_FRE_ORDER_TYPE = value;
					RaisePropertyChanged(MDM_FATY.Prop_DEFAULT_FRE_ORDER_TYPE, oldValue, value);
				}
			}
		}

		[Property("DESCRIPTION", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string DESCRIPTION
		{
			get { return _dESCRIPTION; }
			set
			{
				if ((_dESCRIPTION == null) || (value == null) || (!value.Equals(_dESCRIPTION)))
				{
                    object oldValue = _dESCRIPTION;
					_dESCRIPTION = value;
					RaisePropertyChanged(MDM_FATY.Prop_DESCRIPTION, oldValue, value);
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
					RaisePropertyChanged(MDM_FATY.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		#endregion
	} // MDM_FATY
}

