// Business class MDM_PTRO generated from MDM_PTRO
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
	[ActiveRecord("MDM_PTRO")]
	public partial class MDM_PTRO : EntityBase<MDM_PTRO>
	{
		#region Property_Names

		public static string Prop_RID = "RID";
		public static string Prop_PARTY_ROLE012 = "PARTY_ROLE012";
		public static string Prop_DESCRIPTION = "DESCRIPTION";
		public static string Prop_CREATETIME = "CREATETIME";

		#endregion

		#region Private_Variables

		private string _rid;
		private string _pARTY_ROLE012;
		private string _dESCRIPTION;
		private DateTime? _cREATETIME;


		#endregion

		#region Constructors

		public MDM_PTRO()
		{
		}

		public MDM_PTRO(
			string p_rid,
			string p_pARTY_ROLE012,
			string p_dESCRIPTION,
			DateTime? p_cREATETIME)
		{
			_rid = p_rid;
			_pARTY_ROLE012 = p_pARTY_ROLE012;
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

		[Property("PARTY_ROLE012", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string PARTY_ROLE012
		{
			get { return _pARTY_ROLE012; }
			set
			{
				if ((_pARTY_ROLE012 == null) || (value == null) || (!value.Equals(_pARTY_ROLE012)))
				{
                    object oldValue = _pARTY_ROLE012;
					_pARTY_ROLE012 = value;
					RaisePropertyChanged(MDM_PTRO.Prop_PARTY_ROLE012, oldValue, value);
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
					RaisePropertyChanged(MDM_PTRO.Prop_DESCRIPTION, oldValue, value);
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
					RaisePropertyChanged(MDM_PTRO.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		#endregion
	} // MDM_PTRO
}

