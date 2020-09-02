// Business class SQM_ITEMTYPE_REF generated from SQM_ITEMTYPE_REF
// Creator: rw
// Created Date: [2018-09-05]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;

namespace Com.Feiliks.QDM
{
	[ActiveRecord("SQM_ITEMTYPE_REF")]
	public partial class SQM_ITEMTYPE_REF : EntityBase<SQM_ITEMTYPE_REF>
	{
		#region Property_Names

		public static string Prop_ITEMTYPE = "ITEMTYPE";
		public static string Prop_ITEMTYPENAME = "ITEMTYPENAME";
		public static string Prop_PRODUCT = "PRODUCT";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";

		#endregion

		#region Private_Variables

		private string _iTEMTYPE;
		private string _iTEMTYPENAME;
		private string _pRODUCT;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rid;
		private string _sTATUS;
		private string _mEMO;


		#endregion

		#region Constructors

		public SQM_ITEMTYPE_REF()
		{
		}

		public SQM_ITEMTYPE_REF(
			string p_iTEMTYPE,
			string p_iTEMTYPENAME,
			string p_pRODUCT,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rid,
			string p_sTATUS,
			string p_mEMO)
		{
			_iTEMTYPE = p_iTEMTYPE;
			_iTEMTYPENAME = p_iTEMTYPENAME;
			_pRODUCT = p_pRODUCT;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_rid = p_rid;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
		}

		#endregion

		#region Properties

		[Property("ITEMTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string ITEMTYPE
		{
			get { return _iTEMTYPE; }
			set
			{
				if ((_iTEMTYPE == null) || (value == null) || (!value.Equals(_iTEMTYPE)))
				{
                    object oldValue = _iTEMTYPE;
					_iTEMTYPE = value;
					RaisePropertyChanged(SQM_ITEMTYPE_REF.Prop_ITEMTYPE, oldValue, value);
				}
			}
		}

		[Property("ITEMTYPENAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 1000)]
		public string ITEMTYPENAME
		{
			get { return _iTEMTYPENAME; }
			set
			{
				if ((_iTEMTYPENAME == null) || (value == null) || (!value.Equals(_iTEMTYPENAME)))
				{
                    object oldValue = _iTEMTYPENAME;
					_iTEMTYPENAME = value;
					RaisePropertyChanged(SQM_ITEMTYPE_REF.Prop_ITEMTYPENAME, oldValue, value);
				}
			}
		}

		[Property("PRODUCT", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string PRODUCT
		{
			get { return _pRODUCT; }
			set
			{
				if ((_pRODUCT == null) || (value == null) || (!value.Equals(_pRODUCT)))
				{
                    object oldValue = _pRODUCT;
					_pRODUCT = value;
					RaisePropertyChanged(SQM_ITEMTYPE_REF.Prop_PRODUCT, oldValue, value);
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
					RaisePropertyChanged(SQM_ITEMTYPE_REF.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(SQM_ITEMTYPE_REF.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(SQM_ITEMTYPE_REF.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(SQM_ITEMTYPE_REF.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

		[PrimaryKey("RID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string RID
		{
			get { return _rid; }
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
					RaisePropertyChanged(SQM_ITEMTYPE_REF.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(SQM_ITEMTYPE_REF.Prop_MEMO, oldValue, value);
				}
			}
		}

		#endregion
	} // SQM_ITEMTYPE_REF
}

