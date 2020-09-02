// Business class MDM_INSASN generated from MDM_INSASN
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
	[ActiveRecord("MDM_INSASN")]
	public partial class MDM_INSASN : EntityBase<MDM_INSASN>
	{
		#region Property_Names

		public static string Prop_RID = "RID";
		public static string Prop_INSSET_ID = "INSSET_ID";
		public static string Prop_INS_ID = "INS_ID";
		public static string Prop_SEQ_NUMBER = "SEQ_NUMBER";
		public static string Prop_CREATETIME = "CREATETIME";

		#endregion

		#region Private_Variables

		private string _rid;
		private string _iNSSET_ID;
		private string _iNS_ID;
		private System.Decimal? _sEQ_NUMBER;
		private DateTime? _cREATETIME;


		#endregion

		#region Constructors

		public MDM_INSASN()
		{
		}

		public MDM_INSASN(
			string p_rid,
			string p_iNSSET_ID,
			string p_iNS_ID,
			System.Decimal? p_sEQ_NUMBER,
			DateTime? p_cREATETIME)
		{
			_rid = p_rid;
			_iNSSET_ID = p_iNSSET_ID;
			_iNS_ID = p_iNS_ID;
			_sEQ_NUMBER = p_sEQ_NUMBER;
			_cREATETIME = p_cREATETIME;
		}

		#endregion

		#region Properties

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
		}

		[Property("INSSET_ID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string INSSET_ID
		{
			get { return _iNSSET_ID; }
			set
			{
				if ((_iNSSET_ID == null) || (value == null) || (!value.Equals(_iNSSET_ID)))
				{
                    object oldValue = _iNSSET_ID;
					_iNSSET_ID = value;
					RaisePropertyChanged(MDM_INSASN.Prop_INSSET_ID, oldValue, value);
				}
			}
		}

		[Property("INS_ID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string INS_ID
		{
			get { return _iNS_ID; }
			set
			{
				if ((_iNS_ID == null) || (value == null) || (!value.Equals(_iNS_ID)))
				{
                    object oldValue = _iNS_ID;
					_iNS_ID = value;
					RaisePropertyChanged(MDM_INSASN.Prop_INS_ID, oldValue, value);
				}
			}
		}

		[Property("SEQ_NUMBER", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
		public System.Decimal? SEQ_NUMBER
		{
			get { return _sEQ_NUMBER; }
			set
			{
				if (value != _sEQ_NUMBER)
				{
                    object oldValue = _sEQ_NUMBER;
					_sEQ_NUMBER = value;
					RaisePropertyChanged(MDM_INSASN.Prop_SEQ_NUMBER, oldValue, value);
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
					RaisePropertyChanged(MDM_INSASN.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		#endregion
	} // MDM_INSASN
}

