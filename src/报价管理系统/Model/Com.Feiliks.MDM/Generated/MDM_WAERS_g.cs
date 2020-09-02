// Business class MDM_WAERS generated from MDM_WAERS
// Creator: rw
// Created Date: [2018-06-20]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Com.Feiliks.MDM
{
	[ActiveRecord("MDM_WAERS")]
	public partial class MDM_WAERS : EntityBase<MDM_WAERS>
	{
		#region Property_Names

		public static string Prop_RID = "RID";
		public static string Prop_WAERS = "WAERS";
		public static string Prop_LTEXT = "LTEXT";
		public static string Prop_KTEXT = "KTEXT";
		public static string Prop_CREATETIME = "CREATETIME";

		#endregion

		#region Private_Variables

		private string _rid;
		private string _wAERS;
		private string _lTEXT;
		private string _kTEXT;
		private DateTime? _cREATETIME;


		#endregion

		#region Constructors

		public MDM_WAERS()
		{
		}

		public MDM_WAERS(
			string p_rid,
			string p_wAERS,
			string p_lTEXT,
			string p_kTEXT,
			DateTime? p_cREATETIME)
		{
			_rid = p_rid;
			_wAERS = p_wAERS;
			_lTEXT = p_lTEXT;
			_kTEXT = p_kTEXT;
			_cREATETIME = p_cREATETIME;
		}

		#endregion

		#region Properties

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
		}

		[Property("WAERS", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string WAERS
		{
			get { return _wAERS; }
			set
			{
				if ((_wAERS == null) || (value == null) || (!value.Equals(_wAERS)))
				{
                    object oldValue = _wAERS;
					_wAERS = value;
					RaisePropertyChanged(MDM_WAERS.Prop_WAERS, oldValue, value);
				}
			}
		}

		[Property("LTEXT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string LTEXT
		{
			get { return _lTEXT; }
			set
			{
				if ((_lTEXT == null) || (value == null) || (!value.Equals(_lTEXT)))
				{
                    object oldValue = _lTEXT;
					_lTEXT = value;
					RaisePropertyChanged(MDM_WAERS.Prop_LTEXT, oldValue, value);
				}
			}
		}

		[Property("KTEXT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string KTEXT
		{
			get { return _kTEXT; }
			set
			{
				if ((_kTEXT == null) || (value == null) || (!value.Equals(_kTEXT)))
				{
                    object oldValue = _kTEXT;
					_kTEXT = value;
					RaisePropertyChanged(MDM_WAERS.Prop_KTEXT, oldValue, value);
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
					RaisePropertyChanged(MDM_WAERS.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		#endregion
	} // MDM_WAERS
}

