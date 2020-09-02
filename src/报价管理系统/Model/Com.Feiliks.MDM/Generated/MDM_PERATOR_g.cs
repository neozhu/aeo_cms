// Business class MDM_PERATOR generated from MDM_PERATOR
// Creator: rw
// Created Date: [2018-06-28]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Com.Feiliks.MDM
{
	[ActiveRecord("MDM_PERATOR")]
	public partial class MDM_PERATOR : EntityBase<MDM_PERATOR>
	{
		#region Property_Names

		public static string Prop_RID = "RID";
		public static string Prop_DOMVALUE_L = "DOMVALUE_L";
		public static string Prop_DOMVALUE_H = "DOMVALUE_H";
		public static string Prop_DDTEXT = "DDTEXT";
		public static string Prop_CREATETIME = "CREATETIME";

		#endregion

		#region Private_Variables

		private string _rid;
		private string _dOMVALUE_L;
		private string _dOMVALUE_H;
		private string _dDTEXT;
		private DateTime? _cREATETIME;


		#endregion

		#region Constructors

		public MDM_PERATOR()
		{
		}

		public MDM_PERATOR(
			string p_rid,
			string p_dOMVALUE_L,
			string p_dOMVALUE_H,
			string p_dDTEXT,
			DateTime? p_cREATETIME)
		{
			_rid = p_rid;
			_dOMVALUE_L = p_dOMVALUE_L;
			_dOMVALUE_H = p_dOMVALUE_H;
			_dDTEXT = p_dDTEXT;
			_cREATETIME = p_cREATETIME;
		}

		#endregion

		#region Properties

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
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
					RaisePropertyChanged(MDM_PERATOR.Prop_DOMVALUE_L, oldValue, value);
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
					RaisePropertyChanged(MDM_PERATOR.Prop_DOMVALUE_H, oldValue, value);
				}
			}
		}

		[Property("DDTEXT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 400)]
		public string DDTEXT
		{
			get { return _dDTEXT; }
			set
			{
				if ((_dDTEXT == null) || (value == null) || (!value.Equals(_dDTEXT)))
				{
                    object oldValue = _dDTEXT;
					_dDTEXT = value;
					RaisePropertyChanged(MDM_PERATOR.Prop_DDTEXT, oldValue, value);
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
					RaisePropertyChanged(MDM_PERATOR.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		#endregion
	} // MDM_PERATOR
}

