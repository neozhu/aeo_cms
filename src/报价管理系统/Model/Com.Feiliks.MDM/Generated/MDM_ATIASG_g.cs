// Business class MDM_ATIASG generated from MDM_ATIASG
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
	[ActiveRecord("MDM_ATIASG")]
	public partial class MDM_ATIASG : EntityBase<MDM_ATIASG>
	{
		#region Property_Names

		public static string Prop_RID = "RID";
		public static string Prop_FAGTYPEID103 = "FAGTYPEID103";
		public static string Prop_AGITMTYPE = "AGITMTYPE";
		public static string Prop_CREATETIME = "CREATETIME";

		#endregion

		#region Private_Variables

		private string _rid;
		private string _fAGTYPEID103;
		private string _aGITMTYPE;
		private DateTime? _cREATETIME;


		#endregion

		#region Constructors

		public MDM_ATIASG()
		{
		}

		public MDM_ATIASG(
			string p_rid,
			string p_fAGTYPEID103,
			string p_aGITMTYPE,
			DateTime? p_cREATETIME)
		{
			_rid = p_rid;
			_fAGTYPEID103 = p_fAGTYPEID103;
			_aGITMTYPE = p_aGITMTYPE;
			_cREATETIME = p_cREATETIME;
		}

		#endregion

		#region Properties

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
		}

		[Property("FAGTYPEID103", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string FAGTYPEID103
		{
			get { return _fAGTYPEID103; }
			set
			{
				if ((_fAGTYPEID103 == null) || (value == null) || (!value.Equals(_fAGTYPEID103)))
				{
                    object oldValue = _fAGTYPEID103;
					_fAGTYPEID103 = value;
					RaisePropertyChanged(MDM_ATIASG.Prop_FAGTYPEID103, oldValue, value);
				}
			}
		}

		[Property("AGITMTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string AGITMTYPE
		{
			get { return _aGITMTYPE; }
			set
			{
				if ((_aGITMTYPE == null) || (value == null) || (!value.Equals(_aGITMTYPE)))
				{
                    object oldValue = _aGITMTYPE;
					_aGITMTYPE = value;
					RaisePropertyChanged(MDM_ATIASG.Prop_AGITMTYPE, oldValue, value);
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
					RaisePropertyChanged(MDM_ATIASG.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		#endregion
	} // MDM_ATIASG
}

