// Business class MDM_SERVICE_PRODUCT_ID generated from MDM_SERVICE_PRODUCT_ID
// Creator: rw
// Created Date: [2018-06-11]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Com.Feiliks.MDM
{
	[ActiveRecord("MDM_SERVICE_PRODUCT_ID")]
	public partial class MDM_SERVICE_PRODUCT_ID : EntityBase<MDM_SERVICE_PRODUCT_ID>
	{
		#region Property_Names

		public static string Prop_RID = "RID";
		public static string Prop_SERVICE_PRODUCT_ID = "SERVICE_PRODUCT_ID";
		public static string Prop_SERVICE_TYPE = "SERVICE_TYPE";
		public static string Prop_FLOW_SERVICE = "FLOW_SERVICE";
		public static string Prop_CREATETIME = "CREATETIME";

		#endregion

		#region Private_Variables

		private string _rid;
		private string _sERVICE_PRODUCT_ID;
		private string _sERVICE_TYPE;
		private string _fLOW_SERVICE;
		private DateTime? _cREATETIME;


		#endregion

		#region Constructors

		public MDM_SERVICE_PRODUCT_ID()
		{
		}

		public MDM_SERVICE_PRODUCT_ID(
			string p_rid,
			string p_sERVICE_PRODUCT_ID,
			string p_sERVICE_TYPE,
			string p_fLOW_SERVICE,
			DateTime? p_cREATETIME)
		{
			_rid = p_rid;
			_sERVICE_PRODUCT_ID = p_sERVICE_PRODUCT_ID;
			_sERVICE_TYPE = p_sERVICE_TYPE;
			_fLOW_SERVICE = p_fLOW_SERVICE;
			_cREATETIME = p_cREATETIME;
		}

		#endregion

		#region Properties

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
		}

		[Property("SERVICE_PRODUCT_ID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 200)]
		public string SERVICE_PRODUCT_ID
		{
			get { return _sERVICE_PRODUCT_ID; }
			set
			{
				if ((_sERVICE_PRODUCT_ID == null) || (value == null) || (!value.Equals(_sERVICE_PRODUCT_ID)))
				{
                    object oldValue = _sERVICE_PRODUCT_ID;
					_sERVICE_PRODUCT_ID = value;
					RaisePropertyChanged(MDM_SERVICE_PRODUCT_ID.Prop_SERVICE_PRODUCT_ID, oldValue, value);
				}
			}
		}

		[Property("SERVICE_TYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
		public string SERVICE_TYPE
		{
			get { return _sERVICE_TYPE; }
			set
			{
				if ((_sERVICE_TYPE == null) || (value == null) || (!value.Equals(_sERVICE_TYPE)))
				{
                    object oldValue = _sERVICE_TYPE;
					_sERVICE_TYPE = value;
					RaisePropertyChanged(MDM_SERVICE_PRODUCT_ID.Prop_SERVICE_TYPE, oldValue, value);
				}
			}
		}

		[Property("FLOW_SERVICE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 20)]
		public string FLOW_SERVICE
		{
			get { return _fLOW_SERVICE; }
			set
			{
				if ((_fLOW_SERVICE == null) || (value == null) || (!value.Equals(_fLOW_SERVICE)))
				{
                    object oldValue = _fLOW_SERVICE;
					_fLOW_SERVICE = value;
					RaisePropertyChanged(MDM_SERVICE_PRODUCT_ID.Prop_FLOW_SERVICE, oldValue, value);
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
					RaisePropertyChanged(MDM_SERVICE_PRODUCT_ID.Prop_CREATETIME, oldValue, value);
				}
			}
		}

		#endregion
	} // MDM_SERVICE_PRODUCT_ID
}

