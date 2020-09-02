// Business class MDM_ORG generated from MDM_ORG
// Creator: rw
// Created Date: [2019-12-10]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Castle.ActiveRecord;
using Aim.Data;
	
namespace Com.Feiliks.MDM
{
	[ActiveRecord("MDM_ORG")]
	public partial class MDM_ORG : EntityBase<MDM_ORG>
	{
		#region Property_Names

		public static string Prop_SFLG = "SFLG";
		public static string Prop_PFLG = "PFLG";
		public static string Prop_OBJID = "OBJID";
		public static string Prop_LANGTYPE = "LANGTYPE";
		public static string Prop_ORGKEY = "ORGKEY";
		public static string Prop_ORGNAME = "ORGNAME";
		public static string Prop_CREATETIME = "CREATETIME";
		public static string Prop_CREATEUSER = "CREATEUSER";
		public static string Prop_MODIFYTIME = "MODIFYTIME";
		public static string Prop_MODIFYUSER = "MODIFYUSER";
		public static string Prop_RID = "RID";
		public static string Prop_STATUS = "STATUS";
		public static string Prop_MEMO = "MEMO";

		#endregion

		#region Private_Variables

		private string _sFLG;
		private string _pFLG;
		private string _oBJID;
		private string _lANGTYPE;
		private string _orgkey;
		private string _oRGNAME;
		private DateTime? _cREATETIME;
		private string _cREATEUSER;
		private DateTime? _mODIFYTIME;
		private string _mODIFYUSER;
		private string _rID;
		private string _sTATUS;
		private string _mEMO;


		#endregion

		#region Constructors

		public MDM_ORG()
		{
		}

		public MDM_ORG(
			string p_sFLG,
			string p_pFLG,
			string p_oBJID,
			string p_lANGTYPE,
			string p_orgkey,
			string p_oRGNAME,
			DateTime? p_cREATETIME,
			string p_cREATEUSER,
			DateTime? p_mODIFYTIME,
			string p_mODIFYUSER,
			string p_rID,
			string p_sTATUS,
			string p_mEMO)
		{
			_sFLG = p_sFLG;
			_pFLG = p_pFLG;
			_oBJID = p_oBJID;
			_lANGTYPE = p_lANGTYPE;
			_orgkey = p_orgkey;
			_oRGNAME = p_oRGNAME;
			_cREATETIME = p_cREATETIME;
			_cREATEUSER = p_cREATEUSER;
			_mODIFYTIME = p_mODIFYTIME;
			_mODIFYUSER = p_mODIFYUSER;
			_rID = p_rID;
			_sTATUS = p_sTATUS;
			_mEMO = p_mEMO;
		}

		#endregion

		#region Properties

	[Property("SFLG", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4)]
		public string SFLG
		{
			get { return _sFLG; }
			set
			{
				if ((_sFLG == null) || (value == null) || (!value.Equals(_sFLG)))
				{
                    object oldValue = _sFLG;
					_sFLG = value;
					RaisePropertyChanged(MDM_ORG.Prop_SFLG, oldValue, value);
				}
			}
		}

		[Property("PFLG", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 4)]
		public string PFLG
		{
			get { return _pFLG; }
			set
			{
				if ((_pFLG == null) || (value == null) || (!value.Equals(_pFLG)))
				{
                    object oldValue = _pFLG;
					_pFLG = value;
					RaisePropertyChanged(MDM_ORG.Prop_PFLG, oldValue, value);
				}
			}
		}

		[Property("OBJID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 100)]
		public string OBJID
		{
			get { return _oBJID; }
			set
			{
				if ((_oBJID == null) || (value == null) || (!value.Equals(_oBJID)))
				{
                    object oldValue = _oBJID;
					_oBJID = value;
					RaisePropertyChanged(MDM_ORG.Prop_OBJID, oldValue, value);
				}
			}
		}

		[PrimaryKey("ORGKEY", Generator = PrimaryKeyType.Assigned, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
		public string ORGKEY
		{
			get { return _orgkey; }
            set { _orgkey = value; }
		}

		[Property("ORGNAME", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 160)]
		public string ORGNAME
		{
			get { return _oRGNAME; }
			set
			{
				if ((_oRGNAME == null) || (value == null) || (!value.Equals(_oRGNAME)))
				{
                    object oldValue = _oRGNAME;
					_oRGNAME = value;
					RaisePropertyChanged(MDM_ORG.Prop_ORGNAME, oldValue, value);
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
					RaisePropertyChanged(MDM_ORG.Prop_CREATETIME, oldValue, value);
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
					RaisePropertyChanged(MDM_ORG.Prop_CREATEUSER, oldValue, value);
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
					RaisePropertyChanged(MDM_ORG.Prop_MODIFYTIME, oldValue, value);
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
					RaisePropertyChanged(MDM_ORG.Prop_MODIFYUSER, oldValue, value);
				}
			}
		}

		[Property("RID", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 72)]
		public string RID
		{
			get { return _rID; }
			set
			{
				if ((_rID == null) || (value == null) || (!value.Equals(_rID)))
				{
                    object oldValue = _rID;
					_rID = value;
					RaisePropertyChanged(MDM_ORG.Prop_RID, oldValue, value);
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
					RaisePropertyChanged(MDM_ORG.Prop_STATUS, oldValue, value);
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
					RaisePropertyChanged(MDM_ORG.Prop_MEMO, oldValue, value);
				}
			}
		}


		[Property("LANGTYPE", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 40)]
		public string LANGTYPE
		{
			get { return _lANGTYPE; }
			set
			{
				if ((_lANGTYPE == null) || (value == null) || (!value.Equals(_lANGTYPE)))
				{
                    object oldValue = _lANGTYPE;
					_lANGTYPE = value;
					RaisePropertyChanged(MDM_ORG.Prop_LANGTYPE, oldValue, value);
				}
			}
		}

	

		#endregion
	} // MDM_ORG
}

